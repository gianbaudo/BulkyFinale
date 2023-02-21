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
	public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
	{
		private readonly ApplicationDbContext _db;
		public CoverTypeRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(CoverType coverType)
		{
			_db.CoverTypes.Update(coverType);
		}
	}


}
