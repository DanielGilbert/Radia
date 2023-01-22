namespace Dagidirli.Modules
{
    public class ListingModule : IListingModule
    {
        public IResult ProcessRequest(string arg)
        {
            return Results.Ok(arg);
        }

        public IResult ProcessRequest()
        {
            return Results.Extensions.View("Index", new Todo(1, "Go back to work!", false));
        }
    }

    record Todo(int Id, string Name, bool IsComplete);
}
