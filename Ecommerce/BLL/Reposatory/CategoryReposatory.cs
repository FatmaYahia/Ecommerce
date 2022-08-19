using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ecommerce.BLL.Reposatory
{
    public class CategoryReposatory
    {
        ECommerceEntities DB = new ECommerceEntities();
        public List<Category> GetAll()
        {
            List<Category> categories = DB.Categories.ToList();
            return categories;
        }
        public Category GetById(int id)
        {
            Category category = DB.Categories.Find(id);
            return category;
        }
        public void AddCategory(Category category)
        {
            DB.Categories.Add(category);
            DB.SaveChanges();
        }
        public void EditCategory(Category category)
        {
            DB.Entry(category).State = EntityState.Modified;
            DB.SaveChanges();
        }

    }
}