using Domain.Entities;

namespace Domain.Validation
{
    public static class CustomerValidator
    {
        public static bool Validate(Customer customer, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (customer.CustomerId <= 0)
            {
                errorMessage = "ID клієнта має бути більше 0";
                return false;
            }

            if (customer.Age < 18 || customer.Age > 120)
            {
                errorMessage = "Вік має бути в діапазоні 18-120";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.Country))
            {
                errorMessage = "Країна не може бути порожньою";
                return false;
            }

            return true;
        }
    }
}
