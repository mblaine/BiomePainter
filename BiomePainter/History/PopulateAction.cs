using System;

namespace BiomePainter.History
{
    public class PopulateAction : IAction
    {
        public byte[,] PopulatedFlags = new byte[32, 32];

        void IDisposable.Dispose()
        {
        }
    }
}
