using System;
using System.Drawing;
using System.Linq;

namespace Common.Draws
{
    public static class Tools
    {
        public static Color GetRandomColor(int redMax=255)
        {
            var random = new Random();
            return Color.FromArgb(255,random.Next(0, redMax), random.Next(9, 255), random.Next(0, 255));
        }

        public static int[,] GenerateGraphMatrix(int nodesAmount)
        {
            var r = new Random();
            var matrix =  new int[nodesAmount, nodesAmount];

            for (var i = 0; i < nodesAmount; ++i)
            {
                var intLine = Enumerable.Range(0, nodesAmount)
                    .Select(x => nodesAmount % 2 == 0 && x % 2 == 1 
                        ? 0 
                        : r.Next(1, 10))
                    .ToArray();
                for (var j = 0; j < nodesAmount; j++)
                {
                    if(i == j)
                    {
                        continue;
                    }

                    matrix[i, j] = intLine[j];
                }
            }

            return matrix;
        }
    }
}