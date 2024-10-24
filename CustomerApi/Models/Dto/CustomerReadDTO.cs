namespace CustomerApi.Models.Dto
{
    public class CustomerReadDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }

        public int LoyaltyPoints { get; set; }

        public List<string> FavoriteDishes { get; set; }
        public List<int> OrderIds { get; set; }
        public List<string> Reservations { get; set; }
        public List<string> Feedbacks { get; set; }
    }
}
