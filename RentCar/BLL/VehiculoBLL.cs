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
   public  class VehiculoBLL
    {
        public async static Task<bool> Guardar(Vehiculo vehiculo)
        {
            if (!await Existe(vehiculo.VehiculoId))
                return await Insertar(vehiculo);
            else
                return await Modificar(vehiculo);
        }

        private async static Task<bool> Insertar(Vehiculo vehiculo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                #region Asignando poliza
                vehiculo.Poliza.FechaInicial = DateTime.Now;
                vehiculo.Poliza.FechaFinal = DateTime.Today.AddDays(365);
                vehiculo.Poliza.MontoAsegurado = vehiculo.Valor * 1.01m;
                vehiculo.Poliza.FechaLimitePago = DateTime.Today.AddDays(90);
                #endregion

                contexto.Vehiculos.Add(vehiculo);
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

        public async static Task<bool> Modificar(Vehiculo vehiculo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(vehiculo).State = EntityState.Modified;

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
                var vehiculo = contexto.Vehiculos.Find(id);

                if (vehiculo != null)
                {
                    contexto.Vehiculos.Remove(vehiculo);
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

        public async static Task<Vehiculo> Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Vehiculo vehiculo;

            try
            {
                vehiculo = await contexto.Vehiculos
                    .Where(v => v.VehiculoId == id)
                    .Include(v => v.Poliza)
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

            return vehiculo;
        }

        public async static Task<bool> Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = await contexto.Vehiculos.AnyAsync(e => e.VehiculoId == id);
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

        public async static Task<List<Vehiculo>> GetVehiculos()
        {
            Contexto contexto = new Contexto();

            List<Vehiculo> vehiculo = new List<Vehiculo>();
            await Task.Delay(01); //Para dar tiempo a renderizar el mensaje de carga

            try
            {
                vehiculo = await contexto.Vehiculos.Include(v => v.Poliza).AsNoTracking().ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await contexto.DisposeAsync();
            }

            return vehiculo;
        }
    }
}
