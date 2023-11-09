using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolecka4ITB
{
    public class Circle : Shape
    {
        private float radius;
        public float Radius { get => radius; set => radius = value; }

        public Circle(Point center, Color color, bool fill) : base(center, color, fill) {
            radius = 0;
        }

        public override void Draw(Graphics g, bool showCenters) {
            if (fill) {
                g.FillEllipse(colorBrush,
                    origin.X - radius,
                    origin.Y - radius,
                    2 * radius,
                    2 * radius);
            } else {
                g.DrawEllipse(colorPen,
                    origin.X - radius,
                    origin.Y - radius,
                    2 * radius,
                    2 * radius);
            }

            // highlight
            if(highlighted) {
                g.DrawEllipse(outlinePen,
                    origin.X - radius,
                    origin.Y - radius,
                    2 * radius,
                    2 * radius);
            }

            if(showCenters) {
                g.DrawLine(centerPen, origin.X - crossSize, origin.Y, origin.X + crossSize, origin.Y);
                g.DrawLine(centerPen, origin.X, origin.Y - crossSize, origin.X, origin.Y + crossSize);
            }
        }

        public override void ChangeSize(float radius) {
            this.radius = radius;
        }

        public override bool ContainsPoint(Point location, out float currentDistance) {
            currentDistance = location.Distance(origin);
            return currentDistance < radius;
        }

        // EVEN THOUGH THIS MIGHT MAKE OUR LIFE MUCH EASIER, IT MAY NOT BE THE BEST SOLUTION
        // -- fixed by ChatGPT
        internal void Edit(float r, int x, int y, Color color, bool fill) {
            this.radius = r;
            this.origin = new Point(x, y);
            this.color = color;
            this.fill = fill;
        }

        public override float CalculateSize(Point location) {
            return location.Distance(origin);
        }
    }
}
