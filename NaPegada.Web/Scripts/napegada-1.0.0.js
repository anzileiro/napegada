$(function () {
    $('.alerts-user').tooltip();

    $('.dropdown-toggle').on('click', function () {
        
    });
});

//$(function () {
//    $('#form-register').on('submit', function () {
//        userVM.getUserInput();
//        func.send('Register', 'post', userVM);
//    });
//});

//var func = {
//    'send': function (event, url_, type_, data_) {
//        $.ajax({
//            url: url_,
//            type: type_,
//            data: data_,
//            success: function (x) {
//                alert(x);
//            }
//        });
//    }
//};

//var userVM = {
//    'User': {
//        'Name': '',
//        'Mail': '',
//        'Password': ''
//    },
//    'getUserInput': function () {
//        userVM.User.Mail = $('#User_Mail').val();
//        userVM.User.Password = $('#User_Password').val();
//    }
//};