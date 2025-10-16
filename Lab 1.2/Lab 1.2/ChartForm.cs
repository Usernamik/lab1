using Domain.Entities;
using ScottPlot;
using ScottPlot.WinForms;

namespace Lab_1._2
{
    public class ChartForm : Form
    {
        private CustomerFeedbackData _data;
        private FormsPlot formsPlot1 = null!;
        private ComboBox comboBoxChartType = null!;

        public ChartForm(CustomerFeedbackData data)
        {
            _data = data;
            InitializeComponent();
            LoadChart1();
        }

        private void InitializeComponent()
        {
            this.formsPlot1 = new FormsPlot();
            this.comboBoxChartType = new ComboBox();

            this.SuspendLayout();

            this.formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.formsPlot1.Location = new Point(12, 50);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new Size(960, 538);
            this.formsPlot1.TabIndex = 0;

            this.comboBoxChartType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxChartType.FormattingEnabled = true;
            this.comboBoxChartType.Items.AddRange(new object[] {
                "Розподіл за категоріями продуктів",
                "Середня задоволеність за якістю сервісу",
                "Розподіл за країнами"
            });
            this.comboBoxChartType.Location = new Point(12, 12);
            this.comboBoxChartType.Name = "comboBoxChartType";
            this.comboBoxChartType.Size = new Size(300, 23);
            this.comboBoxChartType.TabIndex = 1;
            this.comboBoxChartType.SelectedIndex = 0;
            this.comboBoxChartType.SelectedIndexChanged += ComboBoxChartType_SelectedIndexChanged;

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(984, 600);
            this.Controls.Add(this.comboBoxChartType);
            this.Controls.Add(this.formsPlot1);
            this.Name = "ChartForm";
            this.Text = "Графіки";
            this.ResumeLayout(false);
        }

        private void ComboBoxChartType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (comboBoxChartType.SelectedIndex)
            {
                case 0:
                    LoadChart1();
                    break;
                case 1:
                    LoadChart2();
                    break;
                case 2:
                    LoadChart3();
                    break;
            }
        }

        // Групую дані по категоріям і будую стовпчасту діаграму
        private void LoadChart1()
        {
            formsPlot1.Plot.Clear();

            var categoryGroups = _data.Feedbacks
                .GroupBy(f => f.Category)
                .Select(g => new { Category = g.Key.ToString(), Count = g.Count() })
                .OrderBy(x => x.Category)
                .ToList();

            double[] values = categoryGroups.Select(x => (double)x.Count).ToArray();
            string[] labels = categoryGroups.Select(x => x.Category).ToArray();

            var barPlot = formsPlot1.Plot.Add.Bars(values);
            formsPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                Enumerable.Range(0, labels.Length).Select(i => (double)i).ToArray(),
                labels
            );

            formsPlot1.Plot.Title("Розподіл за категоріями продуктів");
            formsPlot1.Plot.YLabel("Кількість");
            formsPlot1.Plot.XLabel("Категорія");

            formsPlot1.Refresh();
        }

        // Розраховую середню задоволеність для кожного рівня якості сервісу
        private void LoadChart2()
        {
            formsPlot1.Plot.Clear();

            var qualityGroups = _data.Feedbacks
                .GroupBy(f => f.ServiceQuality)
                .Select(g => new { Quality = g.Key.ToString(), AvgSatisfaction = g.Average(f => (double)f.SatisfactionScore) })
                .OrderBy(x => x.Quality)
                .ToList();

            double[] values = qualityGroups.Select(x => x.AvgSatisfaction).ToArray();
            string[] labels = qualityGroups.Select(x => x.Quality).ToArray();

            var barPlot = formsPlot1.Plot.Add.Bars(values);
            formsPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                Enumerable.Range(0, labels.Length).Select(i => (double)i).ToArray(),
                labels
            );

            formsPlot1.Plot.Title("Середня задоволеність за якістю сервісу");
            formsPlot1.Plot.YLabel("Середня оцінка задоволеності");
            formsPlot1.Plot.XLabel("Якість сервісу");

            formsPlot1.Refresh();
        }

        // Відбираю топ-10 країн за кількістю відгуків
        private void LoadChart3()
        {
            formsPlot1.Plot.Clear();

            var countryGroups = _data.Feedbacks
                .GroupBy(f => f.Customer?.Country != null ? f.Customer.Country : "Unknown")
                .Select(g => new { Country = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToList();

            double[] values = countryGroups.Select(x => (double)x.Count).ToArray();
            string[] labels = countryGroups.Select(x => x.Country).ToArray();

            var barPlot = formsPlot1.Plot.Add.Bars(values);
            formsPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                Enumerable.Range(0, labels.Length).Select(i => (double)i).ToArray(),
                labels
            );

            formsPlot1.Plot.Title("Топ-10 країн за кількістю відгуків");
            formsPlot1.Plot.YLabel("Кількість відгуків");
            formsPlot1.Plot.XLabel("Країна");

            formsPlot1.Refresh();
        }
    }
}
