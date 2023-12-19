using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Common.Draws.enums;
using System.Data;
using System.IO.Packaging;

namespace Common.Draws;

public class DrawService
{
    public readonly GraphDrawService _graphDrawService;
    public Modes _currentMode = Modes.Select;
    public bool _isMoving;
    private readonly Random _rnd;
    public Vertex _selectedVertex;
    public Action<int> AddNewLabelList;
    public PictureBox GraphView;
    public Vertex SourceVertex;
    public Vertex TargetVertex;
    public Action UpdateMatrix;
    public List<Package> _packages;
    private List<(int, int, int)> _toTable;
    private Inform form;

    public DrawService(PictureBox graphView, Action updateMatrix)
    {
        VertexList = new List<Vertex>();
        EdgeList = new List<Edge>();
        GraphView = graphView;
        UpdateMatrix = updateMatrix;
        _rnd = new Random();
        _graphDrawService = new GraphDrawService();
        _packages = new();
        _toTable = new();
        form = new();
    }

    public List<Vertex> VertexList { get; set; }

    public List<Edge> EdgeList { get; set; }

    public DataTable DataTable { get; }
    public int[,] Matrix
    {
        get => _graphDrawService.MatrixWrap.Matrix;
        set => _graphDrawService.MatrixWrap.Matrix = value;
    }

    public void Redraw()
    {
        if (GraphView.Image != null) GraphView.Image.Dispose();

        var image = new Bitmap(GraphView.Width, GraphView.Height);
        var graphics = Graphics.FromImage(image);

        foreach (var vertex in VertexList) vertex.Draw(graphics);

        foreach (var edge in EdgeList) edge.Draw(graphics);

        GraphView.Image = image;
        graphics.Dispose();
    }
    public void Redraw(Graphics graphics, Bitmap image, Color color)
    {
        //if (GraphView.Image != null) GraphView.Image.Dispose();

        foreach (var vertex in VertexList) vertex.Draw(graphics);

        foreach (var edge in EdgeList) edge.Draw(graphics);

        if (_packages.Count != 0)
        {
            for (int i = 0; i < _packages.Count; i++)
            {
                var pckg = EdgeList.Find(item => item.From.Index == _packages[i].LastPosition && item.To.Index == _packages[i].Position);
                if (pckg != null) pckg.DrawAnimatedPackage(graphics, 100, color);
            }
        }

        GraphView.Image = new Bitmap(image);
        //graphics.Dispose();
    }
    #region For Vertex and Edges
    public Edge FindEdge(int from, int to)
    {
        foreach (var edge in EdgeList)
            if (edge.From.Index == from && edge.To.Index == to)
                return edge;

        return null;
    }

    public Vertex FindVertex(int index)
    {
        foreach (var vertex in VertexList)
            if (vertex.Index == index)
                return vertex;

        return null;
    }

    public void ResetColors()
    {
        foreach (var vertex in VertexList) vertex.ResetColor();

        foreach (var edge in EdgeList) edge.ResetColor();
    }

    #region DeleteEdges

    public void DeleteEdgesConnectedTheVertex(Vertex vertex)
    {
        var toDelete = new List<Edge>();

        foreach (var edge in EdgeList)
            if (edge.From.Index == vertex.Index || edge.To.Index == vertex.Index)
                toDelete.Add(edge);

        foreach (var edge in toDelete) RemoveEdge(edge);
    }

    public void DeleteEdgesFromTheVertex(Vertex vertex)
    {
        var toDelete = new List<Edge>();

        foreach (var edge in EdgeList)
            if (edge.From.Index == vertex.Index)
                toDelete.Add(edge);

        foreach (var edge in toDelete) RemoveEdge(edge);
    }

    public void DeleteEdgesToTheVertex(Vertex vertex)
    {
        var toDelete = new List<Edge>();

        foreach (var edge in EdgeList)
            if (edge.To.Index == vertex.Index)
                toDelete.Add(edge);

        foreach (var edge in toDelete) RemoveEdge(edge);
    }

    #endregion

    public Vertex CreateVertex()
    {
        var location = new Point(_rnd.Next(50, GraphView.Width), _rnd.Next(50, GraphView.Height));
        var value = VertexList.Count();
        var vertex = new Vertex(location, value);
        VertexList.Add(vertex);
        _graphDrawService.AddVertex();
        UpdateMatrix();

        return vertex;
    }

