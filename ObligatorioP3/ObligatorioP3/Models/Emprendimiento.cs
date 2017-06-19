using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3.Models
{
    public class Emprendimiento
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public decimal Costo { get; set; }

        [Required]
        public int Tiempo { get; set; }

        public int PuntajeTotal { get; set; }

        public Financiador Financiador { get; set; }
    }
}