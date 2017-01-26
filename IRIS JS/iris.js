//MODIFICAR VALORES DEFAULT
var urlDefault = 'index.html';
var server = 'http://localhost';
var puerto = 8083;
var version = 1;

//NO MODIFICAR NADA!! - *CORE*
if(typeof jQuery=='undefined'){
    var script = document.createElement('script');
    script.src = 'https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js';
    script.type = 'text/javascript';
    document.getElementsByTagName('head')[0].appendChild(script);
}

var conn = server+':'+puerto+'/v'+version+'/';
var iris = {
    login: function(user,password,recordar,success,error){
        if(user != '' && password != ''){
            $.get(conn+'auth/'+user+'/'+password+'/', function(d) {
                if(recordar){
                    localStorage.setItem('token', d.token);
                }else{
                    sessionStorage.setItem('token', d.token);
                }
                success(d);
            }).fail(function(e) {
                error(e.responseJSON);
            });
        }else{
            error({
                code: 4001,
                message: 'Alguno de los parametros esta vacio',
                status: false
            });
        }
    },
    logout: function(){
        sessionStorage.clear();
        localStorage.clear();
        window.location.href = urlDefault;
    },
    sendSms: function(apik,apis,para,texto,success,error){
        if(apik != '' && apis != '' && para != '' && texto != ''){
            $.post(conn+'sms/'+apik+'/'+apis+'/',JSON.stringify({
                to: para,
                text: texto
            }), function( d ) {
                success(d);
            }).fail(function(e) {
                error(e.responseJSON);
            });
        }else{
            error({
                code: 4001,
                message: 'Alguno de los parametros esta vacio',
                status: false
            });
        }
    },
    getUser: function(success,error){
        var token = sessionStorage.getItem('token') || localStorage.getItem('token');
        validarSesion(function(){
            $.get(conn+'user/'+token, function(d) {
                localStorage.setItem('user', JSON.stringify(d));
                success(d);
            }).fail(function(e) {
                error(e);
            });
        });
    },
    newUser: function(user,password,level,success,error){
        var token = sessionStorage.getItem('token') || localStorage.getItem('token');
        validarSesion(function(){
            if(user != '' && password != '' && level != ''){
                $.post(conn+'user/'+token,JSON.stringify({
                    usuario: user,
                    psdw: password,
                    nivel: level
                }), function( d ) {
                    success(d);
                }).fail(function(e) {
                    error(e.responseJSON);
                });
            }else{
                error({
                    code: 4001,
                    message: 'Alguno de los parametros esta vacio',
                    status: false
                });
            }
        });
    }
}

function validarSesion(ok){
    var token = sessionStorage.getItem('token') || localStorage.getItem('token');
    if(!token){
        sessionStorage.clear();
        localStorage.clear();
        if(location.href.split("/").slice(-1)[0] != urlDefault){
            window.location.href = urlDefault;
        }
    }else{
        ok();
    }
}