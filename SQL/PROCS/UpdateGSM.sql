--ELIMINAMOS EL PROCEDIMIENTO SI EXISTE PARA ACTUALIZAR
IF OBJECTPROPERTY(object_id('dbo.UpdateGSM'), N'IsProcedure') = 1
	DROP PROCEDURE [dbo].[UpdateGSM]
GO
--CREAMOS EL PROCEDIMIENTO
CREATE PROCEDURE [dbo].[UpdateGSM]
	@DATA VARCHAR(MAX),
	@RES VARCHAR(MAX) OUTPUT
AS
DECLARE
	--VARIABLES DE SERVIDOR
	@SC INT,
	@SS VARCHAR(MAX),
	@SI INT,
	--VARIABLES DE PUERTO
	@PP INT,
	@PS BIT,
	@PD INT,
	@PO varchar(MAX),
	@PI varchar(MAX),
	@PN varchar(MAX),
	@PID INT,
	--VARIABLES DE PROCEDIMIENTO
	@cnt INT = 1

	SELECT
		@SC = max(case when Nombre ='cantidad' then convert(INT,StringValue) else '' end),
		@SS = max(case when Nombre ='server' then convert(Varchar(max),StringValue) else '' end)
	FROM parseJSON(@DATA)
	SET @RES = '{}';
	SELECT @SI = id from servidores where [url] = @SS

WHILE @cnt != (@SC + 1)
	BEGIN
		SELECT 
			@PS= max(case when Nombre ='status' then convert(INT,StringValue) else '' end),
			@PP= max(case when Nombre ='port' then convert(INT,StringValue) else '' end),
			@PD= max(case when Nombre ='st' then convert(INT,StringValue) else '' end),
			@PO= max(case when Nombre ='opr' then convert(Varchar(max),StringValue) else '' end),
			@PI= max(case when Nombre ='imei' then convert(Varchar(max),StringValue) else '' end),
			@PN= max(case when Nombre ='sn' then convert(Varchar(max),StringValue) else '' end)
		FROM parseJSON(@DATA)
		WHERE [parent_ID] = @cnt
		UPDATE puertos SET [status] = @PS, st = @PD, opr = @PO, sn = @PN, imei = @PI WHERE puerto = @PP AND ids = @SI
		SET @cnt = @cnt + 1;
	END;