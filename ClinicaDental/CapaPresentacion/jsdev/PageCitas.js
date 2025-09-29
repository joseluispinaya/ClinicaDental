
function ObtenerFechaA() {
    const d = new Date();
    const month = (d.getMonth() + 1).toString().padStart(2, '0');
    const day = d.getDate().toString().padStart(2, '0');
    return `${day}/${month}/${d.getFullYear()}`;
}

$(document).ready(function () {
    $.datepicker.setDefaults($.datepicker.regional["es"])
    $("#txtFechaCita").datepicker({ dateFormat: "dd/mm/yy" });
    $("#txtFechaCita").val(ObtenerFechaA());

    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month, basicWeek, basicDay'
        }
    });

    $('#timepicker2').timepicker({ showMeridian: false });
    cargarListaPaci();
    cargarPacientesFil();

})

function cargarListaPaci() {

    $.ajax({
        type: "POST",
        url: "PageCitas.aspx/ListaPacientes",
        data: {},
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (response) {
            if (response.d.Estado) {

                console.log(response.d.Data);
                //console.log(response);
            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }

        }
    });
}
function cargarPacientesFil() {

    $("#cboBuscarPaciente").select2({
        ajax: {
            url: "PageCitas.aspx/ObtenerPacientesFiltro",
            dataType: 'json',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            delay: 250,
            data: function (params) {
                return JSON.stringify({ busqueda: params.term });
            },
            processResults: function (data) {

                return {
                    results: data.d.Data.map((item) => ({
                        id: item.IdPaciente,
                        NroCi: item.NroCi,
                        text: item.Nombres,
                        Apellidos: item.Apellidos,
                        paciente: item
                    }))
                };
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            }
        },
        language: "es",
        placeholder: 'Buscar Paciente',
        minimumInputLength: 1,
        templateResult: formatoRes
    });
}

function formatoRes(data) {

    var imagenes = "Imagenes/odontologia.png";
    // Esto es por defecto, ya que muestra el "buscando..."
    if (data.loading)
        return data.text;

    var contenedor = $(
        `<table width="100%">
            <tr>
                <td style="width:60px">
                    <img style="height:60px;width:60px;margin-right:10px" src="${imagenes}"/>
                </td>
                <td>
                    <p style="font-weight: bolder;margin:2px">${data.text} ${data.Apellidos}</p>
                    <p style="margin:2px">${data.NroCi}</p>
                </td>
            </tr>
        </table>`
    );

    return contenedor;
}

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();

});

// Evento para manejar la selección del cliente
$("#cboBuscarPaciente").on("select2:select", function (e) {

    var data = e.params.data.paciente;
    $("#txtIdPaciente").val(data.IdPaciente);
    $("#txtNombrePac").val(data.Nombres + " " + data.Apellidos);
    $("#txtNroci").val(data.NroCi);
    $("#txtAlergia").val(data.Alergias);

    $("#cboBuscarPaciente").val("").trigger("change")
    //console.log(data);
});