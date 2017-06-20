using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObligatorioP3.Models;

namespace ObligatorioP3.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(ObligatorioP3.ViewModels.UsuarioViewModel miUsuario)
        {
            try
            {
                using (ObliEmprendimientosContext db = new ObliEmprendimientosContext()) {
                    var usuario = db.Usuarios.Where(u => u.Email == miUsuario.Email && u.Password == miUsuario.Password);
                    return View();
                }
                //return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        // GET: Usuario/Edit/5
        public ActionResult Login()
        {
            return View();
        }

        //// GET: Usuario/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Usuario/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Usuario/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        // POST: Usuario/SearchUsuario

        //// GET: Usuario/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////POST: Usuario/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Usuario/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Usuario/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
