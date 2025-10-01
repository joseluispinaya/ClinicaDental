<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="PageAtencion.aspx.cs" Inherits="CapaPresentacion.PageAtencion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="vendor/select2/select2.min.css" rel="stylesheet">
    <style>
        .select2 {
            width: 100% !important;
        }

        .textocenter {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-8">
            <div class="row">
                <div class="col-sm-12">
                    <div class="card shadow mb-4">
                        <div class="card-header bg-second-primary">
                            <h6 class="m-0 font-weight-bold text-white">Servicios</h6>
                        </div>
                        <div class="card-body">
                            <input type="hidden" value="0" id="txtIdTratamiento">
                            <div class="form-row align-items-end">
                                <div class="form-group col-sm-4">
                                    <label for="txtNombreserv">Servicio</label>
                                    <input type="text" class="form-control form-control-sm" id="txtNombreserv" disabled>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtDescriSe">Descripcion</label>
                                    <input type="text" class="form-control form-control-sm" id="txtDescriSe" disabled>
                                </div>
                                <div class="form-group col-sm-2">
                                    <button type="button" class="btn btn-primary btn-block btn-sm" id="btnBuscar"><i class="fas fa-search mr-2"></i>Buscar</button>
                                </div>
                            </div>
                            
                            <div class="form-row align-items-end">
                                <div class="form-group col-sm-3">
                                    <label for="txtCodigo">Codigo</label>
                                    <input type="text" class="form-control form-control-sm" id="txtCodigo" disabled>
                                </div>
                                <div class="form-group col-sm-3">
                                    <label for="txtPrecioSe">Precio</label>
                                    <input type="text" class="form-control form-control-sm" id="txtPrecioSe" disabled>
                                </div>
                                <div class="form-group col-sm-3">
                                    <label for="txtCant">Cantidad</label>
                                    <input type="number" class="form-control form-control-sm" id="txtCant">
                                </div>
                                <div class="form-group col-sm-3">
                                    <button type="button" class="btn btn-success btn-block btn-sm" id="btnAgregar"><i class="fas fa-plus-circle mr-2"></i>Agregar</button>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <table class="table table-striped table-sm" id="tbAtencion" cellspacing="0" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th></th>
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
            </div>

        </div>
        <div class="col-sm-4">

            <div class="row">
                <div class="col-sm-12">
                    <div class="card shadow mb-2">
                        <div class="card-body">
                            <h6 class="mb-1 font-weight-bold text-primary">Datos del Paciente</h6>

                            <input type="hidden" value="0" id="txtIdPaciente">
                            <div class="form-group">
                                <label for="cboBuscarPaciente">Buscar Paciente</label>
                                <select class="form-control form-control-sm" id="cboBuscarPaciente">
                                    <option value=""></option>
                                </select>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-sm-8 mb-0">
                                    <label for="txtNombrePac">Paciente:</label>
                                    <input type="text" class="form-control form-control-sm" id="txtNombrePac" disabled>
                                </div>
                                <div class="form-group col-sm-4 mb-0">
                                    <label for="txtNroci">Nro CI:</label>
                                    <input type="text" class="form-control form-control-sm" id="txtNroci" disabled>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="card shadow mb-4">
                        <div class="card-header bg-second-primary">
                            <h6 class="m-0 font-weight-bold text-white">Detalle</h6>
                        </div>
                        <div class="card-body" id="loadingAc">
                            <div class="input-group input-group-sm mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="inputGroupIGV">Total Pago:</span>
                                </div>
                                <input type="text" class="form-control text-right" aria-label="Small"
                                    aria-describedby="inputGroupIGV" id="txtTotalPago" disabled>
                            </div>

                            <div class="input-group input-group-sm mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="inputGroupDescuento">Descuento:</span>
                                </div>
                                <input type="text" class="form-control text-right" aria-label="Small"
                                    aria-describedby="inputGroupDescuento" id="txtDescuento">
                            </div>

                            <div class="form-group mb-3">
                                <button class="btn btn-primary btn-sm btn-block" type="button" id="btnCalcular">Calcular Pago</button>
                            </div>

                            <div class="input-group input-group-sm mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="inputGroupTotal">Total a Pagar:</span>
                                </div>
                                <input type="text" class="form-control text-right" aria-label="Small"
                                    aria-describedby="inputGroupTotal" id="txtTotalFin" disabled>
                            </div>

                            <div class="form-group mb-0">
                                <button class="btn btn-success btn-sm btn-block" type="button" id="btnRegistrarPaga">Registrar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalServicios" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6>Lista de Servicios</h6>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <table id="tbServicios" class="table table-sm table-striped" cellspacing="0" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Servicio</th>
                                        <th>Descripcion</th>
                                        <th>Precio</th>
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
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="vendor/select2/select2.min.js"></script>
    <script src="vendor/select2/es.min.js"></script>
    <script src="jsdev/PageAtencion.js" type="text/javascript"></script>

</asp:Content>
