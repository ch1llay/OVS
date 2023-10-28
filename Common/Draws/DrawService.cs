using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Draws.enums;

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


    public DrawService(PictureBox graphView, Action updateMatrix)
    {
        VertexList = new List<Vertex>();
        EdgeList = new List<Edge>();
        GraphView = graphView;
        UpdateMatrix = updateMatrix;
        _rnd = new Random();
        _graphDrawService = new GraphDrawService();
    }
    
    public List<Vertex> VertexList { get; set; }

    public List<Edge> EdgeList { get; set; }

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

    #region CreateSmth

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
        for (var i = 0; i < value; i++) CreateEdge(FindVertex(value), FindVertex(i));
        DeleteEdgesFromTheVertex(vertex);
        for (var i = 0; i < value; i++) CreateEdge(FindVertex(i), FindVertex(value));
        DeleteEdgesToTheVertex(vertex);
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
        var edge = new Edge(from, to, 1, new Pen(Tools.GetRandomColor(), Config.DefaultEdgePenSize));
        EdgeList.Add(edge);
        _graphDrawService.AddEdge(from.Index, to.Index, 1);

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

    #endregion //CreateSmth
}