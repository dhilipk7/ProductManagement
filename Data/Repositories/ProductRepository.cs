using ProductManagement.Data.Entities;
using ProductManagement.Data.DbContexts;
using ProductManagement.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetAllByCondition(decimal price)
        {
            return await _context.Products.Where(product=>product.Price>price).ToListAsync();
        }
        public async Task<Product> GetById(int productId)
        {
            return await _context.Products.FindAsync(productId);

        }

        public async Task<Product> Add(Product product)
        {
            var existingProduct = await _context.Products.Where(p => p.Name == product.Name).FirstOrDefaultAsync();

            if (existingProduct == null)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Product name altready exists");
            }
            return product;
        }
        public async Task<Product> UpdateById(int productId, Product product)
        {

            Product? existingProduct = await _context.Products.Where(p => p.Name == product.Name && p.ProductId != productId).FirstOrDefaultAsync();
            if (existingProduct == null)
            {
                existingProduct= _context.Products.Find(productId);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;

                    _context.SaveChanges();
                }
                return existingProduct;
            }
            else
            {
                throw new Exception("Product name altready exists");
            }

        }
        public async Task<bool> DeleteById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

             _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
    }
}
