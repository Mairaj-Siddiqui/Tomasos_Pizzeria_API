using TomasosPizzeriaAPI.Data.Entities;
using TomasosPizzeriaAPI.DTOs;

namespace TomasosPizzeriaAPI.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<(bool IsValid, Order? Order)> PlaceOrderAsync(int userId, List<int> dishIds);
        Task<List<Order>> GetUserOrdersAsync(int userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<bool> UpdateOrderAsync(int orderId, List<int> dishIds, int userId, string role);
        Task<bool> DeleteOrderAsync(int orderId, int userId, string role);
    }
}
