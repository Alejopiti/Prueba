using PruebaDALE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PruebaDALE.Controllers
{
    public class HomeController : Controller
    {
        ModeloPrueba db = new ModeloPrueba();
        private ComprobanteLogic cl = new ComprobanteLogic();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cliente()
        {
            return View(db.cliente.ToList());
        }
        
        public ActionResult RegistrarCliente(cliente _Cliente)
        {
            if (_Cliente.Nombre == "" | _Cliente.Nombre == null | _Cliente.Nombre == " ")
            {
                ModelState.AddModelError("Cliente", "Falta el nombre del cliente");
                return View(_Cliente);
            }
            else if (_Cliente.Apellido == "" | _Cliente.Apellido == null | _Cliente.Apellido == " ")
            {
                ModelState.AddModelError("Apellido", "Falta el apellido del cliente");
                return View(_Cliente);
            }
            else if (_Cliente.Cedula <= 0 )
            {
                ModelState.AddModelError("Cedula", "Falta el número de cedula");
                return View(_Cliente);
            }
            else
            {
                db.cliente.Add(_Cliente);
                db.SaveChanges();
                return RedirectToAction("Cliente");
            }
        }
        
        [HttpGet]
        public ActionResult EditarCliente(int id)
        {
            return View(db.cliente.Find(id));
        }
        
        [HttpPost]
        public ActionResult EditarCliente(cliente _Cliente)
        {
            if (_Cliente.Nombre == "" | _Cliente.Nombre == null | _Cliente.Nombre == " ")
            {
                ModelState.AddModelError("Cliente", "Falta el nombre del cliente");
                return View(_Cliente);
            }
            else if (_Cliente.Apellido == "" | _Cliente.Apellido == null | _Cliente.Apellido == " ")
            {
                ModelState.AddModelError("Apellido", "Falta el apellido del cliente");
                return View(_Cliente);
            }
            else if (_Cliente.Cedula <= 0 )
            {
                ModelState.AddModelError("Cedula", "Falta el número de cedula");
                return View(_Cliente);
            }
            else 
            { 
                db.Entry(_Cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Cliente");
            }
        }

        public ActionResult EliminarCliente(int id)
        {
            var datos = db.cliente.Find(id);
            db.cliente.Remove(datos);
            db.SaveChanges();
            return RedirectToAction("Cliente");
        }
        
        public ActionResult Productos()
        {
            return View(db.producto.ToList());
        }

        public ActionResult Registrarproducto(producto _producto)
        {
            if (_producto.Nombre == "" | _producto.Nombre == null | _producto.Nombre == " ")
            {
                return View(_producto);
            }
            else if (_producto.Valor_unitario <= 0)
            {
                ModelState.AddModelError("Valor_unitario",  "¿ Cual es el valor unitario del producto?");
                return View(_producto);
            }
            else
            {
                db.producto.Add(_producto);
                db.SaveChanges();
                return RedirectToAction("Productos");
            }
        }

        public JsonResult BuscarProducto(string nombre)
        {
            return Json(cl.BuscarProducto(nombre), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Editarproducto(int id)
        {
            return View(db.producto.Find(id));
        }

        [HttpPost]
        public ActionResult Editarproducto(producto _producto)
        {
            if (_producto.Nombre == "" | _producto.Nombre == null | _producto.Nombre == " ")
            {
                return View(_producto);
            }
            else if (_producto.Valor_unitario <= 0)
            {
                ModelState.AddModelError("Valor_unitario",  "¿ Cual es el valor unitario del producto?");
                return View(_producto);
            }
            else
            {
                db.Entry(_producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Productos");
            }
        }

        public ActionResult EliminarProducto(int id)
        {
            var datos = db.producto.Find(id);
            db.producto.Remove(datos);
            db.SaveChanges();
            return RedirectToAction("Productos");
        }

        public ActionResult Ventas()
        {
            return View(db.Comprobantes.ToList());
        }
        public ActionResult RegistrarVenta()
        {
            ViewBag.Cliente = new SelectList(db.cliente, "id", "Celular");
            ViewBag.Medio_Pagoid = new SelectList(db.Medio_Pagos, "id", "Nombre");
            //ViewBag.Colaboradoresid = new SelectList(db.Colaboradores.Where(x => x.idsucursal == idsucursal), "id", "Nombre");
            return View(new ComprobanteViewModel());
        }
        [HttpPost]
        public ActionResult RegistrarVenta(ComprobanteViewModel model, string action)
        {
            ViewBag.Cliente = new SelectList(db.cliente, "id", "Celular");
            ViewBag.Medio_Pagoid = new SelectList(db.Medio_Pagos, "id", "Nombre");
            decimal Total = model.Total();
            decimal TotalMonto = model.Efectivo + model.Nequi + model.Daviplata;
            if (action == "generar")
            {
                model.Estado = "Generada";
                var cliente = db.cliente.Where(x => x.Nombre == model.Cliente).FirstOrDefault();
                model.Cliente = cliente.id.ToString();
                if (model.Abono == true)
                {
                    if (TotalMonto >= Total)
                    {
                        ModelState.AddModelError("monto", "El cliente pago todo esto no es un abono");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.Cliente))
                        {
                            if (cl.Registrar(model.ToModel()))
                            {
                                var ult = db.Comprobantes.ToList().LastOrDefault();
                                return RedirectToAction("Detalle", "Home", new { ult.id });
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("cliente", "Debe agregar un cliente para el Comprobantes");
                        }
                    }
                }
                else
                {
                    if (TotalMonto < Total)
                    {
                        ModelState.AddModelError("monto", "Esto es un abono la persona no ha pagado todo");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.Cliente))
                        {
                            if (cl.Registrar(model.ToModel()))
                            {
                                var ult = db.Comprobantes.ToList().LastOrDefault();
                                return RedirectToAction("Detalle", "Home", new { ult.id });
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("cliente", "Debe agregar un cliente para el Comprobantes");
                        }
                    }
                }
            }
            else if (action == "agregar_producto")
            {
                // Si no ha pasado nuestra validación, mostramos el mensaje personalizado de error
                if (!model.SeAgregoUnProductoValido())
                {
                    ModelState.AddModelError("producto_agregar", "Solo puede agregar un servicio válido al detalle");
                }
                else if (model.ExisteEnDetalle(model.CabeceraProductoId))
                {
                    ModelState.AddModelError("producto_agregar", "El producto elegido ya existe en el detalle");
                }
                else
                {
                    //verificamos disponibilidad 
                    model.AgregarItemADetalle();
                }
            }
            else if (action == "retirar_producto")
            {
                model.RetirarItemDeDetalle();
            }
            else
            {
                throw new Exception("Acción no definida ..");
            }

            return View(model);
        }
        public JsonResult BuscarCliente(string nombre)
        {
            return Json(cl.BuscarCliente(nombre), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Detalle (int id)
        {
            return View(cl.Obtener(id));
        }
    }
}