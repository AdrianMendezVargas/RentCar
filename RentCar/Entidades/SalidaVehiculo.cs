using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentCar.Entidades
{
    public class SalidaVehiculo
    {
        [Key]
        public int SalidaId { get; set; }
        public int VehiculoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Kilometraje { get; set; }
        public string Nombres { get; set; }
        public decimal PrecioDia { get; set; }
        public string RazonSalida { get; set; }
        public string Comentario { get; set; }

        public SalidaVehiculo()
        {
            SalidaId = 0;
            VehiculoId = 0;
            ClienteId = 0;
            Fecha = DateTime.Now;
            Marca = "";
            Modelo = "";
            Kilometraje = 0;
            Nombres = "";
            PrecioDia = 0m;
            RazonSalida = "";
            Comentario = "";
        }


    }
}
