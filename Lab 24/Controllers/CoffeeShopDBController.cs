using Lab_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_24.Controllers
{
    public class CoffeeShopDBController : Controller
    {
        // GET: CoffeeShopDB
        CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
        // GET: Movie
        public ActionResult Index()
        {
            ViewBag.ItemList = ORM.Items.ToList();
            return View();
        }
        public ActionResult UpdateItem(int itemID)
        {
            Item found = ORM.Items.Find(itemID);
            return View(found);
        }
        public ActionResult SaveChanges(Item updateItem)
        {
            Item originalItem = ORM.Items.Find(updateItem.Id);
            if (originalItem != null && ModelState.IsValid)
            {
                originalItem.Name = updateItem.Name;
                originalItem.Description = updateItem.Description;
                originalItem.Quantity = updateItem.Quantity;
                originalItem.Price = updateItem.Price;

                ORM.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Something did not go right. try again!";
                return RedirectToAction("UpdateItem", updateItem.Id);
            }

        }
        public ActionResult DeleteItem(int itemID)
        {
            Item found = ORM.Items.Find(itemID);
            ORM.Items.Remove(found);

            ORM.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddItem()
        {
            return View();
        }
        public ActionResult SaveNewItem(Item newItem)
        {
            if (ModelState.IsValid)
            {
                ORM.Items.Add(newItem);

                ORM.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Something did not go right.";
                return View("AddItem");
            }
        }

    }
}