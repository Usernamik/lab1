using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace Data.Reports
{
    public class DocxReportService
    {
        // Створюю Word документ з титульною сторінкою, описом датасету, метриками та висновками
        public void GenerateReport(CustomerFeedbackData data, string filePath)
        {
            using var wordDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document);
            var mainPart = wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            var body = mainPart.Document.AppendChild(new Body());

            AddTitlePage(body);
            AddDatasetDescription(body, data);
            AddKeyMetrics(body, data);
            AddConclusions(body, data);

            mainPart.Document.Save();
        }

        private void AddTitlePage(Body body)
        {
            AddParagraph(body, "Звіт фітбеку від користувачів", true, "28", true);
            AddParagraph(body, "Аналіз даних відгуків клієнтів", false, "20", true);
            AddEmptyParagraph(body, 3);
            AddParagraph(body, $"Студент: Лісовський  Данило Олександрович", false, "14");
            AddParagraph(body, $"Група: КН 22", false, "14");
            AddParagraph(body, $"Варіант: 19", false, "14");
            AddEmptyParagraph(body, 2);
            AddParagraph(body, $"Дата: {DateTime.Now:dd.MM.yyyy}", false, "14");
        }

        private void AddDatasetDescription(Body body, CustomerFeedbackData data)
        {
            AddParagraph(body, "ОПИС ДАТАСЕТУ", true, "16");
            AddEmptyParagraph(body, 1);
            AddParagraph(body, $"Датасет містить інформацію про відгуки клієнтів та їх задоволеність.");
            AddParagraph(body, $"Кількість записів: {data.TotalRecords}");
            AddParagraph(body, $"Кількість унікальних клієнтів: {data.TotalCustomers}");
            AddEmptyParagraph(body, 1);
            AddParagraph(body, "Основні поля датасету:", true);
            AddParagraph(body, "• Інформація про клієнта: вік, стать, країна");
            AddParagraph(body, "• Інформація про продукт: назва, категорія (Bronze/Silver/Gold)");
            AddParagraph(body, "• Метрики: якість сервісу, частота покупок, оцінка відгуку, рівень лояльності, оцінка задоволеності");
        }

        // Створюю таблицю з ключовими метриками (середні значення, кількості)
        private void AddKeyMetrics(Body body, CustomerFeedbackData data)
        {
            AddParagraph(body, "КЛЮЧОВІ Змінні", true, "16");
            AddEmptyParagraph(body, 1);

            var table = new Table();
            var tableProperties = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 }
                )
            );
            table.AppendChild(tableProperties);

            AddTableRow(table, "Метрика", "Значення", true);
            AddTableRow(table, "Загальна кількість відгуків", data.TotalRecords.ToString());
            AddTableRow(table, "Середня оцінка відгуку", $"{data.Feedbacks.Average(f => (double)f.FeedbackScore):F2}");
            AddTableRow(table, "Середня задоволеність клієнтів", $"{data.Feedbacks.Average(f => (double)f.SatisfactionScore):F2}");
            AddTableRow(table, "Середня частота покупок", $"{data.Feedbacks.Average(f => f.PurchaseFrequency):F2}");

            body.AppendChild(table);
        }

        private void AddConclusions(Body body, CustomerFeedbackData data)
        {
            AddParagraph(body, "ВИСНОВКИ", true, "16");
            AddEmptyParagraph(body, 1);

            var topCategory = data.Feedbacks.GroupBy(f => f.Category)
                .OrderByDescending(g => g.Average(f => (double)f.SatisfactionScore))
                .FirstOrDefault();

            AddParagraph(body, $"1. Проаналізовано {data.TotalRecords} відгуків від {data.TotalCustomers} унікальних клієнтів.");
            AddParagraph(body, $"2. Найкраща категорія за задоволеністю: {(topCategory?.Key.ToString() != null ? topCategory.Key.ToString() : "N/A")} " +
                $"з середньою оцінкою {topCategory?.Average(f => (double)f.SatisfactionScore):F2}.");
            AddParagraph(body, $"3. Середня задоволеність клієнтів складає {data.Feedbacks.Average(f => (double)f.SatisfactionScore):F2} балів.");
            AddParagraph(body, "4. Система успішно реалізує імпорт/експорт даних у форматах CSV, JSON, XML, XLSX.");
            AddParagraph(body, "5. Реалізовано фільтрацію, сортування, групування та візуалізацію даних.");
        }

        private void AddParagraph(Body body, string text, bool bold = false, string fontSize = "12", bool centered = false)
        {
            var paragraph = body.AppendChild(new Paragraph());
            var run = paragraph.AppendChild(new Run());
            var runProperties = run.AppendChild(new RunProperties());

            if (bold)
            {
                runProperties.AppendChild(new Bold());
            }

            runProperties.AppendChild(new FontSize { Val = fontSize });

            if (centered)
            {
                var paragraphProperties = new ParagraphProperties();
                paragraphProperties.AppendChild(new Justification { Val = JustificationValues.Center });
                paragraph.InsertAt(paragraphProperties, 0);
            }

            run.AppendChild(new Text(text));
        }

        private void AddEmptyParagraph(Body body, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                body.AppendChild(new Paragraph());
            }
        }

        // Додаю рядок до таблиці   
        private void AddTableRow(Table table, string cell1, string cell2, bool isHeader = false)
        {
            var row = new TableRow();

            var tc1 = new TableCell();
            var p1 = new Paragraph(new Run(new Text(cell1)));
            if (isHeader)
            {
                p1.InsertAt(new ParagraphProperties(new Justification { Val = JustificationValues.Center }), 0);
                p1.Descendants<Run>().First().InsertAt(new RunProperties(new Bold()), 0);
            }
            tc1.Append(p1);
            row.Append(tc1);

            var tc2 = new TableCell();
            var p2 = new Paragraph(new Run(new Text(cell2)));
            if (isHeader)
            {
                p2.InsertAt(new ParagraphProperties(new Justification { Val = JustificationValues.Center }), 0);
                p2.Descendants<Run>().First().InsertAt(new RunProperties(new Bold()), 0);
            }
            tc2.Append(p2);
            row.Append(tc2);

            table.Append(row);
        }

    }
}
