using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentCar.Entidades {
    public class Renta {

        [Key]
        public int RentaId { get; set; }

        [Required]
        public int VehiculoId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public DateTime FechaInicial { get; set; }

        [Required]
        public DateTime FechaFinal { get; set; }

        [Required]
        public decimal MontoTotal { get; set; }

        public virtual bool Activa => DateTime.Now < FechaFinal;


    }
}
