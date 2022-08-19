using AutoMapper;
using Ecommerce.BLL.Reposatory;
using Ecommerce.Models;
using Ecommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ECommerceEntities DB;
        public readonly IMapper Mapper;
        ProductReposatory productReposatory;
        BrandReposatory brandReposatory;
        CategoryReposatory categoryReposatory;

        public ProductController()
        {
            DB = new ECommerceEntities();
            Mapper = AutoMapper.AutoMapping.Mapper;
            productReposatory = new ProductReposatory();
            brandReposatory = new BrandReposatory();
            categoryReposatory = new CategoryReposatory();
        }
        public ActionResult Index()
        {
            ViewBag.Data = "Product";
            return View();
        }
        public ActionResult GetDataTable()
        {
            return PartialView();
        }
        public ActionResult GetData()
        {
            var products = productReposatory.GetAll().Select(x => new { ID = x.ID, Name = x.Name, BrandName = x.Brand.Name, CategoryName = x.Category.Name, Description = x.ShortDescription, Price = x.Price });
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddOrEditOrDetails(string trigger, int id = 0)
        {
            if (id == 0)
            {
                List<Brand> brands = brandReposatory.GetAll();
                List<Category> categories = categoryReposatory.GetAll();
                ViewBag.brands = new SelectList(brands, "ID", "Name");
                ViewBag.categories = new SelectList(categories, "ID", "Name");
                return PartialView(new ProductVM());
            }
            else
            {
                ViewBag.trigger = trigger;
                Product product = productReposatory.GetById(id);
                List<Brand> brands = brandReposatory.GetAll();
                List<Category> categories = categoryReposatory.GetAll();
                ViewBag.brands = new SelectList(brands, "ID", "Name", product.BrandFK);
                ViewBag.categories = new SelectList(categories, "ID", "Name", product.CategoryFK);
                ProductVM productVM = new ProductVM();
                Mapper.Map(product, productVM);
                return PartialView(productVM);
            }
        }
        [HttpPost]
        public ActionResult AddOrEditOrDetails(ProductVM productVM)
        {
            string message = "Failed to edit";
            bool done = false;
            if (productVM.ID == 0)
            {
                if (ModelState.IsValid)
                {
                    Product product = new Product();
                    Mapper.Map(productVM, product);
                    productReposatory.AddProduct(product);
                    if (DB.SaveChanges() > -1)
                    {
                        message = "Added Successfully!";
                        done = true;
                    }
                    else
                    {
                        message = "Failed to add!";
                        done = false;
                    }
                    return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { message = message, done = done }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Product product = productReposatory.GetById(productVM.ID);
                Mapper.Map(productVM,product);
                productReposatory.EditProduct(product);
                if (DB.SaveChanges() > -1)
                {
                    message = "Edited Successfully!";
                    done = true;
                }
                else
                {
                    message = "Failed to edit";
                    done = false;
                }
                return Json(new { message = message, done = done }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Delete(int id)
        {
            string message = "";
            bool done;
            try
            {
                Product _product = DB.Products.Find(id);

                DB.Products.Remove(_product);
                DB.SaveChanges();
                done = true;
                message = "Deleted Successfully";
                return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "this Product has related data..";
                done = false;
                return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult GetImage(int id)
        {
            var productImages = DB.ProductImages.Where(x => x.ProductFK == id).Select(x => new { ID = x.ID, Image = x.Image }).ToList();
            return Json(productImages, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddImage(int id)
        {
            Product product = productReposatory.GetById(id);
            ViewBag.Name = product.Name;
            ViewBag.Id = product.ID;
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddImage(FormCollection formCollection)
        {
            int Id = int.Parse(formCollection["ID"]);
            string message;
            bool done;
            ProductImage productImage;
            var files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase image = files[i];
                productImage = new ProductImage();
                productImage.Image = image.FileName;
                string Path = $"/Attachment/ProductImages/{image.FileName}";
                image.SaveAs(Server.MapPath(Path));
                productImage.ProductFK = Id;
                DB.ProductImages.Add(productImage);
            }
            if (DB.SaveChanges() > -1)
            {
                message = "Added Successfully";
                done = true;
            }
            else
            {
                message = "Failed to Add";
                done = false;
            }
            return Json(new { message = message, done = done }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SetMain(int id, int productFK)
        {
            List<ProductImage> productImages = DB.ProductImages.Where(x => x.IsMain == true && x.ProductFK == productFK).ToList();
            foreach (var item in productImages)
            {
                item.IsMain = false;

            }

            ProductImage productImage = DB.ProductImages.Find(id);
            productImage.IsMain = true;
            DB.SaveChanges();
            return Json(new { message = "I'm the main now:)" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteImage(int id)
        {
            string message;
            bool done;
            try
            {
                ProductImage productImage = DB.ProductImages.Find(id);
                DB.ProductImages.Remove(productImage);
                done = true;
                message = "Deleted Successfully";
                DB.SaveChanges();

                return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "this ProductImage has related data..";
                done = false;
                return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}