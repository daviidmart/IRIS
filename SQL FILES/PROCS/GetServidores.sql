--ELIMINAMOS EL PROCEDIMIENTO SI EXISTE PARA ACTUALIZAR
IF OBJECTPROPERTY(object_id('dbo.GetServidores'), N'IsProcedure') = 1
	DROP PROCEDURE [dbo].[GetServidores]
GO
--CREAMOS EL PROCEDIMIENTO
CREATE PROCEDURE [dbo].[GetServidores]
	@DATA VARCHAR(MAX),
	@RES VARCHAR(MAX) OUTPUT
AS
DECLARE
	@C INT,
	@conteo INT = 0,
	@json VARCHAR(MAX),
	--VARIABLES TEMPORALES
	@id int,
	@url VARCHAR(MAX),
	@usr VARCHAR(MAX),
	@pas VARCHAR(MAX)

	SELECT @C = COUNT(*) FROM servidores
	WHILE (@conteo < @C)
		BEGIN
			SET @conteo = @conteo + 1;
			SELECT @id = id, @url = [url], @usr = usr, @pas = pas FROM servidores WHERE id = @conteo
			IF (@conteo < @C AND @conteo != 1)
				SET @json = @json + '{ id:'+CONVERT(VARCHAR(19),@id)+', url:"'+@url+'", usr:"'+@usr+'", pas:"'+@pas+'" },'
			ELSE IF (@conteo = 1)
				SET @json = '{ id:'+CONVERT(VARCHAR(19),@id)+', url:"'+@url+'", usr:"'+@usr+'", pas:"'+@pas+'" }'
			ELSE
				SET @json = @json + '{ id:'+CONVERT(VARCHAR(19),@id)+', url:"'+@url+'", usr:"'+@usr+'", pas:"'+@pas+'" }'
		END;
	SET @RES = '{servidores: ['+@json+']}'
GO  
--PROBAMOS EL PROCEDIMIENTO
DECLARE @RES VARCHAR(MAX);
exec GetServidores @RES OUTPUT
SELECT @RES