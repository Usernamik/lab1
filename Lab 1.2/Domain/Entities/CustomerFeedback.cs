using Domain.Enums;

namespace Domain.Entities
{
    public class CustomerFeedback
    {
        public int CustomerId { get; set; } 
        public string ProductName { get; set; } = string.Empty; 
        public ProductCategory Category { get; set; } 
        public ServiceQuality ServiceQuality { get; set; }
        public int PurchaseFrequency { get; set; } 
        public decimal FeedbackScore { get; set; }
        public LoyaltyLevel LoyaltyLevel { get; set; } 
        public decimal SatisfactionScore { get; set; } 

        public Customer? Customer { get; set; } 
    }
}
