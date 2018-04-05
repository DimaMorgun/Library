using Library.BusinessLogicLayer.Services;
using Library.ViewModelLayer.ViewModels;

using System.Collections.Generic;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class MagazineController : Controller
    {
        private MagazineService _magazineService;

        public MagazineController()
        {
            _magazineService = new MagazineService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<MagazineViewModel> allMagazines = _magazineService.GetAll();

            return View(allMagazines);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            List<MagazineViewModel> allMagazines = _magazineService.GetAll();

            return Json(allMagazines, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(MagazineViewModel magazine)
        {
            _magazineService.Insert(magazine);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            MagazineViewModel magazine = _magazineService.Get(id);

            return View(magazine);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Update(int id)
        {
            MagazineViewModel magazine = _magazineService.Get(id);

            return View(magazine);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Update(MagazineViewModel magazine)
        {
            _magazineService.Update(magazine);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            _magazineService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult SaveToJSON(int id)
        {
            _magazineService.SaveToJSON(id);

            return RedirectToAction("Index");
        }
    }
}