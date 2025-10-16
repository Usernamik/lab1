using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using OfficeOpenXml;
using CsvHelper;
using Newtonsoft.Json;

namespace Laba_1;

public partial class MainForm : Form
{
    private DataTable dataTable;

    public MainForm()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Title = "Виберіть файл з даними";
        openFileDialog.Filter =
            "CSV файли (*.csv)|*.csv|Excel файли (*.xlsx)|*.xlsx|JSON файли (*.json)|*.json|Всі файли (*.*)|*.*";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFileDialog.FileName;
            string extension = Path.GetExtension(filePath).ToLower();

            try
            {
                switch (extension)
                {
                    case ".csv":
                        dataTable = LoadCsvFile(filePath);
                        break;
                    case ".xlsx":
                        dataTable = LoadExcelFile(filePath);
                        break;
                    case ".json":
                        dataTable = LoadJsonFile(filePath);
                        break;
                    default:
                        MessageBox.Show("Непідтримуваний формат файлу!");
                        return;
                }

                // Якщо дані успішно завантажені — прив'язуємо їх до DataGridView на формі.
                if (dataTable != null && dataTable.Columns.Count > 0)
                {
                    // Шукаємо DataGridView з ім'ям "dataGridView1" у дочірніх контролах, або просто перший DataGridView на формі.
                    var dgv = this.Controls.Find("dataGridView1", true).FirstOrDefault() as DataGridView
                              ?? this.Controls.OfType<DataGridView>().FirstOrDefault();

                    if (dgv != null)
                    {
                        dgv.DataSource = dataTable;
                        dgv.AutoResizeColumns();
                    }
                    else
                    {
                        MessageBox.Show("Файл завантажено успішно, але на формі не знайдено DataGridView для відображення даних.");
                    }
                }
                else
                {
                    MessageBox.Show("Файл завантажено, але дані відсутні або пусті.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження файлу: {ex.Message}");
            }
        }
    }

    private DataTable LoadCsvFile(string filePath)
    {
        DataTable dt = new DataTable();

        using (var reader = new StreamReader(filePath, Encoding.UTF8))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();
            
            if (csv.HeaderRecord != null)
            {
                foreach (string header in csv.HeaderRecord)
                {
                    dt.Columns.Add(header);
                }
            }
            
            while (csv.Read())
            {
                DataRow row = dt.NewRow();
                for (int i = 0; i < csv.HeaderRecord.Length; i++)
                {
                    row[i] = csv.GetField(i);
                }

                dt.Rows.Add(row);
            }
        }

        return dt;
    }

    private DataTable LoadExcelFile(string filePath)
    {
        DataTable dt = new DataTable();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Для EPPlus

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                dt.Columns.Add(worksheet.Cells[1, col].Value?.ToString() ?? $"Column{col}");
            }
            
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var dataRow = dt.NewRow();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    dataRow[col - 1] = worksheet.Cells[row, col].Value?.ToString() ?? "";
                }

                dt.Rows.Add(dataRow);
            }
        }

        return dt;
    }

    private DataTable LoadJsonFile(string filePath)
    {
        string jsonText = File.ReadAllText(filePath, Encoding.UTF8);
        var jsonArray = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(jsonText);

        DataTable dt = new DataTable();
        bool columnsAdded = false;

        foreach (var item in jsonArray)
        {
            if (!columnsAdded)
            {
                foreach (var property in item.Children<Newtonsoft.Json.Linq.JProperty>())
                {
                    dt.Columns.Add(property.Name);
                }

                columnsAdded = true;
            }

            DataRow row = dt.NewRow();
            foreach (var property in item.Children<Newtonsoft.Json.Linq.JProperty>())
            {
                row[property.Name] = property.Value?.ToString() ?? "";
            }

            dt.Rows.Add(row);
        }

        return dt;
    }
}

    

