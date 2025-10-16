namespace Lab_1._2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem exportDocxToolStripMenuItem;
        private ToolStripMenuItem exportXlsxToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem showChartsToolStripMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem generateDocxReportToolStripMenuItem;
        private ToolStripMenuItem generateXlsxReportToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private DataGridView dataGridView1;
        private GroupBox groupBoxFilters;
        private Label labelGender;
        private ComboBox comboBoxGender;
        private Label labelCategory;
        private ComboBox comboBoxCategory;
        private Label labelServiceQuality;
        private ComboBox comboBoxServiceQuality;
        private Label labelLoyalty;
        private ComboBox comboBoxLoyalty;
        private Label labelCountry;
        private TextBox textBoxCountry;
        private Button buttonApplyFilter;
        private Button buttonResetFilter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new MenuStrip();
            this.fileToolStripMenuItem = new ToolStripMenuItem();
            this.importToolStripMenuItem = new ToolStripMenuItem();
            this.exportToolStripMenuItem = new ToolStripMenuItem();
            this.exportDocxToolStripMenuItem = new ToolStripMenuItem();
            this.exportXlsxToolStripMenuItem = new ToolStripMenuItem();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.viewToolStripMenuItem = new ToolStripMenuItem();
            this.showChartsToolStripMenuItem = new ToolStripMenuItem();
            this.reportsToolStripMenuItem = new ToolStripMenuItem();
            this.generateDocxReportToolStripMenuItem = new ToolStripMenuItem();
            this.generateXlsxReportToolStripMenuItem = new ToolStripMenuItem();
            this.helpToolStripMenuItem = new ToolStripMenuItem();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            this.statusStrip1 = new StatusStrip();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.dataGridView1 = new DataGridView();
            this.groupBoxFilters = new GroupBox();
            this.buttonResetFilter = new Button();
            this.buttonApplyFilter = new Button();
            this.textBoxCountry = new TextBox();
            this.labelCountry = new Label();
            this.comboBoxLoyalty = new ComboBox();
            this.labelLoyalty = new Label();
            this.comboBoxServiceQuality = new ComboBox();
            this.labelServiceQuality = new Label();
            this.comboBoxCategory = new ComboBox();
            this.labelCategory = new Label();
            this.comboBoxGender = new ComboBox();
            this.labelGender = new Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBoxFilters.SuspendLayout();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                this.fileToolStripMenuItem,
                this.viewToolStripMenuItem,
                this.reportsToolStripMenuItem,
                this.helpToolStripMenuItem});
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(1200, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";

            // fileToolStripMenuItem
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.importToolStripMenuItem,
                this.exportToolStripMenuItem,
                this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";

            // importToolStripMenuItem
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new Size(180, 22);
            this.importToolStripMenuItem.Text = "Імпорт...";
            this.importToolStripMenuItem.Click += new EventHandler(this.ImportToolStripMenuItem_Click);

            // exportToolStripMenuItem
            this.exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.exportDocxToolStripMenuItem,
                this.exportXlsxToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new Size(180, 22);
            this.exportToolStripMenuItem.Text = "Експорт";

            // exportDocxToolStripMenuItem
            this.exportDocxToolStripMenuItem.Name = "exportDocxToolStripMenuItem";
            this.exportDocxToolStripMenuItem.Size = new Size(180, 22);
            this.exportDocxToolStripMenuItem.Text = "Word (DOCX)...";
            this.exportDocxToolStripMenuItem.Click += new EventHandler(this.ExportDocxToolStripMenuItem_Click);

            // exportXlsxToolStripMenuItem
            this.exportXlsxToolStripMenuItem.Name = "exportXlsxToolStripMenuItem";
            this.exportXlsxToolStripMenuItem.Size = new Size(180, 22);
            this.exportXlsxToolStripMenuItem.Text = "Excel (XLSX)...";
            this.exportXlsxToolStripMenuItem.Click += new EventHandler(this.ExportXlsxToolStripMenuItem_Click);

            // exitToolStripMenuItem
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new Size(180, 22);
            this.exitToolStripMenuItem.Text = "Вихід";
            this.exitToolStripMenuItem.Click += new EventHandler(this.ExitToolStripMenuItem_Click);

            // viewToolStripMenuItem
            this.viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.showChartsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new Size(49, 20);
            this.viewToolStripMenuItem.Text = "Вигляд";

            // showChartsToolStripMenuItem
            this.showChartsToolStripMenuItem.Name = "showChartsToolStripMenuItem";
            this.showChartsToolStripMenuItem.Size = new Size(180, 22);
            this.showChartsToolStripMenuItem.Text = "Показати графіки";
            this.showChartsToolStripMenuItem.Click += new EventHandler(this.ShowChartsToolStripMenuItem_Click);

            // reportsToolStripMenuItem
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.generateDocxReportToolStripMenuItem,
                this.generateXlsxReportToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new Size(51, 20);
            this.reportsToolStripMenuItem.Text = "Звіти";

            // generateDocxReportToolStripMenuItem
            this.generateDocxReportToolStripMenuItem.Name = "generateDocxReportToolStripMenuItem";
            this.generateDocxReportToolStripMenuItem.Size = new Size(180, 22);
            this.generateDocxReportToolStripMenuItem.Text = "Звіт Word (DOCX)...";
            this.generateDocxReportToolStripMenuItem.Click += new EventHandler(this.GenerateDocxReportToolStripMenuItem_Click);

            // generateXlsxReportToolStripMenuItem
            this.generateXlsxReportToolStripMenuItem.Name = "generateXlsxReportToolStripMenuItem";
            this.generateXlsxReportToolStripMenuItem.Size = new Size(180, 22);
            this.generateXlsxReportToolStripMenuItem.Text = "Звіт Excel (XLSX)...";
            this.generateXlsxReportToolStripMenuItem.Click += new EventHandler(this.GenerateXlsxReportToolStripMenuItem_Click);

            // helpToolStripMenuItem
            this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new Size(65, 20);
            this.helpToolStripMenuItem.Text = "Довідка";

            // aboutToolStripMenuItem
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new Size(180, 22);
            this.aboutToolStripMenuItem.Text = "Про програму...";
            this.aboutToolStripMenuItem.Click += new EventHandler(this.AboutToolStripMenuItem_Click);

            // statusStrip1
            this.statusStrip1.Items.AddRange(new ToolStripItem[] {
                this.toolStripStatusLabel1});
            this.statusStrip1.Location = new Point(0, 676);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(1200, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";

            // toolStripStatusLabel1
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(118, 17);
            this.toolStripStatusLabel1.Text = "Готово до роботи";

            // dataGridView1
            this.dataGridView1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left)
                | AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new Point(12, 177);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new Size(1176, 496);
            this.dataGridView1.TabIndex = 2;

            // groupBoxFilters
            this.groupBoxFilters.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left)
                | AnchorStyles.Right)));
            this.groupBoxFilters.Controls.Add(this.buttonResetFilter);
            this.groupBoxFilters.Controls.Add(this.buttonApplyFilter);
            this.groupBoxFilters.Controls.Add(this.textBoxCountry);
            this.groupBoxFilters.Controls.Add(this.labelCountry);
            this.groupBoxFilters.Controls.Add(this.comboBoxLoyalty);
            this.groupBoxFilters.Controls.Add(this.labelLoyalty);
            this.groupBoxFilters.Controls.Add(this.comboBoxServiceQuality);
            this.groupBoxFilters.Controls.Add(this.labelServiceQuality);
            this.groupBoxFilters.Controls.Add(this.comboBoxCategory);
            this.groupBoxFilters.Controls.Add(this.labelCategory);
            this.groupBoxFilters.Controls.Add(this.comboBoxGender);
            this.groupBoxFilters.Controls.Add(this.labelGender);
            this.groupBoxFilters.Location = new Point(12, 27);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new Size(1176, 144);
            this.groupBoxFilters.TabIndex = 3;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Фільтри";

            // labelGender
            this.labelGender.AutoSize = true;
            this.labelGender.Location = new Point(15, 25);
            this.labelGender.Name = "labelGender";
            this.labelGender.Size = new Size(40, 15);
            this.labelGender.TabIndex = 0;
            this.labelGender.Text = "Стать:";

            // comboBoxGender
            this.comboBoxGender.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Location = new Point(15, 43);
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.Size = new Size(200, 23);
            this.comboBoxGender.TabIndex = 1;

            // labelCategory
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new Point(235, 25);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new Size(66, 15);
            this.labelCategory.TabIndex = 2;
            this.labelCategory.Text = "Категорія:";

            // comboBoxCategory
            this.comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new Point(235, 43);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new Size(200, 23);
            this.comboBoxCategory.TabIndex = 3;

            // labelServiceQuality
            this.labelServiceQuality.AutoSize = true;
            this.labelServiceQuality.Location = new Point(455, 25);
            this.labelServiceQuality.Name = "labelServiceQuality";
            this.labelServiceQuality.Size = new Size(94, 15);
            this.labelServiceQuality.TabIndex = 4;
            this.labelServiceQuality.Text = "Якість сервісу:";

            // comboBoxServiceQuality
            this.comboBoxServiceQuality.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxServiceQuality.FormattingEnabled = true;
            this.comboBoxServiceQuality.Location = new Point(455, 43);
            this.comboBoxServiceQuality.Name = "comboBoxServiceQuality";
            this.comboBoxServiceQuality.Size = new Size(200, 23);
            this.comboBoxServiceQuality.TabIndex = 5;

            // labelLoyalty
            this.labelLoyalty.AutoSize = true;
            this.labelLoyalty.Location = new Point(675, 25);
            this.labelLoyalty.Name = "labelLoyalty";
            this.labelLoyalty.Size = new Size(117, 15);
            this.labelLoyalty.TabIndex = 6;
            this.labelLoyalty.Text = "Рівень лояльності:";

            // comboBoxLoyalty
            this.comboBoxLoyalty.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxLoyalty.FormattingEnabled = true;
            this.comboBoxLoyalty.Location = new Point(675, 43);
            this.comboBoxLoyalty.Name = "comboBoxLoyalty";
            this.comboBoxLoyalty.Size = new Size(200, 23);
            this.comboBoxLoyalty.TabIndex = 7;

            // labelCountry
            this.labelCountry.AutoSize = true;
            this.labelCountry.Location = new Point(15, 80);
            this.labelCountry.Name = "labelCountry";
            this.labelCountry.Size = new Size(50, 15);
            this.labelCountry.TabIndex = 8;
            this.labelCountry.Text = "Країна:";

            // textBoxCountry
            this.textBoxCountry.Location = new Point(15, 98);
            this.textBoxCountry.Name = "textBoxCountry";
            this.textBoxCountry.Size = new Size(200, 23);
            this.textBoxCountry.TabIndex = 9;

            // buttonApplyFilter
            this.buttonApplyFilter.Location = new Point(235, 98);
            this.buttonApplyFilter.Name = "buttonApplyFilter";
            this.buttonApplyFilter.Size = new Size(120, 23);
            this.buttonApplyFilter.TabIndex = 10;
            this.buttonApplyFilter.Text = "Застосувати";
            this.buttonApplyFilter.UseVisualStyleBackColor = true;
            this.buttonApplyFilter.Click += new EventHandler(this.ButtonApplyFilter_Click);

            // buttonResetFilter
            this.buttonResetFilter.Location = new Point(365, 98);
            this.buttonResetFilter.Name = "buttonResetFilter";
            this.buttonResetFilter.Size = new Size(120, 23);
            this.buttonResetFilter.TabIndex = 11;
            this.buttonResetFilter.Text = "Скинути";
            this.buttonResetFilter.UseVisualStyleBackColor = true;
            this.buttonResetFilter.Click += new EventHandler(this.ButtonResetFilter_Click);

            // Form1
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 698);
            this.Controls.Add(this.groupBoxFilters);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Аналіз відгуків клієнтів";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxFilters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
