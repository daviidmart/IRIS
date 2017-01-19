CREATE TABLE usuarios(
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](max) NOT NULL,
	[psdw] [varchar](max) NOT NULL,
	[apik] [varchar](max) NOT NULL,
	[apis] [varchar](max) NOT NULL,
	[token] [varchar](max),
	[balance] [float] NOT NULL,
	[price] [float] NOT NULL,
	[nivel] [int] NOT NULL,
	[creado] [datetime] NOT NULL DEFAULT(GETDATE())
)
GO
CREATE TABLE firewall(
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](max) NOT NULL,
	[ip] [varchar](max) NOT NULL,
	[creado] [datetime] NOT NULL DEFAULT(GETDATE())
)