using TheFlowerplace.Models;
using System.Collections.Generic;


public class ProductSeedData
{
    public static List<Product> GetPreconfiguredProducts()
    {
        return new List<Product>
        {
            new Product {Id =1, Name = "Rose", Description = "A classic red rose, perfect for all occasions.", Price = 15.99M, Image = "images/rose.jpg" },
            new Product {Id= 2, Name = "Tulip", Description = "Colorful tulips to brighten your day.", Price = 9.99M, Image = "images/tulip.jpg" },
            new Product {Id= 3,  Name = "Daisy", Description = "Cheerful daisies with a fresh look.", Price = 5.99M, Image = "images/daisy.jpg" },
            new Product {Id= 4,  Name = "Sunflower", Description = "Bright and beautiful sunflowers.", Price = 12.50M, Image = "images/sunflower.jpg" },
            new Product {Id= 5,  Name = "Orchid", Description = "Elegant and exotic orchids.", Price = 22.99M, Image = "images/orchid.jpg" },
            new Product {Id= 6,  Name = "Lily", Description = "Stunning lilies for a beautiful bouquet.", Price = 18.99M, Image = "images/lily.jpg" }
        };
    }
}
