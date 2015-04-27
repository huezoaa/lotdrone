using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using lotdrone.Models;
using Postal;

namespace lotdrone.Controllers
{
    public class NotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Notifications
        //public ActionResult Index()
        //{
        //    return View(db.Notifications.ToList());
        //}
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Index(string searchString)
        {
            //string searchString = id;
            var notes = from n in db.Notifications
                       select n;

            if (!String.IsNullOrEmpty(searchString))
            {
                notes = notes.Where(s => s.LicensePlate.Contains(searchString));
            }
            return View(notes);
        }

        // GET: Notifications/Details/5
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification Notification = db.Notifications.Find(id);
            if (Notification == null)
            {
                return HttpNotFound();
            }
            return View(Notification);
        }

        // GET: Notifications/Create
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Create([Bind(Include = "NotificationID,TimeStamp,Description,LicensePlate,Status")] Notification Notification)
        {
            if (ModelState.IsValid)
            {
                db.Notifications.Add(Notification);
                db.SaveChanges();

                string searchString = Notification.LicensePlate; //AH - notification email - will use this string to search for LicensePlate
                IQueryable<int> cars; //AH - notification email - will use this to read from car db
                IQueryable<string> carowner; //AH - notification email - will use this to get email address of carowner

                cars = from c in db.Cars   //AH - notification email - code to compare entry to LicensePlate
                       where c.LicensePlate == searchString //AH - notification email - code to compare entry to LicensePlate
                       select c.EmployeeID; //AH - notification email - code to compare entry to LicensePlate. Result is EmployeeID

                carowner = from x in db.Employees   //AH - notification email - code to obtain employee email via license plate
                           where x.EmployeeID == cars.FirstOrDefault() //AH - notification email - code to obtain employee email via license plate
                           select x.Email; //AH - notification email - code to obtain employee email via license plate


                if (!String.IsNullOrEmpty(carowner.FirstOrDefault())) //AH - notification email - carowner could be null...
                {
                    var email = new NewNotificationEmail  //AH - notification email
                     {
                         To = carowner.FirstOrDefault(),                    //AH - notification email
                         LicensePlate = Notification.LicensePlate, //AH - notification email
                         Notification = Notification.Description        //AH - notification email
                     };

                    email.Send();                                                      //AH - notification email

                }
                else
                {
                    var email = new NewNotificationEmail            //AH - notification email
                    {
                        To = "AH.development.miami@gmail.com",//AH - notification email
                        LicensePlate = "ERROR ERROR!",              //AH - notification email
                        Notification = "License Plate Number not found: " + Notification.LicensePlate, //AH - notification email
                    };

                    email.Send();                                                       //AH - notification email

                };
           

                return RedirectToAction("Index");
            }

            return View(Notification);
        }

        // GET: Notifications/Edit/5
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification Notification = db.Notifications.Find(id);
            if (Notification == null)
            {
                return HttpNotFound();
            }
            return View(Notification);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Edit([Bind(Include = "NotificationID,TimeStamp,Description,LicensePlate,Status")] Notification Notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Notification);
        }

        // GET: Notifications/Delete/5
        [Authorize(Roles = "canEdit")]  //AH: Adding security to this method.
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification Notification = db.Notifications.Find(id);
            if (Notification == null)
            {
                return HttpNotFound();
            }
            return View(Notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification Notification = db.Notifications.Find(id);
            db.Notifications.Remove(Notification);
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
