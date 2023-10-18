
namespace LabOVS1
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GraphView = new System.Windows.Forms.PictureBox();
            this.MatrixPanel = new System.Windows.Forms.Panel();
            this.InfoTextBox = new System.Windows.Forms.RichTextBox();
            this.GenerateGraphButton = new System.Windows.Forms.Button();
            this.drawVertexButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.findPathButton = new System.Windows.Forms.Button();
            this.drawEdgeButton = new System.Windows.Forms.Button();
            this.deleteAllButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GraphView)).BeginInit();
            this.SuspendLayout();
            // 
            // GraphView
            // 
            this.GraphView.Location = new System.Drawing.Point(316, 68);
            this.GraphView.Name = "GraphView";
            this.GraphView.Size = new System.Drawing.Size(1113, 669);
            this.GraphView.TabIndex = 5;
            this.GraphView.TabStop = false;
            this.GraphView.Click += new System.EventHandler(this.GraphView_Click);
            this.GraphView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GraphView_MouseDown);
            this.GraphView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphView_MouseMove);
            this.GraphView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GraphView_MouseUp);
            // 
            // MatrixPanel
            // 
            this.MatrixPanel.AutoScroll = true;
            this.MatrixPanel.Location = new System.Drawing.Point(11, 316);
            this.MatrixPanel.Name = "MatrixPanel";
            this.MatrixPanel.Size = new System.Drawing.Size(299, 258);
            this.MatrixPanel.TabIndex = 6;
            this.MatrixPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MatrixPanel_Paint_1);
            // 
            // InfoTextBox
            // 
            this.InfoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.InfoTextBox.Location = new System.Drawing.Point(12, 36);
            this.InfoTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.InfoTextBox.Name = "InfoTextBox";
            this.InfoTextBox.Size = new System.Drawing.Size(298, 273);
            this.InfoTextBox.TabIndex = 7;
            this.InfoTextBox.Text = "";
            // 
            // GenerateGraphButton
            // 
            this.GenerateGraphButton.Location = new System.Drawing.Point(12, 580);
            this.GenerateGraphButton.Name = "GenerateGraphButton";
            this.GenerateGraphButton.Size = new System.Drawing.Size(276, 74);
            this.GenerateGraphButton.TabIndex = 8;
            this.GenerateGraphButton.Text = "Сгенерировать граф";
            this.GenerateGraphButton.UseVisualStyleBackColor = true;
            this.GenerateGraphButton.Click += new System.EventHandler(this.GenerateGraphButton_Click);
            // 
            // drawVertexButton
            // 
            this.drawVertexButton.Image = ((System.Drawing.Image)(resources.GetObject("drawVertexButton.Image")));
            this.drawVertexButton.Location = new System.Drawing.Point(860, 8);
            this.drawVertexButton.Margin = new System.Windows.Forms.Padding(2);
            this.drawVertexButton.Name = "drawVertexButton";
            this.drawVertexButton.Size = new System.Drawing.Size(54, 55);
            this.drawVertexButton.TabIndex = 0;
            this.drawVertexButton.Click += new System.EventHandler(this.drawVertexButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Image = ((System.Drawing.Image)(resources.GetObject("selectButton.Image")));
            this.selectButton.Location = new System.Drawing.Point(802, 8);
            this.selectButton.Margin = new System.Windows.Forms.Padding(2);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(54, 55);
            this.selectButton.TabIndex = 9;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.Location = new System.Drawing.Point(988, 11);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(54, 55);
            this.deleteButton.TabIndex = 10;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // findPathButton
            // 
            this.findPathButton.Location = new System.Drawing.Point(12, 659);
            this.findPathButton.Name = "findPathButton";
            this.findPathButton.Size = new System.Drawing.Size(276, 78);
            this.findPathButton.TabIndex = 3;
            this.findPathButton.Text = "Найти \r\nнаикратчайший \r\nпуть";
            this.findPathButton.UseVisualStyleBackColor = true;
            this.findPathButton.Click += new System.EventHandler(this.GetPath_Click);
            // 
            // drawEdgeButton
            // 
            this.drawEdgeButton.Image = ((System.Drawing.Image)(resources.GetObject("drawEdgeButton.Image")));
            this.drawEdgeButton.Location = new System.Drawing.Point(918, 8);
            this.drawEdgeButton.Margin = new System.Windows.Forms.Padding(2);
            this.drawEdgeButton.Name = "drawEdgeButton";
            this.drawEdgeButton.Size = new System.Drawing.Size(54, 55);
            this.drawEdgeButton.TabIndex = 11;
            this.drawEdgeButton.Click += new System.EventHandler(this.drawEdgeButton_Click);
            // 
            // deleteAllButton
            // 
            this.deleteAllButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteAllButton.Image")));
            this.deleteAllButton.Location = new System.Drawing.Point(1046, 8);
            this.deleteAllButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteAllButton.Name = "deleteAllButton";
            this.deleteAllButton.Size = new System.Drawing.Size(185, 55);
            this.deleteAllButton.TabIndex = 12;
            this.deleteAllButton.Click += new System.EventHandler(this.deleteAllButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(1419, 767);
            this.Controls.Add(this.deleteAllButton);
            this.Controls.Add(this.drawEdgeButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.drawVertexButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.GenerateGraphButton);
            this.Controls.Add(this.InfoTextBox);
            this.Controls.Add(this.MatrixPanel);
            this.Controls.Add(this.GraphView);
            this.Controls.Add(this.findPathButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GraphView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox GraphView;
        private System.Windows.Forms.Panel MatrixPanel;
        private System.Windows.Forms.RichTextBox InfoTextBox;
        private System.Windows.Forms.Button GenerateGraphButton;
        private System.Windows.Forms.Button drawVertexButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button findPathButton;
        private System.Windows.Forms.Button drawEdgeButton;
        private System.Windows.Forms.Button deleteAllButton;
    }
}