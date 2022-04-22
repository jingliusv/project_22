using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_22.Shared
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserAddress { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string OrderStatus { get; set; } = "Bearbetas";
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSum { get; set; }
        public List<OrderItem> OrderItems { get; set; } = null!;
    }
}
