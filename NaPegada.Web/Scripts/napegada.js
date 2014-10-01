///<reference path="http://code.jquery.com/jquery-1.9.1.js" />

$(function () {
    $('#btn-entrar').on('click', function () {
        f.ajax('/Usuario/Entrar',
               'post',
               data_ = $('#frm-usuario-entrar').serialize(),
               f.exibirLoad('.load', true),
               function (r) {
                   f.redirecionar(r.url);
               },
               f.exibirLoad('.load', false), undefined);
    });

    $('#btn-sair').on('click', function () {
        f.obter('/Usuario/Sair', undefined, function (r) { f.redirecionar(r.url) });
    });

    $('#btn-registrar').on('click', function () {
        f.ajax('/Usuario/Registrar',
               'post',
               data_ = $('#frm-usuario-registrar').serialize(),
               f.exibirLoad('.load', true),
               function (r) {
                   f.redirecionar(r.url);
               }, f.exibirLoad('.load', false), undefined);
    });
});

var f = {
    'ajax': function (url_, type_, data_, beforeSend_, success_, complete_, error_) {
        $.ajax({
            url: url_,
            type: type_,
            data: data_,
            beforeSend: beforeSend_,
            success: success_,
            complete: complete_,
            error: error_
        });
    },
    'postar': function (url_, data_, callback_, type_) {
        $.post(url_, data_, callback_, type_);
    },
    'obter': function (url_, data_, callback_) {
        $.getJSON(url_, data_, callback_);
    },
    'redirecionar': function (url_) {
        return window.location.href = url_;
    },
    'exibirLoad': function (elemento_, mostrar_) {
        if (mostrar_) {
            $(elemento_).html('<img src="/Content/images/load-100x9.gif" />');
        } else {
            $(elemento_).html('');
        }
    }
};