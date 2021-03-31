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
    public class EquipesController : Controller
    {
        private Boehler_FootEntities db = new Boehler_FootEntities();

        // GET: Equipes
        public ActionResult Index()
        {
            var equipes = db.Equipes.Include(e => e.Entraineur).Include(e => e.Niveau);
            return View(equipes.ToList());
        }

        // GET: Equipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipe equipe = db.Equipes.Find(id);
            if (equipe == null)
            {
                return HttpNotFound();
            }
            return View(equipe);
        }

        // GET: Equipes/Create
        public ActionResult Create()
        {
            ViewBag.idEntraineur = new SelectList(db.Entraineurs, "idEntraineur", "Nom");
            ViewBag.idNiveau = new SelectList(db.Niveaux, "idNiveau", "LibelleNiveau");
            return View();
        }

        // POST: Equipes/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEquipe,LibelleEquipe,idEntraineur,idNiveau")] Equipe equipe)
        {
            if (ModelState.IsValid)
            {
                db.Equipes.Add(equipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEntraineur = new SelectList(db.Entraineurs, "idEntraineur", "Nom", equipe.idEntraineur);
            ViewBag.idNiveau = new SelectList(db.Niveaux, "idNiveau", "LibelleNiveau", equipe.idNiveau);
            return View(equipe);
        }

        // GET: Equipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipe equipe = db.Equipes.Find(id);
            if (equipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEntraineur = new SelectList(db.Entraineurs, "idEntraineur", "Nom", equipe.idEntraineur);
            ViewBag.idNiveau = new SelectList(db.Niveaux, "idNiveau", "LibelleNiveau", equipe.idNiveau);
            return View(equipe);
        }

        // POST: Equipes/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEquipe,LibelleEquipe,idEntraineur,idNiveau")] Equipe equipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEntraineur = new SelectList(db.Entraineurs, "idEntraineur", "Nom", equipe.idEntraineur);
            ViewBag.idNiveau = new SelectList(db.Niveaux, "idNiveau", "LibelleNiveau", equipe.idNiveau);
            return View(equipe);
        }

        // GET: Equipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipe equipe = db.Equipes.Find(id);
            if (equipe == null)
            {
                return HttpNotFound();
            }
            return View(equipe);
        }

        // POST: Equipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipe equipe = db.Equipes.Find(id);
            db.Equipes.Remove(equipe);
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
