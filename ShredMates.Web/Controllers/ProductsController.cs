using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShredMates.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }
    }
}