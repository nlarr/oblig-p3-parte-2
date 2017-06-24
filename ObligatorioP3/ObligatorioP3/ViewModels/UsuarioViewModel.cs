using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ObligatorioP3.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        //public bool Mapear()
        //    { if (this.Email != null) {
                        
        //                // this.UnLibro.NombreArchivoPortada = Archivo.FileName;
        //                //EntreLibrosContext db = new EntreLibrosContext();
        //                //this.UnLibro.MiTema = db.Temas.Find(this.IdTemaSeleccionado);
        //                return true;
        //            }
        //            return false;
        //        }
     }
    
}