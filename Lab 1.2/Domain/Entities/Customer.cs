using Domain.Enums;

namespace Domain.Entities
{
    
    public class Customer
    {
        public int CustomerId { get; set; } 
        public int Age { get; set; } 
        public Gender Gender { get; set; } 
        public string Country { get; set; } = string.Empty;
        public decimal Income { get; set; }

        public List<CustomerFeedback> Feedbacks { get; set; } = new List<CustomerFeedback>(); 
    }
}
