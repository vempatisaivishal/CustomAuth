$(document).ready(function () {
    $.validator.addMethod("EmailDomain", function (value, element, args) {
        let regex = /^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$/;
        return regex.test(value);
    }, 'Email are only from domain.com')
    $.validator.addMethod("PasswordStrength", function (value, element, args) {
        let regex = /^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@@#$&*]).{8,}$/;
        return regex.test(value);
    }, "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character, and be at least 8 characters long.")
    $.validator.addMethod("nowhitespace",
        function (a, b)
        {
            return this.optional(b) || /^\S+$/i.test(a)
        }, "No white space please.")
    $.validator.addMethod("alphanumeric",
        function (a, b) {
            return this.optional(b) || /^\w+$/i.test(a)
        }, "Letters, numbers, and underscores only please.")
    $.validator.addMethod("check_date_of_birth", function (value, element) {

        var birthday = $("#profile_birthday").val();
        var birthdaydate = Date.parse(birthday);
        var difference = Date.now() - birthdaydate;
        var ageYear = new Date(difference);
        var age = Math.abs(ageYear.getUTCFullYear() - 1970);
        return age >= 18;
    }, "You must be at least 18 years of age.");
    $('form').validate({
        rules: {
            FirstName: {
                required: true,
                minlength: 5,
                maxlength: 20,
                nowhitespace: true,
                alphanumeric:true,
            },
            LastName: {
                required: true,
                minlength: 5,
                maxlength: 20,
                nowhitespace: true,
                alphanumeric: true,
            },
            UserName: {
                required: true,
                minlength: 5,
                maxlength: 20,
                nowhitespace: true,
                alphanumeric: true,
            },
            DateOfBirth: {
                required: true,
                check_date_of_birth:true,
            },
            Gender: {
                required: true
            },
            Role: {
                required: true
            },

            Password: {
                required: true,
                minlength: 5,
                maxlength: 20,
                PasswordStrength: true,
                nowhitespace: true,

            },
            ConfirmPassword: {
                required: true,
                minlength: 5,
                maxlength: 20,
                equalTo: '#password',
                nowhitespace: true,
            },
            email: {
                required: true,
                EmailDomain: true,
            },

        },
        messages: {
            Password: {
                required: "please enter the password",
                minlength: "Minimum Length need to be 5",
                maxlength: "Maximum Length need to be 20",

            },
            ConfirmPassword: {
                equalTo: "Please check password and confirm password should match",
                required: "Please Reenter the password",
                minlength: "Minimum Length need to be 5",
                maxlength: "Maximum Length need to be 20",
            },
            UserName: {
                required: "Please enter the User Name",
                minlength: "Minimum Length need to be 5",
                maxlength: "Maximum Length need to be 20",
            },
            FirstName: {
                required: "Please enter the first Name",
                minlength: "The Minimum Length is 5",
                maxlength: "The Maximum Length is 20",
            },
            LastName: {
                required: "Please enter the last Name",
                minlength: "Minimum Length need to be 5",
                maxlength: "Maximum Length need to be 20",

            },
            DateOfBirth: {
                required: "Please Enter the Date Of Birth",
            },
            Gender: {
                required: "Please Enter Your Gender",
            },
            Role: {
                required: "Please enter your Role",
            }

        }
    });
    $("#btn1").on('click', function () {
        console.log($('form').valid());
    });
});