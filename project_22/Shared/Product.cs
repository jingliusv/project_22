using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_22.Shared
{
    public class Product
    {
        public int Id { get; set; }
        public string ArticleNumber { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryName { get; set; } = string.Empty;

    }
}
