using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace project_22.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrder()
        {
            return Ok(await _orderService.PlaceOrder());
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderResponse>>>> GetOrders()
        {
            return Ok(await _orderService.GetOrders());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateOrder(UpdateOrder form, int id)
        {
            return Ok(await _orderService.UpdateOrder(form, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteOrder(int id)
        {
            return Ok(await _orderService.DeleteOrder(id));
        }

    }
}
