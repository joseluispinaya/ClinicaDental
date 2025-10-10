
$(document).ready(function () {
    const usuarioL = sessionStorage.getItem('usuarioCli');

    if (usuarioL) {
        obtenerDetalleUsuarioRP();
    } else {
        window.location.href = 'Login.aspx';
    }

    //obtenerEsquema();
});

$('#salirsis').on('click', function (e) {
    e.preventDefault();
    CerrarSesion();
});

function obtenerDetalleUsuarioRP() {
    const usuarioL = sessionStorage.getItem('usuarioCli');
    if (usuarioL) {
        const usuario = JSON.parse(usuarioL);

        $("#nomUserg").text(usuario.Apellidos);
        //$("#imgUsumast").attr("src", usuario.ImageFull);


    } else {
        console.error('No se encontró información del usuario en sessionStorage.');
        window.location.href = 'Login.aspx';
    }
}


// Función para cerrar sesión
function CerrarSesion() {
    sessionStorage.clear();
    window.location.replace('Login.aspx');
}