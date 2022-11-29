namespace PruebaDALE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("Comprobante")]
    public partial class Comprobante
    {
        public Comprobante()
        {
            ComprobanteDetalle = new List<ComprobanteDetalle>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(300)]
        public string Cliente { get; set; }

        public int Total { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Creado { get; set; }

        public bool Abono { get; set; }

        [Display(Name = "Medio De Pago")]
        public int Medio_PagoId { get; set; }
        public virtual Medio_Pago Medio_Pago { get; set; }

        public int Monto { get; set; }

        public int Efectivo { get; set; }

        public int Nequi { get; set; }

        public int Daviplata { get; set; }
        public string Ncelular { get; set; }
        [Display(Name ="Saldo")]
        public int Vuelto { get; set; }
 
        [Display(Name = "Anulado")]
        public string Comentarios { get; set; }
        
        public int Saldo { get; set; }

        [Required]
        public string Estado { get; set; }
        public string ComentariosFactura { get; set; }
        public string Modificado { get; set; }
        public virtual List<ComprobanteDetalle> ComprobanteDetalle { get; set; }
    }

    #region ViewModels
    public class ComprobanteViewModel {
        #region Cabecera
        public string Cliente { get; set; }
        public string Estado { get; set; }
        [Display(Name ="Colaborador")]
        public string Colaborador { get; set; }
        [Display(Name = "Nuevo Abono")]
        public bool Abono { get; set; }
        [Display(Name ="Medio De Pago")]
        public int Medio_Pagoid { get; set; }
        [Display(Name = "Membresia")]
        public int MembresiasId { get; set; }
        public int Monto { get; set; }
        public int Efectivo { get; set; }
        public string Ncelular { get; set; }
        [Display(Name = "Saldo")]
        public int Vuelto { get; set; }
        public int CabeceraProductoId { get; set; }
        public string CabeceraProductoNombre { get; set; }
        public int CabeceraProductoCantidad { get; set; }
        public decimal CabeceraProductoPrecio { get; set; }
        [Display(Name = "Anulado")]
        public string Comentarios { get; set; }
        [Display(Name = "Comentarios")]
        public string ComentariosFactura { get; set; }
        public int Saldo { get; set; }
        public string FechaEntregaO { get; set; }
        public int Nequi { get; set; }
        public int Daviplata { get; set; }
        #endregion

        #region Contenido
        public List<ComprobanteDetalleViewModel> ComprobanteDetalle { get; set; }
        #endregion

        #region Pie
        public int Total()
        {
            return int.Parse(ComprobanteDetalle.Sum(x => x.Monto()).ToString());
        }
        public DateTime Creado { get; set; }
        #endregion

        public ComprobanteViewModel()
        {
            ComprobanteDetalle = new List<ComprobanteDetalleViewModel>();
            Refrescar();
        }

        public void Refrescar() {
            CabeceraProductoId = 0;
            CabeceraProductoNombre = null;
            CabeceraProductoCantidad = 1;
            CabeceraProductoPrecio = 0;
        }

        public bool SeAgregoUnProductoValido()
        {
            return !(CabeceraProductoId == 0 || string.IsNullOrEmpty(CabeceraProductoNombre) || CabeceraProductoCantidad == 0 || CabeceraProductoPrecio == 0);
        }

        public bool ExisteEnDetalle(int ProductoId)
        {
            return ComprobanteDetalle.Any(x => x.ProductoId == ProductoId);                        
        }

        public void RetirarItemDeDetalle() {
            if (ComprobanteDetalle.Count > 0)
            {
                var detalleARetirar = ComprobanteDetalle.Where(x => x.Retirar)
                                                        .SingleOrDefault();

                ComprobanteDetalle.Remove(detalleARetirar);
            }
        }

        public void AgregarItemADetalle()
        {
            ComprobanteDetalle.Add(new ComprobanteDetalleViewModel
            {
                ProductoId = CabeceraProductoId,
                ProductoNombre = CabeceraProductoNombre,
                PrecioUnitario = CabeceraProductoPrecio,
                Cantidad = CabeceraProductoCantidad,
            });

            Refrescar();
        }

        public Comprobante ToModel()
        {
            var comprobante = new Comprobante();
            comprobante.Cliente = this.Cliente;
            comprobante.Creado = DateTime.Now;
            comprobante.Total = this.Total();
            comprobante.Abono = this.Abono;
            comprobante.Medio_PagoId = this.Medio_Pagoid;
            comprobante.Efectivo = this.Efectivo;
            comprobante.Ncelular = this.Ncelular;
            comprobante.Vuelto = this.Vuelto;
            comprobante.Comentarios = this.Comentarios;
            comprobante.Saldo = this.Saldo + this.Efectivo;
            comprobante.Estado = this.Estado;
            comprobante.ComentariosFactura = this.ComentariosFactura;
            comprobante.Nequi = this.Nequi;
            comprobante.Daviplata= this.Daviplata;
            foreach (var d in ComprobanteDetalle)
            {
                comprobante.ComprobanteDetalle.Add(new ComprobanteDetalle {
                    ProductoId = d.ProductoId,
                    Monto = d.Monto(),
                    PrecioUnitario = d.PrecioUnitario,
                    Cantidad = d.Cantidad
                });
            }

            return comprobante;
        }
    }
    #endregion
}
