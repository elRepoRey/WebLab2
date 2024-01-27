
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheFlowerplace.Data;
using TheFlowerplace.Models;
using TheFlowerplace.Services;

namespace TheFlowerplace
{  
    public class Program    {
       
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddScoped(typeof(SQLService<>));

            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            builder.WebHost.UseUrls($"http://*:{port}");


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
               sqlServerOptionsAction: sqlOptions =>
               {
                   sqlOptions.EnableRetryOnFailure(
                       maxRetryCount: 5,
                       maxRetryDelay: TimeSpan.FromSeconds(30),
                       errorNumbersToAdd: null);
               }));


            var AzureBlobStorageconnectionString = Environment.GetEnvironmentVariable("cad23sdmainstorageaccount") ?? 
                throw new Exception("cad23sdmainstorageaccount string is missing.");           
            builder.Services.AddScoped<IFileService, FileService>(factory =>
                       new FileService(AzureBlobStorageconnectionString));
            builder.Services.AddSingleton(new BlobService(AzureBlobStorageconnectionString));

            builder.Services.AddAuthorization();
            



            var app = builder.Build();

         
            if (app.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/", () => "Welcome to TheFlowerplace!");

            // FileServices endpoints
            app.MapGet("/Files", async (string containerName, IFileService fileService, ILogger<FileService> logger) =>
            {
                logger.LogInformation("Listing all files on blob storage");
                var files = await fileService.ListAllFilesOnBlobStorage(containerName);
                return Results.Ok(files);
            });

            app.MapPost("/File", async (IFormFile file, string containerName, IFileService fileService, ILogger<FileService> logger) =>
            {
                logger.LogInformation("Uploading file to blob storage");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                await fileService.UploadFileAsync(containerName, fileName, memoryStream);
                return Results.Ok(
                    new
                    {
                        containerName,
                        fileName,
                    }
                );
            });

            app.MapPost("/Folder", async (string containerName, string folderPath, IFileService fileService, ILogger<FileService> logger) =>
            {
                logger.LogInformation("Uploading folder to blob storage");
                await fileService.UploadFolderAsync(containerName, folderPath);
                return Results.Ok();
            });

            app.MapDelete("/File", async (string containerName, string fileName, IFileService fileService, ILogger<FileService> logger) =>
            {
                logger.LogInformation("Deleting file from blob storage");
                await fileService.DeleteFileAsync(containerName, fileName);
                return Results.Ok();
            });


            // ProductServices endpoints
            app.MapGet("/Products", async (SQLService<Product> productService, BlobService blobService, ILogger<SQLService<Product>> logger) =>
            {
                logger.LogInformation("Getting all products");
                var products = await productService.GetAllAsync();
                foreach (Product product in products)
                {
                    if (product.Image != null)
                        product.Image = blobService.GenerateBlobReadSasUri("images", product.Image);
                }
                return Results.Ok(products);
            });

            app.MapPost("/Product", async (Product product, SQLService<Product> productService, ILogger<SQLService<Product>> logger) =>
            {
                logger.LogInformation("Adding product: ");
                if (product == null)
                {
                    return Results.BadRequest("Product name cannot be null");
                }
                await productService.AddAsync(product);
                return Results.Ok();
            });

            app.MapPut("/Product", async (Product product, SQLService<Product> productService, IFileService fileService, ILogger<SQLService<Product>> logger) =>
            {
                logger.LogInformation("Updating product");

                if (product == null)
                {
                    return Results.BadRequest("Product  cannot be null");
                }
                if (product.Image != null)
                {
                    try
                    {
                        var currentProducts = await productService.GetAllAsync();
                        var currentProduct = currentProducts.First(p => p.Id == product.Id);
                        var ImageToDelete = currentProduct.Image;
                        if (ImageToDelete != null)
                        {
                            await fileService.DeleteFileAsync("images", ImageToDelete);
                        }

                    }
                    catch (Exception e)
                    {
                        logger.LogError(e.Message);
                    }
                }
                else
                {
                    var currentProducts = await productService.GetAllAsync();
                    var currentProduct = currentProducts.First(p => p.Id == product.Id);
                    product.Image = currentProduct.Image;
                }

                await productService.UpdateAsync(product);
                return Results.Ok();
            });

            app.MapDelete("/Product", async (int id, SQLService<Product> productService, IFileService fileService, ILogger<SQLService<Product>> logger) =>
            {
                logger.LogInformation("Deleting product");
                var product = await productService.GetAllAsync();
                var productToDelete = product.First(p => p.Id == id);
                if (productToDelete != null)
                {
                    await productService.DeleteAsync(productToDelete);
                    if (productToDelete.Image != null)
                    {
                        try
                        {
                            logger.LogInformation("Deleting image from blob storage");
                            await fileService.DeleteFileAsync("images", productToDelete.Image);
                        }
                        catch (Exception e)
                        {
                            logger.LogError(e.Message);
                        }
                    }

                    return Results.Ok();
                }
                return Results.NotFound();
            });

            app.Run();
        }
    }
}
