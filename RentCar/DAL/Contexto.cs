using Microsoft.EntityFrameworkCore;
using RentCar.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentCar.DAL {
    public class Contexto : DbContext{

        public DbSet<Cliente> Clientes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            optionsBuilder.UseSqlite("Data source= RentCar.db");
        }

    }
}
