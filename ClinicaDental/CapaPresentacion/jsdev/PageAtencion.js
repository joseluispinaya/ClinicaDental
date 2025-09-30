
let tablaSer;

$(document).ready(function () {
    cargarPacientesFil();
    cargarPro();
})

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
    //console.log(data);

    $("#cboBuscarPaciente").val("").trigger("change")
});

function cargarPro() {

    $("#cboTipoServicio").select2({
        ajax: {
            url: "PageAtencion.aspx/ObtenerTratamientosFiltro",
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
                        id: item.IdTratamiento,
                        text: item.Nombre,
                        Descripcion: item.Descripcion,
                        Precio: item.Precio
                        //Precio: parseFloat(item.Precio)
                    }))
                };
            },
        },
        language: "es",
        placeholder: 'Buscar Servicio',
        minimumInputLength: 1,
        templateResult: formatoResultados
    });
}

function formatoResultados(data) {
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
                    <p style="font-weight: bolder;margin:2px">${data.text}</p>
                    <p style="margin:2px">${data.Descripcion}</p>
                </td>
            </tr>
        </table>`
    );

    return contenedor;
}

let ProductosParaVentaC = [];

$("#cboTipoServicio").on("select2:select", function (e) {
    const data = e.params.data;
    var imagen = "Imagenes/odontologia.png";

    let producto_encontradov = ProductosParaVentaC.filter(p => p.IdTratamiento == data.id)
    if (producto_encontradov.length > 0) {
        $("#cboTipoServicio").val("").trigger("change")
        toastr.warning("", "El servicio ya fue agregado")
        return false;
    }

    swal({
        title: data.text,
        text: data.Descripcion,
        imageUrl: imagen,
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        inputPlaceholder: "Ingrese Cantidad"
    }, function (valor) {
        if (valor === false) {
            return false;
        }

        if (valor === "") {
            toastr.warning("", "Necesita ingresar la cantidad")
            return false;
        }
        if (isNaN(parseInt(valor))) {
            toastr.warning("", "Debe ser un valor numerico")
            return false;
        }

        if (parseInt(valor) <= 0) {
            toastr.warning("", "La cantidad debe ser mayor a cero");
            return false;
        }

        //PrecioAplicado: parseFloat(data.Precio),
        let productod = {
            IdTratamiento: data.id,
            NombreServicio: data.text,
            Cantidad: parseInt(valor),
            PrecioAplicado: data.Precio,
            ImporteTotal: (parseFloat(valor) * data.Precio)
        }

        ProductosParaVentaC.push(productod)

        mosProdr_Precio();
        $("#cboTipoServicio").val("").trigger("change")
        swal.close();
    }
    )
})

function mosProdr_Precio() {

    if ($.fn.DataTable.isDataTable("#tbAtencion")) {
        $("#tbAtencion").DataTable().destroy();
        $("#tbAtencion tbody").empty();
    }

    tablaSer = $("#tbAtencion").DataTable({
        responsive: true,
        data: ProductosParaVentaC,
        columns: [
            {
                defaultContent: '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                orderable: false,
                searchable: false,
                width: "40px"
            },
            { data: "NombreServicio" },
            { data: "Cantidad" },
            { data: "PrecioAplicado" },
            { data: "ImporteTotal" }
        ],
        dom: "rt",
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });

    const total = ProductosParaVentaC.reduce((acc, item) => acc + parseFloat(item.ImporteTotal || 0), 0);

    $("#txtTotalPago").val(total.toFixed(2));

}

$("#tbAtencion tbody").on("click", ".btn-eliminar", function (e) {
    e.preventDefault();
    let filaSeleccionada;

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const data = tablaSer.row(filaSeleccionada).data();

    // Buscar el índice del objeto en DetalleActili para eliminarlo
    ProductosParaVentaC = ProductosParaVentaC.filter(p => p.IdTratamiento != data.IdTratamiento);

    mosProdr_Precio();
});

function calcularTotales() {

    // monto del descuento, si está vacío lo consideramos como 0
    var montoDescuento = $("#txtDescuento").val().trim() === "" ? 0 : parseFloat($("#txtDescuento").val().trim());

    var totalsindescuento = parseFloat($("#txtTotalPago").val().trim());

    // VALIDACIÓN: el descuento no puede ser mayor al total sin descuento
    if (montoDescuento >= totalsindescuento) {
        toastr.error("", "El descuento no puede ser mayor al Total");
        $("#txtDescuento").focus();
        return;
    }

    // total a pagar restando el descuento en Bs
    const totalPago = totalsindescuento - montoDescuento;

    // mostrar el total con descuento en Bs
    $("#txtTotalFin").val(totalPago.toFixed(2));

}

$('#btnCalcular').on('click', function () {

    if (ProductosParaVentaC.length < 1) {
        swal("Mensaje", "No existen datos para el calculo", "error");
        return;
    }

    var descuento = $("#txtDescuento").val().trim();

    if (isNaN(descuento) || parseFloat(descuento) < 0) {
        toastr.warning("", "Debe ingresar un valor de Descuento válido (0 o positivo)");
        $("#txtDescuento").focus();
        return;
    }

    calcularTotales();
})

// atencion