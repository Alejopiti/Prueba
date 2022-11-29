namespace PruebaDALE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("producto")]
    public partial class producto
    {
        public long id { get; set; }

        public string Nombre { get; set; }

        public int? Valor_unitario { get; set; }
    }
}
