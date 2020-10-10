﻿using Microsoft.EntityFrameworkCore;
using RentCar.DAL;
using RentCar.Entidades;
using RentCar.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.BLL {
    public class RentasBLL {

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

                //if (paso) {
                //    var vehiculoRentado = await VehiculosBLL.Buscar(renta.VehiculoId);
                //    vehiculoRentado.Estado = VehiculoEstado.Rentado;
                //}

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
            Contexto contexto = new Contexto();
            try {
                var renta = contexto.Rentas.Find(id);

                if (renta != null) {
                    contexto.Rentas.Remove(renta);
                    paso = await contexto.SaveChangesAsync() > 0;

                    //if (paso) {
                    //    var vehiculoRentado = await VehiculosBLL.Buscar(renta.VehiculoId);
                    //    vehiculoRentado.Estado = VehiculoEstado.Disponible;
                    //}
                }
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
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

        public async static Task<List<Renta>> GetRentas() {
            Contexto contexto = new Contexto();

            List<Renta> rentas = new List<Renta>();
            await Task.Delay(01); //Para dar tiempo a renderizar el mensaje de carga

            try {
                rentas = await contexto.Rentas.ToListAsync();

            } catch (Exception) {

                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return rentas;

        }

    }
}
