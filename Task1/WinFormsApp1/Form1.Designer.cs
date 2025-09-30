namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textPath = new System.Windows.Forms.TextBox();
            btnBrowse = new System.Windows.Forms.Button();
            butnScan = new System.Windows.Forms.Button();
            progressBar = new System.Windows.Forms.ProgressBar();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            btnDelete = new System.Windows.Forms.Button();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // textPath
            // 
            textPath.Location = new System.Drawing.Point(12, 50);
            textPath.Name = "textPath";
            textPath.Size = new System.Drawing.Size(26, 23);
            textPath.TabIndex = 0;
            textPath.Text = "С";
            textPath.TextChanged += textBox1_TextChanged;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new System.Drawing.Point(44, 50);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new System.Drawing.Size(75, 23);
            btnBrowse.TabIndex = 1;
            btnBrowse.Text = "Огляд";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += button1_Click;
            // 
            // butnScan
            // 
            butnScan.Location = new System.Drawing.Point(125, 50);
            butnScan.Name = "butnScan";
            butnScan.Size = new System.Drawing.Size(127, 24);
            butnScan.TabIndex = 2;
            butnScan.Text = "Знайти Дублікати";
            butnScan.UseVisualStyleBackColor = true;
            butnScan.Click += butnScan_Click;
            // 
            // progressBar
            // 
            progressBar.ForeColor = System.Drawing.Color.Lime;
            progressBar.Location = new System.Drawing.Point(343, 235);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(457, 23);
            progressBar.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(12, 79);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(252, 326);
            dataGridView1.TabIndex = 4;
            // 
            // btnDelete
            // 
            btnDelete.Location = new System.Drawing.Point(270, 378);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(120, 27);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Видалити вибрані";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new System.Drawing.Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(800, 22);
            statusStrip1.TabIndex = 7;
            statusStrip1.Text = "statusStrip1";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new System.Drawing.Point(343, 79);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new System.Drawing.Size(457, 150);
            dataGridView2.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(dataGridView2);
            Controls.Add(statusStrip1);
            Controls.Add(btnDelete);
            Controls.Add(dataGridView1);
            Controls.Add(progressBar);
            Controls.Add(butnScan);
            Controls.Add(btnBrowse);
            Controls.Add(textPath);
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textPath;
        private Button btnBrowse;
        private Button butnScan;
        private ProgressBar progressBar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Button btnDelete;
        private StatusStrip statusStrip1;
        private DataGridView dataGridView2;
    }
}
