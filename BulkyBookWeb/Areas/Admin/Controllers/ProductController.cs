using BulkyBookWeb.Data;
using BulkyBook.Models1;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess1.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBook.Models1.ViewModel;

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
		private readonly IWebHostEnvironment _hostEnvironment;
		public ProductController(IUnityOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_hostEnvironment = hostEnvironment;
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
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //create product
                return View(productVM);
            }
            else
            {
                //update product
            }
            return View(productVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Upsert(ProductVM obj, IFormFile? file)
		{

			if (ModelState.IsValid)
			{
				string wwwRootPath = _hostEnvironment.WebRootPath;
				if (file != null)
				{
					//creiamo un nuovo nome per il file che l'utente ha caricato
					//facciamo in modo che non possano esistere due file con lo stesso nome
					string fileName = Guid.NewGuid().ToString();
					var uploadDir = Path.Combine(wwwRootPath, "images", "products");
					var fileExtension = Path.GetExtension(file.FileName);
					var filePath = Path.Combine(uploadDir, fileName + fileExtension);
					var fileUrlString = filePath[wwwRootPath.Length..].Replace(@"\\", @"\");
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					obj.Product.ImageUrl = fileUrlString;
				}
				_unitOfWork.Product.Add(obj.Product);
				_unitOfWork.Save();
				TempData["success"] = "Product created successfully";
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
