using System;

namespace BiomePainter.History
{
    public interface IAction : IDisposable
    {
        String Description { get; set; }
        IAction PreviousAction { get; set; }
    }
}
