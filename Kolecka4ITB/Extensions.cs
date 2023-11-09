﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolecka4ITB
{
    public static class Extensions
    {
        public static float Distance(this Point a, Point b) {
            return (float) Math.Sqrt(
                Math.Pow(a.X - b.X, 2) +
                Math.Pow(a.Y - b.Y, 2)
                );
        }
    }
}
