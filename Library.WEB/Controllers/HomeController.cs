using Library.BusinessLogicLayer.Services;
using Library.ViewModelLayer.ViewModels;

using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class HomeController : Controller
    {
        private AllPublicationsService _allPublicationsService;

        public HomeController()
        {
            _allPublicationsService = new AllPublicationsService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllPublications()
        {
            AllPublicationsViewModel allPublications = _allPublicationsService.GetAll();

            return View(allPublications);
        }
    }
}