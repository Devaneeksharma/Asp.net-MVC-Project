﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WildernessOdyssey.Models;
using System.IO;

namespace WildernessOdyssey.Controllers
{
    public class TripsController : Controller
    {
        private WildernessModelContainer db = new WildernessModelContainer();

        // GET: Trips
        public ActionResult Index()
        {
            return View(db.Trips.ToList());
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trips trips = db.Trips.Find(id);
            if (trips == null)
            {
                return HttpNotFound();
            }
            return View(trips);
        }

        // GET: Trips/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TripId,TripType,TripName,TripLocation,Duration,StartDate,EndDate,Path")] Trips trips,HttpPostedFileBase postedFile)
        {

            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            trips.Path = myUniqueFileName;
            TryValidateModel(trips);

            if (ModelState.IsValid)
            {
                if (postedFile != null)
                {                    
                    string path = Server.MapPath("~/Content/images/");
                    string fileExtension = Path.GetExtension(postedFile.FileName);
                    string filePath = trips.Path + fileExtension;
                    trips.Path = filePath;
                    postedFile.SaveAs(path + trips.Path);
                    ViewBag.Message = "File uploaded successfully.";
                }

                db.Trips.Add(trips);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trips);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trips trips = db.Trips.Find(id);
            if (trips == null)
            {
                return HttpNotFound();
            }
            return View(trips);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TripId,TripType,TripName,TripLocation,Duration,StartDate,EndDate,Path")] Trips trips)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trips).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trips);
        }

        [Authorize(Roles = "admin")]
        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trips trips = db.Trips.Find(id);
            if (trips == null)
            {
                return HttpNotFound();
            }
            return View(trips);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trips trips = db.Trips.Find(id);
            db.Trips.Remove(trips);
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



        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase postedFile)
        //{
           
        //    return View();
        //}

    }
}
