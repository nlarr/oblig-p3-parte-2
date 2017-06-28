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

        // GET: Financiadors/Create
        public ActionResult Create()
        {
            ViewBag.Mensaje = "";
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
                ViewBag.Mensaje = "";

                var usuarioExistente = db.Usuarios.Where(u => u.Email == financiador.Email).SingleOrDefault();

                if(usuarioExistente != null)
                {
                    ViewBag.Mensaje = "El mail utilizado para registrarse ya fue utilizado.";
                }
                else
                {
                    db.Usuarios.Add(financiador);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Emprendimientoes");
                }
            }

            return View(financiador);
        }

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
