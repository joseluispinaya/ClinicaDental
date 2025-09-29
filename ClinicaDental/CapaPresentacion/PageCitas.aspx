<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="PageCitas.aspx.cs" Inherits="CapaPresentacion.PageCitas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="vendor/timepicker/bootstrap-timepicker.min.css" rel="stylesheet"/>
    <link href="vendor/select2/select2.min.css" rel="stylesheet">
    <link href="vendor/calen/fullcalendar.min.css" rel="stylesheet"/>
    <link href="vendor/jquery-ui/jquery-ui.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@mdi/font@7.4.47/css/materialdesignicons.min.css">
    <style>
        .select2 {
            width: 100% !important;
        }
        #timepicker2 {
            text-align: center;
        }
        #txtFechaCita {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h6 class="mb-3 font-weight-bold text-primary">Registro de citas</h6>

    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-body">
                    <input type="hidden" value="0" id="txtIdPaciente">
                    <div class="form-row align-items-end">
                        <div class="form-group col-sm-3">
                            <label for="cboBuscarPaciente">Buscar Paciente</label>
                            <select class="form-control form-control-sm" id="cboBuscarPaciente">
                                <option value=""></option>
                            </select>
                        </div>
                        <div class="form-group col-sm-3">
                            <label for="txtNombrePac">Nombre</label>
                            <input type="text" class="form-control form-control-sm" id="txtNombrePac" disabled>
                        </div>
                        <div class="form-group col-sm-3">
                            <label for="txtNroci">Nro CI</label>
                            <input type="text" class="form-control form-control-sm" id="txtNroci" disabled>
                        </div>
                        <div class="form-group col-sm-3">
                            <label for="txtAlergia">Alergias</label>
                            <input type="text" class="form-control form-control-sm" id="txtAlergia" disabled>
                        </div>
                    </div>

                    <div class="form-row align-items-end">
                        <div class="form-group col-sm-3">
                            <label for="txtFechaCita">Fecha Cita</label>
                            <input type="text" class="form-control form-control-sm" id="txtFechaCita">
                        </div>
                        <div class="form-group col-sm-2">
                            <label for="timepicker2">Hora Cita</label>
                            <input type="text" class="form-control form-control-sm" id="timepicker2">
                        </div>
                        <div class="form-group col-sm-5">
                            <label for="txtDetalle">Comentario</label>
                            <input type="text" class="form-control form-control-sm" id="txtDetalle">
                        </div>
                        <div class="form-group col-sm-2">
                            <button type="button" class="btn btn-success btn-block btn-sm" id="btnGuardarCit"><i class="fas fa-plus-circle mr-2"></i>Registrar</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <hr/>
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-body">
                    <div id="calendar"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="vendor/timepicker/bootstrap-timepicker.js"></script>

    <script src="vendor/select2/select2.min.js"></script>
    <script src="vendor/select2/es.min.js"></script>

    <script src="vendor/calen/moment.min.js"></script>
    <script src="vendor/calen/fullcalendar.min.js"></script>
    <script src="vendor/calen/es.js"></script>

    <script src="vendor/jquery-ui/jquery-ui.js"></script>
    <script src="vendor/jquery-ui/idioma/datepicker-es.js"></script>

    <script src="jsdev/PageCitas.js" type="text/javascript"></script>
</asp:Content>
