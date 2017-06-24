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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ObligatorioP3.ViewModels.UsuarioViewModel miUsuario)
        {
            ActionResult ret = View();
            ViewBag.ErrorLogin = "";

            if (ModelState.IsValid)
            {
                try
                {
                    using (ObliEmprendimientosContext db = new ObliEmprendimientosContext())
                    {

                        var usuario = db.Usuarios.Where(u => u.Email == miUsuario.Email && u.Password == miUsuario.Password)
                                        .SingleOrDefault();

                        if (usuario != null) // Si la query trajo algo
                        {
                            if (usuario.Rol == "Financiador")
                            {
                                Financiador f = usuario as Financiador;

                                if (f != null) // Si no falló al intentar el casteo como Financiador
                                {
                                    Session["usuario"] = f;
                                    ret = RedirectToAction("Index", "Emprendimientoes");
                                }
                            }
                        }
                        else
                        {
                            ViewBag.ErrorLogin = "Usuario o contraseña inválidos.";
                        }
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return ret;
        }

        public ActionResult Logout()
        {
            Session["usuario"] = null;
            return RedirectToAction("Index", "Emprendimientoes");
        }
    }
}
