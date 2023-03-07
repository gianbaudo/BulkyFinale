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
	public class UnityOfWorks : IUnityOfWork
	{
		private readonly ApplicationDbContext _db;
		public UnityOfWorks(ApplicationDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);
			CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
        }
		public ICategoryRepository Category { get; private set; } = null!;
		public ICoverTypeRepository CoverType { get; private set; } = null!;
        public IProductRepository Product { get; private set; } = null!;

        public void Save()
		{
			_db.SaveChanges();
		}

	}
}
