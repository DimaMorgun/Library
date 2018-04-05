using Library.ViewModelLayer.ViewModels;

using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class HomeController : Controller
    { 
        public ActionResult Index()
        {
            return View();
        }
    }
}