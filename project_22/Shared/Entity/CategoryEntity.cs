using System.ComponentModel.DataAnnotations;

namespace project_22.Shared.Entity
{
    public class CategoryEntity
    {

        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = null!;
        public virtual ICollection<ProductEntity> Products { get; set; } = null!;
    }
}
