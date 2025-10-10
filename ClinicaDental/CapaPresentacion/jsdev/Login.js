
$('#btnInicia').on('click', function () {

    $('#btnInicia').prop('disabled', true);

    //VALIDACIONES DE USUARIO
    if ($("#usuario").val().trim() === "" || $("#password").val().trim() === "") {
        swal("Mensaje", "Complete los datos para iniciar sesion", "warning");
        $('#btnInicia').prop('disabled', false);
        return;
    }

    loginSistema();
})

function loginSistema() {

    $.ajax({
        type: "POST",
        url: "Login.aspx/Logeo",
        data: JSON.stringify({ Usuario: $("#usuario").val().trim(), Clave: $("#password").val().trim() }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            $.LoadingOverlay("show", {
                background: "rgba(0,0,0,0.3)",
                image: "",
                fontawesome: "fa fa-spinner fa-spin"
            });
        },
        success: function (response) {
            $.LoadingOverlay("hide");
            if (response.d.Estado) {

                const usuarioData = response.d.Data;

                if (!usuarioData.Estado) {
                    swal("Atención", "El usuario no se encuentra activo.", "warning");
                } else {
                    sessionStorage.setItem('usuarioCli', JSON.stringify(usuarioData));
                    swal({
                        title: "Bienvenido",
                        text: `Hola ${usuarioData.Nombres || "Usuario"} 👋`,
                        type: "success",
                        timer: 1500,
                        showConfirmButton: false
                    });

                    setTimeout(() => window.location.href = 'Inicio.aspx', 1200);
                }

                $("#usuario, #password").val("");

            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $.LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            swal("Error", "Ocurrió un problema al intentar iniciar sesión. Intente nuevamente.", "error");
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnInicia').prop('disabled', false);
        }
    });
}