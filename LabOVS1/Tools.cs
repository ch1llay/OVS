using System;
using System.Drawing;

namespace LabOVS1
{
    public static class Tools
    {
        public static Color GetRandomColor(int redMax=255)
        {
            var random = new Random();
            return Color.FromArgb(255,random.Next(0, redMax), random.Next(9, 255), random.Next(0, 255));
        }
    }
}