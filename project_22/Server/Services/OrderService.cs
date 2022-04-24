using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace project_22.Server.Services
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder();
        Task<ServiceResponse<List<OrderResponse>>> GetOrders();
        Task<ServiceResponse<bool>> DeleteOrder(int id);
        Task<ServiceResponse<bool>> UpdateOrder(UpdateOrder form, int OrderId);
    }
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public OrderService(DataContext context, ICartService cartService, IAuthService authService, IUserService userService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<ServiceResponse<bool>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if(order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool> { Data = true, Message = $"Beställningen med id nummer {order.Id} tas bort." };
            }
                

            return new ServiceResponse<bool> { Success = false, Message = "Tyvärr, vi kan inte hitta den här beställningen." };
        }

        public async Task<ServiceResponse<List<OrderResponse>>> GetOrders()
        {
            var response = new ServiceResponse<List<OrderResponse>>();

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .Where(o => o.UserId == _authService.GetUserId())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orderResponse = new List<OrderResponse>();
            foreach (var order in orders)
            {
                string productName = string.Empty;
                order.OrderItems.ForEach(i => productName += i.Product.ProductName + "(" + i.Qty + " st) ");

                orderResponse.Add(new OrderResponse
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    ProductInfo = productName,
                    TotalPrice = order.TotalSum,
                    OrderStatus = order.OrderStatus
                });
            }

            response.Data = orderResponse;
            return response;
        }

        public async Task<ServiceResponse<bool>> PlaceOrder()
        {
            var products = (await _cartService.GetCartItemsFromDbAsync()).Data;
            decimal totalPrice = 0;
            products.ForEach(product => totalPrice += product.Price * product.Qty);

            var orderItems = new List<OrderItem>();
            products.ForEach(p => orderItems.Add(new OrderItem
            {
                ProductId = p.ProductId,
                Qty = p.Qty,
                TotalPrice = p.Price * p.Qty
            }));

            //get user info from UserService
            var user = await _userService.GetUserByIdAsync(_authService.GetUserId());

            var order = new Order
            {
                UserId = _authService.GetUserId(), // get user id from ClaimTypes
                OrderDate = DateTime.Now,
                TotalSum = totalPrice,
                OrderItems = orderItems,
                UserName = user.FirstName + " " + user.LastName,
                UserEmail = user.Email,
                UserAddress = user.StreetName + ", " + user.PostalCode + " " + user.City
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(_context.CartItems.Where(c => c.UserId == _authService.GetUserId())); // remove just caritems with the same user id

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateOrder(UpdateOrder form, int OrderId)
        {
            var orderEntity = await _context.Orders.FindAsync(OrderId);
            if(orderEntity != null)
            {
                orderEntity.OrderStatus = form.OrderStatus;
                _context.Entry(orderEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool> { Data = true, Message = $"Vi har uppdaterat beställningsstatus med beställningen id {orderEntity.Id}." };
            }
            return new ServiceResponse<bool> { Success = false, Message = "Tyvärr, vi kunde inte uppdaterat besällningen." };
        }
    }
}
