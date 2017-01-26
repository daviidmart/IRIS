INSERT INTO procedimientos (clave, [proc], nivel, [desc])
VALUES ('NUSUA','CrearUsuario', 2, 'Crear usuario nuevo');
GO
INSERT INTO procedimientos (clave, [proc], nivel, [desc])
VALUES ('OBMID','ObtenerMisDatos', 0, 'Obtener datos de usuario');
GO
INSERT INTO procedimientos (clave, [proc], nivel, [desc])
VALUES ('SMANA','SmsManager', 1, 'Obtener puerto, servidor disponible');
GO
INSERT INTO procedimientos (clave, [proc], nivel, [desc])
VALUES ('SMSTA','ObtenerMensaje', 1, 'Regresa el status de un mensaje');
GO
INSERT INTO procedimientos (clave, [proc], nivel, [desc])
VALUES ('AUTUS','AuthUser', 0, 'Autentecacion usuario / password que regresa token');
GO
INSERT INTO procedimientos (clave, [proc], nivel, [desc])
VALUES ('NUGSM','AddGSM', 2, 'Agregar nuevo servidor gsm y sus respectivos puertos');
GO
INSERT INTO procedimientos (clave, [proc], nivel, [desc])
VALUES ('OBSVS','GetServidores', 0, 'Regresa todos los servidores');
GO
INSERT INTO procedimientos (clave, [proc], nivel, [desc])
VALUES ('UPGSM','UpdateGSM', 0, 'Actualiza el status de un servidor gsm');