$(document).ready(function() {
    $('#btn-signup').click(function (e) {
        if (ValidateUser(e)) {
            $("#formRegistration").submit();
        }
    });
});
function ValidateUser(event) {
    var return_val = true;
   
    if ($('#Email').val().trim() == '') {
        $('#Email').next('span').text('Please enter Email').show();
        return_val = false;
    } else {
        $('#Email').next('span').hide();
        if (fnValidateEmail($('#Email').val().trim()) == false) {
            $('#Email').next('span').text('Please enter valid Email').show();
            return_val = false;
        } else {
            $('#Email').next('span').text('Please enter Email').hide();
        }
    }
    if ($('#Password').val().trim() == '') {
        $('#Password').next('span').show();
        return_val = false;
    } else {
        $('#Password').next('span').hide();
        if (PasswordStrengthCheck($('#Password').val().trim()) == false) {
            return_val = false;
        }
    }
}

