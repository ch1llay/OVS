using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Common.Draws;
using Common.Draws.Components;
using Common.Draws.enums;

namespace Lab1;

public partial class MainForm : Form
{
    public readonly DrawService _drawService;
    private Edge _animatedEdge;

    private int _animationPosition;

    private Point _clickedLocation;

    private List<List<Control>> _controlMatrix;
    private FormWithEntryField _formWithEntryField;

    private List<List<Label>> _labelMatrix;

    private ContextMenuStrip _menu;


    public MainForm()
    {
        InitializeComponent();
        _drawService = new DrawService(GraphView, UpdateMatrix);
        FirstInit();
    }


    private void FirstInit()
    {
        _labelMatrix = new List<List<Label>>();
        _controlMatrix = new List<List<Control>>();
        _drawService._isMoving = false;
        GraphView.Image = new Bitmap(GraphView.Width, GraphView.Height);
    }


    public void DijkstraGraphFilling(List<int> path, int from, int to)
    {
        _drawService.ResetColors();

        var sum = 0;
        var n = path.Count;
        var i = to;
        var color = Color.Red;
        var v1 = path[i];
        var v2 = i;
        var listOfV = new List<int>();

        var noWay = false;
        if (path[i] == -1)
        {
            MessageBox.Show("Пути от " + from + " до " + to + " не существует.");
            noWay = true;
        }

        while (!noWay && path[i] != from)
        {
            v1 = path[i];
            v2 = i;
            i = path[i];

            if (_drawService.FindVertex(v1) != null && _drawService.FindVertex(v2) != null &&
                _drawService.FindEdge(v1, v2) != null)
            {
                _drawService.FindVertex(v1).ChangeColor(color);
                _drawService.FindVertex(v2).ChangeColor(color);
                _drawService.FindEdge(v1, v2).ChangeColorForHighlighting(color);
                listOfV.Add(v2);
            }
        }

        v1 = path[i];
        v2 = i;
        listOfV.Add(v2);
        listOfV.Add(v1);
        listOfV.Reverse();

        if (_drawService.FindVertex(v1) != null && _drawService.FindVertex(v2) != null &&
            _drawService.FindEdge(v1, v2) != null)
        {
            _drawService.FindVertex(v1).ChangeColor(color);
            _drawService.FindVertex(v2).ChangeColor(color);
            _drawService.FindEdge(v1, v2).ChangeColorForHighlighting(color);
        }

        var richTextBoxInfoText = string.Empty;
        richTextBoxInfoText += "Метод Дейкстры: \n";
        richTextBoxInfoText += "Путь ";

        for (var k = 0; k < listOfV.Count - 1; ++k)
        {
            var v = listOfV[k];

            if (_drawService.FindEdge(v, listOfV[k + 1]) == null)
            {
                //MessageBox.Show("Пути от " + from + " до " + to + " не существует.");
                noWay = true;
            }
            else
            {
                sum += _drawService.FindEdge(v, listOfV[k + 1]).Value;
                richTextBoxInfoText += v + " -> ";
            }
        }

        if (!noWay)
        {
            richTextBoxInfoText += listOfV[listOfV.Count - 1];
            richTextBoxInfoText += "\n";
            richTextBoxInfoText += "Длина пути: ";
            richTextBoxInfoText += sum + "\n";
            InfoTextBox.Text += richTextBoxInfoText;
        }
    }

    public void FloidFilling((List<int>, int) values, int from, int to)
    {
        if (values.Item1.Count == 0)
        {
            MessageBox.Show("Пути от " + from + " до " + to + " не существует.");
            return;
        }


        var richTextBoxInfoText = string.Empty;
        richTextBoxInfoText += "Метод Флойда: \n";
        richTextBoxInfoText += "Путь ";
        for (int i = 0; i < values.Item1.Count; i++)
        {
            richTextBoxInfoText += values.Item1[i];
            if (i == values.Item1.Count - 1) break;
            richTextBoxInfoText += "->";
        }
        richTextBoxInfoText += "\n";
        richTextBoxInfoText += "Длина пути: " + values.Item2.ToString();
        InfoTextBox.Text += richTextBoxInfoText;
    }

    public void EditCompleteOk(Vertex vertex, Dictionary<Vertex, int> connected)
    {
        _drawService.EditVertex(vertex, connected);
        _drawService.Redraw();
    }

    public void EditCompleteCancel(EditVertexForm form, Vertex vertex, bool isNewVertex)
    {
        if (isNewVertex) _drawService.RemoveVertex(vertex);

        form.Dispose();
        _drawService.Redraw();
    }

    public void ChooseAlgoDijkstra(List<int> path, int from, int to)
    {
        DijkstraGraphFilling(path, from, to);
        _drawService.Redraw();
    }


