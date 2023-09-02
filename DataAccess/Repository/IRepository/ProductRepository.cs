using DataAccess.Data;
using PageTurners.Models;

namespace DataAccess.Repository.IRepository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Product obj)
    {
        _db.Products.Update(obj);
    }
}