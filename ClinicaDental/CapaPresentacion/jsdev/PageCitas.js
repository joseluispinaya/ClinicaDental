
var table;

$(document).ready(function () {

    //$('#calendar').fullCalendar({
    //    header: {
    //        left: 'prev,next today',
    //        center: 'title',
    //        right: 'month, basicWeek, basicDay'
    //    }
    //});
    listaCitasFull();
    cargarCitasCalendarFull();
})

function cargarCitasCalendarFull() {

    $.ajax({
        type: "POST",
        url: "PageCitas.aspx/ListadeCitasFull",
        data: {},
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (response) {
            if (response.d.Estado) {
                var events = [];

                $.each(response.d.Data, function (i, row) {
                    var fechaHora = row.FechaCita + 'T' + row.HoraCita;

                    events.push({
                        id: row.IdCita,
                        title: row.Estado,
                        start: fechaHora,
                        medico: row.RefDoctor.Nombres,
                        citah: row.FechaHoracita,
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
                        var doc = calEvent.medico;
                        var ci = calEvent.citah;
                        //swal("Mensaje", "Aqui un evento", "warning");
                        swal("Mensaje", "Cita para: " + ci + "\nMedico: " + doc, "success");
                    }
                });
            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }

        }
    });
}

function listaCitasFull() {
    if ($.fn.DataTable.isDataTable("#tbCitasAdmin")) {
        $("#tbCitasAdmin").DataTable().destroy();
        $('#tbCitasAdmin tbody').empty();
    }

    table = $("#tbCitasAdmin").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PageCitas.aspx/ListadeCitasFull',
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
            { "data": "IdCita", "visible": false, "searchable": false },
            { "data": "RefPaciente.Nombres" },
            { "data": "RefDoctor.Nombres" },
            { "data": "FechaHoracita" },
            { "data": "Estado" },
            { "data": "FechaRegistro" }
        ],
        "order": [[0, "desc"]],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}
// fin
