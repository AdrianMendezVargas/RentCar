using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentCar.Entidades {
    public class Poliza {

        [Key]
        public int PolizaId { get; set; }
        public int VehiculoId { get; set; }
        public decimal MontoAsegurado { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaLimitePago { get; set; }
        public virtual string FechaFinalCorta => FechaFinal.ToShortDateString();
        public virtual string FechaInicialCorta => FechaInicial.ToShortDateString();
        public virtual string MontoAseguradoFormateado => string.Format("{0:c}", MontoAsegurado);
        public virtual bool EstaVencida => DateTime.Today > FechaFinal;
        public virtual string Estado => EstaVencida ? "Vencida" : "Vigente";
    }
}
