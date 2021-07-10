namespace RussianBathHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Bannik()
        {
            return View();
        }
        public IActionResult Photos()
        {
            return View();
        }
    }
}
