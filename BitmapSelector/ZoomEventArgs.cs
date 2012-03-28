using System;

namespace BitmapSelector
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
}
