using System;
using System.Collections.Generic;

namespace LabOVS1
{
    public class Pair<T, TU>
    {
        public Pair() { }

        public Pair(T first, TU second)
        {
            First = first;
            Second = second;
        }

        public T First { get; set; }
        public TU Second { get; set; }
    }

    public class Main
    {
        private static Main _instance;
        public readonly List<List<Pair<int, int>>> G;

        public int[,] Matrix;

        private Main()
        {
            Matrix = new int[0, 0];
            G = new List<List<Pair<int, int>>>();
        }
        

        public static Main GetInstance()
        {
            return _instance ??= new Main();
        }

        public void SetMatrix(int[,] matrix)
        {
            Matrix = new int[0, 0];
            G.Clear();
        }

        public void AddEdge(int vertex1, int vertex2, int value)
        {
            Matrix[vertex1, vertex2] = value;
            G[vertex1].Add(new Pair<int, int>(vertex2, value));
        }

        public void AddVertex()
        {
            var n = Matrix.GetLength(0);
            var m = Matrix.GetLength(1);
            var newMatrix = new int[n + 1, m + 1];
            G.Add(new List<Pair<int, int>>());

            for (var i = 0; i < n; ++i)
            for (var j = 0; j < m; ++j)
                newMatrix[i, j] = Matrix[i, j];

            Matrix = newMatrix;
        }

        public void DeleteVertex(int vertex)
        {
            var n = Matrix.GetLength(0);
            var m = Matrix.GetLength(1);

            if (n <= 0)
            {
                return;
            }

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
                    newMatrix[newI, newJ] = Matrix[i, j];

                    if (newMatrix[newI, newJ] > 0)
                    {
                        G[newI].Add(new Pair<int, int>(newJ, newMatrix[newI, newJ]));
                    }
                }

                j++;

                for (; j < m; ++newJ, ++j)
                {
                    newMatrix[newI, newJ] = Matrix[i, j];

                    if (newMatrix[newI, newJ] > 0)
                    {
                        G[newI].Add(new Pair<int, int>(newJ, newMatrix[newI, newJ]));
                    }
                }
            }

            i++;

            for (; i < n; ++newI, ++i)
            {
                var newJ = 0;
                var j = 0;

                for (; j < vertex; ++newJ, ++j)
                {
                    newMatrix[newI, newJ] = Matrix[i, j];

                    if (newMatrix[newI, newJ] > 0)
                    {
                        G[newI].Add(new Pair<int, int>(newJ, newMatrix[newI, newJ]));
                    }
                }

                j++;

                for (; j < n; ++newJ, ++j)
                {
                    newMatrix[newI, newJ] = Matrix[i, j];

                    if (newMatrix[newI, newJ] > 0)
                    {
                        G[newI].Add(new Pair<int, int>(newJ, newMatrix[newI, newJ]));
                    }
                }
            }

            Matrix = newMatrix;
        }

        public List<int> Dijkstra(int s, int finish)
        {
            var n = Matrix.GetLength(0);   
            var path = new List<int>(n);
            //Массив с длинами особо кратчайших путей
            var d = new List<int>(n);
            var was = new List<bool>(n);

            for (var i = 0; i < n; ++i)
            {
                path.Add(0);
                d.Add(0);
                was.Add(false);
            }

            for (var i = 0; i < n; ++i)
            {
                d[i] = int.MaxValue;
                was[i] = false;
                path[i] = 0;
            }

            d[s] = 0;

            for (var i = 0; i < n; ++i)
            {
                var v = -1;

                for (var j = 0; j < n; ++j)
                    if (!was[j] && (v == -1 || d[j] < d[v]))
                    {
                        v = j;
                    }

                if (d[v] == int.MaxValue)
                {
                    break;
                }

                was[v] = true;

                for (var j = 0; j < G[v].Count; ++j)
                {
                    var to = G[v][j].First;
                    var len = G[v][j].Second;

                    if (d[v] + len < d[to])
                    {
                        d[to] = d[v] + len;
                        path[to] = v;
                    }
                }
            }

            return path;
        }

        public int[,] Floid()
        {
            var n = Matrix.GetLength(0);
            var d = new int[n, n];
            var path = new int[n, n];


            for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
            {
                path[i, j] = j;

                if (i == j)
                {
                    continue;
                }

                if (Matrix[i, j] == 0)
                {
                    d[i, j] = int.MaxValue;
                }
                else
                {
                    d[i, j] = Matrix[i, j];
                }
            }

            for (var k = 0; k < n; k++)
            for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                if (d[i, k] + d[k, j] < d[i, j])
                {
                    d[i, j] = d[i, k] + d[k, j];
                    path[i, j] = path[i, k];
                }


            return path;
        }
    }
}