using Microsoft.EntityFrameworkCore;
using RentCar.DAL;
using RentCar.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.BLL
{
    public class SalidasVehiculoBLL
    {
        public async static Task<bool> Guardar(SalidaVehiculo salidas)
        {
            if (!await Existe(salidas.SalidaId))
                return await Insertar(salidas);
            else
                return await Modificar(salidas);
        }

        private async static Task<bool> Insertar(SalidaVehiculo salida)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.salidaVehiculos.Add(salida);
                paso = await contexto.SaveChangesAsync() > 0;
                if (paso) {
                    Vehiculo vehiculo = await VehiculoBLL.Buscar(salida.VehiculoId);
                    if (vehiculo != null) {
                        vehiculo.Estado = Entidades.Enums.VehiculoEstado.Eliminado;
                        await VehiculoBLL.Modificar(vehiculo);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await contexto.DisposeAsync();
            }

            return paso;
        }

        public async static Task<bool> Modificar(SalidaVehiculo salida)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(salida).State = EntityState.Modified;

                paso = await contexto.SaveChangesAsync() > 0;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await contexto.DisposeAsync();
            }
            return paso;
        }

        public async static Task<bool> Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var salida = contexto.salidaVehiculos.Find(id);

                if (salida != null)
                {
                    contexto.salidaVehiculos.Remove(salida);
                    paso = await contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await contexto.DisposeAsync();
            }

            return paso;
        }

        public async static Task<bool> Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = await contexto.salidaVehiculos.AnyAsync(e => e.SalidaId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await contexto.DisposeAsync();
            }

            return encontrado;
        }
        public async static Task<SalidaVehiculo> Buscar(int id)
        {
            Contexto contexto = new Contexto();
            SalidaVehiculo salida;

            try
            {
                salida = await contexto.salidaVehiculos
                    .Where(e => e.SalidaId == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await contexto.DisposeAsync();
            }

            return salida;
        }

        public async static Task<List<SalidaVehiculo>> GetSalidaVehiculo()
        {
            Contexto contexto = new Contexto();

            List<SalidaVehiculo> salida = new List<SalidaVehiculo>();
            await Task.Delay(01); //Para dar tiempo a renderizar el mensaje de carga

            try
            {
               salida = await contexto.salidaVehiculos.ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await contexto.DisposeAsync();
            }

            return salida;
        }
    }
}
