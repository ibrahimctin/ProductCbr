using System.ComponentModel.DataAnnotations;

namespace ProductCbrAssignment.Domain.Entities.Products
{
    public class Product:BaseEntity
    {
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(13)]
        public string Code { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string  ImageUrl { get; set; }
    }
}
