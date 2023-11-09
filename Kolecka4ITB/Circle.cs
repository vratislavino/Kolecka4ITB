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
                    center.X - radius,
                    center.Y - radius,
                    2 * radius,
                    2 * radius);
            } else {
                g.DrawEllipse(colorPen,
                    center.X - radius,
                    center.Y - radius,
                    2 * radius,
                    2 * radius);
            }

            // highlight
            if(highlighted) {
                g.DrawEllipse(outlinePen,
                    center.X - radius,
                    center.Y - radius,
                    2 * radius,
                    2 * radius);
            }

            if(showCenters) {
                g.DrawLine(centerPen, center.X - crossSize, center.Y, center.X + crossSize, center.Y);
                g.DrawLine(centerPen, center.X, center.Y - crossSize, center.X, center.Y + crossSize);
            }
        }

        public override void ChangeSize(float radius) {
            this.radius = radius;
        }

        public override bool ContainsPoint(Point location, out float currentDistance) {
            currentDistance = location.Distance(center);
            return currentDistance < radius;
        }

        // EVEN THOUGH THIS MIGHT MAKE OUR LIFE MUCH EASIER, IT MAY NOT BE THE BEST SOLUTION
        // -- fixed by ChatGPT
        internal void Edit(float r, int x, int y, Color color, bool fill) {
            this.radius = r;
            this.center = new Point(x, y);
            this.color = color;
            this.fill = fill;
        }

    }
}
