///<reference path="http://code.jquery.com/jquery-1.9.1.js" />

$(function () {
    $.validator.unobtrusive.parse($('.form'));

    $('#especie').change(function () {
        var valor = $(this).val();

        carregarRacasDaEspecie(valor);
    });

    var carregarRacasDaEspecie = function (especie) {
        var $racas = $('#raca');

        if (especie == '')
            $racas.html('<option value="">Selecione uma espécie</option>');
        else {
            $racas.html('<option value="">Selecione uma raça</option>');
            $.get('/BaseAsync/Racas/', { especie: especie }, function (data) {
                var racas = [];
                var length = data.Racas.length;

                for (var i = 0; i < length; i++) {
                    var raca = '<option>' + data.Racas[i] + '</option>';
                    racas.push(raca);
                }

                $racas.append(racas.toString());
            }, 'json');
        }            
    };

    $('.form-control').blur(function () {
        $(this).valid();
    });
});