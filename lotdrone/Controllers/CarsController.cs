using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lotdrone.Models;

namespace lotdrone.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cars
        //public ActionResult Index()
        //{
        //    return View(db.Cars.ToList());
        //}

         [Authorize(Roles = "canEdit")]
        public ActionResult Index(string id)
        {
            string searchString = id;
            var cars = from c in db.Cars
                         select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(s => s.LicensePlate.Contains(searchString));
            }
            return View(cars);
        }


        // GET: Cars/Details/5
         [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
         [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Create()
        {
            var EmployeeLst = new List<int>();                //AH:  will use this for dropdown list of employees
            var EmployeeQry = from e in db.Employees      //AH:  will use this for dropdown list of employees    
                              orderby e.LastName                           //AH:  will use this for dropdown list of employees
                              //select e.FirstName + " " + e.LastName;//AH:  will use this for dropdown list of employees
                              select e.EmployeeID; //AH:  will use this for dropdown list of employees

            EmployeeLst.AddRange(EmployeeQry.Distinct()); //AH:  will use this for dropdown list of employees
            ViewBag.LaLista = new SelectList(EmployeeLst); //AH:  will use this for dropdown list of employees

             
             return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Create([Bind(Include = "CarID,Make,Model,Year,Color,LicensePlate,EmployeeID")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Edit([Bind(Include = "CarID,Make,Model,Year,Color,LicensePlate,EmployeeID")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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
