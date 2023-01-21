namespace Dagidirli.Modules
{
    public interface IDagidirliModule
    {
        IResult ProcessRequest();
    }

    public interface IDagidirliModule<T> : IDagidirliModule
    {
        IResult ProcessRequest(T arg);
    }
}
