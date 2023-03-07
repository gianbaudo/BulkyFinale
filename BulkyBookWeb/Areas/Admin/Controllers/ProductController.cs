using BulkyBookWeb.Data;
using BulkyBook.Models1;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess1.Repository.IRepository;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
    {
        //private readonly ApplicationDbContext _db;

        //private readonly ICategoryRepository _db;
        //public CategoryController(ICategoryRepository db)
        //{
        //	_db = db;
        //}

        private readonly IUnityOfWork _unitOfWork;
        public ProductController(IUnityOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }   


        public IActionResult Index()
        {
            // recupero info da db
            IEnumerable<Product> productListObject = _unitOfWork.Product.GetAll();
            return View(productListObject);
        }

        //get Create
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //per evitare che l'utente crei dall'url
        public IActionResult Create(Product product)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError(nameof(obj.Name), $"The name of property {nameof(obj.DisplayOrder)} cannot exactly match the name of property {nameof(obj.Name)}");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDbFirst = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            if (productFromDbFirst == null)
            {
                return NotFound();
            }
            return View(productFromDbFirst);

        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Product product)
		{

			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Update(product);
				_unitOfWork.Save();
				TempData["success"] = "Product updated successfully";
				return RedirectToAction(nameof(Index));
			}
			return View(product);

		}


		//GET
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var productFromDbFirst = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
			if (productFromDbFirst == null)
			{
				return NotFound();
			}
			return View(productFromDbFirst);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePost(int id, [Bind("Id")] Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}
			var productFromDbFirst = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
			if (productFromDbFirst == null)
			{
				return NotFound();
			}
			_unitOfWork.Product.Remove(productFromDbFirst);
			_unitOfWork.Save();
			TempData["success"] = "Product deleted successfully";
			return RedirectToAction(nameof(Index));
		}


	}
}
