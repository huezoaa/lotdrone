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
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private EmployeesRepository repository;  //AH:  will use this for our Mock test

 //**       public EmployeesController(EmployeesRepository repository) //AH: constructor used for Unit test.
//**       {
//**            this.repository = repository;
//**        }

        // GET: Employees
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Index()     //AH:  This is  the original line
 //**  public ViewResult Index() //AH:   type ViewResult  for the unit test.
        {
//**    var employees = repository.GetAll();  //AH:  for Unit test
//**    return View(employees);                       //AH: for unit test
          return View(db.Employees.ToList()); //AH: This is  the original line.
        }

        // GET: Employees/Details/5
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Create([Bind(Include = "EmployeeID,LastName,FirstName,Email,Extension")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Edit([Bind(Include = "EmployeeID,LastName,FirstName,Email,Extension")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
