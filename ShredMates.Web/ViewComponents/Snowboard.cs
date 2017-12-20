using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
using System.Linq;

namespace ShredMates.Web.ViewComponents
{
    public class Snowboard: ViewComponent
    {
        private readonly ShredMatesDbContext db;

        public Snowboard(ShredMatesDbContext db) => this.db = db;

        public IViewComponentResult Invoke()
            => View(this.db.Categories
                        .Where(c => c.Name.Contains("Snowboard"))
                        .OrderBy(c => c.Name));

    }
}
