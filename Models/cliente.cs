namespace PruebaDALE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cliente")]
    public partial class cliente
    {
        public long id { get; set; }

        public long? Cedula { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public long? Telefono { get; set; }
    }
}
