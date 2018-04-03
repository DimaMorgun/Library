using Library.BusinessLogicLayer.Services;
using Library.ViewModelLayer.ViewModels;

using System.Collections.Generic;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class BrochureController : Controller
    {
        private BrochureService _brochureService;

        public BrochureController()
        {
            _brochureService = new BrochureService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<BrochureViewModel> allbrochures = _brochureService.GetAll();

            return View(allbrochures);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            List<BrochureViewModel> allbrochures = _brochureService.GetAll();

            return Json(allbrochures, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BrochureViewModel brochure)
        {
            _brochureService.Insert(brochure);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetByIdView(int id)
        {
            BrochureViewModel brochure = _brochureService.Get(id);

            return View(brochure);
        }

        [HttpGet]
        public ActionResult UpdateView(int id)
        {
            BrochureViewModel brochure = _brochureService.Get(id);

            return View(brochure);
        }

        [HttpPost]
        public ActionResult Update(BrochureViewModel brochure)
        {
            _brochureService.Update(brochure);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteView(int id)
        {
            BrochureViewModel brochure = _brochureService.Get(id);

            return View(brochure);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _brochureService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SaveToJSON(int id)
        {
            _brochureService.SaveToJSON(id);

            return RedirectToAction("Index");
        }
    }
}