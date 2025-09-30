using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private BindingList<FileDisplayModel> _bindingList = new BindingList<FileDisplayModel>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _bindingList;

            // Додаємо колонки
            var deleteColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Видалити",
                DataPropertyName = "Delete",
                Name = "DeleteColumn",
                Width = 70
            };
            dataGridView1.Columns.Add(deleteColumn);

            var isOriginalColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Оригінал",
                DataPropertyName = "IsOriginal",
                Name = "IsOriginalColumn",
                Width = 70,
                ReadOnly = true
            };
            dataGridView1.Columns.Add(isOriginalColumn);

            var sizeColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Розмір (байт)",
                DataPropertyName = "Size",
                Name = "SizeColumn",
                Width = 100,
                ReadOnly = true
            };
            dataGridView1.Columns.Add(sizeColumn);

            var pathColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Шлях до файлу",
                DataPropertyName = "FullPath",
                Name = "PathColumn",
                Width = 400,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            dataGridView1.Columns.Add(pathColumn);

            var hashColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Хеш",
                DataPropertyName = "Hash",
                Name = "HashColumn",
                Width = 200,
                ReadOnly = true
            };
            dataGridView1.Columns.Add(hashColumn);
        }

        private async void butnScan_Click(object sender, EventArgs e)
        {
            string rootPath = textPath.Text;

            if (string.IsNullOrWhiteSpace(rootPath) || !Directory.Exists(rootPath))
            {
                MessageBox.Show("Введіть правильний шлях до директорії.", "Помилка");
                return;
            }

            butnScan.Enabled = false;
            btnDelete.Enabled = false;
            progressBar.Style = ProgressBarStyle.Marquee;

            DuplicateFinder finder = new DuplicateFinder();
            List<DuplicateGroup> results = null;

            try
            {
                results = await finder.FindDuplicatesAsync(rootPath);
                DisplayResults(results);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Виникла помилка при скануванні: {ex.Message}", "Помилка",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                butnScan.Enabled = true;
                btnDelete.Enabled = true;
                progressBar.Style = ProgressBarStyle.Blocks;
            }
        }

        private void DisplayResults(List<DuplicateGroup>? results)
        {
            _bindingList.Clear();

            if (results == null || results.Count == 0)
            {
                MessageBox.Show("Дублікатів не знайдено", "Результат");
                return;
            }

            foreach (var group in results)
            {
                group.MarkOriginal();

                foreach (var file in group.Files)
                {
                    _bindingList.Add(new FileDisplayModel
                    {
                        Delete = !file.IsOriginal,
                        Hash = file.Hash,
                        FullPath = file.FullPath,
                        IsOriginal = file.IsOriginal,
                        Size = file.Size
                    });
                }
            }

            MessageBox.Show($"Знайдено {results.Count} груп дублікатів ({_bindingList.Count} файлів).", "Результат");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textPath.Text = fbd.SelectedPath;
                }
            }
        }

        private List<string> GetSelectedFilesFromGrid()
        {
            var filesToDelete = new List<string>();

            foreach (var item in _bindingList)
            {
                if (item.Delete && !item.IsOriginal)
                {
                    filesToDelete.Add(item.FullPath);
                }
            }

            return filesToDelete;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var filesToDelete = GetSelectedFilesFromGrid();

            if (filesToDelete.Count == 0)
            {
                MessageBox.Show("Не вибрано жодного файлу для видалення.");
                return;
            }

            if (MessageBox.Show($"Ви впевнені, що хочете видалити {filesToDelete.Count} файл(ів)?",
                                "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DuplicateFinder finder = new DuplicateFinder();
                int deletedCount = 0;

                foreach (var file in filesToDelete)
                {
                    if (finder.DeleteDuplicate(file))
                    {
                        deletedCount++;
                    }
                }

                MessageBox.Show($"Видалено {deletedCount} файл(ів).");

                // Оновлюємо список після видалення
                butnScan_Click(null, null);
            }
        }

        public class FileDisplayModel
        {
            public bool Delete { get; set; }
            public string Hash { get; set; }
            public string FullPath { get; set; }
            public bool IsOriginal { get; set; }
            public long Size { get; set; }
        }
    }
}

