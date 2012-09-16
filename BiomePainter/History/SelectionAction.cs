using System;
using System.Drawing;

namespace BiomePainter.History
{
    public class SelectionAction : IAction
    {
        public Bitmap Image = null;
        public String Description { get; set; }
        public IAction PreviousAction { get; set; }

        public SelectionAction(Bitmap image, String description)
        {
            Image = image;
            Description = description;
        }

        public void Dispose()
        {
            if (Image != null)
                Image.Dispose();
            Image = null;
        }
    }
}
