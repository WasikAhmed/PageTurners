using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using PageTurners.Models;

namespace PageTurnersWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            { 
                ModelState.AddModelError("name", "The display order cannot exactly match the name");
            }
            // if (obj.Name != null && obj.Name.ToLower()=="test")
            // {
            //     ModelState.AddModelError("", "Test is an invalid value");
            // }
            if (ModelState.IsValid)
            {
                //_categoryRepo.Add(obj);
                //_categoryRepo.save();

                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Category categoryFrommDb = _categoryRepo.Get(u => u.Id == id);
            // Category? categoryFromDb_1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            // Category? categoryFromDb_2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                //_categoryRepo.update(obj);
                //_categoryRepo.save();
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            //Category? categoryFormDb = _categoryRepo.Get(u => u.Id == id);
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            //Category? obj = _categoryRepo.Get(u => u.Id == id);
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            //_categoryRepo.Remove(obj);
            //_categoryRepo.save();
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}