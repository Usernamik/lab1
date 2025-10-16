using Domain.Entities;
using Domain.Interfaces;
using Data.Providers;
using Data.Logging;
using Data.Reports;
using Domain.Enums;

namespace Lab_1._2
{
    public partial class Form1 : Form
    {
        private CustomerFeedbackData _data = new CustomerFeedbackData();
        private CustomerFeedbackData _filteredData = new CustomerFeedbackData();
        private Logger _logger;
        private string _currentFilePath = string.Empty;
        private List<string> _recentFiles = new List<string>();

        public Form1()
        {
            InitializeComponent();
            _logger = new Logger(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "app.log"));
            _logger.Log("Application started");
            InitializeDataGridView();
            InitializeFilterControls();
            LoadRecentFiles();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerId",
                HeaderText = "CustomerID",
                DataPropertyName = "CustomerId",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Age",
                HeaderText = "Age",
                DataPropertyName = "Age",
                Width = 60
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Gender",
                HeaderText = "Gender",
                DataPropertyName = "Gender",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Country",
                HeaderText = "Country",
                DataPropertyName = "Country",
                Width = 100
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product",
                DataPropertyName = "ProductName",
                Width = 150
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Category",
                HeaderText = "ProductQuality",
                DataPropertyName = "Category",
                Width = 120
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Income",
                HeaderText = "Income",
                DataPropertyName = "Income",
                Width = 100
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ServiceQuality",
                HeaderText = "ServiceQuality",
                DataPropertyName = "ServiceQuality",
                Width = 120
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PurchaseFrequency",
                HeaderText = "PurchaseFrequency",
                DataPropertyName = "PurchaseFrequency",
                Width = 130
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FeedbackScore",
                HeaderText = "FeedbackScore",
                DataPropertyName = "FeedbackScore",
                Width = 120
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoyaltyLevel",
                HeaderText = "LoyaltyLevel",
                DataPropertyName = "LoyaltyLevel",
                Width = 130
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SatisfactionScore",
                HeaderText = "SatisfactionScore",
                DataPropertyName = "SatisfactionScore",
                Width = 150
            });
        }

        private void InitializeFilterControls()
        {
            comboBoxGender.Items.Clear();
            comboBoxGender.Items.Add("Всі");
            comboBoxGender.Items.Add(Gender.Male.ToString());
            comboBoxGender.Items.Add(Gender.Female.ToString());
            comboBoxGender.SelectedIndex = 0;

            comboBoxCategory.Items.Clear();
            comboBoxCategory.Items.Add("Всі");
            comboBoxCategory.Items.Add(ProductCategory.Bronze.ToString());
            comboBoxCategory.Items.Add(ProductCategory.Silver.ToString());
            comboBoxCategory.Items.Add(ProductCategory.Gold.ToString());
            comboBoxCategory.SelectedIndex = 0;

            comboBoxServiceQuality.Items.Clear();
            comboBoxServiceQuality.Items.Add("Всі");
            comboBoxServiceQuality.Items.Add(ServiceQuality.Low.ToString());
            comboBoxServiceQuality.Items.Add(ServiceQuality.Medium.ToString());
            comboBoxServiceQuality.Items.Add(ServiceQuality.High.ToString());
            comboBoxServiceQuality.SelectedIndex = 0;

            comboBoxLoyalty.Items.Clear();
            comboBoxLoyalty.Items.Add("Всі");
            comboBoxLoyalty.Items.Add(LoyaltyLevel.Low.ToString());
            comboBoxLoyalty.Items.Add(LoyaltyLevel.Medium.ToString());
            comboBoxLoyalty.Items.Add(LoyaltyLevel.High.ToString());
            comboBoxLoyalty.SelectedIndex = 0;
        }

        private void LoadRecentFiles()
        {
            // TODO: Завантажити список останніх файлів з налаштувань
        }

        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|JSON files (*.json)|*.json|XML files (*.xml)|*.xml|Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _currentFilePath = openFileDialog.FileName;
                    LoadData(_currentFilePath);
                }
            }
        }

        private void LoadData(string filePath)
        {
            try
            {
                IDataProvider? provider = null;
                string extension = Path.GetExtension(filePath).ToLower();

                switch (extension)
                {
                    case ".csv":
                        provider = new CsvDataProvider();
                        break;
                    case ".json":
                        provider = new JsonDataProvider();
                        break;
                    case ".xml":
                        provider = new XmlDataProvider();
                        break;
                    case ".xlsx":
                        provider = new XlsxDataProvider();
                        break;
                    default:
                        MessageBox.Show("Непідтримуваний формат файлу!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                _data = provider.ReadData(filePath);
                _logger.Log($"Imported {_data.TotalRecords} records from {filePath}");

                ApplyFilters();
                UpdateStatusBar();

                MessageBox.Show($"Успішно завантажено {_data.TotalRecords} записів!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error importing file {filePath}: {ex.Message}", "ERROR");
                MessageBox.Show($"Помилка при завантаженні файлу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            if (_data == null || _data.Feedbacks == null)
            {
                _filteredData = new CustomerFeedbackData();
                dataGridView1.DataSource = null;
                return;
            }

            var filteredFeedbacks = _data.Feedbacks.AsEnumerable();

            // Фільтр по статі
            if (comboBoxGender.SelectedIndex > 0)
            {
                string selectedGender = comboBoxGender.SelectedItem?.ToString() ?? "";
                if (Enum.TryParse<Gender>(selectedGender, out Gender gender))
                {
                    filteredFeedbacks = filteredFeedbacks.Where(f => f.Customer != null && f.Customer.Gender == gender);
                }
            }

            // Фільтр по категорії
            if (comboBoxCategory.SelectedIndex > 0)
            {
                string selectedCategory = comboBoxCategory.SelectedItem?.ToString() ?? "";
                if (Enum.TryParse<ProductCategory>(selectedCategory, out ProductCategory category))
                {
                    filteredFeedbacks = filteredFeedbacks.Where(f => f.Category == category);
                }
            }

            // Фільтр по якості сервісу
            if (comboBoxServiceQuality.SelectedIndex > 0)
            {
                string selectedQuality = comboBoxServiceQuality.SelectedItem?.ToString() ?? "";
                if (Enum.TryParse<ServiceQuality>(selectedQuality, out ServiceQuality quality))
                {
                    filteredFeedbacks = filteredFeedbacks.Where(f => f.ServiceQuality == quality);
                }
            }

            // Фільтр по рівню лояльності
            if (comboBoxLoyalty.SelectedIndex > 0)
            {
                string selectedLoyalty = comboBoxLoyalty.SelectedItem?.ToString() ?? "";
                if (Enum.TryParse<LoyaltyLevel>(selectedLoyalty, out LoyaltyLevel loyalty))
                {
                    filteredFeedbacks = filteredFeedbacks.Where(f => f.LoyaltyLevel == loyalty);
                }
            }

            // Фільтр по країні
            if (!string.IsNullOrWhiteSpace(textBoxCountry.Text))
            {
                string countryFilter = textBoxCountry.Text.Trim();
                filteredFeedbacks = filteredFeedbacks.Where(f => 
                    f.Customer != null && 
                    f.Customer.Country.Contains(countryFilter, StringComparison.OrdinalIgnoreCase));
            }

            var resultList = filteredFeedbacks.ToList();

            _filteredData = new CustomerFeedbackData
            {
                Feedbacks = resultList,
                Customers = resultList.Select(f => f.Customer).Where(c => c != null).Distinct().ToList()!
            };

            var displayList = resultList.Select(f => new
            {
                CustomerId = f.CustomerId,
                Age = f.Customer?.Age ?? 0,
                Gender = f.Customer?.Gender.ToString() ?? "",
                Country = f.Customer?.Country ?? "",
                ProductName = f.ProductName,
                Category = f.Category.ToString(),
                Income = f.Customer?.Income ?? 0,
                ServiceQuality = f.ServiceQuality.ToString(),
                PurchaseFrequency = f.PurchaseFrequency,
                FeedbackScore = f.FeedbackScore,
                LoyaltyLevel = f.LoyaltyLevel.ToString(),
                SatisfactionScore = f.SatisfactionScore
            }).ToList();

            dataGridView1.DataSource = displayList;

            string filterInfo = GetFilterInfo();
            _logger.Log($"Filter applied: {filterInfo}, Results: {resultList.Count}");

            UpdateStatusBar();
        }

        private string GetFilterInfo()
        {
            List<string> filters = new List<string>();

            if (comboBoxGender.SelectedIndex > 0)
                filters.Add($"Gender = {comboBoxGender.SelectedItem}");

            if (comboBoxCategory.SelectedIndex > 0)
                filters.Add($"Category = {comboBoxCategory.SelectedItem}");

            if (comboBoxServiceQuality.SelectedIndex > 0)
                filters.Add($"ServiceQuality = {comboBoxServiceQuality.SelectedItem}");

            if (comboBoxLoyalty.SelectedIndex > 0)
                filters.Add($"Loyalty = {comboBoxLoyalty.SelectedItem}");

            if (!string.IsNullOrWhiteSpace(textBoxCountry.Text))
                filters.Add($"Country contains '{textBoxCountry.Text}'");

            return filters.Count > 0 ? string.Join(", ", filters) : "No filters";
        }

        private void UpdateStatusBar()
        {
            toolStripStatusLabel1.Text = $"Всього записів: {_data.TotalRecords} | Відфільтровано: {_filteredData.TotalRecords}";
        }

        private void ButtonApplyFilter_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ButtonResetFilter_Click(object sender, EventArgs e)
        {
            comboBoxGender.SelectedIndex = 0;
            comboBoxCategory.SelectedIndex = 0;
            comboBoxServiceQuality.SelectedIndex = 0;
            comboBoxLoyalty.SelectedIndex = 0;
            textBoxCountry.Text = "";
            ApplyFilters();
        }

        private void ShowChartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_filteredData == null || _filteredData.Feedbacks.Count == 0)
            {
                MessageBox.Show("Спочатку завантажте дані!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChartForm chartForm = new ChartForm(_filteredData);
            chartForm.ShowDialog();
        }

        private void ExportDocxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_filteredData == null || _filteredData.Feedbacks.Count == 0)
            {
                MessageBox.Show("Немає даних для експорту!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Word documents (*.docx)|*.docx";
                saveFileDialog.FileName = "Report.docx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DocxReportService reportService = new DocxReportService();
                        reportService.GenerateReport(_filteredData, saveFileDialog.FileName);
                        _logger.Log($"DOCX report generated: {saveFileDialog.FileName}");
                        MessageBox.Show("Звіт успішно створено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log($"Error generating DOCX report: {ex.Message}", "ERROR");
                        MessageBox.Show($"Помилка при створенні звіту: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportXlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_filteredData == null || _filteredData.Feedbacks.Count == 0)
            {
                MessageBox.Show("Немає даних для експорту!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = "Report.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XlsxReportService reportService = new XlsxReportService();
                        reportService.GenerateReport(_filteredData, saveFileDialog.FileName);
                        _logger.Log($"XLSX report generated: {saveFileDialog.FileName}");
                        MessageBox.Show("Звіт успішно створено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log($"Error generating XLSX report: {ex.Message}", "ERROR");
                        MessageBox.Show($"Помилка при створенні звіту: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GenerateDocxReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportDocxToolStripMenuItem_Click(sender, e);
        }

        private void GenerateXlsxReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportXlsxToolStripMenuItem_Click(sender, e);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aboutMessage = "Аналіз відгуків клієнтів\n\n" +
                                  "Версія: 1.0\n" +
                                  "Студент: Лісовський Данило Олександрович\n" +
                                  "Група: КН 22\n" +
                                  "Варіант: 19\n\n" +
                                  "Програма для аналізу даних відгуків клієнтів з підтримкою:\n" +
                                  "• Імпорту даних (CSV, JSON, XML, XLSX)\n" +
                                  "• Фільтрації та сортування\n" +
                                  "• Візуалізації даних (графіки)\n" +
                                  "• Генерації звітів (Word, Excel)\n\n" +
                                  "© 2024";

            MessageBox.Show(aboutMessage, "Про програму", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _logger.Log("About dialog shown");
        }
    }
}
