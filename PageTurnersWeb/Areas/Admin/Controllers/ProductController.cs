using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PageTurners.Models;
using PageTurners.Models.ViewModels;

namespace PageTurnersWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public ActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();

            return View(objProductList);
        }
        // public ActionResult Create()
        // {
        //     IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
        //         .GetAll().Select(u=>new SelectListItem
        //         {
        //             Text = u.Name,
        //             Value = u.Id.ToString()
        //         });
        //
        //     //ViewBag.CategoryList = CategoryList;
        //     //ViewData["CategoryList"] = CategoryList;
        //     ProductViewModel productViewModel = new()
        //     {
        //         CategoryList = CategoryList,
        //         Product = new Product()
        //     };
        //     
        //     return View(productViewModel);
        // }
        
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Create(ProductViewModel productViewModel)
        // {
        //     try
        //     {
        //         if (ModelState.IsValid)
        //         {
        //             _unitOfWork.Product.Add(productViewModel.Product);
        //             _unitOfWork.Save();
        //             TempData["success"] = "Product added successfully";
        //             
        //             return RedirectToAction("Index");
        //         }
        //         else
        //         {
        //             productViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        //             {
        //                 Text = u.Name,
        //                 Value = u.Id.ToString()
        //             });
        //             return View(productViewModel);
        //         }
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }
        
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                // create
                return View(productViewModel);
            }
            else
            {
                // update
                productViewModel.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images/product");

                    if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                    {
                        // delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('/'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productViewModel.Product.ImageUrl = @"/images/product/" + fileName;
                }

                if (productViewModel.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productViewModel.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productViewModel.Product);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "Product added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productViewModel = new()
                {
                    CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
                    Product = new Product()
                };
                return View(productViewModel);
            }
        }
        
        
        // public ActionResult Edit(int? id)
        // {
        //     if (id == null || id == 0)
        //     {
        //         return NotFound();
        //     }
        //
        //     Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //     if (productFromDb == null)
        //     {
        //         return NotFound();
        //     }
        //     
        //     return View(productFromDb);
        // }
        //
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Edit(Product obj)
        // {
        //     try
        //     {
        //         _unitOfWork.Product.Update(obj);
        //         _unitOfWork.Save();
        //         TempData["success"] = "Product updated successfully";
        //
        //         return RedirectToAction("Index");
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }

        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePOST(int? id)
        {
            try
            {
                Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }
                
                _unitOfWork.Product.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product deleted successfully";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}