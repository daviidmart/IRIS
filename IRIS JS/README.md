# IRIS JS - V 1.0

### Incluir iris.js en tu html justo antes de cerrar el body

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

### Iniciar sesion

Esta funcion recibe 5 parametros en el siguiente orden y del siguiente tipo:

| PARAMETRO  |         TIPO            |
|:----------:| :---------------------- |
| usuario    | text  	               |
| contraseña | text                    |
| recordar   | bool		               |
| success    | function                |
| error      | function                |

Ejempplo:
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

Ejempplo:
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

Ejempplo:
```javascript
iris.sendSms(apik,apis,para,texto,function(correcto){
    console.log(correcto);
},function(error){
    console.log(error);
});
```
