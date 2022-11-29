create database PruebaDale
use PruebaDale
create table producto
(
id bigint identity(1,1),
Nombre varchar(max),
Valor_unitario int
)
create table cliente
(
id bigint identity(1,1),
Cedula bigint,
Nombre varchar(max),
Apellido varchar(max),
Telefono bigint
)