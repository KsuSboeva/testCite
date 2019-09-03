using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestCite.Dal;
using TestCite.Models;

namespace TestCite.Controllers
{
    public class DynamicValueController : Controller
    {
        private DataContext db = new DataContext();

        // GET: DynamicValue
        public ActionResult Index()
        {
            var dynamicValues = db.dynamicValues.Include(d => d.order);
            return View(dynamicValues.ToList());
        }

        // GET: DynamicValue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicValue dynamicValue = db.dynamicValues.Find(id);
            if (dynamicValue == null)
            {
                return HttpNotFound();
            }
            return View(dynamicValue);
        }

        // GET: DynamicValue/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "orderId", "orderId");
            return View();
        }

        // POST: DynamicValue/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,settingId,settingName,OrderId,templateID,dynamicValue")] DynamicValue dynamicValue)
        {
            if (ModelState.IsValid)
            {
                db.dynamicValues.Add(dynamicValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "orderId", "orderId", dynamicValue.OrderId);
            return View(dynamicValue);
        }

        // GET: DynamicValue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicValue dynamicValue = db.dynamicValues.Find(id);
            if (dynamicValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "orderId", "orderId", dynamicValue.OrderId);
            return View(dynamicValue);
        }

      
        


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var valueToUpdate = db.dynamicValues.Find(id);
            if (TryUpdateModel(valueToUpdate, "",
               new string[] { "settingId", "settingName", "OrderId", "templateID", "dynamicValue" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            ViewBag.OrderId = new SelectList(db.Orders, "orderId", "orderId", valueToUpdate.OrderId);
            return View(valueToUpdate);
        }
            // GET: DynamicValue/Delete/5
            public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicValue dynamicValue = db.dynamicValues.Find(id);
            if (dynamicValue == null)
            {
                return HttpNotFound();
            }
            return View(dynamicValue);
        }

        // POST: DynamicValue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DynamicValue dynamicValue = db.dynamicValues.Find(id);
            db.dynamicValues.Remove(dynamicValue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
