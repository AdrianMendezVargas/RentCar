using Microsoft.EntityFrameworkCore;
using RentCar.DAL;
using RentCar.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.BLL
{
    public class ImportadorBLL
    {
        public async static Task<bool> Guardar(Importador importador)
        {
            if (!await Existe(importador.ImportadorId))
                return await Insertar(importador);
            else
                return await Modificar(importador);
        }

        private async static Task<bool> Insertar(Importador importador)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Importador.Add(importador);
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

        public async static Task<bool> Modificar(Importador importador)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(importador).State = EntityState.Modified;

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
                var importador = contexto.Importador.Find(id);

                if (importador != null)
                {
                    contexto.Importador.Remove(importador);
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

        public async static Task<Importador> Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Importador importador;

            try
            {
                importador = await contexto.Importador
                    .Where(e => e.ImportadorId == id)
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

            return importador;
        }

        public async static Task<bool> Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = await contexto.Importador.AnyAsync(e => e.ImportadorId == id);
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

        public async static Task<List<Importador>> GetImportadores()
        {
            Contexto contexto = new Contexto();

            List<Importador> importador = new List<Importador>();
            await Task.Delay(01); //Para dar tiempo a renderizar el mensaje de carga

            try
            {
                importador = await contexto.Importador.ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await contexto.DisposeAsync();
            }

            return importador;

        }

        public async static Task<List<Importador>> GetList(Expression<Func<Importador, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Importador> Lista = new List<Importador>();
            await Task.Delay(01); //Para dar tiempo a renderizar el mensaje de carga
            try
            {
                Lista = await contexto.Importador.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await contexto.DisposeAsync();
            }
            return Lista;
        }
    }
}
