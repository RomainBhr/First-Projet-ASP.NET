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
    public class EntrainementsController : Controller
    {
        private Boehler_FootEntities db = new Boehler_FootEntities();

        // GET: Entrainements
        public ActionResult Index()
        {
            var entrainements = db.Entrainements.Include(e => e.TypeEntrainement).Include(e => e.Entraineur).Include(e => e.Equipe);
            return View(entrainements.ToList());
        }

        // GET: Entrainements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrainement entrainement = db.Entrainements.Find(id);
            if (entrainement == null)
            {
                return HttpNotFound();
            }
            return View(entrainement);
        }

        // GET: Entrainements/Create
        public ActionResult Create()
        {
            ViewBag.idEntrainement_1 = new SelectList(db.TypeEntrainements, "idEntrainement", "Libelle");
            ViewBag.idEntraineur = new SelectList(db.Entraineurs, "idEntraineur", "Nom");
            ViewBag.idEquipe = new SelectList(db.Equipes, "idEquipe", "LibelleEquipe");
            return View();
        }

        // POST: Entrainements/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEntrainement,DateEntrainement,HeureDebut,HeureFin,idEntrainement_1,idEntraineur,idEquipe")] Entrainement entrainement)
        {
            if (ModelState.IsValid)
            {
                db.Entrainements.Add(entrainement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEntrainement_1 = new SelectList(db.TypeEntrainements, "idEntrainement", "Libelle", entrainement.idEntrainement_1);
            ViewBag.idEntraineur = new SelectList(db.Entraineurs, "idEntraineur", "Nom", entrainement.idEntraineur);
            ViewBag.idEquipe = new SelectList(db.Equipes, "idEquipe", "LibelleEquipe", entrainement.idEquipe);
            return View(entrainement);
        }

        // GET: Entrainements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrainement entrainement = db.Entrainements.Find(id);
            if (entrainement == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEntrainement_1 = new SelectList(db.TypeEntrainements, "idEntrainement", "Libelle", entrainement.idEntrainement_1);
            ViewBag.idEntraineur = new SelectList(db.Entraineurs, "idEntraineur", "Nom", entrainement.idEntraineur);
            ViewBag.idEquipe = new SelectList(db.Equipes, "idEquipe", "LibelleEquipe", entrainement.idEquipe);
            return View(entrainement);
        }

        // POST: Entrainements/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEntrainement,DateEntrainement,HeureDebut,HeureFin,idEntrainement_1,idEntraineur,idEquipe")] Entrainement entrainement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entrainement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEntrainement_1 = new SelectList(db.TypeEntrainements, "idEntrainement", "Libelle", entrainement.idEntrainement_1);
            ViewBag.idEntraineur = new SelectList(db.Entraineurs, "idEntraineur", "Nom", entrainement.idEntraineur);
            ViewBag.idEquipe = new SelectList(db.Equipes, "idEquipe", "LibelleEquipe", entrainement.idEquipe);
            return View(entrainement);
        }

        // GET: Entrainements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrainement entrainement = db.Entrainements.Find(id);
            if (entrainement == null)
            {
                return HttpNotFound();
            }
            return View(entrainement);
        }

        // POST: Entrainements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entrainement entrainement = db.Entrainements.Find(id);
            db.Entrainements.Remove(entrainement);
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
