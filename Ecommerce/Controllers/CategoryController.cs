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
    public class CategoryController : Controller
    {
        // GET: Category
        ECommerceEntities DB;
        public readonly IMapper Mapper;
        CategoryReposatory categoryReposatory;
        public CategoryController()
        {
            DB = new ECommerceEntities();
            Mapper = AutoMapper.AutoMapping.Mapper;
            categoryReposatory = new CategoryReposatory();
        }
        public ActionResult Index()
        {
            ViewBag.Data = "Category";
            return View();
        }
        public ActionResult GetDataTable()
        {
            return PartialView();
        }
        public ActionResult GetData()
        {
            var categories = categoryReposatory.GetAll().Select(category => new { ID = category.ID, Name = category.Name });
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddOrEditOrDetails(string trigger,int id=0)
        {
            if (id == 0)
            {
                return PartialView(new CategoryVM());
            }
            else
            {
                ViewBag.trigger = trigger;
                CategoryVM categoryVM = new CategoryVM();
                categoryVM.ID = id;
                Category category = categoryReposatory.GetById(id);
                Mapper.Map(category, categoryVM);
                return PartialView(categoryVM);

            }
        }
        [HttpPost]
        public ActionResult AddOrEditOrDetails(CategoryVM categoryVM, int id = 0)
        {
            
            if (id == 0)
            {
                if (ModelState.IsValid)
                {
                    Category category = new Category();
                    Mapper.Map(categoryVM, category);
                    categoryReposatory.AddCategory(category);
                    
                    return Json(new { message = "Added Successfully", done = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { message = "Failed to Add", done = false }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                Category category = categoryReposatory.GetById(id);
                Mapper.Map(categoryVM, category);
                categoryReposatory.EditCategory(category);
                return Json(new { message = "Edited Successfully", done = true }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Delete(int id)
        {

            string message = "";
            bool done;
            try
            {
                Category category = categoryReposatory.GetById(id);
                DB.Categories.Remove(category);
                DB.SaveChanges();
                done = true;
                message = "Deleted Successfully";
                return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "this Category has related data..";
                done = false;
                return Json(new { done = false, message = message }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}