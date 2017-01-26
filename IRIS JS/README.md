# IRIS JS - V 1.0

### Instalacion

Incluir iris.js en tu html justo antes de cerrar el body como en el siguiente ejemplo:

```hmtl
<!DOCTYPE html>
<html>
<head>
    <title>Page Title</title>
</head>
<body>
    <h1>Hola mundo</h1>
    <script src="iris.js"></script>
</body>
</html>
```

### Configurar

Modificar los siguientes parametros en iris.js para que funcione de acuerdo a tu servidor.

Ejempplo:
```javascript
var urlDefault = 'index.html';   //URL DEFAULT EJEMPLO login.html - login.php - etc
var server = 'http://localhost'; //URL DEL SERVIDOR
var puerto = 8083;               //PUERTO DEL SERVIDOR
var version = 1;                 //VERSION DEL SERVIDOR
```

### Iniciar sesion

Esta funcion recibe 5 parametros en el siguiente orden y del siguiente tipo:

| PARAMETRO  |         TIPO            |
|:----------:| :---------------------- |
| usuario    | text  	               |
| contraseña | text                    |
| recordar   | bool		               |
| success    | function                |
| error      | function                |

Ejemplo:
```javascript
iris.login('usurio','password',true,function(correcto){
    console.log(correcto);
},function(error){
    console.log(error);
});
```
> **RECORDAR** Por medio del parametro recordar especificamos si la session se va a mantener o se va a borrar al cerrar el navegador.

### Cerrar session

Esta funcion no recibe ningun parametro y al finalizar automaticamente redirige a la pagina default

Ejemplo:
```javascript
iris.logout();
```

### Obtener datos de usuario

Esta funcion recibe 2 parametros en el siguiente orden y del siguiente tipo:

| PARAMETRO  |         TIPO            |
|:----------:| :---------------------- |
| success    | function                |
| error      | function                |

Ejempplo:
```javascript
iris.getUser(function(correcto){
    console.log(correcto);
},function(error){
    console.log(error);
});
```

### Crear nuevo usuario

Esta funcion recibe 5 parametros en el siguiente orden y del siguiente tipo:

| PARAMETRO  |         TIPO            |
|:----------:| :---------------------- |
| usuario    | text  	               |
| contraseña | text                    |
| nivel      | text  	               |
| success    | function                |
| error      | function                |

Ejemplo:
```javascript
iris.newUser('usurio','password', '99',true,function(correcto){
    console.log(correcto);
},function(error){
    console.log(error);
});
```

### Enviar sms

Esta funcion recibe 6 parametros en el siguiente orden y del siguiente tipo:

| PARAMETRO  |         TIPO            |
|:----------:| :---------------------- |
| apikey     | text                    |
| apiSecret  | text                    |
| para       | text                    |
| texto      | text                    |
| success    | function                |
| error      | function                |

Ejemplo:
```javascript
iris.sendSms('KJAHSD1312','ASDJKHHG1238172','4491259348','Hola como estas',function(correcto){
    console.log(correcto);
},function(error){
    console.log(error);
});
```
