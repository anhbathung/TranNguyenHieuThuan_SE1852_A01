using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services
{
    public interface IProductService
    {
        public List<Product> GetAllProducts();
        public void InitializeDataset();
        public Product? GetProductById(int id);
        public bool SaveProduct(Product product);
        public bool UpdateProduct(Product product);
        public bool DeleteProduct(int id);
        public List<Product> SearchProducts(string keyword);
    }
} 