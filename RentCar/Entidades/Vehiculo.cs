using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentCar.Entidades
{
    public class Vehiculo
    {
        [Key]
        public int VehiculoId { get; set; }
        public int PolizaId { get; set; }
        public string Matricula { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public DateTime AñoFabricacion { get; set; }
        public string Estado { get; set; }
        public string Precio { get; set; }
        public string Chassis { get; set; }
        public string Pasajeros { get; set; }
        public string Puertas { get; set; }
        public string Traccion { get; set; }
        public string Comentario { get; set; }
        public string Valor { get; set; }
        public string Tipo { get; set; }

        public Vehiculo()
        {
            VehiculoId = 0;
            PolizaId = 0;
            Matricula = "";
            Marca = "";
            Modelo = "";
            Placa = "";
            AñoFabricacion = DateTime.Now;
            Estado = "";
            Precio = "";
            Chassis = "";
            Pasajeros = "";
            Puertas = "";
            Traccion = "";
            Comentario = "";
            Valor = "";
            Tipo = "";


        }

    }
}
