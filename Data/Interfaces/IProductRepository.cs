using ProductManagement.Data.Entities;

namespace ProductManagement.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<List<Product>> GetAllByCondition(decimal price);
        Task<Product> Add(Product product);
        Task<Product> UpdateById(int id, Product product);
        Task<bool> DeleteById(int id);

    }
}
