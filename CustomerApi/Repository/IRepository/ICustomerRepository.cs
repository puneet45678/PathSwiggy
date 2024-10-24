using CustomerApi.Models;

namespace CustomerApi.Repository.IRepository
{
    public interface ICustomerRepository :IRepository<Customer>
    {
        Task<Customer> UpdateAsync(Customer entity);
        Task AddLoyaltyPointsAsync(Guid customerId, int points);
        Task AddFavoriteDishAsync(Guid customerId, string dish);
        Task AddOrderIdAsync(Guid customerId, int orderId);
        Task AddReservationAsync(Guid customerId, string reservation);
        Task AddFeedbackAsync(Guid customerId, string feedback);
    }
}
