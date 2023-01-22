namespace Radia.Modules
{
    public class StatisticsModule : IStatisticsModule
    {
        public IResult ProcessRequest()
        {
            return Results.Ok("This is a statistics result.");
        }
    }
}
