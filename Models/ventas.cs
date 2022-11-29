namespace PruebaDALE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ventas
    {
        public long id { get; set; }

        public DateTime? FechaVenta { get; set; }

        public int? Subtotal { get; set; }

        public string Vendedor { get; set; }

        public int? Total { get; set; }
    }
}
