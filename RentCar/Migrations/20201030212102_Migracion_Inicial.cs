using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentCar.Migrations
{
    public partial class Migracion_Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombres = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    Cedula = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Importador",
                columns: table => new
                {
                    ImportadorId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: true),
                    PaginaWeb = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Importador", x => x.ImportadorId);
                });

            migrationBuilder.CreateTable(
                name: "Rentas",
                columns: table => new
                {
                    RentaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VehiculoId = table.Column<int>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false),
                    FechaInicial = table.Column<DateTime>(nullable: false),
                    FechaFinal = table.Column<DateTime>(nullable: false),
                    MontoTotal = table.Column<decimal>(nullable: false),
                    Eliminada = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentas", x => x.RentaId);
                });

            migrationBuilder.CreateTable(
                name: "salidaVehiculos",
                columns: table => new
                {
                    SalidaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VehiculoId = table.Column<int>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    Kilometraje = table.Column<int>(nullable: false),
                    Nombres = table.Column<string>(nullable: true),
                    PrecioDia = table.Column<decimal>(nullable: false),
                    RazonSalida = table.Column<string>(nullable: true),
                    Comentario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salidaVehiculos", x => x.SalidaId);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    VehiculoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PolizaId = table.Column<int>(nullable: false),
                    Matricula = table.Column<string>(nullable: true),
                    Placa = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    AñoFabricacion = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    PrecioDia = table.Column<decimal>(nullable: false),
                    Kilometraje = table.Column<int>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Chassis = table.Column<string>(nullable: true),
                    Pasajeros = table.Column<string>(nullable: true),
                    Puertas = table.Column<string>(nullable: true),
                    Traccion = table.Column<string>(nullable: true),
                    Comentario = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    Tipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.VehiculoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Importador");

            migrationBuilder.DropTable(
                name: "Rentas");

            migrationBuilder.DropTable(
                name: "salidaVehiculos");

            migrationBuilder.DropTable(
                name: "Vehiculos");
        }
    }
}
