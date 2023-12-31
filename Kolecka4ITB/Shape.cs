﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolecka4ITB
{
    public abstract class Shape
    {
        protected Point origin;
        public Point Origin { get => origin; set => origin = value; }

        protected Color color;
        public Color Color { get => color; set => color = value; }

        protected bool fill;
        public bool Fill { get => fill; set => fill = value; }

        protected bool highlighted;
        protected static Pen outlinePen = new Pen(Color.Yellow, 4f);
        protected static Pen centerPen = new Pen(Color.Black, 3f);

        protected Brush colorBrush;
        protected Pen colorPen;

        protected int crossSize = 10;

        public Shape(Point center, Color color, bool fill) {
            this.origin = center;
            this.fill = fill;
            this.highlighted = false;

            ChangeColor(color);
        }

        public abstract void Draw(Graphics g, bool showCenters);

        public abstract bool ContainsPoint(Point location, out float currentDistance);

        public abstract void ChangeSize(float size);

        public void Select(bool active) {
            highlighted = active;
        }

        public void ChangeColor(Color newColor) {
            this.color = newColor;
            colorBrush = new SolidBrush(color);
            colorPen = new Pen(color, 4f);
        }

        // TODO: Change this :) 
        public abstract float CalculateSize(Point location);
            
        internal void ChangeX(int value) {
            this.origin.X = value;
        }

        internal void ChangeY(int value) {
            this.origin.Y = value;
        }

        internal void ChangeFill(bool filled) {
            this.fill = filled;
        }
    }
}
