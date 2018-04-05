using Library.BusinessLogicLayer.Services;
using Library.ViewModelLayer.ViewModels;

using System.Collections.Generic;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class PublicationHouseController : Controller
    {
        private PublicationHouseService _publicationHouseService;

        public PublicationHouseController()
        {
            _publicationHouseService = new PublicationHouseService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<PublicationHouseViewModel> allPublicationHouses = _publicationHouseService.GetAll();

            return View(allPublicationHouses);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            List<PublicationHouseViewModel> allBooks = _publicationHouseService.GetAll();

            return Json(allBooks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            PublicationHouseBooksViewModel publicationHouseBooks = _publicationHouseService.GetPublicationHouseBooks();

            return View(publicationHouseBooks);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(PublicationHouseViewModel publicationHouse)
        {
            _publicationHouseService.Insert(publicationHouse);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            PublicationHouseViewModel publicationHouse = _publicationHouseService.Get(id);

            return View(publicationHouse);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Update(int id)
        {
            PublicationHouseBooksViewModel publicationHouseBooks = _publicationHouseService.GetPublicationHouseBooks(id);

            return View(publicationHouseBooks);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Update(PublicationHouseViewModel publicationHouse)
        {
            _publicationHouseService.Update(publicationHouse);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            _publicationHouseService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult SaveToJSON(int id)
        {
            _publicationHouseService.SaveToJSON(id);

            return RedirectToAction("Index");
        }
    }
}