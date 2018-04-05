using Library.BusinessLogicLayer.Services;
using Library.ViewModelLayer.ViewModels;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class PublicationController : Controller
    {
        private AllPublicationsService _allPublicationsService;

        public PublicationController()
        {
            _allPublicationsService = new AllPublicationsService();
        }

        [HttpGet]
        public ActionResult AllPublications()
        {
            AllPublicationsViewModel allPublications = _allPublicationsService.GetAll();

            return View(allPublications);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            AllPublicationsViewModel allMagazines = _allPublicationsService.GetAll();

            return Json(allMagazines, JsonRequestBehavior.AllowGet);
        }
    }
}