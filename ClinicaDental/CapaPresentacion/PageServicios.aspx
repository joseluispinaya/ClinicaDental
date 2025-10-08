<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="PageServicios.aspx.cs" Inherits="CapaPresentacion.PageServicios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="card shadow mb-4">
        <div class="card-header bg-second-primary">
            <h6 class="m-0 font-weight-bold text-white"><i class="fas fa-id-card mr-3"></i>PANEL DE REGISTRO DE SERVICIOS</h6>
        </div>
        <div class="card-body">
            <div class="row justify-content-center mb-4">
                <button type="button" id="btnAddNuevoReg" class="btn btn-success btn-sm mr-3"><i class="fas fa-user-plus mr-2"></i>Nuevo Registro</button>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <table class="table table-bordered table-sm" id="tbServici" cellspacing="0" style="width: 100%">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Servicio</th>
                                <th>Descripcion</th>
                                <th>Precio</th>
                                <th>Estado</th>
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


    <div class="modal fade" id="modalServicios" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 id="myTituloServ">Detalle</h6>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" value="0" id="txtIdServicio">
                    <div class="row">
                        <div class="col-sm-12">

                            <div class="form-row">
                                <div class="form-group col-sm-6">
                                    <label for="txtServicio">Servicio</label>
                                    <input type="text" class="form-control form-control-sm input-validar" id="txtServicio" name="Servicio">
                                </div>
                                <div class="form-group col-sm-3">
                                    <label for="txtPrecio">Precio</label>
                                    <input type="text" class="form-control form-control-sm input-validar" id="txtPrecio" name="Precio">
                                </div>
                                <div class="form-group col-sm-3">
                                    <label for="cboEstado">Estado</label>
                                    <select class="form-control form-control-sm" id="cboEstado">
                                        <option value="1">Activo</option>
                                        <option value="0">No Activo</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtDescipcion">Descripcion</label>
                                <input type="text" class="form-control form-control-sm input-validar" id="txtDescipcion" name="Descripcion">
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnGuardarCambios" class="btn btn-primary btn-sm" type="button">Guardar</button>
                    <button class="btn btn-danger btn-sm" type="button" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <%--<script src="jsdev/PageServicios.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>--%>
    <script src="jsdev/PageServicios.js" type="text/javascript"></script>
</asp:Content>
