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
                   f.redirecionar(r.url);
               },
               undefined, undefined, undefined);
    });

    $('body').on('click', '#btn-sair', function () {
        alert();
        f.ajax('/Usuario/Sair',
               'get',
               undefined,
               function () {
                   alert('Saindo...');
               },
               function (r) {
                   f.redirecionar(r.url);
               },
               undefined, undefined, undefined);
    });

    $('#btn-registrar').on('click', function () {
        f.ajax('/Usuario/Registrar',
               'post',
               data_ = $('#frm-usuario-registrar').serialize(),
               function () {
                   alert('Registrando...');
               },
               function (r) {
                   f.redirecionar(r.url);
               },
               undefined, undefined, undefined);
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
    },
    'redirecionar': function (url_) {
        return window.location.href = url_;
    }
};