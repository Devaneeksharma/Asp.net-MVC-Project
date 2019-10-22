using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WildernessOdyssey.Models;
using Microsoft.AspNet.Identity;

namespace WildernessOdyssey.Controllers
{
    public class UsersBookingsController : Controller
    {
        private WildernessModelContainer db = new WildernessModelContainer();

        // GET: UsersBookings
        public ActionResult Index()
        {
            var usersBookings = db.UsersBookings.Include(u => u.Trip).Include(u => u.AspNetUser);
            return View(usersBookings.ToList());
        }

        // GET: UsersBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersBooking usersBooking = db.UsersBookings.Find(id);
            if (usersBooking == null)
            {
                return HttpNotFound();
            }
            return View(usersBooking);
        }

        // GET: UsersBookings/Create
        public ActionResult Create()
        {
            ViewBag.TripsTripId = new SelectList(db.Trips, "TripId", "TripType");
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: UsersBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,Cost,RattingScale,Comments,TripsTripId,AspNetUserId")] UsersBooking usersBooking)
        {
            if (ModelState.IsValid)
            {
                db.UsersBookings.Add(usersBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TripsTripId = new SelectList(db.Trips, "TripId", "TripType", usersBooking.TripsTripId);
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", usersBooking.AspNetUserId);
            return View(usersBooking);
        }


        // GET: UsersBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersBooking usersBooking = db.UsersBookings.Find(id);
            if (usersBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.TripsTripId = new SelectList(db.Trips, "TripId", "TripType", usersBooking.TripsTripId);
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", usersBooking.AspNetUserId);
            return View(usersBooking);
        }

        // POST: UsersBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,Cost,RattingScale,Comments,TripsTripId,AspNetUserId")] UsersBooking usersBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TripsTripId = new SelectList(db.Trips, "TripId", "TripType", usersBooking.TripsTripId);
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", usersBooking.AspNetUserId);
            return View(usersBooking);
        }

        // GET: UsersBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersBooking usersBooking = db.UsersBookings.Find(id);
            if (usersBooking == null)
            {
                return HttpNotFound();
            }
            return View(usersBooking);
        }

        // POST: UsersBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsersBooking usersBooking = db.UsersBookings.Find(id);
            db.UsersBookings.Remove(usersBooking);
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
