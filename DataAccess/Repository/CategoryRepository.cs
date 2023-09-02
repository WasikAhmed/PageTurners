using System;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using PageTurners.Models;

namespace DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
        private ApplicationDbContext _db;
		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
            _db = db;
		}

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}

