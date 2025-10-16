using ClosedXML.Excel;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;
using Data.Helpers;

namespace Data.Providers
{
    public class XlsxDataProvider : IDataProvider
    {
        public CustomerFeedbackData ReadData(string filePath)
        {
            return ReadData(filePath, "customer_feedback_satisfaction");
        }

        // Читаю Excel файл, знаходжу потрібний аркуш і парсю дані з заголовками
        public CustomerFeedbackData ReadData(string filePath, string sheetName = "customer_feedback_satisfaction")
        {
            var data = new CustomerFeedbackData();
            var customerDict = new Dictionary<int, Customer>();

            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheets.FirstOrDefault(ws => ws.Name == sheetName);
            if (worksheet == null)
                worksheet = workbook.Worksheet(1);

            var firstRowUsed = worksheet.FirstRowUsed();
            if (firstRowUsed == null) return data;

            var headerRow = firstRowUsed.RowUsed();
            var headers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase); 

            for (int col = 1; col <= headerRow.LastCellUsed().Address.ColumnNumber; col++)
            {
                var headerValue = headerRow.Cell(col).GetString();
                if (!string.IsNullOrWhiteSpace(headerValue))
                {
                    headers[headerValue] = col;
                }
            }

            System.Diagnostics.Debug.WriteLine($"Found headers: {string.Join(", ", headers.Keys)}");

            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                try
                {
                    int customerId = row.Cell(headers["CustomerID"]).GetValue<int>();
                    
                    if (customerId == 0)
                    {
                        continue;
                    }

                    if (!customerDict.ContainsKey(customerId))
                    {
                        var customer = new Customer
                        {
                            CustomerId = customerId,
                            Age = row.Cell(headers["Age"]).GetValue<int>(),
                            Gender = EnumHelper.ParseGender(row.Cell(headers["Gender"]).GetString()),
                            Country = row.Cell(headers["Country"]).GetString(),
                            Income = headers.ContainsKey("Income") ? row.Cell(headers["Income"]).GetValue<decimal>() : 0
                        };
                        customerDict[customerId] = customer;
                        data.Customers.Add(customer);
                    }

                    // Визначаємо назву колонки для категорії продукту
                    string categoryColumnName = headers.ContainsKey("ProductQuality") ? "ProductQuality" :
                                               headers.ContainsKey("ProductCategory") ? "ProductCategory" : null;
                    
                    // Парсимо ServiceQuality (може бути число або текст)
                    string serviceQualityValue = headers.ContainsKey("ServiceQuality") ? 
                        row.Cell(headers["ServiceQuality"]).GetString() : "Medium";
                    
                    // Парсимо FeedbackScore (може бути число або текст)
                    string feedbackScoreValue = headers.ContainsKey("FeedbackScore") ? 
                        row.Cell(headers["FeedbackScore"]).GetString() : "0";
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
                    string loyaltyValue = headers.ContainsKey("LoyaltyLevel") ? 
                        row.Cell(headers["LoyaltyLevel"]).GetString() : "Medium";
                    
                    var feedback = new CustomerFeedback
                    {
                        CustomerId = customerId,
                        ProductName = headers.ContainsKey("ProductName") ? row.Cell(headers["ProductName"]).GetString() : "Product",
                        Category = categoryColumnName != null ? 
                            EnumHelper.ParseProductCategory(row.Cell(headers[categoryColumnName]).GetString()) :
                            ProductCategory.Bronze,
                        ServiceQuality = EnumHelper.ParseServiceQuality(serviceQualityValue),
                        PurchaseFrequency = headers.ContainsKey("PurchaseFrequency") ? 
                            (int.TryParse(row.Cell(headers["PurchaseFrequency"]).GetString(), out var pf) ? pf : 0) : 0,
                        FeedbackScore = feedbackScore,
                        LoyaltyLevel = EnumHelper.ParseLoyaltyLevel(loyaltyValue),
                        SatisfactionScore = headers.ContainsKey("SatisfactionScore") ? 
                            (decimal.TryParse(row.Cell(headers["SatisfactionScore"]).GetString(), out var ss) ? ss : 0) : 0,
                        Customer = customerDict[customerId]
                    };

                    data.Feedbacks.Add(feedback);
                    customerDict[customerId].Feedbacks.Add(feedback);
                }
                catch (Exception ex)
                {
                    // Логую помилку, але продовжуємо обробку інших рядків
                    System.Diagnostics.Debug.WriteLine($"Error parsing XLSX row: {ex.Message}");
                    continue;
                }
            }

            return data;
        }

        public void WriteData(CustomerFeedbackData data, string filePath)
        {
            WriteData(data, filePath, "Data");
        }

        public void WriteData(CustomerFeedbackData data, string filePath, string sheetName = "Data")
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            worksheet.Cell(1, 1).Value = "CustomerID";
            worksheet.Cell(1, 2).Value = "Age";
            worksheet.Cell(1, 3).Value = "Gender";
            worksheet.Cell(1, 4).Value = "Country";
            worksheet.Cell(1, 5).Value = "Income";
            worksheet.Cell(1, 6).Value = "ProductQuality";
            worksheet.Cell(1, 7).Value = "ServiceQuality";
            worksheet.Cell(1, 8).Value = "PurchaseFrequency";
            worksheet.Cell(1, 9).Value = "FeedbackScore";
            worksheet.Cell(1, 10).Value = "LoyaltyLevel";
            worksheet.Cell(1, 11).Value = "SatisfactionScore";

            worksheet.Row(1).Style.Font.Bold = true;

            int row = 2;
            foreach (var feedback in data.Feedbacks)
            {
                var customer = data.Customers.FirstOrDefault(c => c.CustomerId == feedback.CustomerId);
                if (customer == null) continue;

                worksheet.Cell(row, 1).Value = customer.CustomerId;
                worksheet.Cell(row, 2).Value = customer.Age;
                worksheet.Cell(row, 3).Value = customer.Gender.ToString();
                worksheet.Cell(row, 4).Value = customer.Country;
                worksheet.Cell(row, 5).Value = customer.Income;
                worksheet.Cell(row, 6).Value = feedback.Category.ToString();
                worksheet.Cell(row, 7).Value = feedback.ServiceQuality.ToString();
                worksheet.Cell(row, 8).Value = feedback.PurchaseFrequency;
                worksheet.Cell(row, 9).Value = feedback.FeedbackScore;
                worksheet.Cell(row, 10).Value = feedback.LoyaltyLevel.ToString();
                worksheet.Cell(row, 11).Value = feedback.SatisfactionScore;

                row++;
            }

            worksheet.Columns().AdjustToContents();
            workbook.SaveAs(filePath);
        }
    }
}
