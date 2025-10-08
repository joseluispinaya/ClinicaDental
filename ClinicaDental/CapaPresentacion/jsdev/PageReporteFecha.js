
var table;
function ObtenerFechaA() {
    const d = new Date();
    const month = (d.getMonth() + 1).toString().padStart(2, '0');
    const day = d.getDate().toString().padStart(2, '0');
    return `${day}/${month}/${d.getFullYear()}`;
}

$(document).ready(function () {
    $.datepicker.setDefaults($.datepicker.regional["es"])

    $("#txtFechaInicio").datepicker({ dateFormat: "dd/mm/yy" });
    $("#txtFechaFin").datepicker({ dateFormat: "dd/mm/yy" });

    $("#txtFechaInicio").val(ObtenerFechaA());
    $("#txtFechaFin").val(ObtenerFechaA());
    listaAtencionesRpt();

});

function listaAtencionesRpt() {
    if ($.fn.DataTable.isDataTable("#tbdarptAtencion")) {
        $("#tbdarptAtencion").DataTable().destroy();
        $('#tbdarptAtencion tbody').empty();
    }
    var request = {
        fechainicio: $("#txtFechaInicio").val(),
        fechafin: $("#txtFechaFin").val()
    };

    table = $("#tbdarptAtencion").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PageReporteFecha.aspx/ReporteAtencFechas',
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
            { "data": "NombrePaciente" },
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
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

$('#btnBuscar').on('click', function () {

    listaAtencionesRpt();

})

function detallePView(IdAtencion) {

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

$("#tbdarptAtencion tbody").on("click", ".btn-detalle", function (e) {
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
    $("#txtPaciented").val(model.NombrePaciente);
    $("#txtTotald").val(model.PrecioTotStr);
    $("#txtDescuentod").val(model.PrecioDescStr);
    $("#txtTotalpad").val(model.TotalPagaStr);

    detallePView(model.IdAtencion);
})

function ReporteDetPdf(dataVenta) {

    if (dataVenta.length < 1) {
        swal("Mensaje", "No se puede Generar reporte Vacio", "error");
        return;
    }

    var fechaini = $("#txtFechaInicio").val();
    var fechafin = $("#txtFechaFin").val();

    const total = parseFloat(
        dataVenta.reduce((acc, item) => acc + parseFloat(item.Totalpagado), 0).toFixed(2)
    );

    //const totalCaja = dataVenta.reduce((acc, item) => acc + parseFloat(item.Totalpagado), 0);
    const Canti = dataVenta.length;

    var props = {
        outputType: jsPDFInvoiceTemplate.OutputType.Save,
        returnJsPDFDocObject: true,
        fileName: "Reportes_AtencionFechas_2025",
        orientationLandscape: true,
        //compress: true,
        logo: {
            src: "../Imagenes/reporttf.png",
            type: 'PNG', //optional, when src= data:uri (nodejs case)
            width: 53.33, //aspect ratio = width/height
            height: 26.66,
            margin: {
                top: 0, //negative or positive num, from the current position
                left: 0 //negative or positive num, from the current position
            }
        },
        business: {
            name: "CLINICA-SU",
            address: "Riberalta Beni",
            phone: "+591 73999726",
            email: "Reporte Caja",
            email_1: "clinica@gmail.com",
            website: "www.clinica.com",
        },
        contact: {
            label: "Reporte General:",
            name: "Atencion",
            address: "Adminstracion de clinica",
            phone: "Soporte tecnico",
            email: "soporte_asba@gmail.com",
            otherInfo: "Codigo interno /0002",
        },
        invoice: {
            label: "Fechas: ",
            num: "- Consulta",
            invDate: `Desde: ${fechaini}`,
            invGenDate: `Hasta: ${fechafin}`,
            headerBorder: false,
            tableBodyBorder: false,
            header: ["Codigo", "Paciente", "Nombre Medico", "Total", "Descuento", "Pagado"],
            table: dataVenta.map((item, index) => [
                item.Codigo,
                item.NombrePaciente,
                item.NombreDoctor,
                item.PrecioTotStr,
                item.PrecioDescStr,
                item.TotalPagaStr
            ]),
            invTotalLabel: "Total Caja:",
            invTotal: total.toString(),
            invCurrency: "/BS",
            row1: {
                col1: 'Total Atnc:',
                col2: Canti.toString(),
                col3: 'REG.',
                style: {
                    fontSize: 10
                }
            },
            invDescLabel: "Gracias por usar nuestro sistema",
            invDesc: "Reporte interno de uso solo para propietario del sistema Riberalta-Bolivia.",
        },
        footer: {
            text: "Este es un documento generado automáticamente.",
        },
        pageEnable: true,
        pageLabel: "Page ",
    };

    var pdfObject = jsPDFInvoiceTemplate.default(props);
    console.log(pdfObject);
}

function cargarDetalleReporteEx() {

    var request = {
        fechainicio: $("#txtFechaInicio").val(),
        fechafin: $("#txtFechaFin").val()
    };

    $.ajax({
        type: "POST",
        url: "PageReporteFecha.aspx/ReporteAtencFechas",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            if (response.d.Estado) {
                var detalle = response.d.Data;
                //console.log(detalle)

                if (detalle.length < 1) {
                    swal("Mensaje", "No se puede Generar reporte Vacio", "error");
                    return;
                }

                ReporteDetPdf(detalle)
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

$('#btnImprimir').on('click', function () {

    if ($("#txtFechaInicio").val().trim() === "" || $("#txtFechaFin").val().trim() === "") {
        toastr.warning("", "Debe Ingresar fecha inicio y fin", "Advertencia");
        return;
    }
    cargarDetalleReporteEx();
    //swal("Mensaje", "Falta Implementar Este boton", "warning");

})

//fin funciones