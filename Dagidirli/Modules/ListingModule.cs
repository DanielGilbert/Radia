using System.Text;

namespace Dagidirli.Modules
{
    public class ListingModule : IListingModule
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ListingModule(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public IResult ProcessRequest(string arg)
        {
            var file = Path.Combine(this.webHostEnvironment.WebRootPath, arg);
            
            return Results.Text(File.ReadAllText(file, Encoding.UTF8), "text/css", Encoding.UTF8);
        }

        public IResult ProcessRequest()
        {
            return Results.Extensions.View("Index", new Todo(1, "Go back to work!", false));
        }
    }

    record Todo(int Id, string Name, bool IsComplete);
}