    private void UpdateMatrix()
    {
        MatrixPanel.SuspendLayout();
        MatrixPanel.Hide();
        foreach (var controlMatrix in _controlMatrix)
            foreach (var lb in controlMatrix)
                lb.Dispose();

        _controlMatrix.Clear();
        var n = _drawService.Matrix.GetLength(0);

        for (var i = 0; i < n + 1; ++i)
        {
            _controlMatrix.Add(new List<Control>());

            if (i == 0)
            {
                for (var j = 0; j < n; ++j)
                {
                    var label = new Label
                    {
                        Parent = MatrixPanel,
                        Width = 40,
                        Height = 23,
                        Location = new Point(10 + 50 * i, 10 + 33 * (j + 1)),
                        Text = "g" + j
                    };

                    label.Show();
                    _controlMatrix[i].Add(label);
                }

                continue;
            }

            for (var j = 0; j < n + 1; ++j)
            {
                if (i == j) continue;

                Label label;

                if (j == 0)
                {
                    label = new Label
                    {
                        Parent = MatrixPanel,
                        Width = 40,
                        Height = 23,
                        Location = new Point(10 + 50 * i, 10 + 33 * j),
                        Text = "g" + (i - 1)
                    };

                    label.Show();
                    _controlMatrix[i].Add(label);

                    continue;
                }

                var textBox = new MyTextBox
                {
                    Parent = MatrixPanel,
                    Width = 40,
                    Height = 23,
                    Location = new Point(10 + 50 * j, 10 + 33 * i),
                    Text = _drawService.Matrix[i - 1, j - 1].ToString()
                };

                textBox.From = i - 1;
                textBox.To = j - 1;
                textBox.Show();
                textBox.TextChanged += TextBoxTextChanged;
                _controlMatrix[i].Add(textBox);
            }
        }

        MatrixPanel.Show();
        MatrixPanel.ResumeLayout();
    }

    private void TextBoxTextChanged(object sender, EventArgs e)
    {
        var textBox = (MyTextBox)sender;

        if (textBox.Text == string.Empty) return;

        var edge = _drawService.FindEdge(textBox.From, textBox.To);

        if (edge == null)
            edge = _drawService.CreateEdge(_drawService.FindVertex(textBox.From),
                _drawService.FindVertex(textBox.To));

        _drawService.EditEdge(edge, int.Parse(textBox.Text));
        UpdateMatrix();
        _drawService.Redraw();
    }

    private void AddNewLabelList(int n)
    {
        var list = new List<Label>();

        for (var i = 0; i < n; ++i)
        {
            var label = new Label();
            label.Parent = MatrixPanel;
            label.Width = 50;
            label.Height = 23;
            label.Location = new Point(10 + 60 * i, 10 + 40 * n);
            label.Text = "0";
            label.Show();
            list.Add(new Label());
        }

        _labelMatrix.Add(list);
    }


    private void EditVertexFormShow(MainForm mainForm, Vertex vertex, bool isNewVertex)
    {
        var newForm = new EditVertexForm(mainForm, vertex, isNewVertex);
        newForm.Show();
    }

    private void WorkWithAddingEdge(int from, int n)
    {
        var fromVertex = _drawService.FindVertex(from);
        var _connected = new Dictionary<Vertex, int>();
        Vertex needVertex = null;

        for (var i = 0; i < _drawService.Matrix.GetLength(0); ++i)
        {
            var value = _drawService.Matrix[fromVertex.Index, i];

            if (value > 0) _connected.Add(_drawService.VertexList[i], value);
        }

        if (!_connected.ContainsKey(_drawService._selectedVertex))
        {
            _connected[_drawService._selectedVertex] = n;
        }
        else
        {
            MessageBox.Show($"Ребро из {from} в {_drawService._selectedVertex.Index} уже есть");
        }

        EditCompleteOk(fromVertex, _connected);
    }

    private void GetPath_Click(object sender, EventArgs e)
    {
        _drawService._currentMode = Modes.GetPath;
        findPathButton.Enabled = false;
        label1.Text = "Выберите исходную вершину";
        label1.ForeColor = Color.Brown;
    }


    private void ShowVertexMenu(Point location)
    {
        if (_menu != null) _menu.Dispose();

        _menu = new ContextMenuStrip();
        _menu.Items.Add("Изменить");
        _menu.Items.Add("Удалить");
        _menu.ItemClicked += VertexMenu_Click;
        _menu.Show(GraphView, location);
    }

    private void ShowEmptyMenu(Point location)
    {
        if (_menu != null) _menu.Dispose();

        _menu = new ContextMenuStrip();
        _menu.Items.Add("Добавить вершину");
        _menu.ItemClicked += EmptyMenu_Click;
        _menu.Show(GraphView, location);
    }

