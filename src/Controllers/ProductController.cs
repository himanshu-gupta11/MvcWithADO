using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC_ADO.DAL;
using MyFirstMVC_ADO.Models;

namespace MyFirstMVC_ADO.Controllers
{

  
    
    public class ProductController : Controller
    {

        //Create a object of  Dal to access all the data from ProductDal
        ProductDAL _ProductDAL = new ProductDAL();
        // GET: Product
        public ActionResult Index()
        {
            var ProductList= _ProductDAL.GetAllProducts();

            if (ProductList.Count == 0)
            {

                TempData["InfoMessage"] = "There is no data available";
            }
            return View(ProductList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var Product = _ProductDAL.GetProductListById(id).FirstOrDefault();
            if (Product == null)
            {

                TempData["Error"] = "No Product is on this productid :"+ id.ToString();
                return RedirectToAction("Index");
            }
            return View(Product);
        }

        // GET: Product/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {   
            try
            {
                // TODO: Add insert logic here
                bool InsertedOrNot = false;
                if (ModelState.IsValid)
                {
                    InsertedOrNot = _ProductDAL.SaveProduct(product);
                    if (InsertedOrNot)
                    {

                        TempData["Success"] = "Product Saved Successfully";
                    }
                    else
                    {
                        TempData["Error"] = "Product Not Saved";

                    }
                   
                }

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {

            var Product=_ProductDAL.GetProductListById(id).FirstOrDefault();
            if(Product == null)
            {

                TempData["Error"] = "No Product is on this productid:" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(Product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit( Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool Isupdated = _ProductDAL.UpdateProduct(product);

                    if (Isupdated)
                    {

                        TempData["Success"] = "Product Updated Successfully";
                    }

                    else
                    {
                        TempData["Error"] = "Product Not Updated ";
                    }

                   
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }

       // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var Product = _ProductDAL.GetProductListById(id).FirstOrDefault();
            if (Product == null)
            {

                TempData["Error"] = "No Product is on this productid:" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(Product);
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult Delete2(int id)
        {
            try
            {
                bool Isdeleted = false;


                if (ModelState.IsValid)
                {
                    Isdeleted = _ProductDAL.DeleteProduct(id);

                    if (Isdeleted)
                    {

                        TempData["Success"] = "Product Deleted Successfully";
                    }
                    else
                    {
                        TempData["Error"] = "Product Not Deleted";

                    }

                }
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
             TempData["Error"] = ex.Message;
                return View();
            }
        }
    }
}
