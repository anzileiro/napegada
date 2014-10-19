///<reference path="http://code.jquery.com/jquery-1.9.1.js" />

$(function () {
    $('#adotar').click(function () {
        $('#adocao').hide(200);
        $('#confirmacao-adocao').show(200);
    });

    $('#cancelar-adocao').click(function () {
        $('#confirmacao-adocao').hide(200);
        $('#adocao').show(200);        
    });

    $('#confirmar-adocao').click(function () {
        var url = $(this).data('url');

        $.post(url, function (data) {
            $('#modal-doacao').modal('hide');
            var titulo = data.Sucesso ? 'Sucesso' : 'Erro';
            $('#titulo-aviso').html(titulo);
            $('#mensagem-aviso').html(data.Mensagem);
            $('#modal-aviso-notificacao').modal('show');
        });
    });
});