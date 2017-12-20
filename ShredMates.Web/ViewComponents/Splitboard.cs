using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
using System.Linq;

namespace ShredMates.Web.ViewComponents
{
    public class Splitboard : ViewComponent
    {
        private readonly ShredMatesDbContext db;

        public Splitboard(ShredMatesDbContext db) => this.db = db;

        public IViewComponentResult Invoke()
         => View(this.db.Categories
                    .Where(c => c.Name.Contains("Splitboard"))
                    .OrderBy(c => c.Name));

    }
}
