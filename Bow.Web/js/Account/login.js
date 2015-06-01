(function () {
    $('#LoginButton').click(function (e) {
        e.preventDefault();
        getToken();
    });

    function getToken() {
        $.ajax({
            url: 'http://apps.reintegracion.gov.co/Token',
            type: 'POST',
            dataType: 'json',
            contentType: "application/x-www-form-urlencoded",
            data: { username: 'desarrollo', password: 'Developers01*', grant_type: 'password' },
            success: function (data, textStatus, xhr) {
                validateSesion(data.token_type, data.access_token)
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Ha ocurrido un problema, inténtelo nuevamente.");
            }
        });
    }

    function validateSesion(tokenType, tokenKey) {
        $.ajax({
            url: 'http://apps.reintegracion.gov.co/api/Users/ValidarProfesional?username=pruebarolessir&password=$Colombia12345$',
            type: 'GET',
            headers: {'Authorization': tokenType+' '+tokenKey},
            success: function (data, textStatus, xhr) {
                abp.ui.setBusy(
                    $('#LoginArea'),
                    abp.ajax({
                        url: abp.appPath + 'Account/Login',
                        type: 'POST',
                        data: JSON.stringify({
                            username: $('#userInput').val(),
                            password: $('#passwordInput').val(),
                            rememberMe: $('#rememberMeInput').is(':checked')
                        })
                    })
                );
                //$.ajax({
                //    url: abp.appPath + 'Account/Login',
                //    type: 'POST',
                //    dataType: 'json',
                //    data: JSON.stringify({
                //        username: $('#userInput').val(),
                //        password: $('#passwordInput').val(),
                //        rememberMe: $('#rememberMeInput').is(':checked')
                //    }),
                //    success: function (data, textStatus, xhr) {
                //        console.log(data);
                //    },
                //    error: function (xhr, textStatus, errorThrown) {
                //        alert("Ha ocurrido un problema, inténtelo nuevamente.");
                //    }
                //});
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Ha ocurrido un problema, inténtelo nuevamente.");
            }
        });
    }
})();