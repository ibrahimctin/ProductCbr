namespace ProductCbrAssignment.Domain.DTOs.Products
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string ImageUrl { get; set; }
    }
}
