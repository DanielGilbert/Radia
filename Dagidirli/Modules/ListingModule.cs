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
            return Results.Ok("This is just the index page");
        }
    }
}
