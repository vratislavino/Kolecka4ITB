using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolecka4ITB
{
    public class Square : Shape
    {
        private float size;

        public Square(Point center, Color color, bool fill) : base(center, color, fill) {
            size = 0;
        }

        public override void ChangeSize(float size) {
            this.size = size;
        }

        public override bool ContainsPoint(Point location, out float currentDistance) {
            currentDistance = location.Distance(center);

            return location.X > center.X - size / 2 && location.X < center.X + size / 2 &&
                location.Y > center.Y - size / 2 && location.Y < center.Y + size / 2;
        }

        public override void Draw(Graphics g, bool showCenters) {
            if (fill) {
                g.FillRectangle(colorBrush,
                    center.X,
                    center.Y,
                    size,
                    size);
            } else {
                g.DrawRectangle(colorPen,
                    center.X,
                    center.Y,
                    size,
                    size);
            }

            // highlight
            if (highlighted) {
                g.DrawRectangle(outlinePen,
                    center.X,
                    center.Y,
                    size,
                    size);
            }

            if (showCenters) {
                g.DrawLine(centerPen, center.X - crossSize, center.Y, center.X + crossSize, center.Y);
                g.DrawLine(centerPen, center.X, center.Y - crossSize, center.X, center.Y + crossSize);
            }
        }
    }
}
