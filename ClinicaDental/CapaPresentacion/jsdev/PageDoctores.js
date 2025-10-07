
var table;

const MODELO_BASE = {
    IdDoctor: 0,
    NroCi: "",
    Nombres: "",
    Apellidos: "",
    Telefono: "",
    Correo: "",
    ClaveHash: "",
    Estado: true
}

$(document).ready(function () {

    cargarListaDoctor();

})

function cargarListaDoctor() {
    if ($.fn.DataTable.isDataTable("#tbDoctores")) {
        $("#tbDoctores").DataTable().destroy();
        $('#tbDoctores tbody').empty();
    }

    table = $("#tbDoctores").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PageDoctores.aspx/ListaDoctores',
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
            { "data": "IdDoctor", "visible": false, "searchable": false },
            { "data": "FullNombreDr" },
            { "data": "NroCi" },
            { "data": "Telefono" },
            { "data": "Correo" },
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

$.fn.inputFilter = function (inputFilter) {
    return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function (e) { // Captura el evento como 'e'
        if (inputFilter(this.value) || e.key === "Backspace" || e.key === " ") { // Se usa 'e' en lugar de 'event'
            this.oldValue = this.value;
            this.oldSelectionStart = this.selectionStart;
            this.oldSelectionEnd = this.selectionEnd;
        } else if (this.hasOwnProperty("oldValue")) {
            this.value = this.oldValue;
            this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
        } else {
            this.value = "";
        }
    });
};

$("#txtCelular").inputFilter(function (value) {
    return /^\d*$/.test(value) && value.length <= 8;
});

$("#txtNombre").inputFilter(function (value) {
    return /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]*$/u.test(value);
});

$("#txtApellidos").inputFilter(function (value) {
    return /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]*$/u.test(value);
});

function mostrarModal(modelo, cboEstadoDeshabilitado = true) {
    // Verificar si modelo es null
    modelo = modelo ?? MODELO_BASE;

    $("#txtIdDoctor").val(modelo.IdDoctor);
    $("#txtNombre").val(modelo.Nombres);
    $("#txtApellidos").val(modelo.Apellidos);
    $("#txtnroci").val(modelo.NroCi);
    $("#txtCelular").val(modelo.Telefono);

    $("#txtCorreo").val(modelo.Correo);
    $("#txtClave").val(modelo.ClaveHash);
    $("#cboEstado").val(modelo.Estado ? 1 : 0);
    //$("#cboEstado").val(modelo.Estado == true ? 1 : 0);

    $("#cboEstado").prop("disabled", cboEstadoDeshabilitado);
    $("#myTitulodr").text(cboEstadoDeshabilitado ? "Nuevo Registro" : "Editar Doctor");

    // Deshabilitar clave si es edición
    //$("#txtClave").prop("disabled", !cboEstadoDeshabilitado); 

    //if (cboEstadoDeshabilitado) {
    //    $("#myTitulodr").text("Nuevo Registro");
    //} else {
    //    $("#myTitulodr").text("Editar Doctor");
    //}

    $("#modalDoctores").modal("show");
}

$("#tbDoctores tbody").on("click", ".btn-editar", function (e) {
    e.preventDefault();
    //let filaSeleccionada;

    //if ($(this).closest("tr").hasClass("child")) {
    //    filaSeleccionada = $(this).closest("tr").prev();
    //} else {
    //    filaSeleccionada = $(this).closest("tr");
    //}

    //const model = table.row(filaSeleccionada).data();
    //mostrarModal(model, false);

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
function guardarOEditarDoctor(url, request) {
    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#modalDoctores").find("div.modal-content").LoadingOverlay("hide");

            if (response.d.Estado) {
                cargarListaDoctor();
                $('#modalDoctores').modal('hide');
                swal("Mensaje", response.d.Mensaje, "success");
            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#modalDoctores").find("div.modal-content").LoadingOverlay("hide");
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

    const modelo = structuredClone(MODELO_BASE);
    modelo["IdDoctor"] = parseInt($("#txtIdDoctor").val());
    modelo["Nombres"] = $("#txtNombre").val().trim();
    modelo["Apellidos"] = $("#txtApellidos").val().trim();
    modelo["NroCi"] = $("#txtnroci").val().trim();
    modelo["Telefono"] = $("#txtCelular").val().trim();
    modelo["Correo"] = $("#txtCorreo").val().trim();
    modelo["ClaveHash"] = $("#txtClave").val().trim();
    modelo["Estado"] = ($("#cboEstado").val() == "1" ? true : false);

    const request = { oDoctor: modelo };
    const url = modelo.IdDoctor === 0
        ? "PageDoctores.aspx/Guardar"
        : "PageDoctores.aspx/Editar";

    $("#modalDoctores").find("div.modal-content").LoadingOverlay("show");
    guardarOEditarDoctor(url, request);
});

// fin funciones