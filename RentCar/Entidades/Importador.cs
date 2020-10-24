using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentCar.Entidades
{
   public  class Importador
    {
        [Key]

        public int ImportadorId { get; set; }
        public string Nombre { get; set; }
        public string PaginaWeb { get; set; }
        public string Telefono { get; set; }
    }
}
