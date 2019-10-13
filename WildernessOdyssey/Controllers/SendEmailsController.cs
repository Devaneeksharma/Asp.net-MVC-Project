using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WildernessOdyssey.Models;
using WildernessOdyssey.Util;
using System.IO;

namespace WildernessOdyssey.Controllers
{
    public class SendEmailsController : Controller
    {
        private WildernessModelContainer db = new WildernessModelContainer();

        // GET: SendEmails
        public ActionResult Index()
        {
            return View(db.SendEmails.ToList());
        }

        // GET: SendEmails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendEmail sendEmail = db.SendEmails.Find(id);
            if (sendEmail == null)
            {
                return HttpNotFound();
            }
            return View(sendEmail);
        }

        // GET: SendEmails/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SendEmails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ToEmail,Subject,Content,Path")] SendEmail sendEmail, HttpPostedFileBase addAttachment)
        {

            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            sendEmail.Path = myUniqueFileName;
            TryValidateModel(sendEmail);

            if (ModelState.IsValid)
            {         

                try
                {
                    String toEmail = sendEmail.ToEmail;
                    String subject = sendEmail.Subject;
                    String contents = sendEmail.Content;

                    string path = Server.MapPath("~/Content/email/");
                    string fileExtension = Path.GetExtension(addAttachment.FileName);
                    string filePath = sendEmail.Path + fileExtension;
                    sendEmail.Path = filePath;
                    addAttachment.SaveAs(path + sendEmail.Path);
                    ViewBag.Message = "File uploaded successfully.";


                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents, path+sendEmail.Path, sendEmail.Path);

                    ViewBag.Result = "Email has been send.";

                    //ModelState.Clear();
                    db.SendEmails.Add(sendEmail);
                    db.SaveChanges();
                    //return View(new SendEmail());
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }


               
                
            }

            return View(sendEmail);
        }

        // GET: SendEmails/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendEmail sendEmail = db.SendEmails.Find(id);
            if (sendEmail == null)
            {
                return HttpNotFound();
            }
            return View(sendEmail);
        }

        // POST: SendEmails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ToEmail,Subject,Content")] SendEmail sendEmail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sendEmail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sendEmail);
        }

        // GET: SendEmails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendEmail sendEmail = db.SendEmails.Find(id);
            if (sendEmail == null)
            {
                return HttpNotFound();
            }
            return View(sendEmail);
        }

        // POST: SendEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SendEmail sendEmail = db.SendEmails.Find(id);
            db.SendEmails.Remove(sendEmail);
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
