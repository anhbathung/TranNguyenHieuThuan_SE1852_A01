using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        static List<Category> categories = new List<Category>();

        public void InitializeDataset()
        {
            categories.Add(new Category
            {
                CategoryId = 1,
                CategoryName = "Laptops",
                Description = "Portable computers and notebooks"
            });
            categories.Add(new Category
            {
                CategoryId = 2,
                CategoryName = "Desktops",
                Description = "Computer and phone accessories"
            });
            categories.Add(new Category
            {
                CategoryId = 3,
                CategoryName = "Smartphones",
                Description = "Mobile phones and devices"
            });
            categories.Add(new Category
            {
                CategoryId = 4,
                CategoryName = "Tablets",
                Description = "Tablet computers and iPads"
            });
        }

        public List<Category> GetAllCategories()
        {
            return categories;
        }

        public Category? GetCategoryById(int id)
        {
            return categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public bool SaveCategory(Category category)
        {
            Category old = categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
            if (old != null)
                return false;
            categories.Add(category);
            return true;
        }

        public bool UpdateCategory(Category category)
        {
            Category old = categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
            if (old == null)
                return false;
            old.CategoryName = category.CategoryName;
            old.Description = category.Description;
            return true;
        }

        public bool DeleteCategory(int id)
        {
            Category old = categories.FirstOrDefault(x => x.CategoryId == id);
            if (old == null)
                return false;
            categories.Remove(old);
            return true;
        }

        public List<Category> SearchCategories(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAllCategories();
            return categories.Where(c => c.CategoryName.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                || c.Description?.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
        }
    }
} 