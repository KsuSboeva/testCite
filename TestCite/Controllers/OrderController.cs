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
using TestCite.ViewModels;

namespace TestCite.Controllers
{
    public class OrderController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.template);
            return View(orders.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.templateID = new SelectList(db.Templates, "Id", "Name");
            return View();
        }

        // POST: Order/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderId,templateID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                Template template = db.Templates.Find(order.templateID);
                foreach (var x in template.Settings)
                {
                    
                    DynamicValue value = new DynamicValue();
                    value.templateID = order.templateID;
                    value.settingId = x.Id;
                    value.settingName = x.Name;
                    value.dynamicValue = x.DefaultValue;
                    value.order = order;
                    //value.orderId = ID;
                    db.dynamicValues.Add(value);
                    db.SaveChanges();
                    Debug.Write(value.Id);

                }
                return RedirectToAction("Additional", new { id = order.orderId });

                db.SaveChanges();
            }

            ViewBag.templateID = new SelectList(db.Templates, "Id", "Name", order.templateID);
            return View(order);
        }

        // GET: DynamicValues/Create
        public ActionResult Additional(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);
            var changeV = new List<DynamicValue>();
            foreach (var v in order.DynamicValues)
            {
                changeV.Add(v);
            }

            return View(changeV.ToList());
        }
       

        [HttpPost, ActionName("Additional")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(List<DynamicValue> dynamics)
        {
            foreach (var dv in dynamics)
            {
                int? id = dv.Id;
                DynamicValue dynamic = db.dynamicValues.Find(id);
                if (ModelState.IsValid)
                {
                    dynamic.dynamicValue = dv.dynamicValue;
                    db.Entry(dynamic).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.templateID = new SelectList(db.Orders, "orderId", "orderId", dynamic.OrderId);
            }


            return RedirectToAction("Index");
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.templateID = new SelectList(db.Templates, "Id", "Name", order.templateID);
            return View(order);
        }

        // POST: Order/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderId,templateID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.templateID = new SelectList(db.Templates, "Id", "Name", order.templateID);
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            var dvalues = new List<DynamicValue>();
            foreach (var x in order.DynamicValues)
            {
                dvalues.Add(db.dynamicValues.Find(x.Id));
            }
            foreach (var y in dvalues)
            {
                db.dynamicValues.Remove(y);
                db.SaveChanges();
            }

            db.Orders.Remove(order);
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
