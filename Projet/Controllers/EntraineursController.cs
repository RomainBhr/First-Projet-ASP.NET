using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet;

namespace Projet.Controllers
{
    public class EntraineursController : Controller
    {
        private Boehler_FootEntities db = new Boehler_FootEntities();

        // GET: Entraineurs
        public ActionResult Index()
        {
            return View(db.Entraineurs.ToList());
        }

        // GET: Entraineurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entraineur entraineur = db.Entraineurs.Find(id);
            if (entraineur == null)
            {
                return HttpNotFound();
            }
            return View(entraineur);
        }

        // GET: Entraineurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entraineurs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEntraineur,Nom,Prenom,Numero,Email,DateDeNaissance")] Entraineur entraineur)
        {
            if (ModelState.IsValid)
            {
                db.Entraineurs.Add(entraineur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entraineur);
        }

        // GET: Entraineurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entraineur entraineur = db.Entraineurs.Find(id);
            if (entraineur == null)
            {
                return HttpNotFound();
            }
            return View(entraineur);
        }

        // POST: Entraineurs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEntraineur,Nom,Prenom,Numero,Email,DateDeNaissance")] Entraineur entraineur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entraineur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entraineur);
        }

        // GET: Entraineurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entraineur entraineur = db.Entraineurs.Find(id);
            if (entraineur == null)
            {
                return HttpNotFound();
            }
            return View(entraineur);
        }

        // POST: Entraineurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entraineur entraineur = db.Entraineurs.Find(id);
            db.Entraineurs.Remove(entraineur);
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
