
namespace LabOVS1
{
    partial class EditVertexForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.VertexComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.HasEdgeCheckBox = new System.Windows.Forms.CheckBox();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.NameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // VertexComboBox
            // 
            this.VertexComboBox.FormattingEnabled = true;
            this.VertexComboBox.Location = new System.Drawing.Point(104, 59);
            this.VertexComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.VertexComboBox.Name = "VertexComboBox";
            this.VertexComboBox.Size = new System.Drawing.Size(138, 28);
            this.VertexComboBox.TabIndex = 0;
            this.VertexComboBox.SelectedIndexChanged += new System.EventHandler(this.VertexComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Вершина";
            // 
            // HasEdgeCheckBox
            // 
            this.HasEdgeCheckBox.AutoSize = true;
            this.HasEdgeCheckBox.Location = new System.Drawing.Point(14, 105);
            this.HasEdgeCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.HasEdgeCheckBox.Name = "HasEdgeCheckBox";
            this.HasEdgeCheckBox.Size = new System.Drawing.Size(108, 24);
            this.HasEdgeCheckBox.TabIndex = 2;
            this.HasEdgeCheckBox.Text = "Есть ребро";
            this.HasEdgeCheckBox.UseVisualStyleBackColor = true;
            this.HasEdgeCheckBox.CheckedChanged += new System.EventHandler(this.HasEdgeCheckBox_CheckedChanged);
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Location = new System.Drawing.Point(128, 103);
            this.ValueTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(114, 27);
            this.ValueTextBox.TabIndex = 3;
            this.ValueTextBox.TextChanged += new System.EventHandler(this.ValueTextBox_TextChanged);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(14, 148);
            this.OKButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(103, 53);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(393, 200);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(9, 11);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Настройки для вершины";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(144, 148);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(98, 53);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(186, 12);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(0, 20);
            this.NameLabel.TabIndex = 8;
            // 
            // EditVertexForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 219);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.ValueTextBox);
            this.Controls.Add(this.HasEdgeCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VertexComboBox);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EditVertexForm";
            this.Text = "Есть ребро";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox VertexComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox HasEdgeCheckBox;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label NameLabel;
    }
}