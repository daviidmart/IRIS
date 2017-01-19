--Este procedimiento comprueba las credenciales del usuario

--ELIMINAMOS EL PROCEDIMIENTO SI EXISTE PARA ACTUALIZAR
IF OBJECTPROPERTY(object_id('dbo.AuthUser'), N'IsProcedure') = 1
	DROP PROCEDURE [dbo].[AuthUser]
GO
--CREAMOS EL PROCEDIMIENTO
CREATE PROCEDURE [dbo].[AuthUser]
    @USER VARCHAR(MAX),   
    @PASW VARCHAR(MAX),
	@TOKEN VARCHAR(MAX)
AS
	--COMPROBAMOS USUARIO Y CONTRASENA
	IF EXISTS(SELECT TOP 1 1 FROM usuarios WHERE usuario = @USER AND psdw = @PASW)
		BEGIN
			UPDATE usuarios SET token = @TOKEN WHERE usuario = @USER AND psdw = @PASW;
			SELECT Mensaje = '{ code: 0}' --AUTENTICACION CORRECTA CODIGO 0
		END
	ELSE
		SELECT Mensaje = '{ code: 1}' --ERROR POR CREDENCIALES INVALIDAS CODIGO 1
GO  
--PROBAMOS EL PROCEDIMIENTO
exec AuthUser 'davidl', '$2a$10$Esev9suggnCmG0QPZ6BAZeS3Vvpzn0NjFxjV3LqycNVU8qi3QvYwG', 'asdf'