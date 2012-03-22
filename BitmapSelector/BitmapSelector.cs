using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BitmapSelector
{
    public partial class BitmapSelector : UserControl
    {
        private BufferedGraphicsContext backbufferContext;
        private BufferedGraphics backbufferGraphics;
        private Graphics g;
        public List<Layer> Layers;
        public String[,] ToolTips;
        public bool ShowToolTips = true;
        public Color SelectionColor = Color.Red;
        public BrushType Brush = BrushType.Round;
        public int BrushDiameter = 1;

        public int Magnification = 1;
        public int OffsetX = 0;
        public int OffsetY = 0;
        public int OffsetStep = 16;

        private bool mouse1Down = false;
        private bool mouse2Down = false;
        private Point mouseLast = new Point(-1, -1);

        public BitmapSelector()
        {
            InitializeComponent();
            //http://inchoatethoughts.com/custom-drawing-controls-in-c-manual-double-buffering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            backbufferContext = BufferedGraphicsManager.Current;

            Layers = new List<Layer>();
            Layers.Add(new Layer(this.Width, this.Height, 0.6f));

            ToolTips = new String[Width, Height];
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if(components != null)
                    components.Dispose();
                if (Layers != null)
                {
                    foreach (Layer l in Layers)
                        l.Dispose();
                }
                if (backbufferGraphics != null)
                    backbufferGraphics.Dispose();
                if (backbufferContext != null)
                    backbufferContext.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecreateBuffers();
            ToolTips = new String[Width, Height];
            foreach (Layer l in Layers)
                l.Resize(Width, Height);
            Redraw();
        }

        private void RecreateBuffers()
        {
            backbufferContext.MaximumBuffer = new Size(Math.Max(this.Width, 1), Math.Max(this.Height, 1));

            if (backbufferGraphics != null)
                backbufferGraphics.Dispose();

            backbufferGraphics = backbufferContext.Allocate(this.CreateGraphics(), new Rectangle(0, 0, Math.Max(this.Width, 1), Math.Max(this.Height, 1)));

            g = backbufferGraphics.Graphics;

            this.Invalidate();
        }

        public void Redraw()
        {
            if (g == null)
                return;

            g.Clear(Color.White);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            ColorMatrix cm = new ColorMatrix();
            ImageAttributes ia = new ImageAttributes();
            Rectangle dest = new Rectangle(0, 0, this.Width, this.Height);
            Rectangle source = new Rectangle(OffsetX, OffsetY, this.Width / Magnification, this.Height / Magnification);
            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                if (!Layers[i].Visable)
                    continue;
                cm.Matrix33 = Layers[i].Opacity;
                ia.SetColorMatrix(cm);
                g.DrawImage(Layers[i].Image, dest, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, ia);
            }

            this.Refresh();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (backbufferGraphics != null)
                backbufferGraphics.Render(e.Graphics);
        }


        private void BitmapSelector_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mouse1Down = false;
            else if (e.Button == MouseButtons.Right)
                mouse2Down = false;
        }

        private void BitmapSelector_MouseCaptureChanged(object sender, EventArgs e)
        {
            mouse1Down = false;
            mouse2Down = false;
        }

        private void BitmapSelector_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = Translate(e.Location);
            if (e.Button == MouseButtons.Left)
                mouse1Down = true;
            else if (e.Button == MouseButtons.Right)
                mouse2Down = true;

            if (BrushDiameter > 1)
            {
                using (Graphics g = Graphics.FromImage(Layers[0].Image))
                {
                    g.CompositingMode = CompositingMode.SourceCopy;
                    SolidBrush b = new SolidBrush(mouse1Down ? SelectionColor : Color.Transparent);
                    if(Brush == BrushType.Round)
                        g.FillEllipse(b, p.X - BrushDiameter / 2, p.Y - BrushDiameter / 2, BrushDiameter, BrushDiameter);
                    else
                        g.FillRectangle(b, p.X - BrushDiameter / 2, p.Y - BrushDiameter / 2, BrushDiameter, BrushDiameter);
                    b.Dispose();
                }
            }
            else if (p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height)
                Layers[0].Image.SetPixel(p.X, p.Y, SelectionColor);

            Redraw();
        }

        private void BitmapSelector_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = Translate(e.Location);

            if (p == mouseLast)
                return;
            if (p.X < 0 || p.X >= Width || p.Y < 0 || p.Y >= Height)
                return;
            if (ShowToolTips && ToolTips[p.X, p.Y] != null && ToolTips[p.X, p.Y].Length > 0)
                toolTip.Show(ToolTips[p.X, p.Y], this, new Point(e.X, e.Y));
            else
                toolTip.Hide(this);

            if (mouse1Down || mouse2Down)
            {
                using (Graphics g = Graphics.FromImage(Layers[0].Image))
                {
                    g.CompositingMode = CompositingMode.SourceCopy;
                    Pen pen = new Pen(mouse1Down ? SelectionColor : Color.Transparent, BrushDiameter);
                    SolidBrush b = new SolidBrush(mouse1Down ? SelectionColor : Color.Transparent);
                    
                    if (Brush == BrushType.Round)
                    {
                        g.DrawLine(pen, mouseLast, p);
                        g.FillEllipse(b, p.X - BrushDiameter / 2, p.Y - BrushDiameter / 2, BrushDiameter, BrushDiameter);
                    }
                    else //Square
                    {
                        if (mouseLast.X == p.X || mouseLast.Y == p.Y)
                        {
                            g.DrawLine(pen, mouseLast, p);
                        }
                        else
                        {
                            Point[] parallelogram = new Point[4];
                            double m = ((double)(p.Y - mouseLast.Y)) / ((double)(p.X - mouseLast.X));
                            if (m < 0.0)
                            {
                                parallelogram[0] = new Point(p.X - (int)(BrushDiameter / 2.0), p.Y - (int)(BrushDiameter / 2.0));
                                parallelogram[1] = new Point(p.X + (int)(BrushDiameter / 2.0), p.Y + (int)(BrushDiameter / 2.0));
                                parallelogram[2] = new Point(mouseLast.X + (int)(BrushDiameter / 2.0), mouseLast.Y + (int)(BrushDiameter / 2.0));
                                parallelogram[3] = new Point(mouseLast.X - (int)(BrushDiameter / 2.0), mouseLast.Y - (int)(BrushDiameter / 2.0));
                            }
                            else
                            {
                                parallelogram[0] = new Point(p.X + (int)(BrushDiameter / 2.0), p.Y - (int)(BrushDiameter / 2.0));
                                parallelogram[1] = new Point(p.X - (int)(BrushDiameter / 2.0), p.Y + (int)(BrushDiameter / 2.0));
                                parallelogram[2] = new Point(mouseLast.X - (int)(BrushDiameter / 2.0), mouseLast.Y + (int)(BrushDiameter / 2.0));
                                parallelogram[3] = new Point(mouseLast.X + (int)(BrushDiameter / 2.0), mouseLast.Y - (int)(BrushDiameter / 2.0));
                            }
                            g.FillPolygon(b, parallelogram);
                        }
                        
                        g.FillRectangle(b, p.X - BrushDiameter / 2, p.Y - BrushDiameter / 2, BrushDiameter, BrushDiameter);
                        
                    }

                    pen.Dispose();
                    b.Dispose();
                }
                Redraw();
            }

            mouseLast = p;
        }

        private void BitmapSelector_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(this);
        }

        public void Reset()
        {
            ToolTips = new String[Width, Height];
            Magnification = 1;
            OffsetX = 0;
            OffsetY = 0;
            foreach (Layer l in Layers)
            {
                using (Graphics g = Graphics.FromImage(l.Image))
                {
                    g.Clear(Color.Transparent);
                }
            }
            Redraw();
        }

        public void SelectAll()
        {
            using (Graphics g = Graphics.FromImage(Layers[0].Image))
            {
                g.Clear(SelectionColor);
            }
        }

        public void SelectNone()
        {
            using (Graphics g = Graphics.FromImage(Layers[0].Image))
            {
                g.Clear(Color.Transparent);
            }
        }

        public void InvertSelection()
        {
            for (int x = 0; x < Layers[0].Image.Width; x++)
            {
                for (int y = 0; y < Layers[0].Image.Height; y++)
                {
                    if (Layers[0].Image.GetPixel(x, y).ToArgb() == SelectionColor.ToArgb())
                        Layers[0].Image.SetPixel(x, y, Color.Transparent);
                    else
                        Layers[0].Image.SetPixel(x, y, SelectionColor);
                }
            }
        }

        public void Zoom(int magnififcation, int offsetX, int offsetY)
        {
            Magnification = magnififcation;
            OffsetX = offsetX;
            OffsetY = offsetY;

            //prevent scrolling past the end of the image
            if (Width - OffsetX < (Width / Magnification))
                OffsetX = Width - (Width / Magnification);
            if(Height - OffsetY < (Height / Magnification))
                OffsetY = Height - (Height / Magnification);
        }

        private Point Translate(Point e)
        {
            return new Point(OffsetX + (e.X / Magnification), OffsetY + (e.Y / Magnification));
        }
    }
}
