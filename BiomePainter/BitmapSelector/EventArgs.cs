using System;

namespace BiomePainter.BitmapSelector
{
    public class ZoomEventArgs : EventArgs
    {
        public int NewMagnification;
        public int NewOffsetX;
        public int NewOffsetY;

        public ZoomEventArgs(int magnification, int offsetX, int offsetY)
            : base()
        {
            NewMagnification = magnification;
            NewOffsetX = offsetX;
            NewOffsetY = offsetY;
        }
    }

    public class BrushDiameterEventArgs : EventArgs
    {
        public int NewBrushDiameter;

        public BrushDiameterEventArgs(int newBrushDiameter)
            : base()
        {
            NewBrushDiameter = newBrushDiameter;
        }
    }

    public class CustomBrushClickEventArgs : EventArgs
    {
        public int MouseX;
        public int MouseY;

        public CustomBrushClickEventArgs(int mouseX, int mouseY)
        {
            MouseX = mouseX;
            MouseY = mouseY;
        }
    }
}