    public Vertex CreateVertex(Point location)
    {
        var value = VertexList.Count();
        var vertex = new Vertex(location, value);
        VertexList.Add(vertex);
        _graphDrawService.AddVertex();
        //for (var i = 0; i < value; i++) CreateEdge(FindVertex(value), FindVertex(i));
        // DeleteEdgesFromTheVertex(vertex);
        //for (var i = 0; i < value; i++) CreateEdge(FindVertex(i), FindVertex(value));
        //DeleteEdgesToTheVertex(vertex);
        UpdateMatrix();

        return vertex;
    }

    public Vertex CreateVertex(List<Pair<Vertex, int>> connected)
    {
        var value = VertexList.Count();
        var vertex = new Vertex(value);
        VertexList.Add(vertex);
        _graphDrawService.AddVertex();
        foreach (var pair in connected) CreateEdge(vertex, pair.To, pair.Value);
        UpdateMatrix();

        return vertex;
    }

    public Vertex CreateVertex(Point location, List<Pair<Vertex, int>> connected)
    {
        var value = VertexList.Count();
        var vertex = new Vertex(location, value);
        VertexList.Add(vertex);
        _graphDrawService.AddVertex();
        AddNewLabelList(value);
        foreach (var pair in connected) CreateEdge(vertex, pair.To, pair.Value);

        return vertex;
    }

    public Edge CreateEdge(Vertex from, Vertex to)
    {
        var edge = new Edge(from, to, 0, new Pen(Tools.GetRandomColor(), Config.DefaultEdgePenSize));
        EdgeList.Add(edge);
        _graphDrawService.AddEdge(from.Index, to.Index, 0);

        return edge;
    }

    public void SetNewMatrix(int[,] matrix)
    {
        _graphDrawService.SetMatrix(matrix);
        EdgeList.Clear();
        VertexList.Clear();
        var n = matrix.GetLength(0);
        for (var i = 0; i < n; i++) CreateVertex();

        for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                if (matrix[i, j] > 0)
                    CreateEdge(FindVertex(i), FindVertex(j), matrix[i, j]);

        UpdateMatrix();
        Redraw();
    }

    public void DeleteAll()
    {
        while (VertexList.Any())
        {
            var vertex = VertexList.First();

            if (vertex != null) RemoveVertex(vertex);
        }

        Redraw();
    }

    public Vertex ClickedVertex(Point location)
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

