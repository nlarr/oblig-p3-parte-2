using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3.Models
{
    [Table("Financiadores")]
    public class Financiador : Usuario
    {
        [Required]
        public string Organizacion { get; set; }

        [Required]
        [Display(Name = "Monto máximo a financiar")]
        public decimal MontoMax { get; set; }

        public virtual List<Emprendimiento> Emprendimientos { get; set; }

        public Financiador()
        {
            this.Rol = "Financiador";
        }
    }
}