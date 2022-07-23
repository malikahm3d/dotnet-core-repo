using BulkyBook.DAL;
using BulkyBook.DAL.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _uow;
        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> ProductsAsObjects = _uow.Product.GetAll();
            return View(ProductsAsObjects);
        }
        public IActionResult Edit(int id)
        {
            var product = _uow.Product.GetFirstOrDefault(Product => Product.Id == id);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product pro)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();   
            }
            _uow.Product.Update(pro);
            _uow.Save();
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new()
            {
                product = new(),
                CategoryList = _uow.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _uow.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            //Product product = new();
            //IEnumerable<SelectListItem> CategoryList = _uow.Category.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString(),
            //    });
            //IEnumerable<SelectListItem> CoverType = _uow.CoverType.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString(),
            //    });

            if(id == null || id == 0)
            {
                //// create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverType"] = CoverType;
                return View(productVM);
            }
            return View();
        }
    }
}
