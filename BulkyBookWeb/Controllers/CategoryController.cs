using BulkyBookWeb.Data;
using BulkyBook.Models1;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess1.Repository.IRepository;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
		//private readonly ApplicationDbContext _db;

		//private readonly ICategoryRepository _db;
		//public CategoryController(ICategoryRepository db)
		//{
		//	_db = db;
		//}

		private readonly IUnityOfWork _unitOfWork;
		public CategoryController(IUnityOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		public IActionResult Index()
        {
            // recupero info da db
            IEnumerable<Category> categoryListObject = _unitOfWork.Category.GetAll();
			return View(categoryListObject);
        }

        //get Create
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //per evitare che l'utente crei dall'url
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError(nameof(obj.Name), $"The name of property {nameof(obj.DisplayOrder)} cannot exactly match the name of property {nameof(obj.Name)}");
            }

            if (ModelState.IsValid)
            {
				_unitOfWork.Category.Add(obj);
				_unitOfWork.Save();
				TempData["success"] = "Category created successfully";
				return RedirectToAction("Index");
            }
            return View(obj);
        }

		//EDIT
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
			if (categoryFromDbFirst == null)
			{
				return NotFound();
			}
			return View(categoryFromDbFirst);

		}
		[HttpPost]
		[ValidateAntiForgeryToken] //per evitare che l'utente crei dall'url
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError(nameof(obj.Name), $"The name of property {nameof(obj.DisplayOrder)} cannot exactly match the name of property {nameof(obj.Name)}");
			}
			if (ModelState.IsValid)
			{
				_unitOfWork.Category.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "Category updated successfully";
				return RedirectToAction(nameof(Index));
			}
			return View(obj);

		}

		//DELETE
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
			if (categoryFromDbFirst == null)
			{
				return NotFound();
			}
			return View(categoryFromDbFirst);

		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePost(int id, [Bind("Id")] Category category)
		{
			if (id != category.Id)
			{
				return NotFound();
			}

			var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
			if (categoryFromDbFirst == null)
			{
				return NotFound();
			}
			_unitOfWork.Category.Remove(categoryFromDbFirst);
			_unitOfWork.Save();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction(nameof(Index));
		}

	}
}