    private void VertexMenu_Click(object sender, ToolStripItemClickedEventArgs e)
    {
        switch (e.ClickedItem.Text)
        {
            case "Изменить":
                {
                    EditVertexFormShow(this, _drawService._selectedVertex, false);

                    break;
                }

            case "Удалить":
                {
                    _drawService.RemoveVertex(_drawService._selectedVertex);

                    break;
                }
        }
    }

    private void EmptyMenu_Click(object sender, ToolStripItemClickedEventArgs e)
    {
        switch (e.ClickedItem.Text)
        {
            case "Добавить вершину":
                {
                    EditVertexFormShow(this, _drawService.CreateVertex(_clickedLocation), true);

                    break;
                }
        }
    }


    private void GraphView_MouseDown(object sender, MouseEventArgs e)
    {
        _clickedLocation = e.Location;

        switch (_drawService._currentMode)
        {
            case Modes.Select:
                {
                    var vertex = _drawService.ClickedVertex(e.Location);

                    if (vertex != null)
                    {
                        _drawService._selectedVertex = vertex;
                        _drawService._isMoving = true;
                        _drawService._selectedVertex.Center = e.Location;
                        _drawService.Redraw();
                    }
                    else
                    {
                        _drawService._isMoving = false;
                    }

                    break;
                }

            case Modes.AddVertex:
                {
                    _drawService._selectedVertex = _drawService.ClickedVertex(e.Location);

                    if (_drawService._selectedVertex == null)
                    {
                        _drawService.CreateVertex(e.Location);
                        _drawService.Redraw();
                    }

                    break;
                }

            case Modes.Delete:
                {
                    _drawService._selectedVertex = _drawService.ClickedVertex(e.Location);

                    if (_drawService._selectedVertex != null)
                    {
                        _drawService.RemoveVertex(_drawService._selectedVertex);
                        _drawService.Redraw();
                    }

                    break;
                }

            case Modes.AddEdge:
                {
                    _drawService._selectedVertex = _drawService.ClickedVertex(e.Location);

                    if (drawEdgeButton.Enabled == false && _drawService.SourceVertex == null)
                    {
                        _drawService.SourceVertex = _drawService._selectedVertex;
                        InfoTextBox.Text = $"Из вершины {_drawService.SourceVertex.Index}";
                        label1.Text = "Выберите целевую вершину";
                        label1.ForeColor = Color.Brown;
                    }
                    else
                    {
                        _drawService.TargetVertex = _drawService._selectedVertex;
                        if (_drawService.TargetVertex == null) return; // защита от миссклика(вроде помогает)
                        InfoTextBox.Text += $" в вершину {_drawService.TargetVertex.Index}";
                        var from = _drawService.SourceVertex.Index;
                        var to = _drawService.TargetVertex.Index;
                        if (_formWithEntryField == null)
                        {
                            _formWithEntryField = new FormWithEntryField(this);
                            Enabled = false;
                            _formWithEntryField.ShowWithAction(() =>
                            {
                                var n = _formWithEntryField?.GetInt() ?? 0;
                                InfoTextBox.Text += $" значение {n}\n";
                                WorkWithAddingEdge(from, n);
                                _drawService.SourceVertex = null;
                                _drawService.TargetVertex = null;
                                _drawService._currentMode = Modes.Select;
                                drawEdgeButton.Enabled = true;
                                label1.Text = "(●'◡'●)";
                                label1.ForeColor = Color.Black;
                                Enabled = true;
                                _formWithEntryField = null;
                            });
                        }
                    }

                    //_drawService.EditVertexFormShow(this,_drawService._selectedVertex, false);
                    break;
                }
            case Modes.GetPath:
                _drawService._selectedVertex = _drawService.ClickedVertex(e.Location);

                if (_drawService._selectedVertex != null)
                {
                    if (findPathButton.Enabled == false && _drawService.SourceVertex == null)
                    {
                        _drawService.SourceVertex = _drawService._selectedVertex;
                        InfoTextBox.Text = $"Из вершины {_drawService.SourceVertex.Index}";
                        label1.Text = "Выберите целевую вершину";
                        label1.ForeColor = Color.Brown;
                    }
                    else
                    {
                        _drawService.TargetVertex = _drawService._selectedVertex;
                        InfoTextBox.Text += $" в вершину {_drawService.TargetVertex.Index}\n";
                        var from = _drawService.SourceVertex.Index;
                        var to = _drawService.TargetVertex.Index;
                        if(checkBox1.Checked == true)
                        {
                            var times = Replay();
                            InfoTextBox.Text += "\n Время метода Дейкстры: " + times.Item1.ToString();
                            InfoTextBox.Text += "\n Время метода Флойда: " + times.Item2.ToString();
                        }
                        else
                        {
                            ChooseAlgoDijkstra(_drawService._graphDrawService.Dijkstra(from, to), from, to);
                            FloidFilling(_drawService._graphDrawService.Floid(from, to), from, to);
                        }
                        _drawService.SourceVertex = null;
                        _drawService.TargetVertex = null;
                        findPathButton.Enabled = true;
                        _drawService._currentMode = Modes.Select;
                        label1.Text = "(●'◡'●)";
                        label1.ForeColor = Color.Black;
                    }
                }

                break;
        }
    }

