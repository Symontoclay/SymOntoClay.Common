using SymOntoClay.Common.Disposing;
using System.Threading;

namespace SymOntoClay.Common.Cancellation
{
    public interface ICancellationContext : ISymOntoClayDisposable, IObjectToString, IObjectToShortString, IObjectToBriefString, IObjectToDbgString
    {
        bool IsCancellationRequested { get; }
        CancellationToken Token { get; }
    }
}
