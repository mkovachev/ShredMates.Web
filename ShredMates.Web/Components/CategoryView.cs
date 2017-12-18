using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
using System.Linq;

namespace ShredMates.Web.Components
{
    public class CategoryView: ViewComponent
    {
        private readonly ShredMatesDbContext db;

        public CategoryView(ShredMatesDbContext db) => this.db = db;

        public IViewComponentResult Invoke()
            => View(this.db.Categories
                        .Where(c => c.Name.Contains("Snowboard"))
                        .OrderBy(c => c.Name));

    }
}
