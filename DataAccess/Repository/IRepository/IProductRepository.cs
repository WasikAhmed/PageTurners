using PageTurners.Models;

namespace DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product obj);
}