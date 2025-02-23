using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPm_Gackan1.Models;
using System.Diagnostics;

namespace RPm_Gackan1.Controllers
{
    public class HomeController : Controller
    {
        public FlorenceeContext db;

        public HomeController(FlorenceeContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        public IActionResult Percent()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
