using System;
using System.Drawing;

namespace BitmapSelector
{
    public class Layer : IDisposable
    {
        public Bitmap Image;
        public bool Visable = true;
        public float Opacity = 1.0f;

        public Layer(int width, int height, float opacity = 1.0f)
        {
            Image = new Bitmap(width, height);
            Opacity = opacity;
        }

        public void Resize(int width, int height)
        {
            if (Image != null)
                Image.Dispose();
            Image = new Bitmap(width, height);
        }

        ~Layer()
        {
            if (Image != null)
            {
                Image.Dispose();
                Image = null;
            }
        }

        public void Dispose()
        {
            if (Image != null)
            {
                Image.Dispose();
                Image = null;
            }
        }
    }
}
