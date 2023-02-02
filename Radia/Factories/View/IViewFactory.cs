using Radia.ViewModels;

namespace Radia.Factories.View
{
    public interface IViewFactory
    {
        public string Create(IViewModel viewModel);
    }
}