using System;
using System.Drawing;

namespace BitmapSelector
{
    public class Layer : IDisposable
    {
        public Bitmap Image;
        public bool Visible = true;
        public float Opacity = 1.0f;
        public bool SaveContentsOnReset = false;

        public Layer(int width, int height, float opacity = 1.0f, bool saveContentsOnReset = false, bool visible = true)
        {
            Image = new Bitmap(width, height);
            Opacity = opacity;
            SaveContentsOnReset = saveContentsOnReset;
            Visible = visible;
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
