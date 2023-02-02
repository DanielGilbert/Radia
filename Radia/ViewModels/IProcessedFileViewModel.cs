using Radia.Models;

namespace Radia.ViewModels
{
    public interface IProcessedFileViewModel
    {
        string Content { get; }
        public IList<IRadiaAncestorInfo> Ancestors { get; }
        IList<IRadiaAncestorInfo> AncestorsWithoutLastElement { get; }
        public IRadiaAncestorInfo? LastAncestor { get; }

    }
}
