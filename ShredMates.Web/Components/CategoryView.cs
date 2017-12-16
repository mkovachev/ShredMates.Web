using Microsoft.AspNetCore.Mvc;
using ShredMates.Data;
using System.Linq;

namespace ShredMates.Web.Components
{
    public class CategoryView: ViewComponent
    {
        private readonly ShredMatesDbContext db;

        public CategoryView(ShredMatesDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var categories = this.db.Categories.OrderBy(c => c.Name);

            return View(categories);
        }

    }
}
