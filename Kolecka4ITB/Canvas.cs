using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kolecka4ITB
{
    public partial class Canvas : UserControl
    {
        public event Action<Shape> ShapeSelected;

        private List<Shape> shapes = new List<Shape>();
        Shape selectedShape = null;

        private bool drawCenters = false;
        public bool DrawCenters { 
            get { return drawCenters; } 
            set { drawCenters = value; }
        }

        public Shape SelectedShape => selectedShape;

        public Canvas() {
            InitializeComponent();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e) {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (Shape s in shapes) {
                s.Draw(e.Graphics, drawCenters);
            }
        }

        public void AddShape(Shape s) {
            shapes.Add(s);
            Refresh();
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e) {

            if (e.Button == MouseButtons.Right) {
                float closestCenter = float.MaxValue;
                Shape closestShape = null;
                float currentDistance;

                foreach (Shape s in shapes) {

                    if (s.ContainsPoint(e.Location, out currentDistance)) {
                        if (currentDistance < closestCenter) {
                            closestShape = s;
                            closestCenter = currentDistance;
                        }
                    }
                }
                SelectShape(closestShape);
            }
        }

        private void SelectShape(Shape newShape) {
            if (selectedShape != null)
                selectedShape.Select(false);

            selectedShape = newShape;
            if (selectedShape != null) {
                selectedShape.Select(true);
                ShapeSelected?.Invoke(selectedShape);
            }
            Refresh();
        }

        internal void RemoveShape(Shape currentShape = null) {
            if (currentShape == null) {
                shapes.Remove(selectedShape);
                selectedShape = null;
                ShapeSelected?.Invoke(null);
            } else {
                shapes.Remove(currentShape);
            }
            Refresh();
        }
        /*
        internal void EditCircle(decimal r, decimal x, decimal y, Color color, bool fill) {
            selectedShape.Edit((float) r, (int) x, (int) y, color, fill);
            Refresh();
        }
        */
    }
}
