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

        //EDIT
        public IActionResult Upsert(int? id)
        {
            Product product = new();

            if (id == null || id == 0)
            {
                //create product
                return View(product);
            }
            else
            {
                //update product
            }
            return View(product);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

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
