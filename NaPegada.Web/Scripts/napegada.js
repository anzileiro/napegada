///<reference path="http://code.jquery.com/jquery-1.9.1.js" />

$(function () {
    //$('#frm-usuario-perfil-dados .form-control').attr('disabled', 'disabled');
    //$('#frm-usuario-perfil-conta .form-control').attr('disabled', 'disabled');
    f.carrefarPerfil();
    $('#btn-modal-entrar').on('click', function () {
        f.resetar(['.msg', '.load', '.form-control'], true);
    });

    $('#btn-modal-registrar').on('click', function () {
        f.resetar(['.msg', '.load', '.form-control'], true);
    });

    $('#btn-entrar').on('click', function () {
        f.configButtons(['#btn-entrar'], false);
        if (!f.patternEmail($('#u-email').val())) {
            $('.msg').html(f.alerta('alert alert-warning alert-dismissible', 'Ops! Digite um email válido. Ex: fulano.tal@mail.com'));
            return false;
        }
        if ($('#u-senha').val() == '') {
            $('.msg').html(f.alerta('alert alert-warning alert-dismissible', 'Ops! Digite sua senha.'));
            return false;
        }

        f.configButtons(['#btn-entrar'], true);
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
                if (retornoJson == '/Site/Home') {
                    $('.msg').html(f.alerta('alert alert-warning alert-dismissible', 'Ops! Usuario ou senha são inválidos.'));
                    f.configButtons(['#btn-entrar'], false);
                } else {
                    f.redirecionar(retornoJson);
                }
            }
        });
    });

    $('#btn-registrar').on('click', function () {
        f.configButtons(['#btn-registrar', '#btn-registrar'], false);
        if (!f.patternEmail($('#u-email-r').val())) {
            $('.msg').html(f.alerta('alert alert-warning alert-dismissible', 'Ops! Digite um email válido. Ex: fulano.tal@mail.com'));
            return false;
        }
        if ($('#u-senha-r').val() == '') {
            $('.msg').html(f.alerta('alert alert-warning alert-dismissible', 'Ops! Digite sua senha.'));
            return false;
        }

        f.configButtons(['#btn-registrar', '#btn-registrar'], true);
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

    $('#btn-sair').on('click', function () {
        f.obter('/Usuario/Sair', undefined, function (r) { f.redirecionar(r.url) });
    });

    $('#btn-modal-perfil').on('click', function () {
        $.getJSON('/Usuario/ObterSecao', undefined, function (r) {
            $('#p-id').text(r.secao.Id);
            $('#p-id').attr('value', r.secao.Id);
            $('#p-nome').val(r.secao.Nome);
            $('#p-email').val(r.secao.Email);
        });
    });

    $('#btn-atualizar-perfil-dados').on('click', function () {
        $('#btn-atualizar-perfil-dados').text('Dados atualizados');
        $('#btn-atualizar-perfil-dados').removeAttr('disabled', 'disabled');
        if ($('#p-nome').val() == '') {
            $('.msg').html(f.alerta('alert alert-warning alert-dismissible', 'Ops! Informe seu nome.'));
        } else {
            f.configButtons(['#btn-atualizar-perfil-dados'], true);
            $.ajax({
                url: '/Usuario/MeuPerfil',
                type: 'post',
                data: $('#frm-usuario-perfil-dados').serialize(),
                beforeSend: f.exibirLoad('.load', true),
                success: function (r) {
                    $('#nome-usuario').text(r.usuario.Nome);
                    $('#email-usuario').text(r.usuario.Email);
                },
                complete: function () {
                    f.exibirLoad('.load', false)
                    $('#btn-atualizar-perfil-dados').text('Dados atualizados');
                    $('#btn-atualizar-perfil-dados').removeAttr('disabled', 'disabled');
                    var id_ = '';
                    $.getJSON('/Usuario/ObterSecao', undefined, function (r) {
                        id_ = JSON.stringify(r.secao.Id);
                    });
                    $.getJSON('/Usuario/ObterUsuario', {'id': id_}, function (r) {
                        $('#nome-usuario').text(r.usuario.Nome);
                        $('#email-usuario').text(r.usuario.Email);
                    });
                }
            });
        }
    }); 
});

//funções
var f = {
    'carrefarPerfil': function () {
        $.getJSON('/Usuario/ObterSecao', undefined, function (r) {
            $('#nome-usuario').text(r.secao.Nome);
            $('#email-usuario').text(r.secao.Email);
        });
    },
    'configButtons': function (elementos, esconder) {
        if (esconder) {
            $.each(elementos, function (k, v) {
                $(v).attr('disabled', 'disabled');
                $(v).text('Entrando...');
            });
        } else {
            $.each(elementos, function (k, v) {
                $(v).removeAttr('disabled', 'disabled');
                $(v).text('Entrar');
            });
        }
    },
    //'#btn-registrar'
    //'#btn-registrar'
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
    'exibirLoad': function (elemento, mostrar) {
        if (mostrar) {
            $(elemento).html('<img src="/Content/images/load-100x9.gif" />');
        } else {
            $(elemento).html('');
        }
    },
    'alerta': function (tipo, msg) {
        return '<div class="' + tipo + '" role="alert"><button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button><strong></strong>' + msg + '</div>';
    },
    'resetar': function (elementos, resetar) {
        if (resetar) {
            $.each(elementos, function (k, v) {
                $(v).html('');
                $(v).val('');
            });
        }
    },
    'patternEmail': function (dados) {
        var pattern = new RegExp(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/);
        return pattern.test(dados);
    }
};