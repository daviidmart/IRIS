# IRIS JS - V 1.0

### Descripción
Libreria para iris con la cual podras enviar sms y comprobar numeros de celular en Mexico. No requiere ninguna libreria.

### Incluir Iris en tu html

```hmtl
<script src="Iris.js"></script>
```


### Instanciado

```javascript
var iris = new Iris("TU KEY","TU SECRET"); //REMPLAZAR CON TUS CREDENCIALES
```

### Enviar SMS

```javascript
iris.send({
    to: '0000000000',               //NUMERO AL CUAL SE VA A ENVIAR EL SMS
    text: 'Hola como estas?',       //TEXTO DEL SMS 160 CARACTERES
    success: function(d){
        console.log('Ok: ',d);      //RESPUESTA SI LA UTENTICACION ES CORRECTA Y EL SMS FUE ENVIADO
    },
    error: function(d){
        console.log('Error: ',d);    //RESPUESTA SI LA AUTENTICACION ES INCORRECTA O EL SMS NO FUE ENVIADO
    }
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
