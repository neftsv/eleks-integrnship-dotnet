// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Script to display password on Registration form
function togglePasswordVisibility() {
    var passwordField = document.getElementById("passwordField");
    var confirmPasswordField = document.getElementById("passwordConfirmation");
    var showPasswordCheckbox = document.getElementById("showPasswordCheckbox");

    if (passwordField.type === "password") {
        passwordField.type = "text";
        confirmPasswordField.type = "text";
    } else {
        passwordField.type = "password";
        confirmPasswordField.type = "password";
    }

    // Оновлення стану чекбокса
    showPasswordCheckbox.checked = (passwordField.type === "text");
}