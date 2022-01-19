using System;
using Microsoft.AspNetCore.Mvc;

namespace RadnetBd.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
