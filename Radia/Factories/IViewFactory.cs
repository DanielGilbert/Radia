using Radia.ViewModels;

namespace Radia
{
    public interface IViewFactory
    {
        public string Create(IViewModel viewModel);
    }
}