using System.Text;
using System.Xml.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;
using Data.Helpers;

namespace Data.Providers
{
    public class XmlDataProvider : IDataProvider
    {
        public CustomerFeedbackData ReadData(string filePath)
        {
            return ReadData(filePath, null);
        }

        public CustomerFeedbackData ReadData(string filePath, Encoding? encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            var data = new CustomerFeedbackData();
            var customerDict = new Dictionary<int, Customer>();

            var doc = XDocument.Load(filePath);
            var root = doc.Root;

            if (root == null) return data;

            foreach (var recordElement in root.Elements("Record"))
            {
                try
                {
                    var customerIdValue = recordElement.Element("CustomerID")?.Value;
                    int customerId = int.Parse(customerIdValue != null ? customerIdValue : "0");

                    if (!customerDict.ContainsKey(customerId))
                    {
                        var customer = new Customer
                        {
                            CustomerId = customerId,
                            Age = int.Parse(recordElement.Element("Age")?.Value != null ? recordElement.Element("Age").Value : "0"),
                            Gender = EnumHelper.ParseGender(recordElement.Element("Gender")?.Value != null ? recordElement.Element("Gender").Value : ""),
                            Country = recordElement.Element("Country")?.Value != null ? recordElement.Element("Country").Value : "",
                            Income = decimal.Parse(recordElement.Element("Income")?.Value ?? "0")
                        };
                        customerDict[customerId] = customer;
                        data.Customers.Add(customer);
                    }

                    // Визначаємо назву елемента для категорії продукту
                    var categoryElement = recordElement.Element("ProductQuality") ?? recordElement.Element("ProductCategory");
                    string categoryValue = categoryElement?.Value ?? "Bronze";

                    // Парсимо ServiceQuality (може бути число або текст)
                    string serviceQualityValue = recordElement.Element("ServiceQuality")?.Value ?? "Medium";
                    
                    // Парсимо FeedbackScore (може бути число або текст)
                    string feedbackScoreValue = recordElement.Element("FeedbackScore")?.Value ?? "0";
                    decimal feedbackScore = 0;
                    if (!decimal.TryParse(feedbackScoreValue, out feedbackScore))
                    {
                        // Якщо це текст (Low/Medium/High), конвертуємо у число
                        feedbackScore = feedbackScoreValue.ToLower() switch
                        {
                            "low" => 2.5m,
                            "medium" => 5.0m,
                            "high" => 8.0m,
                            _ => 5.0m
                        };
                    }
                    
                    // Парсимо LoyaltyLevel (може бути число або текст)
                    string loyaltyValue = recordElement.Element("LoyaltyLevel")?.Value ?? "Medium";

                    var feedback = new CustomerFeedback
                    {
                        CustomerId = customerId,
                        ProductName = recordElement.Element("ProductName")?.Value ?? "Product",
                        Category = EnumHelper.ParseProductCategory(categoryValue),
                        ServiceQuality = EnumHelper.ParseServiceQuality(serviceQualityValue),
                        PurchaseFrequency = int.TryParse(recordElement.Element("PurchaseFrequency")?.Value, out var pf) ? pf : 0,
                        FeedbackScore = feedbackScore,
                        LoyaltyLevel = EnumHelper.ParseLoyaltyLevel(loyaltyValue),
                        SatisfactionScore = decimal.TryParse(recordElement.Element("SatisfactionScore")?.Value, out var ss) ? ss : 0,
                        Customer = customerDict[customerId]
                    };

                    data.Feedbacks.Add(feedback);
                    customerDict[customerId].Feedbacks.Add(feedback);
                }
                catch (Exception ex)
                {
                    // Логую помилку, але продовжуємо обробку інших рядків
                    System.Diagnostics.Debug.WriteLine($"Error parsing XML record: {ex.Message}");
                    continue;
                }
            }

            return data;
        }

        public void WriteData(CustomerFeedbackData data, string filePath)
        {
            WriteData(data, filePath, null);
        }

        public void WriteData(CustomerFeedbackData data, string filePath, Encoding? encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var root = new XElement("CustomerFeedbackData");

            foreach (var feedback in data.Feedbacks)
            {
                var customer = data.Customers.FirstOrDefault(c => c.CustomerId == feedback.CustomerId);
                if (customer == null) continue;

                var recordElement = new XElement("Record",
                    new XElement("CustomerID", customer.CustomerId),
                    new XElement("Age", customer.Age),
                    new XElement("Gender", customer.Gender.ToString()),
                    new XElement("Country", customer.Country),
                    new XElement("Income", customer.Income),
                    new XElement("ProductQuality", feedback.Category.ToString()),
                    new XElement("ServiceQuality", feedback.ServiceQuality.ToString()),
                    new XElement("PurchaseFrequency", feedback.PurchaseFrequency),
                    new XElement("FeedbackScore", feedback.FeedbackScore),
                    new XElement("LoyaltyLevel", feedback.LoyaltyLevel.ToString()),
                    new XElement("SatisfactionScore", feedback.SatisfactionScore)
                );

                root.Add(recordElement);
            }

            var doc = new XDocument(new XDeclaration("1.0", encoding.WebName, "yes"), root);
            doc.Save(filePath);
        }
    }
}
