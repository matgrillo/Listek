using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Listek.Models;
using Microsoft.AspNet.Identity;

namespace Listek.Controllers
{
    public class ToBuysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToBuys
        [Authorize]
        public ActionResult Index()
        {
            
            return View();
        }

        private IEnumerable<ToBuy> GetMyToBuys()

        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);
            return db.ToBuys.ToList().Where(x => x.User == currentUser);
        }

        public ActionResult BuildToDoTable()
        {
            return PartialView("_ToBuyTable", GetMyToBuys());
        }

        // GET: ToBuys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToBuy toBuy = db.ToBuys.Find(id);
            if (toBuy == null)
            {
                return HttpNotFound();
            }
            return View(toBuy);
        }

        // GET: ToBuys/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Izdelek,Nakup")] ToBuy toBuy)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId(); // dobimo ID uporabnika iz baze
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId); //iščemo uporabnika ki ima enak ID kot v bazi(current)
                toBuy.User = currentUser; //določimo uporabnika v bazi
                db.ToBuys.Add(toBuy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toBuy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "Id,Izdelek")] ToBuy toBuy)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId(); // dobimo ID uporabnika iz baze
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId); //iščemo uporabnika ki ima enak ID kot v bazi(current)
                toBuy.User = currentUser; //določimo uporabnika v bazi
                toBuy.Nakup = false;
                db.ToBuys.Add(toBuy);
                db.SaveChanges();
       
            }

            return PartialView("_ToBuyTable", GetMyToBuys());
        }

        // GET: ToBuys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToBuy toBuy = db.ToBuys.Find(id);


            if (toBuy == null)
            {
                return HttpNotFound();
            }

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            if (toBuy.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           

            return View(toBuy);
        }

        // POST: ToBuys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Izdelek,Nakup")] ToBuy toBuy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toBuy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toBuy);
        }
        // ko kliknemo na checkbox "nakup" pri določenem izdelku
        [HttpPost]
        public ActionResult AJAXEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToBuy toBuy = db.ToBuys.Find(id); //poišče ID
            if (toBuy == null)
            {
                return HttpNotFound();
            }
            else //shrani id v bazo in vrne partial view "_ToBuyTable"
            {
                toBuy.Nakup = value;
                db.Entry(toBuy).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_ToBuyTable", GetMyToBuys());
            }
        }

        // GET: ToBuys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToBuy toBuy = db.ToBuys.Find(id);
            if (toBuy == null)
            {
                return HttpNotFound();
            }
            return View(toBuy);
        }

        // POST: ToBuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToBuy toBuy = db.ToBuys.Find(id);
            db.ToBuys.Remove(toBuy);
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

        public JsonResult Uporabnik()
        {
            string upor = User.Identity.GetUserName();
            return Json(upor, JsonRequestBehavior.AllowGet);
        }

    }
}
