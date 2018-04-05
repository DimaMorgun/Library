using Library.BusinessLogicLayer.Services;
using Library.ViewModelLayer.ViewModels;

using System.Collections.Generic;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;

        public BookController()
        {
            _bookService = new BookService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<BookViewModel> allBooks = _bookService.GetAll();

            return View(allBooks);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            List<BookViewModel> allBooks = _bookService.GetAll();

            return Json(allBooks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            BookAuthorsPublicationHousesViewModel bookAuthors = _bookService.GetBookAuthorsPublicationHouses();

            return View(bookAuthors);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(BookViewModel book)
        {
            _bookService.Insert(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            BookViewModel book = _bookService.Get(id);

            return View(book);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Update(int id)
        {
            BookAuthorsPublicationHousesViewModel bookAuthors = _bookService.GetBookAuthorsPublicationHouses(id);

            return View(bookAuthors);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Update(BookViewModel book)
        {
            _bookService.Update(book);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            _bookService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult SaveToJSON(int id)
        {
            _bookService.SaveToJSON(id);

            return RedirectToAction("Index");
        }
    }
}