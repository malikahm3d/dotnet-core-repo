using BulkyBook.DAL;
using BulkyBook.DAL.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uow;
        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            //var objCategoryList = _db.Categories.ToList();
            IEnumerable<Category> objCategoryList = _uow.Category.GetAll();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category cat)
        {
            if (cat.Name == cat.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Cannot be same as Display Order");
            }
            if (!ModelState.IsValid)
            {
                TempData["Fail"] = "Go fuck yourself";
                return View(cat);
                //return BadRequest(ModelState);
                //return BadRequest();
            }
            _uow.Category.Add(cat);
            _uow.Save();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index", "Category");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cat = _uow.Category.GetFirstOrDefault(c => c.Id == id);
            if (!(cat == null))
            {
                return View(cat);
            }
            return BadRequest();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _uow.Category.Update(cat);
            _uow.Save();
            return RedirectToAction("Index", "Category");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var objOfCat = _uow.Category.GetFirstOrDefault(c => c.Id == id);
            if (!(objOfCat == null))
            {
                _uow.Category.Remove(objOfCat);
            }
            return RedirectToAction("Index", "Category");
        }
    }
}
