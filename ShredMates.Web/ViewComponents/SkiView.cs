using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
using System.Linq;

namespace ShredMates.Web.ViewComponents
{
    public class SkiView : ViewComponent
    {
        private readonly ShredMatesDbContext db;

        public SkiView(ShredMatesDbContext db) => this.db = db;

        public IViewComponentResult Invoke()
            => View(this.db.Categories
                        .Where(c => c.Name.Contains("Ski"))
                        .OrderBy(p => p.Name));
    }
}
