﻿(function () {

    $('#LoginButton').click(function (e) {
        e.preventDefault();
        console.log("URL:", window.location.href);
        abp.ui.setBusy(
            $('#LoginArea'),
            abp.ajax({
                url: abp.appPath + 'Account/Login',
                type: 'POST',
                data: JSON.stringify({
                    username: $('#EmailAddressInput').val(),
                    password: $('#PasswordInput').val(),
                    rememberMe: $('#RememberMeInput').is(':checked')
                })
            })
        );
    });

})();