///<reference path="http://code.jquery.com/jquery-1.9.1.js" />

$(function () {
    $('.mensagem').click(function () {
        var url = $(this).data('url');

        $.get(url, function (data) {
            $('#mensagem').html(data);
        });
    })
});