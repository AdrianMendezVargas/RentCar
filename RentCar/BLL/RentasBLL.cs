using Microsoft.EntityFrameworkCore;
using RentCar.DAL;
using RentCar.Entidades;
using RentCar.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.BLL {
    public class RentasBLL {         //TODO: Cuando la renta no este activa, liberar el vehiculo
        public static object VehiculosBLL { get; private set; }

        public async static Task<bool> Guardar(Renta renta) {
            if (!await Existe(renta.RentaId))
                return await Insertar(renta);
            else
                return await Modificar(renta);
        }

        private async static Task<bool> Insertar(Renta renta) {
            bool paso = false;
            Contexto contexto = new Contexto();

            try {
                contexto.Rentas.Add(renta);
                paso = await contexto.SaveChangesAsync() > 0;

                if (paso) {
                    var vehiculoRentado = await VehiculoBLL.Buscar(renta.VehiculoId);
                    if (vehiculoRentado != null) {
                        vehiculoRentado.Estado = VehiculoEstado.Rentado;
                        await VehiculoBLL.Modificar(vehiculoRentado);
                    }
                }

            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return paso;
        }

        public async static Task<bool> Modificar(Renta renta) {
            bool paso = false;
            Contexto contexto = new Contexto();

            try {
                contexto.Entry(renta).State = EntityState.Modified;

                paso = await contexto.SaveChangesAsync() > 0;

            } catch (Exception) {

                throw;
            } finally {
                await contexto.DisposeAsync();
            }
            return paso;
        }

        public async static Task<bool> Eliminar(int id) {
            bool paso = false;

            var renta = await Buscar(id);

            if (renta != null) {
                renta.Eliminada = true;
                paso = await Modificar(renta);

                if (paso) {
                    var vehiculoRentado = await VehiculoBLL.Buscar(renta.VehiculoId);

                    if (vehiculoRentado != null) {
                        vehiculoRentado.Estado = VehiculoEstado.Disponible;
                        await VehiculoBLL.Modificar(vehiculoRentado);
                    }
                }
            }

            return paso;
        }

        public async static Task<Renta> Buscar(int id) {
            Contexto contexto = new Contexto();
            Renta renta;

            try {
                renta = await contexto.Rentas
                    .Where(e => e.RentaId == id)
                    .FirstOrDefaultAsync();
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return renta;
        }


        public async static Task<bool> Existe(int id) {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try {
                encontrado = await contexto.Rentas.AnyAsync(e => e.RentaId == id);
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return encontrado;
        }

        public async static Task<List<Renta>> GetRentas(bool traerEliminadas = false) {
            Contexto contexto = new Contexto();

            List<Renta> rentas = new List<Renta>();
            await Task.Delay(01); //Para dar tiempo a renderizar el mensaje de carga

            try {
                rentas = await contexto.Rentas.Where(r => r.Eliminada == traerEliminadas).ToListAsync();

            } catch (Exception) {

                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return rentas;

        }

    }
}
