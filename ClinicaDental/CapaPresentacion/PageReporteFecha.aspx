<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="PageReporteFecha.aspx.cs" Inherits="CapaPresentacion.PageReporteFecha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="vendor/jquery-ui/jquery-ui.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="card shadow mb-4">
        <div class="card-header bg-second-primary">
            <h6 class="m-0 font-weight-bold text-white">Reporte de Atencion</h6>
        </div>
        <div class="card-body" id="cargann">
            <div class="form-row align-items-end">

                <div class="form-group col-sm-3">
                    <label for="txtFechaInicio">Fecha Inicio</label>
                    <input type="text" class="form-control form-control-sm" id="txtFechaInicio">
                </div>
                <div class="form-group col-sm-3">
                    <label for="txtFechaFin">Fecha Fin</label>
                    <input type="text" class="form-control form-control-sm" id="txtFechaFin">
                </div>
                <div class="form-group col-sm-3">
                    <button type="button" class="btn btn-success btn-block btn-sm" id="btnBuscar"><i class="fas fa-search mr-2"></i>Buscar</button>
                </div>
                <div class="form-group col-sm-3">
                    <button class="btn btn-info btn-block btn-sm" type="button" id="btnImprimir"><i class="fas fa-print mr-2"></i>Reporte</button>
                </div>
            </div>

            <hr />
            <div class="row">
                <div class="col-sm-12">
                    <table id="tbdarptAtencion" class="table table-sm table-striped" cellspacing="0" style="width: 100%">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Codigo</th>
                                <th>Paciente</th>
                                <th>Medico</th>
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
                                    <span class="input-group-text" id="inputGroupfechar">Atendido:</span>
                                </div>
                                <input type="text" class="form-control text-right" aria-label="Small"
                                    aria-describedby="inputGroupfechar" id="txtFechaRegistrod" disabled>
                            </div>

                            <div class="input-group input-group-sm col-sm-4 mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="inputGroupcodigo">Medico:</span>
                                </div>
                                <input type="text" class="form-control text-right" aria-label="Small"
                                    aria-describedby="inputGroupcodigo" id="txtMedicod" disabled>
                            </div>

                            <div class="input-group input-group-sm col-sm-4 mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="inputGroupCambiodolar">Paciente:</span>
                                </div>
                                <input type="text" class="form-control text-right" aria-label="Small"
                                    aria-describedby="inputGroupCambiodolar" id="txtPaciented" disabled>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="input-group input-group-sm col-sm-3 mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="inputGroupnomprod">Total:</span>
                                </div>
                                <input type="text" class="form-control text-right" aria-label="Small"
                                    aria-describedby="inputGroupnomprod" id="txtTotald" disabled>
                            </div>

                            <div class="input-group input-group-sm col-sm-3 mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="inputGroupnroci">Descuento:</span>
                                </div>
                                <input type="text" class="form-control text-right" aria-label="Small"
                                    aria-describedby="inputGroupnroci" id="txtDescuentod" disabled>
                            </div>

                            <div class="input-group input-group-sm col-sm-3 mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="inputGroupnrocelu">Pagado:</span>
                                </div>
                                <input type="text" class="form-control text-right" aria-label="Small"
                                    aria-describedby="inputGroupnrocelu" id="txtTotalpad" disabled>
                            </div>

                            <div class="input-group input-group-sm col-sm-3 mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="inputGroupnrcan">Cantidad:</span>
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
    <script src="vendor/jspdfzero/jspdfzero.js"></script>
    <script src="vendor/jquery-ui/jquery-ui.js"></script>
    <script src="vendor/jquery-ui/idioma/datepicker-es.js"></script>
    
    <script src="jsdev/PageReporteFecha.js" type="text/javascript"></script>
</asp:Content>
