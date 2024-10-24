using CustomerApi.Data;
using CustomerApi.Models;
using CustomerApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Repository
{
    public class CustomerRepository:Repository<Customer> , ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Customer> UpdateAsync(Customer entity)
        {
            _db.Customers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        public async Task AddLoyaltyPointsAsync(Guid customerId, int points)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer != null)
            {
                customer.LoyaltyPoints += points;
                await SaveAsync();
            }
        }

        public async Task AddFavoriteDishAsync(Guid customerId, string dish)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer != null)
            {
                customer.FavoriteDishes.Add(dish);
                await SaveAsync();
            }
        }

        public async Task AddOrderIdAsync(Guid customerId, int orderId)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer != null)
            {
                customer.OrderIds.Add(orderId);
                await SaveAsync();
            }
        }

        public async Task AddReservationAsync(Guid customerId, string reservation)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer != null)
            {
                customer.Reservations.Add(reservation);
                await SaveAsync();
            }
        }

        public async Task AddFeedbackAsync(Guid customerId, string feedback)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer != null)
            {
                customer.Feedbacks.Add(feedback);
                await SaveAsync();
            }
        }


    }
}
