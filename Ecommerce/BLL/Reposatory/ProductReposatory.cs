using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ecommerce.BLL.Reposatory
{
    public class ProductReposatory
    {
        ECommerceEntities DB = new ECommerceEntities();
        public List<Product> GetAll()
        {
            List<Product> products = DB.Products.ToList();
            return products;
        }
        public Product GetById(int id)
        {
            Product product = DB.Products.Find(id);
            return product;
        }
        public void AddProduct(Product product)
        {
            DB.Products.Add(product);
            DB.SaveChanges();
        }
        public void EditProduct(Product product)
        {
            DB.Entry(product).State = EntityState.Modified;
            DB.SaveChanges();
        }
    }
}