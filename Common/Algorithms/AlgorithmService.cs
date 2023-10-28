using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Common.Draws;

namespace Common.Algorithms;

public class AlgorithmService
{
    private MatrixWrapClass _MatrixWrap;
    public readonly List<List<Pair<int, int>>> _g;


    public AlgorithmService(MatrixWrapClass matrix, List<List<Pair<int, int>>> g)
    {
        _MatrixWrap =  matrix;
        _g = g;
    }

    public List<int> Dijkstra(int s, int finish)
    {
        var n = _MatrixWrap.Matrix.GetLength(0);   
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

            for (var j = 0; j < _g[v].Count; ++j)
            {
                var to = _g[v][j].To;
                var len = _g[v][j].Value;

                if (d[v] + len < d[to])
                {
                    d[to] = d[v] + len;
                    path[to] = v;
                }
            }
        }

        return path;
    }

}