﻿using Microsoft.EntityFrameworkCore;
using RentCar.DAL;
using RentCar.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.BLL {
    public class ClientesBLL {

        public async static Task<bool> Guardar(Cliente cliente) {
            if (!await Existe(cliente.ClienteId))
                return await Insertar(cliente);
            else
                return await Modificar(cliente);
        }

        private async static Task<bool> Insertar(Cliente cliente) {
            bool paso = false;
            Contexto contexto = new Contexto();

            try {
                contexto.Clientes.Add(cliente);
                paso = await contexto.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return paso;
        }

        public async static Task<bool> Modificar(Cliente cliente) {
            bool paso = false;
            Contexto contexto = new Contexto();

            try {
                contexto.Entry(cliente).State = EntityState.Modified;

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
                var cliente = contexto.Clientes.Find(id);

                if (cliente != null) {
                    contexto.Clientes.Remove(cliente);
                    paso = await contexto.SaveChangesAsync() > 0;
                }
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return paso;
        }

        public async static Task<Cliente> Buscar(int id) {
            Contexto contexto = new Contexto();
            Cliente cliente;

            try {
                cliente = await contexto.Clientes
                    .Where(e => e.ClienteId == id)
                    .FirstOrDefaultAsync();
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return cliente;
        }


        public async static Task<bool> Existe(int id) {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try {
                encontrado = await contexto.Clientes.AnyAsync(e => e.ClienteId == id);
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return encontrado;
        }

        public async static Task<List<Cliente>> GetClientes() {
            Contexto contexto = new Contexto();

            List<Cliente> clientes = new List<Cliente>();
            await Task.Delay(01); //Para dar tiempo a renderizar el mensaje de carga

            try {
                clientes = await contexto.Clientes.ToListAsync();

            } catch (Exception) {

                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return clientes;

        }

    }
}
