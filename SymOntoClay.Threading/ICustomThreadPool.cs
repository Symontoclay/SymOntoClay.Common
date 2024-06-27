using System;

namespace SymOntoClay.Threading
{
    public interface ICustomThreadPool : IDisposable
    {
        void Run(Action action);
    }
}
