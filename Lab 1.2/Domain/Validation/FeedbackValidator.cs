using Domain.Entities;

namespace Domain.Validation
{
    public static class FeedbackValidator
    {
        public static bool Validate(CustomerFeedback feedback, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (feedback.CustomerId <= 0)
            {
                errorMessage = "ID клієнта має бути більше 0";
                return false;
            }

            if (string.IsNullOrWhiteSpace(feedback.ProductName))
            {
                errorMessage = "Назва продукту не може бути порожньою";
                return false;
            }

            if (feedback.PurchaseFrequency < 0)
            {
                errorMessage = "Частота покупок не може бути від'ємною";
                return false;
            }

            if (feedback.FeedbackScore < 0 || feedback.FeedbackScore > 10)
            {
                errorMessage = "Оцінка відгуку має бути в діапазоні 0-10";
                return false;
            }

            if (feedback.SatisfactionScore < 0 || feedback.SatisfactionScore > 10)
            {
                errorMessage = "Оцінка задоволеності має бути в діапазоні 0-10";
                return false;
            }

            return true;
        }
    }
}
