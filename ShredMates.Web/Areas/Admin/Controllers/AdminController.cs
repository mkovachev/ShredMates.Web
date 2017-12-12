using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShredMates.Web.Infrastructure;

namespace ShredMates.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdminRole)]
    public abstract class AdminController : Controller
    {

    }
}