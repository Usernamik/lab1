using System.ComponentModel;

namespace Laba_1;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
        button1 = new System.Windows.Forms.Button();
        dataGridView1 = new System.Windows.Forms.DataGridView();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(3, 1);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(148, 23);
        button1.TabIndex = 0;
        button1.Text = "Завантажити Файл";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new System.Drawing.Point(3, 30);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.Size = new System.Drawing.Size(477, 313);
        dataGridView1.TabIndex = 1;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(dataGridView1);
        Controls.Add(button1);
        Text = "MainForm";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Button button1;
    private System.Windows.Forms.DataGridView dataGridView1;
}