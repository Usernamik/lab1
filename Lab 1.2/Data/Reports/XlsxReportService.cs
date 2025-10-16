using ClosedXML.Excel;
using Domain.Entities;

namespace Data.Reports
{
    public class XlsxReportService
    {
        // Генерую Excel звіт з трьома аркушами: загальна інформація, аналіз по категоріям, аналіз по країнам
        public void GenerateReport(CustomerFeedbackData data, string filePath)
        {
            using var workbook = new XLWorkbook();

            CreateSummarySheet(workbook, data);
            CreateCategoryAnalysisSheet(workbook, data);
            CreateCountryAnalysisSheet(workbook, data);

            workbook.SaveAs(filePath);
        }

        private void CreateSummarySheet(XLWorkbook workbook, CustomerFeedbackData data)
        {
            var worksheet = workbook.Worksheets.Add("Summary");

            worksheet.Cell(1, 1).Value = "Metric";
            worksheet.Cell(1, 2).Value = "Value";
            worksheet.Range(1, 1, 1, 2).Style.Font.Bold = true;
            worksheet.Range(1, 1, 1, 2).Style.Fill.BackgroundColor = XLColor.LightBlue;

            int row = 2;
            worksheet.Cell(row++, 1).Value = "Total Customers";
            worksheet.Cell(row - 1, 2).Value = data.TotalCustomers;

            worksheet.Cell(row++, 1).Value = "Total Feedbacks";
            worksheet.Cell(row - 1, 2).Value = data.TotalRecords;

            worksheet.Cell(row++, 1).Value = "Average Feedback Score";
            worksheet.Cell(row - 1, 2).Value = data.Feedbacks.Average(f => (double)f.FeedbackScore);
            worksheet.Cell(row - 1, 2).Style.NumberFormat.Format = "0.00";

            worksheet.Cell(row++, 1).Value = "Average Satisfaction Score";
            worksheet.Cell(row - 1, 2).Value = data.Feedbacks.Average(f => (double)f.SatisfactionScore);
            worksheet.Cell(row - 1, 2).Style.NumberFormat.Format = "0.00";

            worksheet.Cell(row++, 1).Value = "Average Purchase Frequency";
            worksheet.Cell(row - 1, 2).Value = data.Feedbacks.Average(f => f.PurchaseFrequency);
            worksheet.Cell(row - 1, 2).Style.NumberFormat.Format = "0.00";

            worksheet.Columns().AdjustToContents();
        }

        // Групую дані по категоріям і розраховую статистику (кількість, середні оцінки)
        private void CreateCategoryAnalysisSheet(XLWorkbook workbook, CustomerFeedbackData data)
        {
            var worksheet = workbook.Worksheets.Add("Category Analysis");

            worksheet.Cell(1, 1).Value = "Category";
            worksheet.Cell(1, 2).Value = "Feedback Count";
            worksheet.Cell(1, 3).Value = "Avg Satisfaction";
            worksheet.Cell(1, 4).Value = "Avg Feedback Score";
            worksheet.Range(1, 1, 1, 4).Style.Font.Bold = true;
            worksheet.Range(1, 1, 1, 4).Style.Fill.BackgroundColor = XLColor.LightGreen;

            var categoryStats = data.Feedbacks
                .GroupBy(f => f.Category)
                .Select(g => new
                {
                    Category = g.Key.ToString(),
                    Count = g.Count(),
                    AvgSatisfaction = g.Average(f => (double)f.SatisfactionScore),
                    AvgFeedback = g.Average(f => (double)f.FeedbackScore)
                })
                .OrderByDescending(x => x.AvgSatisfaction)
                .ToList();

            int row = 2;
            foreach (var stat in categoryStats)
            {
                worksheet.Cell(row, 1).Value = stat.Category;
                worksheet.Cell(row, 2).Value = stat.Count;
                worksheet.Cell(row, 3).Value = stat.AvgSatisfaction;
                worksheet.Cell(row, 3).Style.NumberFormat.Format = "0.00";
                worksheet.Cell(row, 4).Value = stat.AvgFeedback;
                worksheet.Cell(row, 4).Style.NumberFormat.Format = "0.00";
                row++;
            }

            worksheet.Columns().AdjustToContents();
        }

        // Групую дані по країнам і розраховую статистику по клієнтам та відгукам
        private void CreateCountryAnalysisSheet(XLWorkbook workbook, CustomerFeedbackData data)
        {
            var worksheet = workbook.Worksheets.Add("Country Analysis");

            worksheet.Cell(1, 1).Value = "Country";
            worksheet.Cell(1, 2).Value = "Customer Count";
            worksheet.Cell(1, 3).Value = "Total Feedbacks";
            worksheet.Cell(1, 4).Value = "Avg Satisfaction";
            worksheet.Range(1, 1, 1, 4).Style.Font.Bold = true;
            worksheet.Range(1, 1, 1, 4).Style.Fill.BackgroundColor = XLColor.LightCoral;

            var countryStats = data.Customers
                .GroupBy(c => c.Country)
                .Select(g => new
                {
                    Country = g.Key,
                    CustomerCount = g.Count(),
                    TotalFeedbacks = g.Sum(c => c.Feedbacks.Count),
                    AvgSatisfaction = g.SelectMany(c => c.Feedbacks).Any() 
                        ? g.SelectMany(c => c.Feedbacks).Average(f => (double)f.SatisfactionScore) 
                        : 0
                })
                .OrderByDescending(x => x.CustomerCount)
                .ToList();

            int row = 2;
            foreach (var stat in countryStats)
            {
                worksheet.Cell(row, 1).Value = stat.Country;
                worksheet.Cell(row, 2).Value = stat.CustomerCount;
                worksheet.Cell(row, 3).Value = stat.TotalFeedbacks;
                worksheet.Cell(row, 4).Value = stat.AvgSatisfaction;
                worksheet.Cell(row, 4).Style.NumberFormat.Format = "0.00";
                row++;
            }

            worksheet.Columns().AdjustToContents();
        }
    }
}
