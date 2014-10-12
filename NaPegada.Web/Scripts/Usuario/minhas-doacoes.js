///<reference path="http://code.jquery.com/jquery-1.9.1.js" />

$(function () {
    $('.abrir-doacao').click(function (e) {
        $.get($(this).data('url'), abrirForm)

        e.preventDefault();
        return false;
    });

    var abrirForm = function (data) {
        $('#doacao').html(data);
        $('#modal-doacao').modal('show');
    }

    $('.excluir-doacao').click(function (e) {
        $.get($(this).data('url'), function (data) {
            $('#exclusao').html(data);
            $('#modal-deletar-doacao').modal('show');
        })

        e.preventDefault();
        return false;
    });
});