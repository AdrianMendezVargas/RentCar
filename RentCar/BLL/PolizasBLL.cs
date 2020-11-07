using Microsoft.EntityFrameworkCore;
using RentCar.DAL;
using RentCar.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.BLL {
    public class PolizasBLL {

        public async static Task<bool> Guardar(Poliza poliza) {
            if (!await Existe(poliza.PolizaId))
                return await Insertar(poliza);
            else
                return await Modificar(poliza);
        }

        private async static Task<bool> Insertar(Poliza poliza) {
            bool paso = false;
            Contexto contexto = new Contexto();

            try {
                contexto.Polizas.Add(poliza);
                paso = await contexto.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return paso;
        }

        public async static Task<bool> Modificar(Poliza poliza) {
            bool paso = false;
            Contexto contexto = new Contexto();

            try {
                contexto.Entry(poliza).State = EntityState.Modified;

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
            Contexto contexto = new Contexto();
            try {
                var poliza = contexto.Polizas.Find(id);

                if (poliza != null) {
                    contexto.Polizas.Remove(poliza);
                    paso = await contexto.SaveChangesAsync() > 0;
                }
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return paso;
        }

        public async static Task<Poliza> Buscar(int id) {
            Contexto contexto = new Contexto();
            Poliza poliza;

            try {
                poliza = await contexto.Polizas
                    .Where(e => e.PolizaId == id)
                    .FirstOrDefaultAsync();
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return poliza;
        }


        public async static Task<bool> Existe(int id) {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try {
                encontrado = await contexto.Polizas.AnyAsync(e => e.PolizaId == id);
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return encontrado;
        }

        public async static Task<List<Poliza>> GetPolizas() {
            Contexto contexto = new Contexto();

            List<Poliza> polizas = new List<Poliza>();
            await Task.Delay(01); //Para dar tiempo a renderizar el mensaje de carga

            try {
                polizas = await contexto.Polizas.ToListAsync();

            } catch (Exception) {

                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return polizas;

        }

    }
}
