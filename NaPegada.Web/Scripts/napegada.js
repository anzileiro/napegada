///<reference path="http://code.jquery.com/jquery-1.9.1.js" />

$(function () {
    $('#btn-entrar').on('click', function () {
        f.ajax('/Usuario/Entrar',
               'post',
               data_ = $('#frm-usuario-entrar').serialize(),
               function () {
                   alert('Logando...');
               },
               function (r) {
                   return window.location.href = r.url;
               },
               undefined, undefined, undefined);
    });

    $('#btn-registrar').on('click', function () {
        f.ajax('/Usuario/Registrar',
               'post',
               data_ = $('#frm-usuario-registrar').serialize(),
               function () {
                   alert('Before send...');
               },
               function () {
                   alert('Success...');
               },
               function () {
                   alert('Complete...');
               }, function (x) {
                   alert(JSON.stringify(x));
               }, function () {
                   alert('Callback de brinde !!!');
               });
    });
});

var f = {
    'ajax': function (url_, type_, data_, beforeSend_, success_, complete_, error_, callback_) {
        $.ajax({
            url: url_,
            type: type_,
            data: data_,
            beforeSend: beforeSend_,
            success: success_,
            complete: complete_,
            error: error_
        }, callback_);
    }
};