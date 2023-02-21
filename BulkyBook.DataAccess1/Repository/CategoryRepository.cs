using BulkyBook.DataAccess1.Repository.IRepository;
using BulkyBook.Models1;
using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess1.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;
		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		//public void Save()
		//{
		//	_db.SaveChanges();
		//}

		public void Update(Category category)
		{
			_db.Categories.Update(category);
		}
	}

}
