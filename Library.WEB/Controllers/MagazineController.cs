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
            List<MagazineViewModel> allMagazines = _magazineService.GetList();

            return View(allMagazines);
        }

        [HttpGet]
        public ActionResult CreateView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MagazineViewModel magazine)
        {
            _magazineService.Create(magazine);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetByIdView(int id)
        {
            MagazineViewModel magazine = _magazineService.GetByid(id);

            return View(magazine);
        }

        [HttpGet]
        public ActionResult UpdateView(int id)
        {
            MagazineViewModel magazine = _magazineService.GetByid(id);

            return View(magazine);
        }

        [HttpPost]
        public ActionResult Update(MagazineViewModel magazine)
        {
            _magazineService.Create(magazine);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteView(int id)
        {
            MagazineViewModel magazine = _magazineService.GetByid(id);

            return View(magazine);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _magazineService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SaveToJSON(int id)
        {
            _magazineService.SaveToJSON(id);

            return RedirectToAction("Index");
        }
    }
}