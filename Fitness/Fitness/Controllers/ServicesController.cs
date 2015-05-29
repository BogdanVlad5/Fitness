using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitness.Models;
using System.Net;
using System.Net.Mail;

namespace Fitness.Controllers
{
    public class ServicesController : Controller
    {
        private ServiceDBContext db = new ServiceDBContext();

        //
        // GET: /Services/
        
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        [HttpGet]
        public ActionResult Rezervare(int id)
        {
            var model = getByID(id);
            return View(model);
        }
        public RezervareModel getByID(int id){
            var services= db.Services.FirstOrDefault(service=> service.ID == id);
            var rezervareModel = new RezervareModel
            {
                Name = services.Name
            };

            return rezervareModel;

        }

        [HttpPost]
        public ActionResult Rezervare(RezervareModel model)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("from@gmail.com");
            mail.To.Add(model.Email);
            mail.Subject = "Inscriere curs "+ model.Name;
            mail.Body = model.UName+", te-ai inscris cu succes la cursul de "+model.Name;

            System.Net.Mail.Attachment attachment;
            

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("from@gmail.com", "frompassword");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);


            return RedirectToAction("Index","Home");
        }

        //
        // GET: /Services/Details/5

        public ActionResult Details(int id = 0)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // GET: /Services/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Services/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service)
        {
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        //
        // GET: /Services/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // POST: /Services/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        //
        // GET: /Services/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // POST: /Services/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}