using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess1.Repository.IRepository
{
	public interface IRepository
	{
		public interface IRepository<T> where T : class
		{
            //T - Category
            T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? inludeProperties = null);
            IEnumerable<T> GetAll(string? includeProperties = null);
            void Add(T entity);
            void Remove(T entity);
            void RemoveRange(IEnumerable<T> entities);


        }

    }
}
