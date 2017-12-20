using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
using System.Linq;

namespace ShredMates.Web.ViewComponents
{
    public class Kitesurf : ViewComponent
    {
        private readonly ShredMatesDbContext db;

        public Kitesurf(ShredMatesDbContext db) => this.db = db;

        public IViewComponentResult Invoke()
         => View(this.db.Categories
                    .Where(c => c.Name.Contains("Kite"))
                    .OrderBy(c => c.Name));

    }
}