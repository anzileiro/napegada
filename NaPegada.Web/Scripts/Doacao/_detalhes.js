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

    $('#fotos').blur(function () {
        validarFotos();
    });

    $('#salvar-doacao').click(function () {
        validarFotos();
        var $form = $('#form-doacao');

        if ($form.valid())
            $form.submit();
    });

    var validarFotos = function () {
        var $erro = $('#fotos-erro');
        var fotos = $('#fotos')[0].files;
        var hasFile = fotos[0] ? true : false;
        var tooManyFiles = fotos.length > 5;
        var sizeLimitExceeded = getTotalSize(fotos) > 5;
        var invalidFormat = containInvalidFormat(fotos);

        var errorMessage = '';
        errorMessage += !hasFile ? '[Selecione ao menos uma foto]' : '';
        errorMessage += tooManyFiles ? '[Máximo de fotos permitidas: 5]' : '';
        errorMessage += sizeLimitExceeded ? '[Tamanho máximo permitido: 5MB]' : '';
        errorMessage += invalidFormat ? '[Formatos permitidos: JP[E]G/PNG]' : '';

        if (errorMessage != '') {
            $erro.html(errorMessage);
            $erro.show();
        } else {
            $erro.hide();
        }
    };

    var getTotalSize = function (files) {
        var totalSize = 0;
        var length = files.length;

        for (var i = 0; i < length; i++)
            totalSize += files[i].size;

        return totalSize / 1024 / 1024;
    };

    var containInvalidFormat = function (files) {
        var invalidFormat = false;
        var length = files.length;
        var i = 0;

        while (!invalidFormat && i < length) {
            invalidFormat = files[i].type.indexOf('jpg') < 0 && files[i].type.indexOf('png') < 0 && files[i].type.indexOf('jpeg') < 0;
            i++;
        }

        return invalidFormat;
    };
});