using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ObligatorioP3.Models;

namespace ObligatorioP3.Controllers
{
    public class FinanciadorsController : Controller
    {
        private ObliEmprendimientosContext db = new ObliEmprendimientosContext();

        //// GET: Financiadors/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Financiador financiador = db.Usuarios.Find(id);
        //    if (financiador == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(financiador);
        //}

        // GET: Financiadors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Financiadors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Password,Rol,Organizacion,MontoMax")] Financiador financiador)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(financiador);
                db.SaveChanges();
                return RedirectToAction("Index", "Emprendimientoes");
            }

            return View(financiador);
        }

        //// GET: Financiadors/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Financiador financiador = db.Usuarios.Find(id);
        //    if (financiador == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(financiador);
        //}

        //// POST: Financiadors/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Email,Password,Rol,Organizacion,MontoMax")] Financiador financiador)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(financiador).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(financiador);
        //}

        // Metodo autogenerado
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
