using Radia.ViewModels;

namespace Radia.Factories
{
    public interface IViewModelFactory
    {
        IViewModel Create(ViewModelFactoryArgs args);
    }
}
