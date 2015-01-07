var birthdayMonth = $('#BirthdayMonth');
var birthdayDay = $('#BirthdayDay');
var birthdayYear = $('#BirthdayYear');
var birthday = moment([birthdayYear.val(), birthdayMonth.val(), birthdayDay.val()]).format('YYYY-MM-DD');
var lblAge = $('#Age');
var divAge = $('#divAge');

function CalculateAge() {

    if (birthdayMonth.val() != '' && birthdayDay.val() != '' && birthdayYear.val() != '') {

        var birthday = moment([Number(birthdayYear.val()), Number(birthdayMonth.val()) - 1, Number(birthdayDay.val())]);
        var today = moment();
        var age = today.diff(birthday, 'years');

        lblAge.text(age);
    }
}

function ToggleAge() {

    if (birthdayMonth.val() != '' && birthdayDay.val() != '' && birthdayYear.val() != '') {
        divAge.show();
        lblAge.show();
        divAge.prop("style", "display:inline-block;");
    } else {
        divAge.hide();
        lblAge.hide();
    }

}

$(function () {
    //$('.datepicker').datepicker();
    $('#BirthdayYear, #BirthdayMonth, #BirthdayYear').change(CalculateAge);
    $('#BirthdayYear, #BirthdayMonth, #BirthdayYear').change(ToggleAge);
});
