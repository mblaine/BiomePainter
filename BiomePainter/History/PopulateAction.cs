using System;

namespace BiomePainter.History
{
    public class PopulateAction : IAction
    {
        public byte[,] PopulatedFlags = new byte[32, 32];
        public String Description { get; set; }
        public IAction PreviousAction { get; set; }

        public PopulateAction(String description)
        {
            Description = description;
        }

        void IDisposable.Dispose()
        {
        }
    }
}
