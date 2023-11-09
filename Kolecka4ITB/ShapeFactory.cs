using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolecka4ITB
{
    public class ShapeFactory
    {
        public Color Color { get; set; }
        public bool Fill { get; set; }

        public Shape CreateShape(Type type, Point loc) {
             return Activator.CreateInstance(type, new object[] { loc, Color, Fill }) as Shape;
        }

    }
}
