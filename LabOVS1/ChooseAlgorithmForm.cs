using System;
using System.Windows.Forms;

namespace LabOVS1
{
    public partial class ChooseAlgorithmForm : Form
    {
        private readonly Main _algo;
        public string ChosenAlgo;
        private readonly MainForm _myForm;

        public ChooseAlgorithmForm(Main algo, MainForm main)
        {
            InitializeComponent();
            _algo = algo;
            _myForm = main;
            Init();
        }

        private void Init()
        {
            foreach (var vertex in _myForm.VertexList) ToComboBox.Items.Add(vertex);

            foreach (var vertex in _myForm.VertexList) FromComboBox.Items.Add(vertex);

            FromComboBox.SelectedIndex = 0;
            ToComboBox.SelectedIndex = 0;
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            var from = int.Parse(FromComboBox.SelectedItem.ToString());
            var to = int.Parse(ToComboBox.SelectedItem.ToString());
            
            _myForm.ChooseAlgoDijkstra(_algo.Dijkstra(from, to), from, to);
        }

        private void ToComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}