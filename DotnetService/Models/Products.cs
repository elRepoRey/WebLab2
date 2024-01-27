namespace TheFlowerplace.Models
{
    /// <summary>
    /// Represents a product with details such as name, description, price, and image.
    /// </summary>
    public class Product 
    {       
       
        public int Id { get; set; }

       
        public required string Name { get; set; }

        
        public string? Description { get; set; }

    
        public decimal Price { get; set; }

        
        public string? Image { get; set; } 
        

    }
}