            if (point.X * point.X + point.Y * point.Y > vertex.Radius * vertex.Radius) vertex = null;
        }

        return vertex;
    }

    public void UpdateRoads(int[,] matrix)
    {
        _graphDrawService.SetMatrix(matrix);
        EdgeList.Clear();
        VertexList.Clear();
        var n = Matrix.GetLength(0);
        for (var i = 0; i < n; i++) CreateVertex();

        for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                if (Matrix[i, j] > 0)
                    CreateEdge(FindVertex(i), FindVertex(j), Matrix[i, j]);

        UpdateMatrix();
    }

    public void RemoveVertex(Vertex vertex)
    {
        if (_selectedVertex == vertex) _selectedVertex = null;

        DeleteEdgesConnectedTheVertex(vertex);
        _graphDrawService.DeleteVertex(vertex.Index);
        VertexList.Remove(vertex);
        for (var i = 0; i < VertexList.Count; ++i) VertexList[i].Index = i;
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

    public void RemoveEdge(Edge edge)
    {
        Matrix[edge.From.Index, edge.To.Index] = 0;
        EdgeList.Remove(edge);
        UpdateMatrix();
        Redraw();
    }

    public void EditEdge(Edge edge, int value)
    {
        Matrix[edge.From.Index, edge.To.Index] = value;

        if (value == 0)
        {
            RemoveEdge(edge);

            return;
        }

        edge.Value = value;
        var edgeG = _graphDrawService.G[edge.From.Index].FirstOrDefault(e => e.To == edge.To.Index);

        if (edgeG != null)
        {
            edgeG.Value = value;
        }
    }

    public void EditVertex(Vertex vertex, Dictionary<Vertex, int> connected)
    {
        DeleteEdgesFromTheVertex(vertex);

        foreach (var pair in connected)
            if (pair.Value != 0)
                CreateEdge(vertex, pair.Key, pair.Value);

        UpdateMatrix();
        Redraw();
    }

    public Edge CreateEdge(Vertex from, Vertex to, int value)
    {
        var edge = new Edge(from, to, value, new Pen(Tools.GetRandomColor(), Config.DefaultEdgePenSize));
        EdgeList.Add(edge);
        _graphDrawService.AddEdge(from.Index, to.Index, value);

        return edge;
    }

    #endregion
    public void AnimatePackage(int from, int to, Package package, Color color)
    {
        var needEdge = EdgeList.Find(i => i.From.Index == from && i.To.Index == to);
        if (needEdge != null)
        {
            for (int i = 1; i < 100; i++)
            {
                var image = new Bitmap(GraphView.Width, GraphView.Height);
                var gr = Graphics.FromImage(image);
                needEdge.DrawAnimatedPackage(gr, i, color);
                Redraw(gr, image, color);
                gr.Dispose();
                Thread.Sleep(10);
            }
            package.Position = to;
            package.LastPosition = from;
        }
    }

    public void GetData()
    {
        DataGridView dataGridView1 = new();
        dataGridView1.BackgroundColor = System.Drawing.Color.Snow;
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
        dataGridView1.Location = new System.Drawing.Point(0, 0);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.RowTemplate.Height = 29;
        dataGridView1.Size = new System.Drawing.Size(599, 236);
        dataGridView1.TabIndex = 0;


        DataSet data = new DataSet("Table");
        DataTable dataTable = new DataTable("DataTable");
        data.Tables.Add(dataTable);
        dataTable.Columns.Add(new DataColumn("From", typeof(int)));
        dataTable.Columns.Add(new DataColumn("To", typeof(int)));
        dataTable.Columns.Add(new DataColumn("Position", typeof(int)));
        dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
        dataTable.Columns.Add(new DataColumn("Last position", typeof(int)));

        Inform inform = new Inform();
        inform.Location = new Point(-50,0);
        inform.Width = 700;
        inform.Height = 300;
        inform.Controls.Add(dataGridView1);

        foreach (var i in _packages)
        {
            dataTable.Rows.Add(new object[] { i.From, i.To, i.Position, i.Id, i.LastPosition});
        }
        dataGridView1.DataSource = data.Tables[0];

        
        inform.ShowDialog();
    }

    public void RandomRouting(Package package)
    {
        _packages.Add(package);
        while (package.Position != package.To)
        {
            var from = package.Position;
            List<Pair<int, int>> listTo = new();
            var buf = _graphDrawService.G[from];
            for (int i = 0; i < buf.Count; i++)
            {
                if (listTo.Find(it => it.To == buf[i].To && it.Value == buf[i].Value) == null)
                {
                    listTo.Add(buf[i]);
                }
            }
            listTo.RemoveAll(it => it.To == package.LastPosition);
            int index = 0;
            for (; ; )
            {
                if (listTo.Count == 0)
                {
                    index = package.LastPosition; break;
                }
                else if (listTo.Count == 1) break;
                index = _rnd.Next(0, listTo.Count);
                if (index != package.LastPosition)
                    break;
            }
            AnimatePackage(from, listTo[index].To, package, Color.Crimson);
            GetData();
        }
        var image = new Bitmap(GraphView.Width, GraphView.Height);
        var graphics = Graphics.FromImage(image);
        Redraw(graphics, image, Color.Crimson);
        graphics.Dispose();
    }

    public void AvalangeRouting(Package package)
    {
        foreach (var pckg in _packages)
        {
            if (pckg.Position == TargetVertex.Index)
            {
                return;
            }
        }
        Thread? thread = null;
        var from = package.Position;
        List<Pair<int, int>> listTo = new();
        var buf = _graphDrawService.G[from];
        if (_packages.Count == 0) _packages.Add(package);
        for (int i = 0; i < buf.Count; i++)
        {
            if (listTo.Find(it => it.To == buf[i].To && it.Value == buf[i].Value) == null)
            {
                listTo.Add(buf[i]);
            }
        }
        listTo.RemoveAll(it => it.To == package.LastPosition);

        if (listTo.Count == 1)
        {
            thread = new Thread(() => AnimatePackage(from, listTo[0].To, package, Color.DarkViolet));
            thread.Start();
            thread.Join();
            GetData();
            AvalangeRouting(package);
        }
        else if (listTo.Count > 1)
        {
            List<Package> newPackages = new();
            for (int i = 0; i < listTo.Count; i++)
            {
                Package item = new(package);
                item.Id = _packages.Count + 2;
                if (i != listTo.Count - 1)
                {
                    _packages.Add(item);
                    newPackages.Add(item);
                    thread = new Thread(() => AnimatePackage(from, listTo[i].To, item, Color.DarkViolet));
                }
                else
                {
                    newPackages.Add(package);
                    thread = new Thread(() => AnimatePackage(from, listTo[i].To, package, Color.DarkViolet));
                }
                thread.Start();
                thread.Join();
            }
            GetData();
            for (int i = 0; i < newPackages.Count; i++)
            {
                AvalangeRouting(newPackages[i]);
            }
        }
        else return;
        var image = new Bitmap(GraphView.Width, GraphView.Height);
        var graphics = Graphics.FromImage(image);
        Redraw(graphics, image, Color.DarkViolet);
        graphics.Dispose();
        //_packages.Clear();
    }

    public List<(int,int,int)> CreateRoutingTable(Package package, int metric = 1)
    {
        Thread? thread = null;
        var from = package.Position;
        List<Pair<int, int>> listTo = new();
        var buf = _graphDrawService.G[from];
        if (_packages.Count == 0) _packages.Add(package);
        for (int i = 0; i < buf.Count; i++)
        {
            if (listTo.Find(it => it.To == buf[i].To && it.Value == buf[i].Value) == null)
            {
                listTo.Add(buf[i]);
            }
        }
        listTo.RemoveAll(it => it.To == package.LastPosition);
        for (int i = 0; i < listTo.Count; i++)
        {
            _toTable.Add((listTo[i].To, metric, from));
        }

        if (listTo.Count == 1)
        {
            thread = new Thread(() => AnimatePackage(from, listTo[0].To, package, Color.Blue));
            thread.Start();
            thread.Join();
            CreateRoutingTable(package, metric + 1);
        }
        else if (listTo.Count > 1)
        {
            List<Package> newPackages = new();
            for (int i = 0; i < listTo.Count; i++)
            {
                Package item = new(package);
                item.Id = _packages.Count + 2;
                if (i != listTo.Count - 1)
                {
                    _packages.Add(item);
                    newPackages.Add(item);
                    thread = new Thread(() => AnimatePackage(from, listTo[i].To, item, Color.Blue));
                }
                else
                {
                    newPackages.Add(package);
                    thread = new Thread(() => AnimatePackage(from, listTo[i].To, package, Color.Blue));
                }
                thread.Start();
                thread.Join();
            }
            for (int i = 0; i < newPackages.Count; i++)
            {
                CreateRoutingTable(newPackages[i], metric + 1);
            }
        }
        else return null;
        var image = new Bitmap(GraphView.Width, GraphView.Height);
        var graphics = Graphics.FromImage(image);
        Redraw(graphics, image, Color.Blue);
        //Redraw();
        graphics.Dispose();

        return new List<(int, int, int)>(_toTable);
    }

    public List<int>? PreviousExperience(Package package, List<int>path)
    {
        if(_toTable.Count == 0)
        {
            return null;
        }
        Thread? thread;
        if (path == null)
        {
            path = new();
        }


        if(_toTable.FindIndex(val => val.Item1 == package.To && val.Item2 == 1) != -1)
        {
            thread = new Thread(() => AnimatePackage(package.From, package.To, package, Color.Orange));
            thread.Start();
            thread.Join();
            _packages.Add(package);
            GetData();
            return new List<int>() { package.From, package.To};
        }

        var child = _toTable.Find(val => val.Item1 == package.To);
        (int, int, int) parent = (0,0,0);
        for (int i = 0; i < _graphDrawService.G.Count; i++)
        {
            if (_graphDrawService.G[i].FindIndex(pair => pair.To == package.To) != -1)
            {
                parent = _toTable.Find(val => val.Item1 == i);
                
                if (parent.Item2 != child.Item2 - 1)
                    continue;
                else
                    break;
            }
        }
        path.Add(child.Item1);
        while(parent.Item3 != 0)
        {
            path.Add(parent.Item1);
            parent = _toTable.Find(val => val.Item1 == parent.Item3);
        }
        path.Add(parent.Item1);
        path.Add(package.From);
        path.Reverse();
        for(int i = 0; i < path.Count-1; i++)
        {
            thread = new Thread(() => AnimatePackage(path[i], path[i+1], package, Color.Orange));
            thread.Start();
            thread.Join();
            _packages.Add(package);
            GetData();
        }
        return path;
    }

}