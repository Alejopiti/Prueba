
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PruebaDALE.Models
{
    public class ComprobanteLogic
    {
        public bool Registrar(Comprobante comprobante) {
            try
            {
                using (var context = new ModeloPrueba())
                {
                    context.Entry(comprobante).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public Comprobante Obtener(int id)
        {
            using (var context = new ModeloPrueba())
            {
                // Esta consulta incluye el detalle del comprobante, y el producto que tiene cada comprobante. Me refiero a sus relaciones
                return context.Comprobantes.Include(x => x.ComprobanteDetalle.Select(y => y.Producto))
                                          .Where(x => x.id == id)
                                          .SingleOrDefault();
            }
        }

        public List<Comprobante> Listar(int idsucursal)
        {
            using (var context = new ModeloPrueba())
            {
                return context.Comprobantes.OrderByDescending(x => x.Creado)
                                          .ToList();
            }
        }

        public List<cliente> BuscarCliente(string term)
        {
            using (var context = new ModeloPrueba())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                var clientes = context.cliente.OrderBy(x => x.Nombre)
                                        .Where(x => x.Nombre.Contains(term))
                                        .Take(10)
                                        .ToList();

                return clientes;
            }
        }
        public List<producto> BuscarProducto(string term)
        {
            using (var context = new ModeloPrueba())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                var productos = context.producto.OrderBy(x => x.Nombre)
                                        .Where(x => x.Nombre.Contains(term))
                                        .Take(10)
                                        .ToList();

                return productos;
            }
        }
    }
}
