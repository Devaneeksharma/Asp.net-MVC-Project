using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WildernessOdyssey.Models;
using System.IO;

using Microsoft.AspNet.Identity;
using WildernessOdyssey.Util;

namespace WildernessOdyssey.Controllers
{
    public class TripsController : Controller
    {
        private WildernessModelContainer db = new WildernessModelContainer();

        
        // GET: Trips
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null & endDate != null)
            {
                var tripFil = from d in db.Trips
                              select d;
                var result = tripFil.Where(s => s.StartDate >= startDate && s.EndDate <= endDate);
                return View(result);
            }

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
        public ActionResult Create([Bind(Include = "TripId,TripType,TripName,TripLocation,Duration,StartDate,EndDate,Path,MapDesc,Longitude,Latitude,MapDesCription")] Trips trips,HttpPostedFileBase postedFile,bool chkBulk)
        {
            var contents = string.Empty;
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
                    while (true)
                    {
                        if (fileExtension != ".mdb" && fileExtension != ".xml" && fileExtension != ".exe" && fileExtension != ".js")
                        {
                            string filePath = trips.Path + fileExtension;
                            trips.Path = filePath;
                            postedFile.SaveAs(path + trips.Path);
                            ViewBag.UploadMessage = "File uploaded successfully.";
                            break;
                        }
                        else
                        {
                            ViewBag.UploadMessage = fileExtension + " is not allowed. Please try again.";
                            return View(trips);
                        }
                   }

                    if (chkBulk)
                    {
                       contents = "Hi! New trip-" + trips.TripName + " is organised at " + trips.TripLocation + " from " + trips.StartDate.ToString() + " To " + trips.EndDate.ToString();
                       var emailListUsers = db.AspNetUsers.Where(s => s.Email != null).Select(s=>s.Email).ToList();
                        
                        foreach(var em in emailListUsers)
                        {
                            EmailSender es = new EmailSender();
                            es.Send(em, "New Trip at Wilderness Odyssey", contents, path + trips.Path, trips.Path);

                            ViewBag.ResultEmail = "Bulk Email has been send sucessfully.";  


                        }
                    }
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
        public ActionResult Edit([Bind(Include = "TripId,TripType,TripName,TripLocation,Duration,StartDate,EndDate,Path,MapDesc,Longitude,Latitude,MapDesCription")] Trips trips, HttpPostedFileBase postedFile)
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

        public ActionResult Booking(int id)
        {
            var user = User.Identity.GetUserId();
            if (user != null)
            {
                try
                {
                    Trips trips = db.Trips.Find(id);
                    var tripIDBooked = db.UsersBookings.Where(u => u.TripsTripId == trips.TripId && u.AspNetUserId == user).ToList();
                    if (tripIDBooked.Count==0)
                    {
                        UsersBooking userBooking = new UsersBooking();
                        userBooking.BookingId = new int();
                        userBooking.AspNetUserId = user;
                        userBooking.TripsTripId = id;
                        userBooking.Cost = "2000";
                        if (trips != null)
                        {
                            userBooking.EndDate = trips.EndDate;
                        }
                        userBooking.Comments = "";
                        userBooking.RattingScale = "";
                        db.UsersBookings.Add(userBooking);
                        db.SaveChanges();
                        ViewBag.Message = "Thank you! Your Booking is confirmed.";
                    }
                    else
                    {
                        ViewBag.Message = "You are already booked to this trip. please Try again!";
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dve)
                {
                    ViewBag.Message = "Something went wrong. Please try again.";

                }
            }
            else { ViewBag.Message = "Please log In to book with us."; }
            return View("Index",db.Trips.ToList());
        }


         // GET: Trips
        public ActionResult TripMap(DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null & endDate != null)
            {
                var tripFil = from d in db.Trips
                              select d;
                var result = tripFil.Where(s => s.StartDate >= startDate && s.EndDate <= endDate);
                return View(result);
            }

            return View(db.Trips.ToList());
        }

    }
}
