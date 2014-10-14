///<reference path="http://code.jquery.com/jquery-1.9.1.js" />

$(function () {
    $('.abrir-interesse').click(function (e) {
        $.get($(this).data('url'), abrirForm)

        e.preventDefault();
        return false;
    });

    var abrirForm = function (data) {
        $('#interesse').html(data);
        $('#modal-interesse').modal('show');
    }

    $('.excluir-interesse').on('click', function () {
        $.get($(this).data('url'), function (data) {
            $('#exclusao').html(data);
            $('#modal-excluir-interesse').modal('show');
        });
    });
});