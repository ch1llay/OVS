using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Draws.Components
{
    public class FormWithEntryField : Form
    {
        public class ControlData
        {
            public string ReturnData { get; set; }
            public bool IsReady { get; set; }
        }

        private TextBox textBox;
        private Button button;
        private Action _action;
        private Form _parentForm;
        private bool isShowed = false;

        public int GetInt()
        {
            int n = 4;
            if (int.TryParse(textBox.Text, out n) && n <= 10)
            {
                return n;
            }
            else
            {
                MessageBox.Show("Некорректный ввод", "Ошибка");
                return 0;
            }
        }

        public FormWithEntryField(Form parentForm) : base()
        {
            _parentForm = parentForm;
            this.Width = 100;
            this.Height = 100;
            int childLeft = (_parentForm.Width - this.Width) / 2;
            int childTop = (parentForm.Height - this.Height) / 2;
            this.Top = childTop;
            this.Left = childLeft;
            this.Closed += (sender, args) =>
            {
                try
                {
                    if (!isShowed)
                    {
                        _action();
                    }
                }
                catch
                {

                }
            };

            InitializeComponent();
        }

        public void ShowWithAction(Action action)
        {
            this.Show();
            isShowed = true;
            _action = action;
            isShowed = false;
        }

        private void ActionInvoke()
        {
            _action.Invoke();
            isShowed = true;
            this.Close();
            this.Dispose();
        }
        private void InitializeComponent()
        {
            textBox = new TextBox
            {
                Location = new Point(10, 10),
                Width = 100
            };
            button = new Button()
            {
                Location = new Point(10, 30),
                Width = 100,
                Text = "Применить"
            };

            button.MouseClick += (sender, args) =>
            {
                ActionInvoke();
            };
            
            

            textBox.KeyUp += (object sender, System.Windows.Forms.KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ActionInvoke();
                }
            };
            this.Controls.Add(textBox);
            this.Controls.Add(button);
        }
    }
}