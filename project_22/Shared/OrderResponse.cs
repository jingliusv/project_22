using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_22.Shared
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }       
        public string ProductInfo { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
    }
}
