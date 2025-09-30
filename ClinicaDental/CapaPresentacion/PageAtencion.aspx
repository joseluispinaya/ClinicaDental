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
                            <h6 class="m-0 font-weight-bold text-white">Paciente</h6>
                        </div>
                        <div class="card-body">
                            <input type="hidden" value="0" id="txtIdPaciente">
                            <div class="form-row">
                                <div class="form-group col-sm-4">
                                    <label for="cboBuscarPaciente">Buscar Paciente</label>
                                    <select class="form-control form-control-sm" id="cboBuscarPaciente">
                                        <option value=""></option>
                                    </select>
                                </div>
                                <div class="form-group col-sm-5">
                                    <label for="txtNombrePac">Paciente:</label>
                                    <input type="text" class="form-control form-control-sm" id="txtNombrePac" disabled>
                                </div>
                                <div class="form-group col-sm-3">
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
                            <h6 class="m-0 font-weight-bold text-white">Servicios</h6>
                        </div>
                        <div class="card-body">

                            <div class="form-row">

                                <div class="form-group col-sm-6">
                                    <label for="cboTipoServicio">Buscar Servicio</label>
                                    <select class="form-control form-control-sm" id="cboTipoServicio">
                                        <option value=""></option>
                                    </select>
                                </div>

                                <!-- <div class="input-group input-group-sm col-sm-6">
                                <div class="input-group-prepend">
                                    <label class="input-group-text" for="cboTipoServicio">Selec Servicio</label>
                                </div>
                                <select class="custom-select" id="cboTipoServicio">
                                </select>
                            </div> -->

                                <div class="col-sm-6">
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
                    <div class="card shadow mb-4">
                        <div class="card-header bg-second-primary">
                            <h6 class="m-0 font-weight-bold text-white">Detalle</h6>
                        </div>
                        <div class="card-body">
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="vendor/select2/select2.min.js"></script>
    <script src="vendor/select2/es.min.js"></script>
    <script src="jsdev/PageAtencion.js" type="text/javascript"></script>

</asp:Content>
