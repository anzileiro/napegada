///<reference path="http://code.jquery.com/jquery-1.9.1.js" />

$(function () {
    $('#btn-buscar-cep').on('click', function () {
        var url = 'http://viacep.com.br/ws/' + $('#Cep').val() + '/json';
        funcoes.obterJson(
            url,
            undefined,
            function (r) { funcoes.popularEndereco(r) });
    });
});

var funcoes = {
    'obterJson': function (url_, data_, callback_) {
        $.getJSON(url_, data_, callback_);
    },
    'popularEndereco': function (r) {
        $('#Logradouro').val(r.logradouro);
        $('#Uf').val(r.uf);
        $('#Localidade').val(r.localidade);
        $('#Bairro').val(r.bairro);
    }
};