$(function () {
    $('#btn-buscar-cep').on('click', function () {
        $.getJSON('http://viacep.com.br/ws/' + $('#Usuario_Endereco_Cep').val() + '/json', function (retorno) {
            $('#Usuario_Endereco_Logradouro').val(retorno.logradouro);
            $('#Usuario_Endereco_Estado').val(retorno.uf);
            $('#Usuario_Endereco_Cidade').val(retorno.localidade);
            $('#Usuario_Endereco_Bairro').val(retorno.bairro);
        });
    });
});
