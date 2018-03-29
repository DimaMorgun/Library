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
        public ActionResult CreateView()
        {
            PublicationHouseBooksViewModel publicationHouseBooks = _publicationHouseService.GetPublicationHouseBooks();

            return View(publicationHouseBooks);
        }

        [HttpPost]
        public ActionResult Create(PublicationHouseViewModel publicationHouse)
        {
            _publicationHouseService.Insert(publicationHouse);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetByIdView(int id)
        {
            PublicationHouseViewModel publicationHouse = _publicationHouseService.Get(id);

            return View(publicationHouse);
        }

        [HttpGet]
        public ActionResult UpdateView(int id)
        {
            PublicationHouseBooksViewModel publicationHouseBooks = _publicationHouseService.GetPublicationHouseBooks(id);

            return View(publicationHouseBooks);
        }

        [HttpPost]
        public ActionResult Update(PublicationHouseViewModel publicationHouse)
        {
            _publicationHouseService.Update(publicationHouse);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteView(int id)
        {
            PublicationHouseViewModel publicationHouse = _publicationHouseService.Get(id);

            return View(publicationHouse);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _publicationHouseService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SaveToJSON(int id)
        {
            _publicationHouseService.SaveToJSON(id);

            return RedirectToAction("Index");
        }
    }
}