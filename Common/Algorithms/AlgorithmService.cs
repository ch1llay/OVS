using Common.Draws;
using System.Collections.Generic;

namespace Common.Algorithms;

public class AlgorithmService
{
    private readonly MatrixWrapClass _matrixWrap;
    private readonly List<List<Pair<int, int>>> _g;

    public AlgorithmService(MatrixWrapClass matrix, List<List<Pair<int, int>>> g)
    {
        _matrixWrap = matrix;
        _g = g;
    }

    public List<int> Dijkstra(int s, int finish)
    {
        var n = _matrixWrap.Matrix.GetLength(0);
        var path = new List<int>(n);
        //Массив с длинами особо кратчайших путей
        var dist = new List<int>(n);
        var was = new List<bool>(n);

        for (var i = 0; i < n; ++i)
        {
            path.Add(-1);
            dist.Add(int.MaxValue);
            was.Add(false);
        }

        dist[s] = 0;

        for (var i = 0; i < n; ++i)
        {
            var v = -1;

            for (var j = 0; j < n; ++j)
                if (!was[j] && (v == -1 || dist[j] < dist[v]))
                {
                    v = j;
                }

            if (dist[v] == int.MaxValue)
            {
                break;
            }

            was[v] = true;

            for (var j = 0; j < _g[v].Count; ++j)
            {
                var to = _g[v][j].To;
                var len = _g[v][j].Value;

                if (dist[v] + len < dist[to])
                {
                    dist[to] = dist[v] + len;
                    path[to] = v;
                }
            }
        }

        return path;
    }
}