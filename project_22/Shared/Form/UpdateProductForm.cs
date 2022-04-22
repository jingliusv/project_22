using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_22.Shared.Form
{
    public class UpdateProductForm
    {
        [Required]
        public string ArticleNumber { get; set; } = null!;
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
    }
}
