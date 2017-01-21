Iniciar servicio
--------------------



Niveles y permisos
--------------------

Todas las cuentas deben contener el parámetro nivel, este es de tipo entero 
y especifica el nivel de acceso que tienen dentro de la plataforma.


### Tipos de permisos

| ID|             DESCRIPCION                                    |
|:-:| :--------------------------------------------------------- |
| 1 | LOGIN USUARIO/CONTRASEÑA   	                     	         |
| 2 | ACCESO A FUNCIONES AVANZADAS DE LA API (SMS,CAMPAÑAS,ETC)  |
| 3 | MODIFICACION DE SALDOS		                     	           |
| 4 | CREAR USUARIOS (NIVEL INFERIOR)                       	   |
| 5 | ELIMINAR USUARIOS (NIVEL INFERIOR)                    	   |
| 6 | OBTENER, MODIFICAR - MIS DATOS                        	   |
| 7 | OBTENER, MODIFICAR - DATOS DE OTROS (NIVEL INFERIOR)  	   |

> **NIVEL INFERIOR** Especifica que este permiso solo puede afectar, modificar o crear parámetros de cuentas nivel inferior; por ejemplo un usuario nivel 2 solo puede  crear un usuario nivel 0 o 1 pero nunca de su mismo nivel o superiora menos que sea un usuario nivel 99, este puede afectar, modificar o crear parámetros en cuentas de cualquier nivel incluso de el suyo mismo.

### Tipos de cuentas

| ID |       NOMBRE    | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|:--:|:----------------|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
| 99 | SUPER ADMIN     | X | X | X | X | X | X | X |
| 3  | ADMINISTRADOR   | X | X | X | X |   | X | X |
| 2  | SOPORTE         | X |   | X |   |   | X | X |
| 1  | ESTANDAR        | X | X |   |   |   | X |   |
| 0  | TEST            | X |   |   |   |   | X |   |
