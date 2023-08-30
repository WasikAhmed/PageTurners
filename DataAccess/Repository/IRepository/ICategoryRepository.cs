using System;
using PageTurners.Models;

namespace DataAccess.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void update(Category obj);
		void save();
	}
}

