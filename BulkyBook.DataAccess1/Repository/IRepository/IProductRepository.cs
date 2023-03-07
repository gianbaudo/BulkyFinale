using BulkyBook.Models1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BulkyBook.DataAccess1.Repository.IRepository.IRepository;

namespace BulkyBook.DataAccess1.Repository.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{
		void Update(Product product);
		//void Save();
	}

}
