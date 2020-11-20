using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentCar.Migrations
{
    public partial class migracion : Migration
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
                name: "Importadores",
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
                    table.PrimaryKey("PK_Importadores", x => x.ImportadorId);
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
                    ImportadorId = table.Column<int>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Polizas",
                columns: table => new
                {
                    PolizaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VehiculoId = table.Column<int>(nullable: false),
                    MontoAsegurado = table.Column<decimal>(nullable: false),
                    FechaInicial = table.Column<DateTime>(nullable: false),
                    FechaFinal = table.Column<DateTime>(nullable: false),
                    FechaLimitePago = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polizas", x => x.PolizaId);
                    table.ForeignKey(
                        name: "FK_Polizas_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_VehiculoId",
                table: "Polizas",
                column: "VehiculoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Importadores");

            migrationBuilder.DropTable(
                name: "Polizas");

            migrationBuilder.DropTable(
                name: "Rentas");

            migrationBuilder.DropTable(
                name: "salidaVehiculos");

            migrationBuilder.DropTable(
                name: "Vehiculos");
        }
    }
}
