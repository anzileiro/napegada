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
        alert('ae');
    });
});