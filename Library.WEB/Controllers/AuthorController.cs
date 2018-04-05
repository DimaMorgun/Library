using Library.BusinessLogicLayer.Services;
using Library.ViewModelLayer.ViewModels;

using System.Collections.Generic;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class AuthorController : Controller
    {
        private AuthorService _authorService;

        public AuthorController()
        {
            _authorService = new AuthorService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<AuthorViewModel> allAuthors = _authorService.GetAll();

            return View(allAuthors);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            List<AuthorViewModel> allAuthors = _authorService.GetAll();

            return Json(allAuthors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateView()
        {
            AuthorBooksViewModel authorBooks = _authorService.GetAuthorBooks();

            return View(authorBooks);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(AuthorViewModel author)
        {
            _authorService.Insert(author);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetByIdView(int id)
        {
            AuthorViewModel author = _authorService.Get(id);

            return View(author);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateView(int id)
        {
            AuthorBooksViewModel authorBooks = _authorService.GetAuthorBooks(id);

            return View(authorBooks);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Update(AuthorViewModel author)
        {
            _authorService.Update(author);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteView(int id)
        {
            AuthorViewModel author = _authorService.Get(id);

            return View(author);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            _authorService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult SaveToJSON(int id)
        {
            _authorService.SaveToJSON(id);

            return RedirectToAction("Index");
        }
    }
}