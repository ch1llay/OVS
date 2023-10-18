using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LabOVS1
{
    public partial class EditVertexForm : Form
    {
        private MainForm _main;

        private Dictionary<Vertex, int> _connected;

        private bool _isNewVertex;

        private Vertex _selectedVertex;
        private Vertex _showedVertex;

        public EditVertexForm(MainForm main, Vertex selectedVertex, bool isNewVertex)
        {
            InitializeComponent();
            Init(main, selectedVertex, isNewVertex);

            if (!isNewVertex)
            {
                ShowInfo();
            }
        }

        private void Init(MainForm main, Vertex selectedVertex, bool isNewVertex)
        {
            _main = main;
            _connected = new Dictionary<Vertex, int>();
            _selectedVertex = selectedVertex;

            foreach (var vertex in _main.VertexList)
                if (vertex.Index != selectedVertex.Index)
                {
                    VertexComboBox.Items.Add(vertex);
                }

            if (VertexComboBox.Items.Count > 0)
            {
                VertexComboBox.SelectedIndex = 0;
            }

            NameLabel.Text = _selectedVertex.Index.ToString();
            _isNewVertex = isNewVertex;

            if (!_isNewVertex)
            {
                for (var i = 0; i < _main.Matrix.GetLength(0); ++i)
                {
                    var value = _main.Matrix[_selectedVertex.Index, i];

                    if (value > 0)
                    {
                        _connected.Add(_main.VertexList[i], value);
                    }
                }
            }
        }

        private void ShowInfo()
        {
            if (_showedVertex == null)
            {
                return;
            }

            if (_connected.ContainsKey(_showedVertex) && _connected[_showedVertex] > 0)
            {
                HasEdgeCheckBox.Checked = true;
            }
            else
            {
                HasEdgeCheckBox.Checked = false;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            _main.EditCompleteOk(this, _selectedVertex, _connected);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            _main.EditCompleteCancel(this, _selectedVertex, _isNewVertex);
        }

        private void VertexComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _showedVertex = (Vertex) VertexComboBox.SelectedItem;
            ShowInfo();
        }

        private void HasEdgeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_showedVertex == null)
            {
                return;
            }

            if (HasEdgeCheckBox.Checked)
            {
                if (_connected.ContainsKey(_showedVertex))
                {
                    if (_connected[_showedVertex] < 1)
                    {
                        _connected[_showedVertex] = 1;
                    }
                }
                else
                {
                    _connected.Add(_showedVertex, 1);
                }

                ValueTextBox.Text = _connected[_showedVertex].ToString();
            }
            else
            {
                _connected[_showedVertex] = 0;
                ValueTextBox.Text = "";
            }
        }

        private void ValueTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _connected[_showedVertex] = int.Parse(ValueTextBox.Text);
            }
            catch (Exception exp)
            {
                //FUC U
            }
        }
    }
}