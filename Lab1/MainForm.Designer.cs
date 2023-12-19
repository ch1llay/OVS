
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
            checkBox1 = new System.Windows.Forms.CheckBox();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            routingButton = new System.Windows.Forms.Button();
            Journal = new System.Windows.Forms.Button();
            typeRouting = new System.Windows.Forms.ComboBox();
            checkUDP = new System.Windows.Forms.CheckBox();
            checkTCP = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)GraphView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // GraphView
            // 
            GraphView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            GraphView.Location = new System.Drawing.Point(315, 68);
            GraphView.Name = "GraphView";
            GraphView.Size = new System.Drawing.Size(1476, 815);
            GraphView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
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
            MatrixPanel.Location = new System.Drawing.Point(9, 341);
            MatrixPanel.Name = "MatrixPanel";
            MatrixPanel.Size = new System.Drawing.Size(299, 259);
            MatrixPanel.TabIndex = 6;
            MatrixPanel.Paint += MatrixPanel_Paint_1;
            // 
            // InfoTextBox
            // 
            InfoTextBox.BackColor = System.Drawing.SystemColors.Control;
            InfoTextBox.Location = new System.Drawing.Point(11, 36);
            InfoTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            InfoTextBox.Name = "InfoTextBox";
            InfoTextBox.Size = new System.Drawing.Size(298, 273);
            InfoTextBox.TabIndex = 7;
            InfoTextBox.Text = "";
            // 
            // GenerateGraphButton
            // 
            GenerateGraphButton.AutoSize = true;
            GenerateGraphButton.Location = new System.Drawing.Point(11, 625);
            GenerateGraphButton.Name = "GenerateGraphButton";
            GenerateGraphButton.Size = new System.Drawing.Size(277, 75);
            GenerateGraphButton.TabIndex = 8;
            GenerateGraphButton.Text = "Сгенерировать граф";
            GenerateGraphButton.UseVisualStyleBackColor = true;
            GenerateGraphButton.Click += GenerateGraphButton_Click;
            // 
            // drawVertexButton
            // 
            drawVertexButton.Image = (System.Drawing.Image)resources.GetObject("drawVertexButton.Image");
            drawVertexButton.Location = new System.Drawing.Point(859, 8);
            drawVertexButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            drawVertexButton.Name = "drawVertexButton";
            drawVertexButton.Size = new System.Drawing.Size(66, 55);
            drawVertexButton.TabIndex = 0;
            drawVertexButton.Click += drawVertexButton_Click;
            // 
            // selectButton
            // 
            selectButton.Image = (System.Drawing.Image)resources.GetObject("selectButton.Image");
            selectButton.Location = new System.Drawing.Point(802, 8);
            selectButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            selectButton.Name = "selectButton";
            selectButton.Size = new System.Drawing.Size(54, 55);
            selectButton.TabIndex = 9;
            selectButton.Click += selectButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Image = (System.Drawing.Image)resources.GetObject("deleteButton.Image");
            deleteButton.Location = new System.Drawing.Point(987, 11);
            deleteButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new System.Drawing.Size(54, 52);
            deleteButton.TabIndex = 10;
            deleteButton.Click += deleteButton_Click;
            // 
            // findPathButton
            // 
            findPathButton.AutoSize = true;
            findPathButton.Location = new System.Drawing.Point(11, 725);
            findPathButton.Name = "findPathButton";
            findPathButton.Size = new System.Drawing.Size(277, 77);
            findPathButton.TabIndex = 3;
            findPathButton.Text = "Найти \r\nнаикратчайший \r\nпуть";
            findPathButton.UseVisualStyleBackColor = true;
            findPathButton.Click += GetPath_Click;
            // 
            // drawEdgeButton
            // 
            drawEdgeButton.Image = (System.Drawing.Image)resources.GetObject("drawEdgeButton.Image");
            drawEdgeButton.Location = new System.Drawing.Point(930, 8);
            drawEdgeButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            drawEdgeButton.Name = "drawEdgeButton";
            drawEdgeButton.Size = new System.Drawing.Size(54, 55);
            drawEdgeButton.TabIndex = 11;
            drawEdgeButton.Click += drawEdgeButton_Click;
            // 
            // deleteAllButton
            // 
            deleteAllButton.Image = (System.Drawing.Image)resources.GetObject("deleteAllButton.Image");
            deleteAllButton.Location = new System.Drawing.Point(1046, 8);
            deleteAllButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            deleteAllButton.Name = "deleteAllButton";
            deleteAllButton.Size = new System.Drawing.Size(75, 55);
            deleteAllButton.TabIndex = 12;
            deleteAllButton.Click += deleteAllButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(25, 7);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(135, 28);
            label1.TabIndex = 13;
            label1.Text = "Окно вывода";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(9, 818);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(113, 24);
            checkBox1.TabIndex = 14;
            checkBox1.Text = "Сравнивать";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Enabled = false;
            numericUpDown1.Location = new System.Drawing.Point(156, 851);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(52, 27);
            numericUpDown1.TabIndex = 16;
            numericUpDown1.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(9, 853);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(138, 20);
            label2.TabIndex = 17;
            label2.Text = "Кол-во сравнений";
            // 
            // routingButton
            // 
            routingButton.BackColor = System.Drawing.Color.Snow;
            routingButton.Location = new System.Drawing.Point(315, 17);
            routingButton.Name = "routingButton";
            routingButton.Size = new System.Drawing.Size(151, 40);
            routingButton.TabIndex = 18;
            routingButton.Text = "Маршрутизация";
            routingButton.UseVisualStyleBackColor = false;
            routingButton.Click += routingButton_Click;
            // 
            // Journal
            // 
            Journal.BackColor = System.Drawing.Color.Snow;
            Journal.Location = new System.Drawing.Point(1544, 21);
            Journal.Name = "Journal";
            Journal.Size = new System.Drawing.Size(94, 29);
            Journal.TabIndex = 19;
            Journal.Text = "Журнал";
            Journal.UseVisualStyleBackColor = false;
            Journal.Click += Journal_Click;
            // 
            // typeRouting
            // 
            typeRouting.FormattingEnabled = true;
            typeRouting.Items.AddRange(new object[] { "Случайная", "Лавинная", "По предыдущему опыту" });
            typeRouting.Location = new System.Drawing.Point(532, 24);
            typeRouting.Name = "typeRouting";
            typeRouting.Size = new System.Drawing.Size(193, 28);
            typeRouting.TabIndex = 20;
            typeRouting.Text = "(нет)";
            // 
            // checkUDP
            // 
            checkUDP.AutoSize = true;
            checkUDP.Checked = true;
            checkUDP.CheckState = System.Windows.Forms.CheckState.Checked;
            checkUDP.Location = new System.Drawing.Point(1198, 26);
            checkUDP.Name = "checkUDP";
            checkUDP.Size = new System.Drawing.Size(60, 24);
            checkUDP.TabIndex = 21;
            checkUDP.Text = "UDP";
            checkUDP.UseVisualStyleBackColor = true;
            checkUDP.CheckedChanged += checkUDP_CheckedChanged;
            // 
            // checkTCP
            // 
            checkTCP.AutoSize = true;
            checkTCP.Location = new System.Drawing.Point(1283, 26);
            checkTCP.Name = "checkTCP";
            checkTCP.Size = new System.Drawing.Size(55, 24);
            checkTCP.TabIndex = 22;
            checkTCP.Text = "TCP";
            checkTCP.UseVisualStyleBackColor = true;
            checkTCP.CheckedChanged += checkTCP_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.Color.DarkSeaGreen;
            ClientSize = new System.Drawing.Size(1791, 912);
            Controls.Add(checkTCP);
            Controls.Add(checkUDP);
            Controls.Add(typeRouting);
            Controls.Add(Journal);
            Controls.Add(routingButton);
            Controls.Add(label2);
            Controls.Add(numericUpDown1);
            Controls.Add(checkBox1);
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
            Name = "MainForm";
            Text = "Лабораторные работы 1-2. Алгоритмы Дейкстры и Флойда. ";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)GraphView).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button routingButton;
        private System.Windows.Forms.Button Journal;
        private System.Windows.Forms.ComboBox typeRouting;
        private System.Windows.Forms.CheckBox checkUDP;
        private System.Windows.Forms.CheckBox checkTCP;
    }
}