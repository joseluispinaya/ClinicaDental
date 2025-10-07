<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="PageListaCitas.aspx.cs" Inherits="CapaPresentacion.PageListaCitas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="vendor/timepicker/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="vendor/select2/select2.min.css" rel="stylesheet">
    <link href="vendor/calen/fullcalendar.min.css" rel="stylesheet" />
    <link href="vendor/jquery-ui/jquery-ui.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@mdi/font@7.4.47/css/materialdesignicons.min.css">
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
    <div class="card shadow">
        <div class="card-header bg-second-primary">
            <h6 class="m-0 font-weight-bold text-white">Agendar Citas</h6>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-sm-12">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs nav-justified" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="home-tab-2" data-toggle="tab" href="#home-2" role="tab"
                                aria-controls="home-2" aria-selected="true">
                                <span class="d-block d-sm-none"><i class="fa fa-home"></i></span>
                                <span class="d-none d-sm-block">Citas Agendadas</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="profile-tab-2" data-toggle="tab" href="#profile-2" role="tab"
                                aria-controls="profile-2" aria-selected="false">
                                <span class="d-block d-sm-none"><i class="fa fa-user"></i></span>
                                <span class="d-none d-sm-block">Registrar Cita</span>
                            </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content p-3" style="background-color: #fff; border: 1px solid #eeeeee;">
                        <div class="tab-pane fade show active" id="home-2" role="tabpanel" aria-labelledby="home-tab-2">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div id="calendar"></div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="profile-2" role="tabpanel" aria-labelledby="profile-tab-2">
                            <div class="row" id="loadi">
                                <div class="col-sm-6">
                                    <h6 class="mb-3 font-weight-bold text-primary">Datos del Paciente</h6>
                                    <input type="hidden" value="0" id="txtIdPaciente">
                                    <div class="form-row align-items-end">
                                        <div class="form-group col-sm-6">
                                            <label for="cboBuscarPaciente">Buscar Paciente</label>
                                            <select class="form-control form-control-sm" id="cboBuscarPaciente">
                                                <option value=""></option>
                                            </select>
                                        </div>
                                        <div class="form-group col-sm-6">
                                            <label for="txtNombrePac">Paciente:</label>
                                            <input type="text" class="form-control form-control-sm" id="txtNombrePac" disabled>
                                        </div>
                                    </div>

                                    <div class="form-row align-items-end">
                                        <div class="form-group col-sm-3">
                                            <label for="txtNroci">Nro CI:</label>
                                            <input type="text" class="form-control form-control-sm" id="txtNroci" disabled>
                                        </div>
                                        <div class="form-group col-sm-3">
                                            <label for="txtEdadPac">Edad:</label>
                                            <input type="text" class="form-control form-control-sm" id="txtEdadPac" disabled>
                                        </div>
                                        <div class="form-group col-sm-6">
                                            <label for="txtAlergia">Alergias:</label>
                                            <input type="text" class="form-control form-control-sm" id="txtAlergia" disabled>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-6">
                                    <h6 class="mb-3 font-weight-bold text-primary">Registro de citas</h6>
                                    <div class="form-row align-items-end">
                                        <div class="form-group col-sm-4">
                                            <label for="txtMedicodis">Medico Disponible:</label>
                                            <input type="text" class="form-control form-control-sm" id="txtMedicodis" disabled>
                                        </div>
                                        <div class="form-group col-sm-4">
                                            <label for="txtFechaActual">Fecha Hora Actual</label>
                                            <input type="text" class="form-control form-control-sm textocenter" id="txtFechaActual" disabled>
                                        </div>
                                        <div class="form-group col-sm-4">
                                            <button type="button" class="btn btn-success btn-block btn-sm" id="btnGuardarCit"><i class="fas fa-plus-circle mr-2"></i>Registrar</button>
                                        </div>
                                    </div>

                                    <div class="form-row align-items-end">
                                        <div class="form-group col-sm-3">
                                            <label for="txtFechaCita">Fecha Cita</label>
                                            <input type="text" class="form-control form-control-sm textocenter" id="txtFechaCita">
                                        </div>
                                        <div class="form-group col-sm-3">
                                            <label for="timepicker2">Hora Cita</label>
                                            <input type="text" class="form-control form-control-sm textocenter" id="timepicker2">
                                        </div>
                                        <div class="form-group col-sm-6">
                                            <label for="txtDetalle">Comentario</label>
                                            <input type="text" class="form-control form-control-sm" id="txtDetalle">
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalDetalleCi" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div class="modal-content" id="loatei">
                <div class="modal-header">
                    <h6 id="myTituloprcix">Detalle</h6>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" value="0" id="txtIdCitaAt">
                    <div class="row">
                        <div class="col-sm-12">

                            <div class="form-row">
                                <div class="form-group col-sm-6">
                                    <label for="txtNombrePaci">Paciente</label>
                                    <input type="text" class="form-control form-control-sm" id="txtNombrePaci" disabled>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtMedicoAt">Medico</label>
                                    <input type="text" class="form-control form-control-sm" id="txtMedicoAt" disabled>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group col-sm-4">
                                    <label for="txtFechCi">Fecha Cita</label>
                                    <input type="text" class="form-control form-control-sm" id="txtFechCi" disabled>
                                </div>
                                <div class="form-group col-sm-4">
                                    <label for="txtHoraCit">Hora</label>
                                    <input type="text" class="form-control form-control-sm" id="txtHoraCit" disabled>
                                </div>
                                <div class="form-group col-sm-4">
                                    <label for="txtRegFec">Registrado</label>
                                    <input type="text" class="form-control form-control-sm" id="txtRegFec" disabled>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtComentAtc">Comentario</label>
                                <input type="text" class="form-control form-control-sm" id="txtComentAtc" disabled>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnGuardarAtencitt" class="btn btn-primary btn-sm" type="button">Atender</button>
                    <button class="btn btn-danger btn-sm" type="button" data-dismiss="modal">Cancelar</button>
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

    <script src="jsdev/PageListaCitas.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>
    <%--<script src="jsdev/PageListaCitas.js" type="text/javascript"></script>--%>
</asp:Content>
