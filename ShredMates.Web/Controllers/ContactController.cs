using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ShredMates.Web.Controllers
{
    public class ContactController : Controller
    {
        public async Task<IActionResult> Index() => await Task.Run(() => View());
    }
}