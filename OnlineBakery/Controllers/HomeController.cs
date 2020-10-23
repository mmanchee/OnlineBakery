using Microsoft.AspNetCore.Mvc;

namespace OnlineBakery.Controllers
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