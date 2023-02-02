namespace Radia.Modules
{
    public interface IRadiaModule
    {
        IResult ProcessRequest();
    }

    public interface IRadiaModule<T> : IRadiaModule
    {
        IResult ProcessRequest(T arg);
    }
}