    private (long, long) Replay()
    {
        Stopwatch stopwatch = new Stopwatch();
        var vertexes = _drawService.VertexList;
        var countReplay = 0;
        countReplay = (int)numericUpDown1.Value;
        stopwatch.Start();
        for (int i = 0; i < countReplay; i++)
        {
            for (int j = 0; j < vertexes.Count; j++)
                for (int k = 0; k < vertexes.Count; k++)
                {
                    _drawService._graphDrawService.Dijkstra(j,k);
                }
        }
        stopwatch.Stop();
        var dijkstraTime = stopwatch.ElapsedMilliseconds;
        stopwatch.Restart();
        for (int i = 0; i < countReplay; i++)
        {
            for (int j = 0; j < vertexes.Count; j++)
                for (int k = 0; k < vertexes.Count; k++)
                {
                    _drawService._graphDrawService.Floid(j,k);
                }
        }
        stopwatch.Stop();
        var floidTime = stopwatch.ElapsedMilliseconds;

        return (dijkstraTime, floidTime);
    }


    private void GraphView_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_drawService._isMoving) return;

        if (e.Button != MouseButtons.Left) return;

        if (_drawService._selectedVertex == null) return;

        _drawService._selectedVertex.Center = e.Location;
        _drawService.Redraw();
    }

    private void GraphView_MouseUp(object sender, MouseEventArgs e)
    {
        if (!_drawService._isMoving) return;

        if (e.Button != MouseButtons.Left) return;

        if (_drawService._selectedVertex == null) return;

        _drawService._selectedVertex.Center = e.Location;
        _drawService._isMoving = false;
        _drawService.Redraw();
    }

    private void GenerateGraphButton_Click(object sender, EventArgs e)
    {
        if (_formWithEntryField == null)
        {
            _formWithEntryField = new FormWithEntryField(this);
            Enabled = false;
            _formWithEntryField.ShowWithAction(() =>
            {
                var matrix = Tools.GenerateGraphMatrix(_formWithEntryField.GetInt());
                _drawService.SetNewMatrix(matrix);
                Enabled = true;
                _formWithEntryField = null;
            });
        }
    }


    private void MainForm_Load(object sender, EventArgs e)
    {
    }

    private void GraphView_Click(object sender, EventArgs e)
    {
    }

    private void selectButton_Click(object sender, EventArgs e)
    {
        selectButton.Enabled = false;
        drawVertexButton.Enabled = true;
        drawEdgeButton.Enabled = true;
        deleteButton.Enabled = true;
        findPathButton.Enabled = true;
        _drawService._currentMode = Modes.Select;
    }

    private void drawVertexButton_Click(object sender, EventArgs e)
    {
        selectButton.Enabled = true;
        drawVertexButton.Enabled = false;
        drawEdgeButton.Enabled = true;
        deleteButton.Enabled = true;
        findPathButton.Enabled = true;
        _drawService._currentMode = Modes.AddVertex;
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
        selectButton.Enabled = true;
        drawVertexButton.Enabled = true;
        drawEdgeButton.Enabled = true;
        deleteButton.Enabled = false;
        findPathButton.Enabled = true;
        _drawService._currentMode = Modes.Delete;
    }

    private void drawEdgeButton_Click(object sender, EventArgs e)
    {
        selectButton.Enabled = true;
        drawVertexButton.Enabled = true;
        drawEdgeButton.Enabled = false;
        deleteButton.Enabled = true;
        findPathButton.Enabled = true;
        _drawService._currentMode = Modes.AddEdge;
    }

    private void InfoTextBox_TextChanged(object sender, EventArgs e)
    {
    }

    private void MatrixPanel_Paint(object sender, PaintEventArgs e)
    {
    }

    private void MatrixPanel_Paint_1(object sender, PaintEventArgs e)
    {
    }

    private void deleteAllButton_Click(object sender, EventArgs e)
    {
        _drawService.DeleteAll();
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox1.Checked == true)
            numericUpDown1.Enabled = true;
        else
            numericUpDown1.Enabled = false;
    }
}