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
        
        // GET: Emprendimientos finaciados por autenticado
        public ActionResult EmprendimientosFinanciados()
        {
            ActionResult ret = View();
            ViewBag.MensajeNoTiene = "";
            int financiadorId = (int)TempData["financiadorId"];
            Financiador financiador = db.Usuarios.Find(financiadorId) as Financiador;            
            var emprendimientosMios = from e in db.Emprendimientos.Include("Financiador") select e;
            emprendimientosMios = emprendimientosMios.Where(e => e.Financiador.Id == financiadorId);
            if (emprendimientosMios.Count() == 0)
            {
                ViewBag.MensajeNoTiene = "No tienes ningun emprendimiento financiado";
            }else
            {
                ret=View(emprendimientosMios.ToList());
            }
            
            return ret;
        }

        // GET: Emprendimientoes
        public ActionResult Index(string sortOrder, string costoMin, string costoMax, string tiempoMax, bool? financiable)
        {
            var emprendimientos = from e in db.Emprendimientos.Include("Financiador") select e;
            ViewBag.MensajeInicializar = "";

            if (emprendimientos.Count() == 0)
            {
                ViewBag.MensajeInicializar = "No hay emprendimientos en el sistema, es necesario ";
            }
            else
            {
                if (!String.IsNullOrEmpty(costoMin))
                {
                    decimal costoMinD;
                    bool costoMinOk = decimal.TryParse(costoMin, out costoMinD);
                    if (costoMinOk)
                    {
                        emprendimientos = emprendimientos.Where(e => e.Costo >= costoMinD);
                    }
                }

                if (!String.IsNullOrEmpty(costoMax))
                {
                    decimal costoMaxD;
                    bool costoMaxOk = decimal.TryParse(costoMax, out costoMaxD);
                    if (costoMaxOk)
                    {
                        emprendimientos = emprendimientos.Where(e => e.Costo <= costoMaxD);
                    }
                }

                if (!String.IsNullOrEmpty(tiempoMax))
                {
                    decimal tiempoMaxD;
                    bool tiempoMaxOk = decimal.TryParse(tiempoMax, out tiempoMaxD);
                    if (tiempoMaxOk)
                    {
                        emprendimientos = emprendimientos.Where(e => e.Tiempo <= tiempoMaxD);
                    }
                }

                if (Session["Usuario"] != null)
                {
                    Financiador f = Session["Usuario"] as Financiador;
                    if (f != null)
                    {
                        if (financiable != null && financiable == true)
                        {
                            emprendimientos = emprendimientos.Where(e => e.Costo <= f.MontoMax && e.Financiador == null);
                        }
                    }
                }

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
        // GET:Mejores  Emprendimientoes
        public ActionResult IndexRankMejores()
        {
            ActionResult ret = View();
            List<Emprendimiento> emprendimientosOrdenados = db.Emprendimientos.OrderByDescending(e => e.PuntajeTotal).ToList();
            int cantEmprend = emprendimientosOrdenados.Count();
            if (cantEmprend == 0)
            {
                ViewBag.MensajeInicializar = "No hay emprendimientos en el sistema, es necesario ";
            }
            else
            {
                if (cantEmprend >= 10) {
                    ret = View(emprendimientosOrdenados.Take(cantEmprend / 10).ToList());
                }else
                {
                    ret = View(emprendimientosOrdenados.Take(1).ToList());
                }
                
            }

            return ret;
        }

        // GET: Emprendimientoes/Details/5
        [HttpGet]

        public ActionResult Details(int? id, string mensaje)
        {
            ViewBag.FinanciarSuccess = "";
            ViewBag.FinanciarError = "";

            if (!String.IsNullOrEmpty(mensaje))
            {
                switch (mensaje)
                {
                    case "success":
                        ViewBag.FinanciarSuccess = "Felicitaciones! Usted es el Financiador de este proyecto";
                        break;
                    case "error_ya_financiado":
                        ViewBag.FinanciarError = "Este Emprendimiento ya cuenta con un Financiador";
                        break;
                    case "error_costo_elevado":
                        ViewBag.FinanciarError = "Usted no puede permitirse el costo de este emprendimiento";
                        break;
                }
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Emprendimiento emprendimiento = db.Emprendimientos.Include("Financiador")
                                            .Where(e => e.Id == id).SingleOrDefault() as Emprendimiento;

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

        public ActionResult Financiar()
        {
            ViewBag.FinanciarSuccess = "";
            ViewBag.FinanciarError = "";

            int financiadorId = (int)TempData["financiadorId"];
            int emprendimientoId = (int)TempData["emprendimientoId"];

            Emprendimiento emprendimiento = db.Emprendimientos.Include("Financiador")
                                            .Where(e => e.Id == emprendimientoId).SingleOrDefault() as Emprendimiento;

            Financiador financiador = db.Usuarios.Find(financiadorId) as Financiador;

            if (emprendimiento == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                if (emprendimiento.Financiador == null) // Lo traigo fresco de la BD para evitar problemas de concurrencia
                {
                    if (emprendimiento.Costo <= financiador.MontoMax)
                    {
                        emprendimiento.Financiador = financiador;
                        db.SaveChanges();
                        return (RedirectToAction("Details", new { id = emprendimientoId, mensaje = "success" }));
                    }
                    else
                    {
                        return (RedirectToAction("Details", new { id = emprendimientoId, mensaje = "error_costo_elevado" }));
                    }
                }
                else
                {
                    return (RedirectToAction("Details", new { id = emprendimientoId, mensaje = "error_ya_financiado" }));
                }
            }
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
