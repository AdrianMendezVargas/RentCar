using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentCar.Entidades {
    public class Cliente {
        [Key]
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public Cliente() {
            ClienteId = 0;
            Nombres = "";
            Apellidos = "";
            Cedula = "";
            Email = "";
            Direccion = "";
            FechaNacimiento = DateTime.Now.Date;
            Telefono = "";
        }

    }
}
