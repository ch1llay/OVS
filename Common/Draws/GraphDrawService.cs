using System;
using System.Collections.Generic;
using Common.Algorithms;

namespace Common.Draws;

public class Pair<T, TU>
{
    public Pair()
    {
    }

    public Pair(T to, TU value)
    {
        To = to;
        Value = value;
    }

    public T To { get; set; }
    public TU Value { get; set; }
}

public class MatrixWrapClass
{
    public int[,] Matrix;

    public MatrixWrapClass()
    {
        Matrix = new int[0, 0];
    }
}

public class GraphDrawService
{
    private static GraphDrawService _instance;
    public readonly List<List<Pair<int, int>>> G;
    private readonly AlgorithmService _algorithmService;

    public MatrixWrapClass MatrixWrap;

    public GraphDrawService()
    {
        MatrixWrap = new MatrixWrapClass();
        G = new List<List<Pair<int, int>>>();
        _algorithmService = new AlgorithmService(MatrixWrap, G);
    }

    public List<int> Dijkstra(int s, int finish)
    {
        return _algorithmService.Dijkstra(s, finish);
    }

    public static GraphDrawService GetInstance()
    {
        return _instance ??= new GraphDrawService();
    }

    public void SetMatrix(int[,] matrix)
    {
        MatrixWrap.Matrix = new int[0, 0];
        G.Clear();
    }

    public void AddEdge(int vertex1, int vertex2, int value)
    {
        if (value == 0)
        {
            return;
        }

        MatrixWrap.Matrix[vertex1, vertex2] = value;
        G[vertex1].Add(new Pair<int, int>(vertex2, value));
    }

    public void AddVertex()
    {
        var n = MatrixWrap.Matrix.GetLength(0);
        var m = MatrixWrap.Matrix.GetLength(1);
        var newMatrix = new int[n + 1, m + 1];
        G.Add(new List<Pair<int, int>>());

        for (var i = 0; i < n; ++i)
            for (var j = 0; j < m; ++j)
                newMatrix[i, j] = MatrixWrap.Matrix[i, j];

        MatrixWrap.Matrix = newMatrix;
    }

    public void DeleteVertex(int vertex)
    {
        var n = MatrixWrap.Matrix.GetLength(0);
        var m = MatrixWrap.Matrix.GetLength(1);

        if (n <= 0) return;

        var newMatrix = new int[n - 1, m - 1];
        var newI = 0;
        var i = 0;
        G.Clear();
        for (var k = 0; k < n - 1; ++k) G.Add(new List<Pair<int, int>>());

        for (; i < vertex; ++newI, ++i)
        {
            var newJ = 0;
            var j = 0;

            for (; j < vertex; ++newJ, ++j)
            {
                newMatrix[newI, newJ] = MatrixWrap.Matrix[i, j];

                if (newMatrix[newI, newJ] > 0) G[newI].Add(new Pair<int, int>(newJ, newMatrix[newI, newJ]));
            }

            j++;

            for (; j < m; ++newJ, ++j)
            {
                newMatrix[newI, newJ] = MatrixWrap.Matrix[i, j];

                if (newMatrix[newI, newJ] > 0) G[newI].Add(new Pair<int, int>(newJ, newMatrix[newI, newJ]));
            }
        }

        i++;

        for (; i < n; ++newI, ++i)
        {
            var newJ = 0;
            var j = 0;

            for (; j < vertex; ++newJ, ++j)
            {
                newMatrix[newI, newJ] = MatrixWrap.Matrix[i, j];

                if (newMatrix[newI, newJ] > 0) G[newI].Add(new Pair<int, int>(newJ, newMatrix[newI, newJ]));
            }

            j++;

            for (; j < n; ++newJ, ++j)
            {
                newMatrix[newI, newJ] = MatrixWrap.Matrix[i, j];

                if (newMatrix[newI, newJ] > 0) G[newI].Add(new Pair<int, int>(newJ, newMatrix[newI, newJ]));
            }
        }

        MatrixWrap.Matrix = newMatrix;
    }


    public (List<int>,int) Floid(int from, int to)
    {
        var n = MatrixWrap.Matrix.GetLength(0);
        var d = new int[n, n];
        //var path = new int[n, n];
        List<int> path = new List<int>();



        for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
            {
                //path[i, j] = j;

                if (i == j) continue;

                if (MatrixWrap.Matrix[i, j] == 0)
                    d[i, j] = /*int.MaxValue;*/ 11;
                else
                    d[i, j] = MatrixWrap.Matrix[i, j];
            }

        List<List<int>> example = new List<List<int>>();
        for (int i = 0; i < n; i++)
        {
            example.Add(new List<int>());
            for (int j = 0; j < n; j++)
            {
                example[i].Add(-1);
            }
        }

        for (var k = 0; k < n; k++)
            for (var i = 0; i < n; i++)
                for (var j = 0; j < n; j++)
                    if (d[i, k] + d[k, j] < d[i, j])
                    {
                        //d[i, j] = d[i, k] + d[k, j];
                        //path[i, j] = path[i, k];
                        d[i, j] = Math.Min(d[i, j], d[i, k] + d[k, j]);
                        example[i][j] = k; // запоминает промежуточные фазы(развивать этот момент)
                    }
        path.Add(to);
        int dist = d[from, to];
        while(true)
        {
            if (example[from][to] == -1)
            {
                if (d[from, to] != 11)
                {
                    path.Add(from);
                    break;
                }
                else
                {
                    path.Clear();
                    break;
                }
            }
            else
            {
                path.Add(example[from][to]);
                to = example[from][to];
            }
        }
        path.Reverse();

        return (path, dist);
    }
}