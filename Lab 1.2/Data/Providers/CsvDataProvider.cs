using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;
using Data.Helpers;

namespace Data.Providers
{
    public class CsvDataProvider : IDataProvider
    {
        public CustomerFeedbackData ReadData(string filePath)
        {
            return ReadData(filePath, ",", null);
        }

        public CustomerFeedbackData ReadData(string filePath, string delimiter = ",", Encoding? encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var data = new CustomerFeedbackData();
            var customerDict = new Dictionary<int, Customer>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = delimiter,
                HasHeaderRecord = true,
                MissingFieldFound = null,
                BadDataFound = null
            };

            using var reader = new StreamReader(filePath, encoding);
            using var csv = new CsvReader(reader, config);

            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                try
                {
                    int customerId = csv.GetField<int>("CustomerID");

                    if (!customerDict.ContainsKey(customerId))
                    {
                        var customer = new Customer();
                        customer.CustomerId = customerId;
                        customer.Age = csv.GetField<int>("Age");
                        customer.Gender = EnumHelper.ParseGender(csv.GetField("Gender"));
                        customer.Country = csv.GetField("Country");
                        customer.Income = csv.TryGetField<decimal>("Income", out var income) ? income : 0;
                        
                        customerDict[customerId] = customer;
                        data.Customers.Add(customer);
                    }

                    // Визначаємо назву колонки для категорії продукту
                    string categoryValue;
                    if (csv.TryGetField("ProductQuality", out categoryValue))
                    {
                        // Використовуємо ProductQuality
                    }
                    else if (csv.TryGetField("ProductCategory", out categoryValue))
                    {
                        // Використовуємо ProductCategory
                    }
                    else
                    {
                        categoryValue = "Bronze";
                    }

                    // Парсимо ServiceQuality (може бути число або текст)
                    string serviceQualityValue = csv.TryGetField<string>("ServiceQuality", out var sqVal) ? sqVal ?? "Medium" : "Medium";
                    
                    // Парсимо FeedbackScore (може бути число або текст)
                    string feedbackScoreValue = csv.TryGetField<string>("FeedbackScore", out var fsVal) ? fsVal ?? "0" : "0";
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
                    string loyaltyValue = csv.TryGetField<string>("LoyaltyLevel", out var lvVal) ? lvVal ?? "Medium" : "Medium";

                    var feedback = new CustomerFeedback();
                    feedback.CustomerId = customerId;
                    feedback.ProductName = csv.TryGetField<string>("ProductName", out var productName) ? productName : "Product";
                    feedback.Category = EnumHelper.ParseProductCategory(categoryValue);
                    feedback.ServiceQuality = EnumHelper.ParseServiceQuality(serviceQualityValue);
                    feedback.PurchaseFrequency = csv.TryGetField<int>("PurchaseFrequency", out var pf) ? pf : 0;
                    feedback.FeedbackScore = feedbackScore;
                    feedback.LoyaltyLevel = EnumHelper.ParseLoyaltyLevel(loyaltyValue);
                    feedback.SatisfactionScore = csv.TryGetField<decimal>("SatisfactionScore", out var ss) ? ss : 0;
                    feedback.Customer = customerDict[customerId];

                    data.Feedbacks.Add(feedback);
                    customerDict[customerId].Feedbacks.Add(feedback);
                }
                catch (Exception ex)
                {
                    // Логуємо помилку, але продовжуємо обробку інших рядків
                    System.Diagnostics.Debug.WriteLine($"Error parsing CSV row: {ex.Message}");
                    continue;
                }
            }

            return data;
        }

        public void WriteData(CustomerFeedbackData data, string filePath)
        {
            WriteData(data, filePath, ",", null);
        }

        public void WriteData(CustomerFeedbackData data, string filePath, string delimiter = ",", Encoding? encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = delimiter
            };

            using var writer = new StreamWriter(filePath, false, encoding);
            using var csv = new CsvWriter(writer, config);

            csv.WriteField("CustomerID");
            csv.WriteField("Age");
            csv.WriteField("Gender");
            csv.WriteField("Country");
            csv.WriteField("Income");
            csv.WriteField("ProductQuality");
            csv.WriteField("ServiceQuality");
            csv.WriteField("PurchaseFrequency");
            csv.WriteField("FeedbackScore");
            csv.WriteField("LoyaltyLevel");
            csv.WriteField("SatisfactionScore");
            csv.NextRecord();

            foreach (var feedback in data.Feedbacks)
            {
                var customer = data.Customers.FirstOrDefault(c => c.CustomerId == feedback.CustomerId);
                if (customer == null) continue;

                csv.WriteField(customer.CustomerId);
                csv.WriteField(customer.Age);
                csv.WriteField(customer.Gender.ToString());
                csv.WriteField(customer.Country);
                csv.WriteField(customer.Income);
                csv.WriteField(feedback.Category.ToString());
                csv.WriteField(feedback.ServiceQuality.ToString());
                csv.WriteField(feedback.PurchaseFrequency);
                csv.WriteField(feedback.FeedbackScore);
                csv.WriteField(feedback.LoyaltyLevel.ToString());
                csv.WriteField(feedback.SatisfactionScore);
                csv.NextRecord();
            }
        }
    }
}
