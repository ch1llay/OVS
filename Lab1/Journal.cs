using System;
using System.Data;
using System.Windows.Forms;
using Lab1;

namespace LabOVS1
{
    public partial class Journal : Form
    {
        MainForm _main;
        public Journal()
        {
            InitializeComponent();
        }

        public Journal(MainForm form)
        {
            InitializeComponent();
            _main = form;
        }

        private void Journal_Load(object sender, EventArgs e)
        {
            DataSet data = _main._data;
            if(data != null)
            {
                dataGridView1.DataSource = data.Tables[0];
            }
            else
            {
                MessageBox.Show("Data is Empty", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
