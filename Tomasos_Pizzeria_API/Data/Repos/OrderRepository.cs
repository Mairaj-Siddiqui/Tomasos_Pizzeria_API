using Microsoft.EntityFrameworkCore;
using Tomasos_Pizzeria_API.Context;
using TomasosPizzeriaAPI.Data.Entities;
using TomasosPizzeriaAPI.Data.Interfaces;

namespace TomasosPizzeriaAPI.Data.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TomasosDbContext _context;

        public OrderRepository(TomasosDbContext context)
        {
            _context = context;
        }

        public async Task<(bool IsValid, Order? Order)> PlaceOrderAsync(int userId, List<int> dishIds)
        {
            if (dishIds == null || !dishIds.Any()) return (false, null);

            var dishes = await _context.Dishes
                .Where(d => dishIds.Contains(d.Id))
                .ToListAsync();

            if (dishes.Count != dishIds.Count)
                return (false, null);

            var order = new Order
            {
                UserId = userId,
                TotalPrice = dishes.Sum(d => d.Price),
                OrderDishes = dishes.Select(d => new OrderDish { DishId = d.Id }).ToList(),
                OrderDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return (true, order);
        }

        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDishes).ThenInclude(od => od.Dish)
                .OrderByDescending(o => o.OrderDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDishes).ThenInclude(od => od.Dish)
                .OrderByDescending(o => o.OrderDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> UpdateOrderAsync(int orderId, List<int> dishIds, int userId, string role)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDishes)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return false;
            if (order.UserId != userId && role != "Admin") return false;

            var dishes = await _context.Dishes
                .Where(d => dishIds.Contains(d.Id))
                .ToListAsync();

            if (dishes.Count != dishIds.Count) return false;

            order.OrderDishes.Clear();
            order.OrderDishes = dishes.Select(d => new OrderDish { DishId = d.Id }).ToList();
            order.TotalPrice = dishes.Sum(d => d.Price);
            order.OrderDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int orderId, int userId, string role)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDishes)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return false;
            if (order.UserId != userId && role != "Admin") return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
