using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess1.Repository.IRepository
{
	public interface IUnityOfWork
	{
		ICategoryRepository Category { get; }
		void Save();
	}

}
