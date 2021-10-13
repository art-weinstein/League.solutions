using Microsoft.AspNetCore.Mvc;

namespace Soccer.Controllers
{
    public class HomeController : Controller
    {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}