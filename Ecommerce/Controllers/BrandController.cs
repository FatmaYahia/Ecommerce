using AutoMapper;
using Ecommerce.BLL.Reposatory;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        ECommerceEntities DB;
        public readonly IMapper Mapper;
        BrandReposatory brandReposatory;
        public BrandController()
        {
            DB = new ECommerceEntities();
            Mapper = AutoMapper.AutoMapping.Mapper;
            brandReposatory = new BrandReposatory();
        }
        public ActionResult Index()
        {
            ViewBag.Data = "Brand";
            return View();
        }
        public ActionResult GetDataTable()
        {
            return PartialView();
        }
        public ActionResult GetData()
        {
            var brands = brandReposatory.GetAll().Select(x => new { ID = x.ID, Name = x.Name }).ToList();
            return Json(brands, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddOrEditOrDetails(string trigger, int id = 0)
        {
            if (id == 0)
            {
                return PartialView(new Brand());
            }
            else
            {
                ViewBag.Trigger = trigger;
                Brand brand = brandReposatory.GetById(id);
                return PartialView(brand);
            }
        }
        [HttpPost]
        public ActionResult AddOrEditOrDetails(FormCollection formCollection)
        {
            string message = "Fails to Add!";
            bool done = false;
            int id = int.Parse(formCollection["ID"]);
            if (id == 0)
            {
                string Path = "";
                Brand brand = new Brand();
                brand.Name = formCollection["Name"];
                brand.Description = formCollection["Description"];
                var files = Request.Files;
                HttpPostedFileBase image = files.Get("Image");
                if (image.ContentLength != 0)
                {
                    Path = $"~/Attachment/Brands/{image.FileName}";
                    brand.Image = image.FileName;
                }
                brandReposatory.AddBrand(brand);
                if (DB.SaveChanges() > -1)
                {
                    image.SaveAs(Server.MapPath(Path));
                    message = "Add Successfully!";
                    done = true;
                } 
                return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);
            }

            else
            {

                Brand brand = brandReposatory.GetById(id);
                brand.Name = formCollection["Name"];
                brand.Description = formCollection["Description"];
                var files = Request.Files;
                HttpPostedFileBase image = files.Get("Image");
                string Path = "";
                if (image.ContentLength != 0)
                {
                    Path = $"~/Attachment/Brands/{image.FileName}";
                    image.SaveAs(Server.MapPath(Path));
                    brand.Image = image.FileName;
                }
                brandReposatory.EditBrand(brand);
                if (DB.SaveChanges() > -1)
                {
                    message = "Edited Successfully!";
                    done = true;
                }
                else
                {
                    message = "Fails to Edit!";
                    done = false;
                }
                return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(int id)
        {
            string message = "";
            bool done;
            try
            {
                Brand brand = DB.Brands.Find(id);
                DB.Brands.Remove(brand);
                DB.SaveChanges();
                done = true;
                message = "Deleted Successfully";
                return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "this brand has related data..";
                done = false;
                return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}