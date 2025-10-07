
var table;

const MODELO_BASE = {
    IdPaciente: 0,
    NroCi: "",
    Nombres: "",
    Apellidos: "",
    FechaNacimiento: "",
    Genero: "M",
    Telefono: "",
    Alergias: ""
}

function FechaActual() {
    const d = new Date();
    const month = (d.getMonth() + 1).toString().padStart(2, '0');
    const day = d.getDate().toString().padStart(2, '0');
    return `${day}/${month}/${d.getFullYear()}`;
}

$(document).ready(function () {
    $.datepicker.setDefaults($.datepicker.regional["es"])
    $("#txtFechaNacido").datepicker({ dateFormat: "dd/mm/yy" });
    $("#txtFechaNacido").val(FechaActual());

    cargarListaPaci();

})

function cargarListaPaci() {
    if ($.fn.DataTable.isDataTable("#tbpacientes")) {
        $("#tbpacientes").DataTable().destroy();
        $('#tbpacientes tbody').empty();
    }

    table = $("#tbpacientes").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PagePacientes.aspx/ListaPacientes',
            "type": "POST", // Cambiado a POST
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            "data": function (d) {
                return JSON.stringify(d);
            },
            "dataSrc": function (json) {
                //console.log("Response from server:", json.d.objeto);
                if (json.d.Estado) {
                    return json.d.Data; // Asegúrate de que esto apunta al array de datos
                } else {
                    return [];
                }
            }
        },
        "columns": [
            { "data": "IdPaciente", "visible": false, "searchable": false },
            { "data": "FullNombre" },
            { "data": "NroCi" },
            { "data": "Telefono" },
            { "data": "Edad" },
            {
                "data": "Genero",
                "render": function (data) {
                    if (data === "M") {
                        return '<span class="badge badge-info">Masculino</span>';
                    } else {
                        return '<span class="badge badge-danger">Femenino</span>';
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

//$("#txtnroci").inputFilter(function (value) {
//    return /^\d*$/.test(value) && value.length <= 10;
//});

function mostrarModal(modelo, cboEstadoDeshabilitado = true) {
    // Verificar si modelo es null
    modelo = modelo ?? MODELO_BASE;

    $("#txtIdPaciente").val(modelo.IdPaciente);
    $("#txtNombre").val(modelo.Nombres);
    $("#txtApellidos").val(modelo.Apellidos);
    $("#txtnroci").val(modelo.NroCi);
    $("#txtCelular").val(modelo.Telefono);
    $("#cboSexo").val(modelo.Genero === "M" ? 1 : 0);
    $("#txtFechaNacido").val(modelo.FechaNacimiento === "" ? FechaActual() : modelo.FechaNacimiento);
    $("#txtAlergias").val(modelo.Alergias);

    if (cboEstadoDeshabilitado) {
        $("#myTitulopr").text("Nuevo Registro");
    } else {
        $("#myTitulopr").text("Editar Paciente");
    }

    $("#modalPaciente").modal("show");
}

$("#tbpacientes tbody").on("click", ".btn-editar", function (e) {
    e.preventDefault();
    let filaSeleccionada;

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const model = table.row(filaSeleccionada).data();
    mostrarModal(model, false);
})

$('#btnAddNuevoReg').on('click', function () {
    mostrarModal(null, true);
    //$("#modalPaciente").modal("show");
})

function habilitarBoton() {
    $('#btnGuardarCambios').prop('disabled', false);
}

function registrarPaciente() {

    const modelo = structuredClone(MODELO_BASE);
    modelo["IdPaciente"] = parseInt($("#txtIdPaciente").val());
    modelo["Nombres"] = $("#txtNombre").val().trim();
    modelo["Apellidos"] = $("#txtApellidos").val().trim();
    modelo["NroCi"] = $("#txtnroci").val().trim();
    modelo["Telefono"] = $("#txtCelular").val().trim();
    modelo["Genero"] = ($("#cboSexo").val() == "1" ? "M" : "F");
    modelo["FechaNacimiento"] = $("#txtFechaNacido").val().trim();
    modelo["Alergias"] = $("#txtAlergias").val().trim();

    var request = {
        oPaciente: modelo
    };

    $.ajax({
        type: "POST",
        url: "PagePacientes.aspx/Guardar",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $(".modal-content").LoadingOverlay("show");
        },
        success: function (response) {
            $(".modal-content").LoadingOverlay("hide");
            if (response.d.Estado) {
                cargarListaPaci();
                $('#modalPaciente').modal('hide');
                swal("Mensaje", response.d.Mensaje, "success");
            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $(".modal-content").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            habilitarBoton();
        }
    });
}

function editarPacientes() {

    const modelo = structuredClone(MODELO_BASE);
    modelo["IdPaciente"] = parseInt($("#txtIdPaciente").val());
    modelo["Nombres"] = $("#txtNombre").val().trim();
    modelo["Apellidos"] = $("#txtApellidos").val().trim();
    modelo["NroCi"] = $("#txtnroci").val().trim();
    modelo["Telefono"] = $("#txtCelular").val().trim();
    modelo["Genero"] = ($("#cboSexo").val() == "1" ? "M" : "F");
    modelo["FechaNacimiento"] = $("#txtFechaNacido").val().trim();
    modelo["Alergias"] = $("#txtAlergias").val().trim();

    var request = {
        oPaciente: modelo
    };

    $.ajax({
        type: "POST",
        url: "PagePacientes.aspx/Editar",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $(".modal-content").LoadingOverlay("show");
        },
        success: function (response) {
            $(".modal-content").LoadingOverlay("hide");
            if (response.d.Estado) {
                cargarListaPaci();
                $('#modalPaciente').modal('hide');
                swal("Mensaje", response.d.Mensaje, "success");
            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $(".modal-content").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            habilitarBoton();
        }
    });
}

$('#btnGuardarCambios').on('click', function () {

    // Deshabilitar el botón para evitar múltiples envíos
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

    if (parseInt($("#txtIdPaciente").val()) === 0) {
        registrarPaciente();
    } else {
        editarPacientes();
    }
})

// fin funciones