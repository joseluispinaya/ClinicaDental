
let estadoRese = false;

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

    //$('#calendar').fullCalendar({
    //    header: {
    //        left: 'prev,next today',
    //        center: 'title',
    //        right: 'month, basicWeek, basicDay'
    //    }
    //});
    $('#timepicker2').timepicker({ showMeridian: false });
    cargarCitasCalendar();
    cargarPacientesFil();
    ObtenerHora();
})

function ObtenerHora() {
    const fechaActual = new Date();
    let showMer = fechaActual.getHours();
    let jornada = showMer >= 12 ? 'PM' : 'AM';
    const fechact = ObtenerFechaA();

    const hora = String(fechaActual.getHours()).padStart(2, '0');
    //const min = String(fechaActual.getMinutes()).padStart(2, '0');
    //`${fechact} : ${min}`;
    $("#txtFechaActual").val(`${fechact} ${hora} ${jornada}`);
}

function cargarCitasCalendar() {

    //const usuario = JSON.parse(sessionStorage.getItem('usuarioIn'));
    //var request = { IdInmobi: usuario.IdInmobiliaria }
    var request = { IdDoctor: 1 }

    $.ajax({
        type: "POST",
        url: "PageListaCitas.aspx/ObtenerCitasPorDoctor",
        data: JSON.stringify(request),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (response) {
            if (response.d.Estado) {
                var events = [];

                //console.log(data.d.objeto);
                //start: row.FechaReserva,
                //var fechaHora = row.FechaReserva + 'T' + row.Hora; // Concatenar en formato ISO 8601 (yyyy-MM-ddTHH:mm)
                $.each(response.d.Data, function (i, row) {
                    var fechaHora = row.FechaCita + 'T' + row.HoraCita;

                    events.push({
                        id: row.IdCita,
                        title: row.Estado,
                        start: fechaHora,
                        detalles: row.Detalles,
                        activo: row.Activo,
                        color: row.Color,
                        textColor: 'white'
                    });
                });

                $('#calendar').fullCalendar('destroy');
                $('#calendar').fullCalendar({
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month, basicWeek, basicDay'
                    },
                    editable: true,
                    events: events,
                    eventClick: function (calEvent, jsEvent, view) {

                        //$("#txtIdVisi").val("0");
                        //$("#lblferegi").text(calEvent.fechareg);
                        estadoRese = calEvent.activo;
                        detalleSolicit(calEvent.id);
                    }
                });
            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }

        }
    });
}

function detalleSolicit($idCita) {


    var request = {
        IdCita: $idCita
    };

    $.ajax({
        type: "POST",
        url: "PageListaCitas.aspx/DetalleCitasci",
        data: JSON.stringify(request),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response.d.Estado) {

                $("#txtIdCitaAt").val($idCita);
                var datos = response.d.Data;

                $("#txtNombrePaci").val(datos.RefPaciente.Nombres);
                $("#txtMedicoAt").val(datos.RefDoctor.Nombres);
                $("#txtFechCi").val(datos.FechaCita);
                $("#txtHoraCit").val(datos.HoraCita);
                $("#txtRegFec").val(datos.FechaRegistro);
                $("#txtComentAtc").val(datos.Detalles);


                if (estadoRese) {
                    $("#btnGuardarAtencitt").prop("disabled", false).text("Generar Atención");
                } else {
                    $("#btnGuardarAtencitt").prop("disabled", true).text("Ya se Atendido");
                }

                $("#modalDetalleCi").modal("show");
            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
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
    $("#txtEdadPac").val(data.Edad);
    $("#txtAlergia").val(data.Alergias);
    //console.log(data);

    $("#cboBuscarPaciente").val("").trigger("change")
});

function registroCitas() {

    //const cliente = JSON.parse(sessionStorage.getItem('clienteIn'));
    //IdDoctor: parseInt(cliente.IdDoctor),
    var modelo = {
        IdPaciente: parseInt($("#txtIdPaciente").val()),
        IdDoctor: 2,
        FechaCita: $("#txtFechaCita").val(),
        HoraCita: $("#timepicker2").val(),
        Detalles: $("#txtDetalle").val().trim()
    }

    var request = {
        oCita: modelo
    };

    $.ajax({
        type: "POST",
        url: "PageListaCitas.aspx/RegistrarCitas",
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $("#loadi").LoadingOverlay("show");
        },
        success: function (response) {
            $("#loadi").LoadingOverlay("hide");
            if (response.d.Estado) {
                cargarCitasCalendar();
                swal("Mensaje", response.d.Mensaje, "success");

            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#loadi").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            swal("Error", "Ocurrió un problema intente mas tarde", "error");
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnGuardarCit').prop('disabled', false);
        }
    });
}

$('#btnGuardarCit').on('click', function () {

    $('#btnGuardarCit').prop('disabled', true);

    // const cliente = sessionStorage.getItem('clienteIn');

    // if (!cliente) {
    //     swal("Mensaje", "Debe iniciar sesion", "warning");
    //     $('#btnGuardarReserva').prop('disabled', false);
    //     return;
    // }

    //var fechaReseStra = $("#txtFechaRese").val();
    var fechacitaStra = $("#txtFechaCita").val().trim();

    if (fechacitaStra === "") {
        toastr.warning("", "Debe ingresar una fecha para la cita");
        $('#btnGuardarCit').prop('disabled', false);
        return;
    }

    var fechaResePartsa = fechacitaStra.split('/');
    var fechaResea = new Date(fechaResePartsa[2], fechaResePartsa[1] - 1, fechaResePartsa[0]);
    var fechaActual = new Date();
    fechaActual.setHours(0, 0, 0, 0);

    var horaReser = $("#timepicker2").val();

    if ($("#txtDetalle").val().trim() === "") {
        toastr.warning("", "Debe agregar un comentario");
        $("#txtDetalle").focus();
        $('#btnGuardarCit').prop('disabled', false);
        return;
    }
    if (horaReser === "") {
        toastr.warning("", "Debe Seleccionar la Hora");
        $('#btnGuardarCit').prop('disabled', false);
        return;
    }

    if (fechaResea <= fechaActual) {
        swal("Mensaje", "Debe ingresar una fecha mayor a la actual", "warning");
        $('#btnGuardarCit').prop('disabled', false);
        return;
    }


    registroCitas();
});

function atenderLaCita() {

    var request = {
        idCita: parseInt($("#txtIdCitaAt").val())
    };

    $.ajax({
        type: "POST",
        url: "PageListaCitas.aspx/RegistrarAtencionCitr",
        data: JSON.stringify(request),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            $("#loatei").LoadingOverlay("show");
        },
        success: function (response) {
            $("#loatei").LoadingOverlay("hide");
            if (response.d.Estado) {
                cargarCitasCalendar();
                $('#modalDetalleCi').modal('hide');
                swal("Mensaje", response.d.Mensaje, "success");

            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#loatei").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        }
    });
}

$('#btnGuardarAtencitt').on('click', function () {

    $('#modalDetalleCi').modal('hide');
    swal("Mensaje", "Atencion realizada", "warning");

    //const usuario = sessionStorage.getItem('usuarioIn');

    //if (!usuario) {
    //    swal("Mensaje", "Debe iniciar sesion nuevamente", "warning");
    //    return;
    //}

    //atenderLaCita();
})

// mas datos