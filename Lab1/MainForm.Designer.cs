
namespace Lab1
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
            GraphView = new System.Windows.Forms.PictureBox();
            MatrixPanel = new System.Windows.Forms.Panel();
            InfoTextBox = new System.Windows.Forms.RichTextBox();
            GenerateGraphButton = new System.Windows.Forms.Button();
            drawVertexButton = new System.Windows.Forms.Button();
            selectButton = new System.Windows.Forms.Button();
            deleteButton = new System.Windows.Forms.Button();
            findPathButton = new System.Windows.Forms.Button();
            drawEdgeButton = new System.Windows.Forms.Button();
            deleteAllButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)GraphView).BeginInit();
            SuspendLayout();
            // 
            // GraphView
            // 
            GraphView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            GraphView.Location = new System.Drawing.Point(276, 51);
            GraphView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            GraphView.Name = "GraphView";
            GraphView.Size = new System.Drawing.Size(1145, 611);
            GraphView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            GraphView.TabIndex = 5;
            GraphView.TabStop = false;
            GraphView.Click += GraphView_Click;
            GraphView.MouseDown += GraphView_MouseDown;
            GraphView.MouseMove += GraphView_MouseMove;
            GraphView.MouseUp += GraphView_MouseUp;
            // 
            // MatrixPanel
            // 
            MatrixPanel.AutoScroll = true;
            MatrixPanel.Location = new System.Drawing.Point(8, 256);
            MatrixPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            MatrixPanel.Name = "MatrixPanel";
            MatrixPanel.Size = new System.Drawing.Size(262, 194);
            MatrixPanel.TabIndex = 6;
            MatrixPanel.Paint += MatrixPanel_Paint_1;
            // 
            // InfoTextBox
            // 
            InfoTextBox.BackColor = System.Drawing.SystemColors.Control;
            InfoTextBox.Location = new System.Drawing.Point(10, 27);
            InfoTextBox.Name = "InfoTextBox";
            InfoTextBox.Size = new System.Drawing.Size(261, 206);
            InfoTextBox.TabIndex = 7;
            InfoTextBox.Text = "";
            // 
            // GenerateGraphButton
            // 
            GenerateGraphButton.AutoSize = true;
            GenerateGraphButton.Location = new System.Drawing.Point(10, 469);
            GenerateGraphButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            GenerateGraphButton.Name = "GenerateGraphButton";
            GenerateGraphButton.Size = new System.Drawing.Size(242, 56);
            GenerateGraphButton.TabIndex = 8;
            GenerateGraphButton.Text = "Сгенерировать граф";
            GenerateGraphButton.UseVisualStyleBackColor = true;
            GenerateGraphButton.Click += GenerateGraphButton_Click;
            // 
            // drawVertexButton
            // 
            drawVertexButton.Image = (System.Drawing.Image)resources.GetObject("drawVertexButton.Image");
            drawVertexButton.Location = new System.Drawing.Point(752, 6);
            drawVertexButton.Margin = new System.Windows.Forms.Padding(2);
            drawVertexButton.Name = "drawVertexButton";
            drawVertexButton.Size = new System.Drawing.Size(58, 41);
            drawVertexButton.TabIndex = 0;
            drawVertexButton.Click += drawVertexButton_Click;
            // 
            // selectButton
            // 
            selectButton.Image = (System.Drawing.Image)resources.GetObject("selectButton.Image");
            selectButton.Location = new System.Drawing.Point(702, 6);
            selectButton.Margin = new System.Windows.Forms.Padding(2);
            selectButton.Name = "selectButton";
            selectButton.Size = new System.Drawing.Size(47, 41);
            selectButton.TabIndex = 9;
            selectButton.Click += selectButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Image = (System.Drawing.Image)resources.GetObject("deleteButton.Image");
            deleteButton.Location = new System.Drawing.Point(864, 8);
            deleteButton.Margin = new System.Windows.Forms.Padding(2);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new System.Drawing.Size(47, 39);
            deleteButton.TabIndex = 10;
            deleteButton.Click += deleteButton_Click;
            // 
            // findPathButton
            // 
            findPathButton.AutoSize = true;
            findPathButton.Location = new System.Drawing.Point(10, 544);
            findPathButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            findPathButton.Name = "findPathButton";
            findPathButton.Size = new System.Drawing.Size(242, 58);
            findPathButton.TabIndex = 3;
            findPathButton.Text = "Найти \r\nнаикратчайший \r\nпуть";
            findPathButton.UseVisualStyleBackColor = true;
            findPathButton.Click += GetPath_Click;
            // 
            // drawEdgeButton
            // 
            drawEdgeButton.Image = (System.Drawing.Image)resources.GetObject("drawEdgeButton.Image");
            drawEdgeButton.Location = new System.Drawing.Point(814, 6);
            drawEdgeButton.Margin = new System.Windows.Forms.Padding(2);
            drawEdgeButton.Name = "drawEdgeButton";
            drawEdgeButton.Size = new System.Drawing.Size(47, 41);
            drawEdgeButton.TabIndex = 11;
            drawEdgeButton.Click += drawEdgeButton_Click;
            // 
            // deleteAllButton
            // 
            deleteAllButton.Image = (System.Drawing.Image)resources.GetObject("deleteAllButton.Image");
            deleteAllButton.Location = new System.Drawing.Point(915, 6);
            deleteAllButton.Margin = new System.Windows.Forms.Padding(2);
            deleteAllButton.Name = "deleteAllButton";
            deleteAllButton.Size = new System.Drawing.Size(66, 41);
            deleteAllButton.TabIndex = 12;
            deleteAllButton.Click += deleteAllButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(22, 5);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(105, 21);
            label1.TabIndex = 13;
            label1.Text = "Окно вывода";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.Color.DarkSeaGreen;
            ClientSize = new System.Drawing.Size(1413, 684);
            Controls.Add(label1);
            Controls.Add(deleteAllButton);
            Controls.Add(drawEdgeButton);
            Controls.Add(deleteButton);
            Controls.Add(drawVertexButton);
            Controls.Add(selectButton);
            Controls.Add(GenerateGraphButton);
            Controls.Add(InfoTextBox);
            Controls.Add(MatrixPanel);
            Controls.Add(GraphView);
            Controls.Add(findPathButton);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "MainForm";
            Text = "Лабораторная работ 1. Алгоритм Дейкстры";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)GraphView).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Label label1;
    }
}