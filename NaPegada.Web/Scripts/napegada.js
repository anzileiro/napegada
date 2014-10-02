///<reference path="http://code.jquery.com/jquery-1.9.1.js" />

$(function () {
    $('#btn-entrar').on('click', function () {
        var retornoJson = undefined;
        $.ajax({
            url: '/Usuario/Entrar',
            type: 'post',
            data: $('#frm-usuario-entrar').serialize(),
            beforeSend: f.exibirLoad('.load', true),
            success: function (r) {
                retornoJson = r.url;
            },
            complete: function () {
                f.exibirLoad('.load', false)
                f.redirecionar(retornoJson);
            }
        });
    });

    $('#btn-sair').on('click', function () {
        f.obter('/Usuario/Sair', undefined, function (r) { f.redirecionar(r.url) });
    });

    $('#btn-registrar').on('click', function () {
        var dados = $('#frm-usuario-registrar').serialize();
        $.ajax({
            url: '/Usuario/Registrar',
            type: 'post',
            data: dados,
            beforeSend: f.exibirLoad('.load', true),
            complete: function () {
                f.postar('/Usuario/Entrar', data_ = dados, function (r) {
                    f.exibirLoad('.load', false);
                    f.redirecionar(r.url);

                });
            }
        });
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