namespace Domain.Entities
{
    public class CustomerFeedbackData
    {
        public List<Customer> Customers { get; set; } = new List<Customer>(); 
        public List<CustomerFeedback> Feedbacks { get; set; } = new List<CustomerFeedback>(); 

        public int TotalRecords => Feedbacks.Count; 
        public int TotalCustomers => Customers.Count; 
    }
}
