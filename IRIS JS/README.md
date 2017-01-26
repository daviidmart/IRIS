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

### Comprobar numero

```javascript    
iris.check({
    number: '0000000000',           //NUMERO A COMPROBAR EN TIPO STRING
    success: function(d){
        console.log('Ok: ',d);      //RESPUESTA SI LA UTENTICACION ES CORRECTA Y EL NUMERO ES VALIDO
    },
    error: function(d){
        console.log('Error: ',d);   //RESPUESTA SI LA AUTENTICACION ES INCORRECTA O EL NUMERO ES INVALIDO
    }
});
```
