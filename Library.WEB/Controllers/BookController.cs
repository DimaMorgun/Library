
using Library.BusinessLogicLayer.Services;
using Library.ViewModelLayer.ViewModels;

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
            return View(_bookService.GetList());
        }

        [HttpGet]
        public ActionResult CreateView()
        {
            return View(_bookService.GetBookAuthors());
        }

        [HttpPost]
        public ActionResult Create(BookViewModel book, int[] selectediItems)
        {
            _bookService.Create(book, selectediItems);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetByIdView(int id)
        {
            return View(_bookService.GetByid(id));
        }

        [HttpGet]
        public ActionResult UpdateView(int id)
        {
            return View(_bookService.GetBookAuthors(id));
        }

        [HttpPost]
        public ActionResult Update(BookViewModel book, int[] selectediItems)
        {
            _bookService.Update(book, selectediItems);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteView(int id)
        {
            return View(_bookService.GetByid(id));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _bookService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}