
var table;

const MODELO_BASE = {
    IdTratamiento: 0,
    Nombre: "",
    Descripcion: "",
    Precio: 0.0,
    Estado: true
}

$(document).ready(function () {

    cargarListaTratamientos();

})

function cargarListaTratamientos() {
    if ($.fn.DataTable.isDataTable("#tbServici")) {
        $("#tbServici").DataTable().destroy();
        $('#tbServici tbody').empty();
    }

    table = $("#tbServici").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PageAtencion.aspx/ListaServicios',
            "type": "POST", // Cambiado a POST
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            "data": function (d) {
                return JSON.stringify(d);
            },
            "dataSrc": function (json) {
                if (json.d.Estado) {
                    return json.d.Data;
                } else {
                    return [];
                }
            }
        },
        "columns": [
            { "data": "IdTratamiento", "visible": false, "searchable": false },
            { "data": "Nombre" },
            { "data": "Descripcion" },
            { "data": "PrecioStr" },
            {
                "data": "Estado",
                "render": function (data) {
                    if (data === true) {
                        return '<span class="badge badge-info">Activo</span>';
                    } else {
                        return '<span class="badge badge-danger">No Activo</span>';
                    }
                }
            },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm"><i class="fas fa-pencil-alt"></i></button>',
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


function mostrarModal(modelo, cboEstadoDeshabilitado = true) {
    // Verificar si modelo es null
    modelo = modelo ?? MODELO_BASE;

    $("#txtIdServicio").val(modelo.IdTratamiento);
    $("#txtServicio").val(modelo.Nombre);
    $("#txtDescipcion").val(modelo.Descripcion);
    $("#txtPrecio").val(modelo.Precio);
    $("#cboEstado").val(modelo.Estado ? 1 : 0);

    $("#cboEstado").prop("disabled", cboEstadoDeshabilitado);
    $("#myTituloServ").text(cboEstadoDeshabilitado ? "Nuevo Registro" : "Editar Servicio");

    $("#modalServicios").modal("show");
}

$("#tbServici tbody").on("click", ".btn-editar", function (e) {
    e.preventDefault();

    let filaSeleccionada = $(this).closest("tr").hasClass("child")
        ? $(this).closest("tr").prev()
        : $(this).closest("tr");

    const model = table.row(filaSeleccionada).data();
    mostrarModal(model, false);
});

$('#btnAddNuevoReg').on('click', function () {
    mostrarModal(null, true);
});

function habilitarBoton() {
    $('#btnGuardarCambios').prop('disabled', false);
}

// Función genérica para guardar o editar
function guardarOEditarServicio(url, request) {
    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#modalServicios").find("div.modal-content").LoadingOverlay("hide");

            if (response.d.Estado) {
                cargarListaTratamientos();
                $('#modalServicios').modal('hide');
                swal("Mensaje", response.d.Mensaje, "success");
            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#modalServicios").find("div.modal-content").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            habilitarBoton();
        }
    });
}

// Guardar o editar al hacer clic
$('#btnGuardarCambios').on('click', function () {
    $('#btnGuardarCambios').prop('disabled', true);

    var precioStr = $("#txtPrecio").val().trim();

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() === "");

    if (inputs_sin_valor.length > 0) {
        const campo = inputs_sin_valor[0].name;
        const $inputVacio = $(`input[name="${campo}"]`);
        toastr.warning("", `Debe completar el campo: "${campo}"`);
        $inputVacio.focus();
        habilitarBoton();
        return;
    }

    if (precioStr === "" || isNaN(precioStr) || parseFloat(precioStr) <= 0) {
        toastr.warning("", "Debe ingresar un Precio válido (mayor a 0)");
        $("#txtPrecio").focus();
        habilitarBoton();
        return;
    }

    const modelo = structuredClone(MODELO_BASE);
    modelo["IdTratamiento"] = parseInt($("#txtIdServicio").val());
    modelo["Nombre"] = $("#txtServicio").val().trim();
    modelo["Descripcion"] = $("#txtDescipcion").val().trim();
    modelo["Precio"] = parseFloat(parseFloat($("#txtPrecio").val()).toFixed(2));
    modelo["Estado"] = ($("#cboEstado").val() == "1" ? true : false);

    const request = { oServicio: modelo };
    const url = modelo.IdTratamiento === 0
        ? "PageServicios.aspx/Guardar"
        : "PageServicios.aspx/Editar";

    $("#modalServicios").find("div.modal-content").LoadingOverlay("show");
    guardarOEditarServicio(url, request);
});

// fin funciones