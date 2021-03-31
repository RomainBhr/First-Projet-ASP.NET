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
    public class AdherentsController : Controller
    {
        private Boehler_FootEntities db = new Boehler_FootEntities();

        // GET: Adherents
        public ActionResult Index()
        {
            var adherents = db.Adherents.Include(a => a.Niveau);
            return View(adherents.ToList());
        }

        // GET: Adherents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = db.Adherents.Find(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            return View(adherent);
        }

        // GET: Adherents/Create
        public ActionResult Create()
        {
            ViewBag.idNiveau = new SelectList(db.Niveaux, "idNiveau", "LibelleNiveau");
            return View();
        }

        // POST: Adherents/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMembre,Nom,Prenom,DateDeNaissance,Adresse,Ville,Numero,Email,idNiveau")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Adherents.Add(adherent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idNiveau = new SelectList(db.Niveaux, "idNiveau", "LibelleNiveau", adherent.idNiveau);
            return View(adherent);
        }

        // GET: Adherents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = db.Adherents.Find(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            ViewBag.idNiveau = new SelectList(db.Niveaux, "idNiveau", "LibelleNiveau", adherent.idNiveau);
            return View(adherent);
        }

        // POST: Adherents/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMembre,Nom,Prenom,DateDeNaissance,Adresse,Ville,Numero,Email,idNiveau")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adherent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idNiveau = new SelectList(db.Niveaux, "idNiveau", "LibelleNiveau", adherent.idNiveau);
            return View(adherent);
        }

        // GET: Adherents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = db.Adherents.Find(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            return View(adherent);
        }

        // POST: Adherents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adherent adherent = db.Adherents.Find(id);
            db.Adherents.Remove(adherent);
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
