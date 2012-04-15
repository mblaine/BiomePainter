using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BitmapSelector
{
    public partial class BitmapSelector : UserControl
    {
        public delegate void ZoomEventHandler(Object sender, ZoomEventArgs e);
        public event ZoomEventHandler ZoomEvent;

        public delegate void BrushDiameterEventHandler(Object sender, BrushDiameterEventArgs e);
        public event BrushDiameterEventHandler BrushDiameterChanged;

        public event EventHandler SelectionChanged;

        private BufferedGraphicsContext backbufferContext;
        private BufferedGraphics backbufferGraphics;
        private Graphics g;
        public List<Layer> Layers;
        public String[,] ToolTips;
        public bool ShowToolTips = true;
        public Color SelectionColor = Color.Red;
        public BrushType Brush = BrushType.Round;
        public int BrushDiameter = 1;
        public int BrushDiameterMax = 50;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Rectangle SelectionBounds { get; set; }

        public int Magnification = 1;
        public int OffsetX = 0;
        public int OffsetY = 0;
        public int OffsetStep = 16;

        public int MagnificationMax = 10;

        private Point mouseDownLast = new Point(-1, -1);
        private bool mouse1Down = false;
        private bool mouse2Down = false;
        private Point mouseLast = new Point(-1, -1);
        private bool cursorVisible = true;

        public readonly int SelectionLayerIndex = 0;
        public readonly int BrushLayerIndex = 1;

        public BitmapSelector()
        {
            InitializeComponent();
            //http://inchoatethoughts.com/custom-drawing-controls-in-c-manual-double-buffering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            backbufferContext = BufferedGraphicsManager.Current;

            Layers = new List<Layer>();
            Layers.Add(new Layer(this.Width, this.Height, 0.6f, true));
            Layers.Add(new Layer(this.Width, this.Height, 0.7f));

            ToolTips = new String[Width, Height];

            scrollHorizontal.Maximum = Width;
            scrollVertical.Maximum = Height;
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

        //returns the index of the newly added layer
        public int AddLayer(Layer l)
        {
            Layers.Add(l);
            return Layers.Count - 1;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecreateBuffers();
            ToolTips = new String[Width, Height];
            foreach (Layer l in Layers)
                l.Resize(Width, Height);

            scrollHorizontal.Maximum = Width;
            scrollVertical.Maximum = Height;
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
            Rectangle source = new Rectangle(OffsetX, OffsetY, (int)Math.Round(((double)this.Width) / ((double)Magnification)), (int)Math.Round(((double)this.Height) / ((double)Magnification)));
            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                if (!Layers[i].Visible)
                    continue;
                cm.Matrix33 = Layers[i].Opacity;
                ia.SetColorMatrix(cm);
                g.DrawImage(Layers[i].Image, dest, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, ia);
            }

            if (scrollHorizontal.Visible && scrollVertical.Visible)
            {
                //draw over the corner between the scrollbars
                dest = new Rectangle(scrollVertical.Location.X, scrollHorizontal.Location.Y, scrollVertical.Width, scrollHorizontal.Height);
                Brush b = new SolidBrush(SystemColors.Control);
                g.FillRectangle(b, dest);
                b.Dispose();
            }

            this.Refresh();
        }

        private void RedrawBrushLayer(Point p, bool justClear = false)
        {
            using (Graphics g = Graphics.FromImage(Layers[BrushLayerIndex].Image))
            {
                g.Clear(Color.Transparent);
                if (!SelectionBounds.IsEmpty)
                    g.SetClip(SelectionBounds);

                if (!justClear)
                {
                    if (SelectionBounds.IsEmpty || SelectionBounds.Contains(p))
                        Layers[BrushLayerIndex].Image.SetPixel(p.X, p.Y, Color.Black);
                    Brush b = new SolidBrush(Color.Black);
                    if (Brush == BrushType.Rectangle)
                    {
                        if ((mouse1Down || mouse2Down) && mouseDownLast.X != -1 && mouseDownLast.Y != -1)
                        {
                            g.FillRectangle(b, Rectangle.FromLTRB(Math.Min(mouseDownLast.X, p.X), Math.Min(mouseDownLast.Y, p.Y), Math.Max(mouseDownLast.X, p.X), Math.Max(mouseDownLast.Y, p.Y)));
                        }
                    }
                    else if (Brush == BrushType.Round)
                    {
                        g.FillEllipse(b, p.X - BrushDiameter / 2, p.Y - BrushDiameter / 2, BrushDiameter, BrushDiameter);
                    }
                    else if (Brush == BrushType.Square)
                    {
                        g.FillRectangle(b, p.X - BrushDiameter / 2, p.Y - BrushDiameter / 2, BrushDiameter, BrushDiameter);
                    }
                    b.Dispose();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (backbufferGraphics != null)
                backbufferGraphics.Render(e.Graphics);
            base.OnPaint(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch(keyData)
            {
                case Keys.Left:
                    PanLeft();
                    return true;
                case Keys.Right:
                    PanRight();
                    return true;
                case Keys.Up:
                    PanUp();
                    return true;
                case Keys.Down:
                    PanDown();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PanLeft()
        {
            scrollHorizontal.Value = Math.Max(scrollHorizontal.Value - 5, 0);
            Zoom(Magnification, scrollHorizontal.Value, OffsetY);
        }

        private void PanRight()
        {
            scrollHorizontal.Value = Math.Min(scrollHorizontal.Value + 5, scrollHorizontal.Maximum);
            Zoom(Magnification, scrollHorizontal.Value, OffsetY);
        }

        private void PanUp()
        {
            scrollVertical.Value = Math.Max(scrollVertical.Value - 5, 0);
            Zoom(Magnification, OffsetX, scrollVertical.Value);
        }

        private void PanDown()
        {
            scrollVertical.Value = Math.Min(scrollVertical.Value + 5, scrollVertical.Maximum);
            Zoom(Magnification, OffsetX, scrollVertical.Value);
        }

        private void BitmapSelector_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mouse1Down = false;
            else if (e.Button == MouseButtons.Right)
                mouse2Down = false;

            if (Brush == BrushType.Rectangle)
            {
                if (mouseDownLast.X != -1 && mouseDownLast.Y != -1)
                {
                    Point p = Translate(e.Location);
                    using (Graphics g = Graphics.FromImage(Layers[SelectionLayerIndex].Image))
                    {
                        if (!SelectionBounds.IsEmpty)
                            g.SetClip(SelectionBounds);
                        g.CompositingMode = CompositingMode.SourceCopy;
                        Brush b = new SolidBrush(e.Button == MouseButtons.Left ? SelectionColor : Color.Transparent);
                        g.FillRectangle(b, Rectangle.FromLTRB(Math.Min(mouseDownLast.X, p.X), Math.Min(mouseDownLast.Y, p.Y), Math.Max(mouseDownLast.X, p.X), Math.Max(mouseDownLast.Y, p.Y)));
                        b.Dispose();
                    }
                    RedrawBrushLayer(new Point(), true);
                    Redraw();
                }
            }

            mouseDownLast.X = -1;
            mouseDownLast.Y = -1;
            OnSelectionChanged();
            if(!cursorVisible)
            {
                Cursor.Show();
                cursorVisible = true;
            }
        }

        private void BitmapSelector_MouseCaptureChanged(object sender, EventArgs e)
        {
            if(mouse1Down || mouse2Down)
                OnSelectionChanged();
            mouse1Down = false;
            mouse2Down = false;
            mouseDownLast.X = -1;
            mouseDownLast.Y = -1;
            if (!cursorVisible)
            {
                Cursor.Show();
                cursorVisible = true;
            }
        }

        private void BitmapSelector_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = Translate(e.Location);
            if (e.Button == MouseButtons.Left)
                mouse1Down = true;
            else if (e.Button == MouseButtons.Right)
                mouse2Down = true;
            if (cursorVisible)
            {
                Cursor.Hide();
                cursorVisible = false;
            }
            if (Brush == BrushType.Rectangle)
            {
                mouseDownLast = p;
            }
            else if (Brush == BrushType.Fill)
            {
            }
            else if (BrushDiameter > 1)
            {
                using (Graphics g = Graphics.FromImage(Layers[SelectionLayerIndex].Image))
                {
                    if (!SelectionBounds.IsEmpty)
                        g.SetClip(SelectionBounds);
                    g.CompositingMode = CompositingMode.SourceCopy;
                    SolidBrush b = new SolidBrush(mouse1Down ? SelectionColor : Color.Transparent);
                    if (Brush == BrushType.Round)
                        g.FillEllipse(b, p.X - BrushDiameter / 2, p.Y - BrushDiameter / 2, BrushDiameter, BrushDiameter);
                    else
                        g.FillRectangle(b, p.X - BrushDiameter / 2, p.Y - BrushDiameter / 2, BrushDiameter, BrushDiameter);
                    b.Dispose();
                }
            }
            else if (p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height && (SelectionBounds.IsEmpty || SelectionBounds.Contains(p)))
                Layers[SelectionLayerIndex].Image.SetPixel(p.X, p.Y, mouse1Down ? SelectionColor : Color.Transparent);

            Redraw();
        }

        private void BitmapSelector_MouseMove(object sender, MouseEventArgs e)
        {
            //so we can capture the mouse wheel event
            if (!Focused)
                this.Focus();

            Point p = Translate(e.Location);

            if (p == mouseLast)
                return;
            if (p.X < 0 || p.X >= Width || p.Y < 0 || p.Y >= Height)
                return;
            if (ShowToolTips && ToolTips[p.X, p.Y] != null && ToolTips[p.X, p.Y].Length > 0)
                toolTip.Show(ToolTips[p.X, p.Y], this, new Point(e.X + 1, e.Y + 1));
            else
                toolTip.Hide(this);

            bool needToRedraw = false;
            if ((mouse1Down || mouse2Down) && (Brush == BrushType.Round || Brush == BrushType.Square))
            {
                using (Graphics g = Graphics.FromImage(Layers[SelectionLayerIndex].Image))
                {
                    if (!SelectionBounds.IsEmpty)
                        g.SetClip(SelectionBounds);
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
                        if (BrushDiameter == 1)
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

                needToRedraw = true;
            }

            if (Layers[BrushLayerIndex].Visible)
            {
                RedrawBrushLayer(p);
                needToRedraw = true;
            }

            if (needToRedraw)
                Redraw();
            
            mouseLast = p;
        }

        private void BitmapSelector_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(this);

            if (Layers[BrushLayerIndex].Visible)
            {
                RedrawBrushLayer(new Point(), true);
                Redraw();
            }
        }

        private void BitmapSelector_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                BrushDiameter = BrushDiameter + e.Delta / 120;
                if (BrushDiameter > BrushDiameterMax)
                    BrushDiameter = BrushDiameterMax;
                else if (BrushDiameter < 1)
                    BrushDiameter = 1;

                if (Layers[BrushLayerIndex].Visible && this.ClientRectangle.Contains(e.Location))
                {
                    RedrawBrushLayer(Translate(e.Location));
                    Redraw();
                }
                OnBrushDiameterChanged(new BrushDiameterEventArgs(BrushDiameter));
            }
            else if (Control.ModifierKeys == Keys.Alt)
            {
                if (e.Delta > 0)
                {
                    for (int i = 0; i < e.Delta / 120; i++)
                        PanUp();
                }
                else
                {
                    for (int i = 0; i < -e.Delta / 120; i++)
                        PanDown();
                }
            }
            else if (Control.ModifierKeys == Keys.Shift)
            {
                if (e.Delta > 0)
                {
                    for (int i = 0; i < e.Delta / 120; i++)
                        PanLeft();
                }
                else
                {
                    for (int i = 0; i < -e.Delta / 120; i++)
                        PanRight();
                }
            }
            else
            {
                Point p = new Point(e.Location.X, e.Location.Y);
                if (p.X < 0)
                    p.X = 0;
                if (p.X > Width)
                    p.X = Width;
                if (p.Y < 0)
                    p.Y = 0;
                if (p.Y > Height)
                    p.Y = Height;

                p = Translate(p);

                double percentOffsetX = ((double)(p.X - OffsetX)) / (((double)Width) / (double)Magnification);
                double percentOffsetY = ((double)(p.Y - OffsetY)) / (((double)Width) / (double)Magnification);

                int newMagnification = Magnification + e.Delta / 120;
                if (newMagnification <= 0)
                    newMagnification = 1;
                else if (newMagnification > MagnificationMax)
                    newMagnification = MagnificationMax;

                Zoom(newMagnification, OffsetX, OffsetY, false);
                Zoom(Magnification, p.X - (int)Math.Round(percentOffsetX * (((double)Width) / (double)Magnification)), p.Y - (int)Math.Round(percentOffsetY * (((double)Width) / (double)Magnification)));
            }
        }

        public void Reset()
        {
            ToolTips = new String[Width, Height];
            Magnification = 1;
            OffsetX = 0;
            OffsetY = 0;
            OnZoom(new ZoomEventArgs(1, 0, 0));
            scrollHorizontal.Visible = false;
            scrollVertical.Visible = false;
            scrollHorizontal.Value = 0;
            scrollVertical.Value = 0;
            foreach (Layer l in Layers)
            {
                if (!l.SaveContentsOnReset)
                {
                    using (Graphics g = Graphics.FromImage(l.Image))
                    {
                        g.Clear(Color.Transparent);
                    }
                }
            }
            Redraw();

            if (!Layers[SelectionLayerIndex].SaveContentsOnReset)
                OnSelectionChanged();
        }

        public void SelectAll()
        {
            using (Graphics g = Graphics.FromImage(Layers[SelectionLayerIndex].Image))
            {
                if (!SelectionBounds.IsEmpty)
                    g.SetClip(SelectionBounds);
                g.Clear(SelectionColor);
            }

            OnSelectionChanged();
        }

        public void SelectNone()
        {
            using (Graphics g = Graphics.FromImage(Layers[SelectionLayerIndex].Image))
            {
                if (!SelectionBounds.IsEmpty)
                    g.SetClip(SelectionBounds);
                g.Clear(Color.Transparent);
            }

            OnSelectionChanged();
        }

        public void InvertSelection()
        {
            int xstart = 0;
            int xend = Layers[SelectionLayerIndex].Image.Width;
            int ystart = 0;
            int yend = Layers[SelectionLayerIndex].Image.Height;

            if (!SelectionBounds.IsEmpty)
            {
                xstart = SelectionBounds.X;
                xend = SelectionBounds.X + SelectionBounds.Width;
                ystart = SelectionBounds.Y;
                yend = SelectionBounds.Y + SelectionBounds.Height;
            }

            for (int x = xstart; x < xend ; x++)
            {
                for (int y = ystart; y < yend; y++)
                {
                    if (Layers[SelectionLayerIndex].Image.GetPixel(x, y).ToArgb() == SelectionColor.ToArgb())
                        Layers[SelectionLayerIndex].Image.SetPixel(x, y, Color.Transparent);
                    else
                        Layers[SelectionLayerIndex].Image.SetPixel(x, y, SelectionColor);
                }
            }

            OnSelectionChanged();
        }

        private void scrollVertical_Scroll(object sender, ScrollEventArgs e)
        {
            Zoom(Magnification, OffsetX, e.NewValue);
        }

        private void scrollHorizontal_Scroll(object sender, ScrollEventArgs e)
        {
            Zoom(Magnification, e.NewValue, OffsetY);
        }

        public void Zoom(int magnififcation, int offsetX, int offsetY, bool redraw = true)
        {
            Magnification = magnififcation;
            OffsetX = offsetX;
            OffsetY = offsetY;

            int scaledWidth = (int)Math.Round(((double)Width) / ((double)Magnification));
            int scaledHeight = (int)Math.Round(((double)Height) / ((double)Magnification));

            int scrollWidth = Magnification <= 1 ? 0 : (int)Math.Round(((double)scrollVertical.Width) / ((double)Magnification)); ;
            int scrollHeight = Magnification <= 1 ? 0 : (int)Math.Round(((double)scrollHorizontal.Height) / ((double)Magnification));

            //prevent scrolling past the end of the image
            if (OffsetX < 0)
                OffsetX = 0;
            if (Width - OffsetX < scaledWidth - scrollWidth)
                OffsetX = Width - scaledWidth + scrollWidth;
            if (OffsetY < 0)
                OffsetY = 0;
            if (Height - OffsetY < scaledHeight - scrollHeight)
                OffsetY = Height - scaledHeight + scrollHeight;

            if (Magnification <= 1)
            {
                scrollHorizontal.Visible = false;
                scrollVertical.Visible = false;
            }
            else
            {
                scrollHorizontal.Visible = true;
                scrollHorizontal.Value = OffsetX;
                scrollHorizontal.Maximum = Width - scaledWidth + scrollWidth + scrollHorizontal.LargeChange - 1;
                scrollVertical.Visible = true;
                scrollVertical.Value = OffsetY;
                scrollVertical.Maximum = Height - scaledHeight + scrollHeight + scrollVertical.LargeChange - 1;
            }

            if(redraw)
                Redraw();
            OnZoom(new ZoomEventArgs(Magnification, OffsetX, OffsetY));
        }

        private Point Translate(Point e)
        {
            Matrix m = new Matrix(1, 0, 0, 1, 0, 0);
            m.Scale(Magnification, Magnification);
            m.Invert();
            Point[] p = new Point[] { e };
            m.TransformPoints(p);
            p[0].Offset(OffsetX, OffsetY);
            return p[0];
        }

        protected virtual void OnZoom(ZoomEventArgs e)
        {
            if (ZoomEvent != null)
                ZoomEvent(this, e);
        }

        protected virtual void OnBrushDiameterChanged(BrushDiameterEventArgs e)
        {
            if (BrushDiameterChanged != null)
                BrushDiameterChanged(this, e);
        }

        protected virtual void OnSelectionChanged()
        {
            if (SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
        }
    }
}
