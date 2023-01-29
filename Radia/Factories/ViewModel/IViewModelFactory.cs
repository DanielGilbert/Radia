using Radia.ViewModels;

namespace Radia.Factories.ViewModel
{
    public interface IViewModelFactory
    {
        IViewModel Create(ViewModelFactoryArgs args);
    }
}
