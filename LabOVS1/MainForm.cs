using LabOVS1.enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LabOVS1
{
    public partial class MainForm : Form
    {
        private Main _algo;
        private Edge _animatedEdge;

        private int _animationPosition;

        private bool _bMove;

        private Point _clickedLocation;
        private List<List<Control>> _controlMatrix;


        private readonly string _fileName = "Matritsa.txt";
        private Point _finalLocation;

        private Point _firstLocation;

        private Modes _flagB = Modes.Select;

        private List<List<Label>> _labelMatrix;

        private ContextMenuStrip _menu;

        private Random _rnd;

        private Vertex _selectedVertex;
        public Vertex SourceVertex;
        public Vertex TargetVertex;

        public MainForm()
        { 
            File.Open(_fileName, FileMode.OpenOrCreate).Close();
            InitializeComponent();
            FirstInit();
        }

        public List<Vertex> VertexList { get; private set; }

        public List<Edge> EdgeList { get; private set; }

        public int[,] Matrix
        {
            get => _algo.Matrix;
            private set => _algo.Matrix = value;
        }

        private void FirstInit()
        {
            _firstLocation = new Point(-1, -1);
            _finalLocation = new Point(-1, -1);
            VertexList = new List<Vertex>();
            EdgeList = new List<Edge>();
            _labelMatrix = new List<List<Label>>();
            _controlMatrix = new List<List<Control>>();
            _bMove = false;
            _algo = Main.GetInstance();
            GraphView.Image = new Bitmap(GraphView.Width, GraphView.Height);
            _rnd = new Random();
            
        }

        private void Redraw()
        {
            if (GraphView.Image != null)
            {
                GraphView.Image.Dispose();
            }

            var image = new Bitmap(GraphView.Width, GraphView.Height);
            var graphics = Graphics.FromImage(image);

            foreach (var vertex in VertexList) vertex.Draw(graphics);

            foreach (var edge in EdgeList) edge.Draw(graphics);

            GraphView.Image = image;
            graphics.Dispose();
        }

        public Edge FindEdge(int from, int to)
        {
            foreach (var edge in EdgeList)
                if (edge.From.Index == from && edge.To.Index == to)
                {
                    return edge;
                }

            return null;
        }

        public Vertex FindVertex(int index)
        {
            foreach (var vertex in VertexList)
                if (vertex.Index == index)
                {
                    return vertex;
                }

            return null;
        }

        private void ResetColors()
        {
            foreach (var vertex in VertexList) vertex.ResetColor();

            foreach (var edge in EdgeList) edge.ResetColor();
        }

        public void Dijkstra(List<int> path, int from, int to)
        {
            ResetColors();
            
            var sum = 0;
            var n = path.Count;
            var i = to;
            var color = Color.Red;
            var v1 = path[i];
            var v2 = i;
            var listOfV = new List<int>();

            while (path[i] != from)
            {
                v1 = path[i];
                v2 = i;
                i = path[i];

                if (FindVertex(v1) != null && FindVertex(v2) != null && FindEdge(v1, v2) != null)
                {
                    FindVertex(v1).ChangeColor(color);
                    FindVertex(v2).ChangeColor(color);
                    FindEdge(v1, v2).ChangeColorForHighlighting(color);
                    listOfV.Add(v2);
                }
            }

            v1 = path[i];
            v2 = i;
            listOfV.Add(v2);
            listOfV.Add(v1);
            listOfV.Reverse();

            if (FindVertex(v1) != null && FindVertex(v2) != null && FindEdge(v1, v2) != null)
            {
                FindVertex(v1).ChangeColor(color);
                FindVertex(v2).ChangeColor(color);
                FindEdge(v1, v2).ChangeColorForHighlighting(color);
            }

            var info = string.Empty;
            info += "Путь ";
            var noWay = false;

            for (var k = 0; k < listOfV.Count - 1; ++k)
            {
                var v = listOfV[k];

                if (FindEdge(v, listOfV[k + 1]) == null)
                {
                    MessageBox.Show("Пути от " + from + " до " + to + " не существует.");
                    noWay = true;
                }
                else
                {
                    sum += FindEdge(v, listOfV[k + 1]).Value;
                    info += v + " -> ";
                }
            }

            if (!noWay)
            {
                info += listOfV[listOfV.Count - 1];
                info += "\n";
                info += "Длина пути: ";
                info += sum;
                InfoTextBox.Text += info;
            }
        }

        public void EditCompleteOk(EditVertexForm form, Vertex vertex, Dictionary<Vertex, int> connected)
        {
            EditVertex(vertex, connected);
            form.Dispose();
            Redraw();
        }

        public void EditCompleteCancel(EditVertexForm form, Vertex vertex, bool isNewVertex)
        {
            if (isNewVertex)
            {
                RemoveVertex(vertex);
            }

            form.Dispose();
            Redraw();
        }

        public void ChooseAlgoDijkstra(List<int> path, int from, int to)
        {
            Dijkstra(path, from, to);
            Redraw();
        }

        public void ChooseAlgoCancel(ChooseAlgorithmForm form)
        {
            form.Dispose();
            Redraw();
        }

        private void EditVertex(Vertex vertex, Dictionary<Vertex, int> connected)
        {
            DeleteEdgesFromTheVertex(vertex);

            foreach (var pair in connected)
                if (pair.Value != 0)
                {
                    CreateEdge(vertex, pair.Key, pair.Value);
                }

            UpdateMatrix();
            Redraw();
        }

        private void EditEdge(Edge edge, int value)
        {
            Matrix[edge.From.Index, edge.To.Index] = value;

            if (value == 0)
            {
                RemoveEdge(edge);

                return;
            }

            edge.Value = value;
        }

        public void RemoveEdge(Edge edge)
        {
            Matrix[edge.From.Index, edge.To.Index] = 0;
            EdgeList.Remove(edge);
            UpdateMatrix();
            Redraw();
        }

        public void AddVertex(Vertex vertex)
        {
            if (_selectedVertex == null)
            {
                var index = 0;
                for (var i = 0; i < VertexList.Count; ++i) index++;
                UpdateMatrix();
                Redraw();
            }
        }

        public void RemoveVertex(Vertex vertex)
        {
            if (_selectedVertex == vertex)
            {
                _selectedVertex = null;
            }

            DeleteEdgesConnectedTheVertex(vertex);
            _algo.DeleteVertex(vertex.Index);
            VertexList.Remove(vertex);
            for (var i = 0; i < VertexList.Count; ++i) VertexList[i].Index = i;
            UpdateMatrix();
            Redraw();
        }

        private void UpdateMatrix()
        {
            MatrixPanel.SuspendLayout();
            MatrixPanel.Hide();
            foreach (var controlMatrix in _controlMatrix)
            foreach (var lb in controlMatrix)
                lb.Dispose();

            _controlMatrix.Clear();
            var n = Matrix.GetLength(0);

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
                    if (i == j)
                    {
                        continue;
                    }

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
                        Text = Matrix[i - 1, j - 1].ToString()
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
            var textBox = (MyTextBox) sender;

            if (textBox.Text == string.Empty)
            {
                return;
            }

            var edge = FindEdge(textBox.From, textBox.To);

            if (edge == null)
            {
                edge = CreateEdge(FindVertex(textBox.From), FindVertex(textBox.To));
            }

            EditEdge(edge, int.Parse(textBox.Text));
            UpdateMatrix();
            Redraw();
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

        public void UpdateRoads(int[,] matrix)
        {
            _algo.SetMatrix(matrix);
            EdgeList.Clear();
            VertexList.Clear();
            var n = matrix.GetLength(0);
            for (var i = 0; i < n; i++) CreateVertex();

            for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                if (matrix[i, j] > 0)
                {
                    CreateEdge(FindVertex(i), FindVertex(j), matrix[i, j]);
                }

            UpdateMatrix();
        }

        private void EditVertexFormShow(MainForm mainForm, Vertex vertex, bool isNewVertex)
        {
            var newForm = new EditVertexForm(mainForm, vertex, isNewVertex);
            newForm.Show();
        }

        private void ChooseAlgoFormShow()
        {
            var newForm = new ChooseAlgorithmForm(_algo, this);
            newForm.Show();
        }

        private void GetPath_Click(object sender, EventArgs e)
        {
            _flagB = Modes.GetPath;
            findPathButton.Enabled = false;
            MessageBox.Show("Выберите начальную вершину");
        }

        private Vertex ClickedVertex(Point location)
        {
            Vertex vertex = null;
            var min = int.MaxValue;

            foreach (var v in VertexList)
            {
                var curMin = Math.Abs(v.Center.X - location.X) + Math.Abs(v.Center.Y - location.Y);

                if (curMin < min)
                {
                    min = curMin;
                    vertex = v;
                }
            }

            if (vertex != null)
            {
                var point = new Point(location.X - vertex.Center.X, location.Y - vertex.Center.Y);

                if (point.X * point.X + point.Y * point.Y > vertex.Radius * vertex.Radius)
                {
                    vertex = null;
                }
            }

            return vertex;
        }

        private void ShowVertexMenu(Point location)
        {
            if (_menu != null)
            {
                _menu.Dispose();
            }

            _menu = new ContextMenuStrip();
            _menu.Items.Add("Изменить");
            _menu.Items.Add("Удалить");
            _menu.ItemClicked += VertexMenu_Click;
            _menu.Show(GraphView, location);
        }

        private void ShowEmptyMenu(Point location)
        {
            if (_menu != null)
            {
                _menu.Dispose();
            }

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
                    EditVertexFormShow(this, _selectedVertex, false);

                    break;
                }

                case "Удалить":
                {
                    RemoveVertex(_selectedVertex);

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
                    EditVertexFormShow(this, CreateVertex(_clickedLocation), true);

                    break;
                }
            }
        }

        private void DeleteAll()
        {
            while(VertexList.Any())
            {
                var vertex = VertexList.First();

                if (vertex != null)
                {
                    RemoveVertex(vertex);
                }
            }

            Redraw();
        }
        private void GraphView_MouseDown(object sender, MouseEventArgs e)
        {
            _clickedLocation = e.Location;

            switch (_flagB)
            {
                case Modes.Select:
                {
                    _selectedVertex = ClickedVertex(e.Location);

                    if (_selectedVertex != null)
                    {
                        _bMove = true;
                        _selectedVertex.Center = e.Location;
                        Redraw();
                    }
                    else
                    {
                        _bMove = false;
                    }

                    break;
                }

                case Modes.AddVertex:
                {
                    _selectedVertex = ClickedVertex(e.Location);

                    if (_selectedVertex == null)
                    {
                        CreateVertex(e.Location);
                        Redraw();
                    }

                    break;
                }

                case Modes.Delete:
                {
                    _selectedVertex = ClickedVertex(e.Location);

                    if (_selectedVertex != null)
                    {
                        RemoveVertex(_selectedVertex);
                        Redraw();
                    }

                    break;
                }

                case Modes.AddEdge:
                {
                    _selectedVertex = ClickedVertex(e.Location);

                    if (_selectedVertex != null)
                    {
                        EditVertexFormShow(this, _selectedVertex, false);
                    }

                    break;
                }
                case Modes.GetPath:
                    _selectedVertex = ClickedVertex(e.Location);

                    if (_selectedVertex != null)
                    {
                        if (findPathButton.Enabled == false && SourceVertex == null)
                        {
                            SourceVertex = _selectedVertex;
                            InfoTextBox.Text = $"Из вершины {SourceVertex.Index}";
                            MessageBox.Show("Выберите до которой вершины ищем путь");
                        }
                        else
                        {
                            TargetVertex = _selectedVertex;
                            InfoTextBox.Text += $" в вершину {TargetVertex.Index}\n";
                            var from = SourceVertex.Index;
                            var to = TargetVertex.Index;
                            ChooseAlgoDijkstra( _algo.Dijkstra(from, to), from, to);
                            SourceVertex = null;
                            TargetVertex = null;
                            findPathButton.Enabled = true;
                            _flagB = Modes.Select;
                        }
                    }
                    
                    break;
                    
            }
        }

        private void GraphView_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (!_bMove)
            {
                return;
            }

            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            if (_selectedVertex == null)
            {
                return;
            }

            _selectedVertex.Center = e.Location;
            Redraw();
        }

        private void GraphView_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_bMove)
            {
                return;
            }

            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            if (_selectedVertex == null)
            {
                return;
            }

            _selectedVertex.Center = e.Location;
            _bMove = false;
            Redraw();
        }

        private void GenerateGraphButton_Click(object sender, EventArgs e)
        {

            var r = new Random();
            var n = r.Next(3, 5);
            var matrix = new int[n, n];

            for (var i = 0; i < n; ++i)
            {
                var intLine = Enumerable.Range(0, n).Select(x => n % 2 == 0 && x % 2 == 1 ? 0 : r.Next(1, 10) ).ToArray();
                for (var j = 0; j < n; j++)
                {
                    if(i == j)
                    {
                        continue;
                    }

                    matrix[i, j] = intLine[j];
                }
            }

            SetNewMatrix(matrix);

        }

        public void SetNewMatrix(int[,] matrix)
        {
            _algo.SetMatrix(matrix);
            EdgeList.Clear();
            VertexList.Clear();
            var n = matrix.GetLength(0);
            for (var i = 0; i < n; i++) CreateVertex();

            for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                if (matrix[i, j] > 0)
                {
                    CreateEdge(FindVertex(i), FindVertex(j), matrix[i, j]);
                }

            UpdateMatrix();
            Redraw();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void GraphView_Click(object sender, EventArgs e) { }

        private void selectButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = false;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            _flagB = Modes.Select;
        }

        private void drawVertexButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = true;
            drawVertexButton.Enabled = false;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            _flagB = Modes.AddVertex;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = false;
            _flagB = Modes.Delete;
        }

        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = false;
            deleteButton.Enabled = true;
            _flagB = Modes.AddEdge;
        }

        private void InfoTextBox_TextChanged(object sender, EventArgs e) { }

        private void MatrixPanel_Paint(object sender, PaintEventArgs e) { }

        private void MatrixPanel_Paint_1(object sender, PaintEventArgs e) { }

        #region DeleteEdges

        private void DeleteEdgesConnectedTheVertex(Vertex vertex)
        {
            var toDelete = new List<Edge>();

            foreach (var edge in EdgeList)
                if (edge.From.Index == vertex.Index || edge.To.Index == vertex.Index)
                {
                    toDelete.Add(edge);
                }

            foreach (var edge in toDelete) RemoveEdge(edge);
        }

        private void DeleteEdgesFromTheVertex(Vertex vertex)
        {
            var toDelete = new List<Edge>();

            foreach (var edge in EdgeList)
                if (edge.From.Index == vertex.Index)
                {
                    toDelete.Add(edge);
                }

            foreach (var edge in toDelete) RemoveEdge(edge);
        }

        private void DeleteEdgesToTheVertex(Vertex vertex)
        {
            var toDelete = new List<Edge>();

            foreach (var edge in EdgeList)
                if (edge.To.Index == vertex.Index)
                {
                    toDelete.Add(edge);
                }

            foreach (var edge in toDelete) RemoveEdge(edge);
        }

        #endregion

        #region CreateSmth

        private Vertex CreateVertex()
        {
            var location = new Point(_rnd.Next(50, GraphView.Width), _rnd.Next(50, GraphView.Height));
            var value = VertexList.Count();
            var vertex = new Vertex(location, value);
            VertexList.Add(vertex);
            _algo.AddVertex();
            UpdateMatrix();

            return vertex;
        }

        private Vertex CreateVertex(Point location)
        {
            var value = VertexList.Count();
            var vertex = new Vertex(location, value);
            VertexList.Add(vertex);
            _algo.AddVertex();
            for (var i = 0; i < value; i++) CreateEdge(FindVertex(value), FindVertex(i));
            DeleteEdgesFromTheVertex(vertex);
            for (var i = 0; i < value; i++) CreateEdge(FindVertex(i), FindVertex(value));
            DeleteEdgesToTheVertex(vertex);
            UpdateMatrix();

            return vertex;
        }

        private Vertex CreateVertex(List<Pair<Vertex, int>> connected)
        {
            var value = VertexList.Count();
            var vertex = new Vertex(value);
            VertexList.Add(vertex);
            _algo.AddVertex();
            foreach (var pair in connected) CreateEdge(vertex, pair.First, pair.Second);
            UpdateMatrix();

            return vertex;
        }

        private Vertex CreateVertex(Point location, List<Pair<Vertex, int>> connected)
        {
            var value = VertexList.Count();
            var vertex = new Vertex(location, value);
            VertexList.Add(vertex);
            _algo.AddVertex();
            AddNewLabelList(value);
            foreach (var pair in connected) CreateEdge(vertex, pair.First, pair.Second);

            return vertex;
        }

        private Edge CreateEdge(Vertex from, Vertex to)
        {
            var edge = new Edge(from, to, 1, Tools.GetRandomColor());
            EdgeList.Add(edge);
            _algo.AddEdge(from.Index, to.Index, 1);

            return edge;
        }

       
        
        private Edge CreateEdge(Vertex from, Vertex to, int value)
        {
           var edge = new Edge(from, to, value, Tools.GetRandomColor());
            EdgeList.Add(edge);
            _algo.AddEdge(from.Index, to.Index, value);

            return edge;
        }

        #endregion //CreateSmth

        private void deleteAllButton_Click(object sender, EventArgs e)
        {
            DeleteAll();
        }
    }
}