//using Library.DataAccessLayer.Models;
//using Library.DataAccessLayer.UnitOfWork;
//using Library.WEB.Models;

//using System.Linq;
//using System.Web.Mvc;

//namespace Library.WEB.Controllers
//{
//    public class AuthorController : Controller
//    {
//        private UnitOfWork _unitOfWork;

//        public AuthorController()
//        {
//            _unitOfWork = new UnitOfWork();
//        }

//        [HttpGet]
//        public ActionResult Index()
//        {
//            return View(_unitOfWork.Authors.GetList());
//        }

//        [HttpGet]
//        public ActionResult CreateView()
//        {
//            var authorBooksViewModel = new AuthorBooksViewModel
//            {
//                Books = _unitOfWork.Books.GetList()
//            };
//            _unitOfWork.Save();

//            return View(authorBooksViewModel);
//        }

//        [HttpPost]
//        public ActionResult Create(Author author, int[] selectedBooks)
//        {
//            if (selectedBooks != null)
//            {
//                foreach (var book in _unitOfWork.Books.GetList().Where(x => selectedBooks.Contains(x.BookId)))
//                {
//                    author.Books.Add(book);
//                }
//            }
//            _unitOfWork.Authors.Create(author);
//            _unitOfWork.Save();

//            return RedirectToAction("Index");
//        }

//        [HttpGet]
//        public ActionResult GetByIdView(int id)
//        {
//            return View(_unitOfWork.Authors.GetByid(id));
//        }

//        [HttpGet]
//        public ActionResult UpdateView(int id)
//        {
//            var authorBooksViewModel = new AuthorBooksViewModel
//            {
//                Author = _unitOfWork.Authors.GetByid(id),
//                Books = _unitOfWork.Books.GetList()
//            };

//            return View(authorBooksViewModel);
//        }

//        [HttpPost]
//        public ActionResult Update(Author author, int[] selectedBooks)
//        {
//            Author newAuthor = _unitOfWork.Authors.GetByid(author.AuthorId);
//            newAuthor.Name = author.Name;
//            newAuthor.Birthday = author.Birthday;
//            newAuthor.Deathday = author.Deathday;

//            newAuthor.Books.Clear();
//            if (selectedBooks != null)
//            {
//                foreach (var book in _unitOfWork.Books.GetList().Where(x => selectedBooks.Contains(x.BookId)))
//                {
//                    newAuthor.Books.Add(book);
//                }
//            }
//            _unitOfWork.Authors.Update(newAuthor);
//            _unitOfWork.Save();

//            return RedirectToAction("Index");
//        }

//        [HttpGet]
//        public ActionResult DeleteView(int id)
//        {
//            return View(_unitOfWork.Authors.GetByid(id));
//        }

//        [HttpPost]
//        public ActionResult Delete(int id)
//        {
//            _unitOfWork.Authors.Delete(id);
//            _unitOfWork.Save();

//            return RedirectToAction("Index");
//        }
//    }
//}