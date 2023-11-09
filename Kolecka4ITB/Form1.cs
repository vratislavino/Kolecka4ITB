using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kolecka4ITB
{
    public partial class Form1 : Form
    {

        ShapeFactory factory = new ShapeFactory();

        private bool holdingMouseButton = false;
        private Shape currentShape = null;

        public Form1() {
            InitializeComponent();
            rNum.Maximum = (decimal)Math.Sqrt(canvas1.Width*canvas1.Width + canvas1.Height*canvas1.Height);
            xNum.Maximum = canvas1.Width;
            yNum.Maximum = canvas1.Height;

            factory.Fill = checkBox1.Checked;
            factory.Color = colorDialog1.Color;

            canvas1.ShapeSelected += OnShapeSelected;

            panel1.Hide();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type shapeType = typeof(Shape);
            var types = assembly.GetTypes().Where(t => shapeType.IsAssignableFrom(t)).ToList();
            types.Remove(shapeType);

            foreach(var t in types) {
                comboBox1.Items.Add(t.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void OnShapeSelected(Shape shape) {
            if (shape == null) {
                panel1.Hide();
            } else {
                panel1.Show();
                //rNum.Value = (decimal) shape.Radius;
                xNum.Value = shape.Origin.X;
                yNum.Value = shape.Origin.Y;
                button2.BackColor = shape.Color;
                checkBox3.Checked = shape.Fill;
            }
        }

        private void canvas1_MouseDown(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                holdingMouseButton = true;
                string typ = "Kolecka4ITB." + comboBox1.SelectedItem;
                currentShape = factory.CreateShape(Type.GetType(typ), e.Location);
                canvas1.AddShape(currentShape);
            }
        }

        private void canvas1_MouseMove(object sender, MouseEventArgs e) {
            if(holdingMouseButton) {
                
                if(e.Location.X < 0 
                    || e.Location.X > canvas1.Width 
                    || e.Location.Y < 0 
                    || e.Location.Y > canvas1.Height
                ) {
                    canvas1.RemoveShape(currentShape);
                    currentShape = null;
                    holdingMouseButton = false;
                    canvas1.Refresh();
                    return;
                }

                currentShape.ChangeSize(currentShape.CalculateSize(e.Location));
                canvas1.Refresh();
            }

        }

        private void canvas1_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                currentShape = null;
                holdingMouseButton = false;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            var res = colorDialog1.ShowDialog();
            if(res == DialogResult.OK) {
                button1.BackColor = colorDialog1.Color;
                factory.Color = colorDialog1.Color;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            factory.Fill = checkBox1.Checked;
        }

        private void button3_Click(object sender, EventArgs e) {
            canvas1.RemoveShape();
        }

        private void rNum_ValueChanged(object sender, EventArgs e) {
            canvas1.SelectedShape?.ChangeSize((float)rNum.Value);
            canvas1.Refresh();
        }

        private void xNum_ValueChanged(object sender, EventArgs e) {
            canvas1.SelectedShape?.ChangeX((int) xNum.Value);
            canvas1.Refresh();
        }

        private void yNum_ValueChanged(object sender, EventArgs e) {
            canvas1.SelectedShape?.ChangeY((int) yNum.Value);
            canvas1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e) {
            colorDialog1.Color = button2.BackColor;
            if(colorDialog1.ShowDialog() == DialogResult.OK) {
                button2.BackColor = colorDialog1.Color;
                canvas1.SelectedShape?.ChangeColor(colorDialog1.Color);
                canvas1.Refresh();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            canvas1.SelectedShape?.ChangeFill(checkBox3.Checked);
            canvas1.Refresh();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            canvas1.DrawCenters = checkBox2.Checked;
            canvas1.Refresh();
        }

    }
}
