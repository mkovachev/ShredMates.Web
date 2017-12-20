using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
using System.Linq;

namespace ShredMates.Web.ViewComponents
{
    public class Wakeboard : ViewComponent
    {
        private readonly ShredMatesDbContext db;

        public Wakeboard(ShredMatesDbContext db) => this.db = db;

        public IViewComponentResult Invoke()
         => View(this.db.Categories
                    .Where(c => c.Name.Contains("Wakeboard"))
                    .OrderBy(c => c.Name));

    }
}
