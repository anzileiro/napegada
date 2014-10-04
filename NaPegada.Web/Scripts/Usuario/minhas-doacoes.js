$(function () {
    $('.abrir-doacao').click(function (e) {
        $.get($(this).data('url'), abrirForm)

        e.preventDefault();
        return false;
    });

    var abrirForm = function (data) {
        $('#doacao').replaceWith(data);
        $('#modal-doacao').modal('show');
    }
});