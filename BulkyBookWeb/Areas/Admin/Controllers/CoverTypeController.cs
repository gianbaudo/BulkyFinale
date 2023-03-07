using BulkyBookWeb.Data;
using BulkyBook.Models1;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess1.Repository.IRepository;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CoverTypeController : Controller
    {
        //private readonly ApplicationDbContext _db;

        //private readonly ICategoryRepository _db;
        //public CategoryController(ICategoryRepository db)
        //{
        //	_db = db;
        //}

        private readonly IUnityOfWork _unitOfWork;
        public CoverTypeController(IUnityOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }   


        public IActionResult Index()
        {
            // recupero info da db
            IEnumerable<CoverType> coverTypeListObject = _unitOfWork.CoverType.GetAll();
            return View(coverTypeListObject);
        }

        //get Create
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //per evitare che l'utente crei dall'url
        public IActionResult Create(CoverType coverType)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError(nameof(obj.Name), $"The name of property {nameof(obj.DisplayOrder)} cannot exactly match the name of property {nameof(obj.Name)}");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                TempData["success"] = "CoverType created successfully";
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        //EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDbFirst);

        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(CoverType obj)
		{

			if (ModelState.IsValid)
			{
				_unitOfWork.CoverType.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "CoverType updated successfully";
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
			var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
			if (coverTypeFromDbFirst == null)
			{
				return NotFound();
			}
			return View(coverTypeFromDbFirst);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePost(int id, [Bind("Id")] CoverType coverType)
		{
			if (id != coverType.Id)
			{
				return NotFound();
			}
			var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
			if (coverTypeFromDbFirst == null)
			{
				return NotFound();
			}
			_unitOfWork.CoverType.Remove(coverTypeFromDbFirst);
			_unitOfWork.Save();
			TempData["success"] = "CoverType deleted successfully";
			return RedirectToAction(nameof(Index));
		}


	}
}
