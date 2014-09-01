$(function () {
    $('#btn-buscar-cep').on('click', function () {
        usuario.obterEnderecoPorCep($('#Usuario_Endereco_Cep').val());
    });
});

var usuario = {
    'obterEnderecoPorCep': function (_cep) {
        $.ajax({
            url: 'http://viavirtual.com.br/webservicecep.php?cep=' + _cep,
            type: 'get',
            success: function (r) {
                alert(JSON.stringify(r));
            }
        });
    }
};