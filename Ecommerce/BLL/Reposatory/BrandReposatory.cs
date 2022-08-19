using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ecommerce.BLL.Reposatory
{
    public class BrandReposatory
    {
        ECommerceEntities DB = new ECommerceEntities();
        public List<Brand> GetAll()
        {
            List<Brand> brands = DB.Brands.ToList();
            return brands;
        }
        public Brand GetById(int id)
        {
            Brand brand = DB.Brands.Find(id);
            return brand;
        }
        public void AddBrand(Brand brand)
        {
            DB.Brands.Add(brand);
            DB.SaveChanges();
        }
        public void EditBrand(Brand brand)
        {
            DB.Entry(brand).State = EntityState.Modified;
            DB.SaveChanges();
        }
    }
}