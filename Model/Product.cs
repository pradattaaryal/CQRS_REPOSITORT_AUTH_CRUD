namespace practices.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; } // Image path
    }
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; } // Image file
    }
    public class UpdateProductCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; } // Image file
    }
    public class GetProductByIdQuery
    {
        public int Id { get; set; }
    }
    public class DeleteProductCommand
    {
        public int Id { get; set; }
    }

}
