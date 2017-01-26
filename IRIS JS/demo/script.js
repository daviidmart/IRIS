setTimeout(function(){
    iris.getUser(function(s){
        $('#apik').val(s.apik);
        $('#apis').val(s.apis);
        $('#uname').text('Usuario: '+s.usuario);
        $('#saldo').text('Saldo: '+s.balance);
        $('#user-block').show();
    },function(e){
        alert(e.message);
    });
    //INICIAR SESSION
    $( "#login" ).click(function() {
        var user = $( "#user" ).val();
        var pasw = $( "#pasw" ).val();
        iris.login(user,pasw, true,function(success){
            $('#lresp').text(JSON.stringify(success,null,4));
            iris.getUser(function(s){
                $('#apik').val(s.apik);
                $('#apis').val(s.apis);
                $('#uname').text('Usuario: '+s.usuario);
                $('#saldo').text('Saldo: '+s.balance);
                $('#user-block').show();
            },function(e){
                alert(e.message);
            });
        }, function(error){
            $('#lresp').text(JSON.stringify(error,null,4));
        });
    });
    //ENVIAR SMS
    $( "#enviar" ).click(function() {
        var apik = $( "#apik" ).val();
        var apis = $( "#apis" ).val();
        var para = $( "#para" ).val();
        var mens = $( "#mens" ).val();
        
        iris.sendSms(apik,apis,para,mens,function(success){
            $('#mresp').text(JSON.stringify(success,null,4));
        }, function(error){
            $('#mresp').text(JSON.stringify(error,null,4));
        });
    });
    //CREAR USUARIO
    $( "#nuevo" ).click(function() {
        var nuser = $( "#nuser" ).val();
        var npas = $( "#npas" ).val();
        var nniv = $( "#nniv" ).val();
        
        iris.newUser(nuser,npas,nniv,function(success){
            $('#nresp').text(JSON.stringify(success,null,4));
        }, function(error){
            $('#nresp').text(JSON.stringify(error,null,4));
        });
    });
    //CERRAR SESION
    $( "#cerrar" ).click(function() {
        iris.logout();
    });
}, 1500);