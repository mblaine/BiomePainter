using System.Drawing;

namespace BiomePainter.History
{
    public class SelectionAction : IAction
    {
        public Bitmap Image = null;

        public SelectionAction(Bitmap image)
        {
            Image = image;
        }

        ~SelectionAction()
        {
            if (Image != null)
                Image.Dispose();
            Image = null;
        }

        public void Dispose()
        {
            if (Image != null)
                Image.Dispose();
            Image = null;
        }
    }
}
