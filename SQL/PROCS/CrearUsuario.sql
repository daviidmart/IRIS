--Este procedimiento comprueba la API Key y el API Secret y si la ip se encuantra autorizada

--ELIMINAMOS EL PROCEDIMIENTO SI EXISTE PARA ACTUALIZAR
IF OBJECTPROPERTY(object_id('dbo.CrearUsuario'), N'IsProcedure') = 1
	DROP PROCEDURE [dbo].[CrearUsuario]
GO
--CREAMOS EL PROCEDIMIENTO
CREATE PROCEDURE [dbo].[CrearUsuario]
    @USER VARCHAR(MAX),   
    @TOKEN VARCHAR(MAX),
	@DATA VARCHAR(MAX)
AS
DECLARE
	@n varchar(max)
	--COMPROBAMOS SI EXISTE UN USUARIO CON LOS PARAMETROS Y SI SU IP SE ENCUENTRA AUTORIZADA
	IF EXISTS(SELECT TOP 1 1 FROM usuarios WHERE usuario = @USER AND token = @TOKEN)
	BEGIN
		IF EXISTS(SELECT TOP 1 1 FROM usuarios WHERE usuario = @USER AND token = @TOKEN AND nivel > 1)
		BEGIN
			INSERT INTO usuarios( usuario, psdw, apik, apis, token, balance, price, nivel )
			SELECT 
				max(case when Nombre ='usuario' then convert(Varchar(max),StringValue) else '' end) as [usuario],
				max(case when Nombre ='psdw' then convert(Varchar(max),StringValue) else '' end) as [psdw],
				max(case when Nombre ='apik' then convert(Varchar(max),StringValue) else '' end) as [apik],
				max(case when Nombre ='apis' then convert(Varchar(max),StringValue) else '' end) as [apis],
				max(case when Nombre ='token' then convert(Varchar(max),StringValue) else '' end) as [token],
				max(case when Nombre ='balance' then convert(float,StringValue) else 0 end) as [balance],
				max(case when Nombre ='price' then convert(float,StringValue) else 0 end) as [price],
				max(case when Nombre ='nivel' then convert(int,StringValue) else 0 end) as [nivel]
			FROM parseJSON(@DATA) 
			WHERE ValueType != 'object'
		END
	END
	ELSE
	BEGIN
		SELECT Mensaje = '{ code: 1}' --ERROR POR CREDENCIALES INVALIDAS CODIGO 1
	END
GO  
--PROBAMOS EL PROCEDIMIENTO
exec CrearUsuario 'davidl', '11D3tkBcV1vCnCqRf8Ef10zfrAGzKF', '{ "usuario": "davidl", "psdw": "$2a$10$Esev9suggnCmG0QPZ6BAZeS3Vvpzn0NjFxjV3LqycNVU8qi3QvYwG", "apik": "A3493LAZ", "apis": "$2a$10$Esev9suggnCmG0QPZ6BAZePjJ69xbQfXgVBcOl3xYlaQnanglcK3y", "token": "", "balance": 100, "price": 0.4, "nivel": 99 }'