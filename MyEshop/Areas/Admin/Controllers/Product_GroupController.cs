using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyEshop.Areas.Admin.Controllers
{
    public class Product_GroupController : Controller
    {
        private MyEshop_DBEntities db = new MyEshop_DBEntities();

        // GET: Admin/Product_Group
        public ActionResult Index()
        {
            var product_Group = db.Product_Group.Where(p => p.ParentID==null);
            return View(product_Group.ToList());
        }

        // GET: Admin/Product_Group/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Group product_Group = db.Product_Group.Find(id);
            if (product_Group == null)
            {
                return HttpNotFound();
            }
            return View(product_Group);
        }

        // GET: Admin/Product_Group/Create
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.Product_Group, "GroupID", "GroupTitle");
            return View();
        }

        // POST: Admin/Product_Group/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle,ParentID")] Product_Group product_Group)
        {
            if (ModelState.IsValid)
            {
                db.Product_Group.Add(product_Group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentID = new SelectList(db.Product_Group, "GroupID", "GroupTitle", product_Group.ParentID);
            return View(product_Group);
        }

        // GET: Admin/Product_Group/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Group product_Group = db.Product_Group.Find(id);
            if (product_Group == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = new SelectList(db.Product_Group, "GroupID", "GroupTitle", product_Group.ParentID);
            return View(product_Group);
        }

        // POST: Admin/Product_Group/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle,ParentID")] Product_Group product_Group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(db.Product_Group, "GroupID", "GroupTitle", product_Group.ParentID);
            return View(product_Group);
        }

        // GET: Admin/Product_Group/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Group product_Group = db.Product_Group.Find(id);
            if (product_Group == null)
            {
                return HttpNotFound();
            }
            return View(product_Group);
        }

        // POST: Admin/Product_Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Group product_Group = db.Product_Group.Find(id);
            db.Product_Group.Remove(product_Group);
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
