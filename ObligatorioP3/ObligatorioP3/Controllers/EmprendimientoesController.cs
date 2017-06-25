using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ObligatorioP3.Models;
using System.IO;

namespace ObligatorioP3.Controllers
{
    public class EmprendimientoesController : Controller
    {
        private ObliEmprendimientosContext db = new ObliEmprendimientosContext();

        // GET: Emprendimientoes
        public ActionResult Index(string sortOrder)
        {
            var emprendimientos = from e in db.Emprendimientos select e;

            if (emprendimientos.Count() == 0)
            {
                ViewBag.MensajeInicializar = "No hay emprendimientos en el sistema, es necesario ";
            }
            else
            {
                ViewBag.CostoSortParm = sortOrder == "costo_asc" ? "costo_desc" : "costo_asc";

                switch (sortOrder)
                {
                    case "costo_asc":
                        emprendimientos = emprendimientos.OrderBy(e => e.Costo);
                        break;
                    case "costo_desc":
                        emprendimientos = emprendimientos.OrderByDescending(e => e.Costo);
                        break;
                }
            }

            return View(emprendimientos.ToList());
        }

        // GET: Emprendimientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprendimiento emprendimiento = db.Emprendimientos.Find(id);
            if (emprendimiento == null)
            {
                return HttpNotFound();
            }
            return View(emprendimiento);
        }

        public ActionResult Inicializar()
        {
            string path = Server.MapPath("~/ArchivoTexto/WCFarchivo.log");
            StreamReader sReader = new StreamReader(path);
            string linea = "";

            while ((linea = sReader.ReadLine()) != null)
            {
                string[] lineaSplit = linea.Split('#');

                decimal costo = 0;
                bool okCosto = decimal.TryParse(lineaSplit[2], out costo);
                int duracion = 0;
                bool okDuracion = int.TryParse(lineaSplit[3], out duracion);
                int puntajeTotal = 0;
                bool okPuntaje = int.TryParse(lineaSplit[4], out puntajeTotal);

                if (okCosto && okDuracion && okPuntaje)
                {
                    Emprendimiento emprendimiento = new Emprendimiento
                    {
                        Titulo = lineaSplit[1],
                        Costo = costo,
                        Tiempo = duracion,
                        PuntajeTotal = puntajeTotal,
                        Descripcion = lineaSplit[5]
                    };
                    db.Emprendimientos.Add(emprendimiento);
                }
            }
            sReader.Close();
            db.SaveChanges();

            return (RedirectToAction("Index"));
        }

        // Pendiente: condiciones para financiar: usuario loggueado como financiador y monto accesible
        public ActionResult Financiar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprendimiento emprendimiento = db.Emprendimientos.Find(id);
            if (emprendimiento == null)
            {
                return HttpNotFound();
            }
            return View(emprendimiento);
        }

        // Este método se generó solo, así que por las dudas lo dejo
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
