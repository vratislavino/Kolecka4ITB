using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
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

        public override float CalculateSize(Point location) {
            int xdiff = location.X - origin.X;
            int ydiff = location.Y - origin.Y;
            return xdiff > ydiff ? xdiff : ydiff;
        }

        public override void ChangeSize(float size) {
            this.size = size;

        }

        public override bool ContainsPoint(Point location, out float currentDistance) {
            currentDistance = location.Distance(origin);

            return location.X > origin.X && location.X < origin.X + size &&
                location.Y > origin.Y && location.Y < origin.Y + size;
        }

        public override void Draw(Graphics g, bool showCenters) {
            if (fill) {
                g.FillRectangle(colorBrush,
                    origin.X,
                    origin.Y,
                    size,
                    size);
            } else {
                g.DrawRectangle(colorPen,
                    origin.X,
                    origin.Y,
                    size,
                    size);
            }

            // highlight
            if (highlighted) {
                g.DrawRectangle(outlinePen,
                    origin.X,
                    origin.Y,
                    size,
                    size);
            }

            if (showCenters) {

                g.DrawLine(centerPen, 
                    origin.X + size / 2 - crossSize, 
                    origin.Y + size / 2, 
                    origin.X +size / 2 + crossSize, 
                    origin.Y+size / 2 );
                g.DrawLine(centerPen, 
                    origin.X + size / 2, 
                    origin.Y + size / 2 - crossSize, 
                    origin.X + size / 2, 
                    origin.Y + size / 2 + crossSize);
            }
        }
    }
}
