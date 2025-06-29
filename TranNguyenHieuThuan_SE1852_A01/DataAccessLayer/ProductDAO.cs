using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        static List<Product> products = new List<Product>();

        public void InitializeDataset()
        {
            if (products.Count > 0) return;
            products.Add(new Product
            {
                ProductId = 1,
                ProductName = "Laptop Dell XPS 13",
                CategoryId = 1,
                UnitPrice = 1299.99m,
                UnitsInStock = 50
            });
            products.Add(new Product
            {
                ProductId = 2,
                ProductName = "Lenovo ThinkCentre M720",
                CategoryId = 2,
                UnitPrice = 1199.99m,
                UnitsInStock = 30
            });
            products.Add(new Product
            {
                ProductId = 3,
                ProductName = "iPhone 15 Pro",
                CategoryId = 2,
                UnitPrice = 999.99m,
                UnitsInStock = 100
            });
            products.Add(new Product
            {
                ProductId = 4,
                ProductName = "MacBook Pro M3",
                CategoryId = 1,
                UnitPrice = 1599.99m,
                UnitsInStock = 25
            });
            products.Add(new Product
            {
                ProductId = 5,
                ProductName = "Samsung Galaxy S24",
                CategoryId = 2,
                UnitPrice = 799.99m,
                UnitsInStock = 75
            });
            products.Add(new Product
            {
                ProductId = 6,
                ProductName = "iPad Air",
                CategoryId = 3,
                UnitPrice = 599.99m,
                UnitsInStock = 60
            });
            products.Add(new Product
            {
                ProductId = 7,
                ProductName = "iMac 24-inch M1",
                CategoryId = 2,
                UnitPrice = 1799.99m,
                UnitsInStock = 40
            });
        }

        public List<Product> GetAllProducts()
        {
            return products;
        }

        public Product? GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.ProductId == id);
        }

        public bool SaveProduct(Product product)
        {
            Product old = products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (old != null)
                return false;
            products.Add(product);
            return true;
        }

        public bool UpdateProduct(Product product)
        {
            Product old = products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (old == null)
                return false;
            old.ProductName = product.ProductName;
            old.CategoryId = product.CategoryId;
            old.UnitPrice = product.UnitPrice;
            old.UnitsInStock = product.UnitsInStock;
            return true;
        }

        public bool DeleteProduct(int id)
        {
            Product old = products.FirstOrDefault(x => x.ProductId == id);
            if (old == null)
                return false;
            products.Remove(old);
            return true;
        }

        public List<Product> SearchProducts(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAllProducts();
            return products.Where(p => p.ProductName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
} 