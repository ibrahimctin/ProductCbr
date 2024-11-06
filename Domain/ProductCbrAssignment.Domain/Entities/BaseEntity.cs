using System.ComponentModel.DataAnnotations;

namespace ProductCbrAssignment.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public string Id { get; set; }
    }
}