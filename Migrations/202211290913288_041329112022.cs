namespace PruebaDALE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _041329112022 : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.ComprobanteDetalle",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    ComprobanteId = c.Int(nullable: false),
                    ProductoId = c.Int(nullable: false),
                    Cantidad = c.Int(nullable: false),
                    PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Producto_id = c.Long(),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Comprobante", t => t.ComprobanteId, cascadeDelete: true)
                .Index(t => t.ComprobanteId);
            
            CreateTable(
                "dbo.Comprobante",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Cliente = c.String(nullable: false, maxLength: 300),
                        Total = c.Int(nullable: false),
                        Creado = c.DateTime(nullable: false),
                        Abono = c.Boolean(nullable: false),
                        Medio_PagoId = c.Int(nullable: false),
                        Monto = c.Int(nullable: false),
                        Efectivo = c.Int(nullable: false),
                        Nequi = c.Int(nullable: false),
                        Daviplata = c.Int(nullable: false),
                        Ncelular = c.String(),
                        Vuelto = c.Int(nullable: false),
                        Comentarios = c.String(),
                        Saldo = c.Int(nullable: false),
                        Estado = c.String(nullable: false),
                        ComentariosFactura = c.String(),
                        Modificado = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Medio_Pago", t => t.Medio_PagoId, cascadeDelete: true)
                .Index(t => t.Medio_PagoId);

            CreateTable(
                "dbo.Medio_Pago",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    Nombre = c.String(),
                })
                .PrimaryKey(t => t.id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComprobanteDetalle", "Producto_id", "dbo.producto");
            DropForeignKey("dbo.Comprobante", "Medio_PagoId", "dbo.Medio_Pago");
            DropForeignKey("dbo.ComprobanteDetalle", "ComprobanteId", "dbo.Comprobante");
            DropIndex("dbo.Comprobante", new[] { "Medio_PagoId" });
            DropIndex("dbo.ComprobanteDetalle", new[] { "Producto_id" });
            DropIndex("dbo.ComprobanteDetalle", new[] { "ComprobanteId" });
            DropTable("dbo.producto");
            DropTable("dbo.Medio_Pago");
            DropTable("dbo.Comprobante");
            DropTable("dbo.ComprobanteDetalle");
            DropTable("dbo.cliente");
        }
    }
}
