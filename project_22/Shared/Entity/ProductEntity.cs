using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_22.Shared.Entity
{
    [Index(nameof(ArticleNumber), IsUnique = true)]
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string ArticleNumber { get; set; } = string.Empty;
        [Required, StringLength(50)]
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; } = null!;
    }
}
