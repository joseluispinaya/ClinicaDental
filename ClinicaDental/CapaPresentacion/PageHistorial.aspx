<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="PageHistorial.aspx.cs" Inherits="CapaPresentacion.PageHistorial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="vendor/select2/select2.min.css" rel="stylesheet">
    <style>
        .select2 {
            width: 100% !important;
        }

        #tbDetalleHis tfoot {
            background-color: #f8f9fa; /* Gris claro tipo Bootstrap */
            font-weight: bold;
            border-top: 2px solid #dee2e6;
        }

            #tbDetalleHis tfoot tr td {
                padding: 4px 8px; /* Reduce el espacio interno */
                border: none !important; /* Elimina líneas extra */
                vertical-align: middle;
            }

            #tbDetalleHis tfoot tr:last-child td {
                border-bottom: none;
            }

            #tbDetalleHis tfoot p {
                margin: 0; /* Quita el margen de los <p> */
                text-align: center;
            }

            #tbDetalleHis tfoot tr td.text-right {
                text-align: right;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="card shadow mb-4">
        <div class="card-header bg-second-primary">
            <h6 class="m-0 font-weight-bold text-white"><i class="fas fa-id-card mr-3"></i>PANEL DE HISTORIAL CLINICO</h6>
        </div>
        <div class="card-body">
            <input type="hidden" value="0" id="txtIdPaciente">
            <div class="row">
                <div class="col-sm-4">
                    <div class="form-group">
                        <select class="form-control form-control-sm" id="cboBuscarPaciente">
                            <option value=""></option>
                        </select>
                    </div>
                </div>
                <div class="col-sm-8">
                    <div class="form-row">
                        <div class="input-group input-group-sm col-sm-6">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Nombre del Paciente:</span>
                            </div>
                            <input type="text" class="form-control text-right" id="txtNombrePac" disabled>
                        </div>

                        <div class="input-group input-group-sm col-sm-6">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Detalle de Alergias:</span>
                            </div>
                            <input type="text" class="form-control text-right" id="txtAlergia" disabled>
                        </div>
                    </div>
                </div>
            </div>


            <div class="form-row">
                <div class="input-group input-group-sm col-sm-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Nro de Cedula:</span>
                    </div>
                    <input type="text" class="form-control text-right" id="txtNroci" disabled>
                </div>

                <div class="input-group input-group-sm col-sm-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Edad del Paciente:</span>
                    </div>
                    <input type="text" class="form-control text-right" id="txtEdadPac" disabled>
                </div>
                <div class="input-group input-group-sm col-sm-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Fecha de Nacimiento:</span>
                    </div>
                    <input type="text" class="form-control text-right" id="txtFechaNapac" disabled>
                </div>
                <div class="input-group input-group-sm col-sm-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Numero de Celular:</span>
                    </div>
                    <input type="text" class="form-control text-right" id="txtCelularpac" disabled>
                </div>
            </div>


            <hr />
            <h6 class="mb-3 font-weight-bold text-primary">Historial Clinico del Paciente</h6>
            <div class="row" id="cargann">
                <div class="col-sm-12">
                    <table class="table table-bordered table-sm" id="tbpacientesHisto" cellspacing="0" style="width: 100%">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Codigo</th>
                                <th>Atendido por</th>
                                <th>Total</th>
                                <th>Descuento</th>
                                <th>Pagado</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>

    <div class="modal fade" id="modalDatadet" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6>Detalle</h6>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-row">
                                <div class="input-group input-group-sm col-sm-4 mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="inputGroupfechar">Fecha de Atencion:</span>
                                    </div>
                                    <input type="text" class="form-control text-right" aria-label="Small"
                                        aria-describedby="inputGroupfechar" id="txtFechaRegistrod" disabled>
                                </div>

                                <div class="input-group input-group-sm col-sm-4 mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="inputGroupcodigo">Atendido por:</span>
                                    </div>
                                    <input type="text" class="form-control text-right" aria-label="Small"
                                        aria-describedby="inputGroupcodigo" id="txtMedicod" disabled>
                                </div>

                                <div class="input-group input-group-sm col-sm-4 mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="inputGroupnrcan">Cant. de Servicios:</span>
                                    </div>
                                    <input type="text" class="form-control text-right" aria-label="Small"
                                        aria-describedby="inputGroupnrcan" id="txtTocant" disabled>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <table id="tbDetalleHis" class="table table-sm table-striped">
                                        <thead>
                                            <tr>
                                                <th>Servicio</th>
                                                <th>Cantidad</th>
                                                <th>Precio</th>
                                                <th>Total</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="3" class="text-right">Total:</td>
                                                <td>
                                                    <p id="costoTotalc">0</p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="text-right">Descuento:</td>
                                                <td>
                                                    <p id="descuento">0</p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="text-right">Total Pago:</td>
                                                <td>
                                                    <p id="totalcaja">0</p>
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <!-- <a href="#" class="btn btn-primary btn-sm" target="_blank" id="linkImprimir">Imprimir</a> -->
                    <button class="btn btn-danger btn-sm" type="button" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="vendor/select2/select2.min.js"></script>
    <script src="vendor/select2/es.min.js"></script>
    <%--<script src="jsdev/PageHistorial.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>--%>
    <script src="jsdev/PageHistorial.js" type="text/javascript"></script>
</asp:Content>
