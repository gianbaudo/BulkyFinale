﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess1.Repository.IRepository
{
	internal interface IRepository
	{
		public interface IRepository<T> where T : class
		{
			//T - Category
			T? GetFirstOrDefault(Expression<Func<T, bool>> filter);
			IEnumerable<T> GetAll();
			void Add(T entity);
			void Remove(T entity);
			void RemoveRange(IEnumerable<T> entities);

		}

	}
}