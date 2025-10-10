
var table;

$(document).ready(function () {
    historiPacientesFil();
})
function historiPacientesFil() {

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
        templateResult: formatoRespu
    });
}

function formatoRespu(data) {

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
    $("#txtEdadPac").val(data.Edad);
    $("#txtAlergia").val(data.Alergias);
    $("#txtCelularpac").val(data.Telefono);
    $("#txtFechaNapac").val(data.FechaNacimiento);
    detalleHistoPaciente(data.IdPaciente);
    //console.log(data);

    $("#cboBuscarPaciente").val("").trigger("change")
});

function detalleHistoPaciente(idPaciente) {
    if ($.fn.DataTable.isDataTable("#tbpacientesHisto")) {
        $("#tbpacientesHisto").DataTable().destroy();
        $('#tbpacientesHisto tbody').empty();
    }

    var request = {
        IdPaciente: idPaciente
    };

    table = $("#tbpacientesHisto").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PageHistorial.aspx/DetalleAtencionPaciente',
            "type": "POST", // Cambiado a POST
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            "data": function () {
                return JSON.stringify(request);
            },
            "dataSrc": function (json) {
                //console.log("Response from server:", json.d.Data);
                if (json.d.Estado) {
                    return json.d.Data; // Asegúrate de que esto apunta al array de datos
                } else {
                    return [];
                }
            },
            "beforeSend": function () {
                $("#cargann").LoadingOverlay("show");
            },
            "complete": function () {
                $("#cargann").LoadingOverlay("hide");
            }
        },
        "columns": [
            { "data": "IdAtencion", "visible": false, "searchable": false },
            { "data": "Codigo" },
            { "data": "NombreDoctor" },
            { "data": "PrecioTotStr" },
            { "data": "PrecioDescStr" },
            { "data": "TotalPagaStr" },
            {
                "defaultContent": '<button class="btn btn-info btn-detalle btn-sm"><i class="fas fa-eye"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "40px"
            }
        ],
        "order": [[0, "desc"]],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

function detalleView(IdAtencion) {

    $("#tbDetalleHis tbody").html("");

    var request = { IdAtencion: IdAtencion }

    $.ajax({
        type: "POST",
        url: "PageReporteFecha.aspx/DetalleHistorialCli",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            if (response.d.Estado) {
                var detalle = response.d.Data;

                $("#txtTocant").val(detalle.length + " Atenciones");

                $("#tbDetalleHis tbody").html("");

                $.each(detalle, function (index, item) {
                    $("<tr>").append(
                        $("<td>").text(item.RefTratamiento.Nombre),
                        $("<td>").text(item.Cantidad),
                        $("<td>").text(item.PrecioStr),
                        $("<td>").text(item.TotalInStr)
                    ).appendTo("#tbDetalleHis tbody");
                });

                $("#modalDatadet").modal("show");
                //swal("Mensaje", response.d.Mensaje, "success");

            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        }
    });
}

$("#tbpacientesHisto tbody").on("click", ".btn-detalle", function (e) {
    e.preventDefault();
    let filaSeleccionada;

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const model = table.row(filaSeleccionada).data();
    $("#txtFechaRegistrod").val(model.FechaAtencion);
    $("#txtMedicod").val(model.NombreDoctor);
    $("#costoTotalc").text(model.PrecioTotStr);
    $("#descuento").text(model.PrecioDescStr);
    $("#totalcaja").text(model.TotalPagaStr);

    detalleView(model.IdAtencion)

})

//fin funciones