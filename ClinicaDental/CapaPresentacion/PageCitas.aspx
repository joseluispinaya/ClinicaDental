<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="PageCitas.aspx.cs" Inherits="CapaPresentacion.PageCitas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="vendor/calen/fullcalendar.min.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="card shadow">
    <div class="card-header bg-second-primary">
        <h6 class="m-0 font-weight-bold text-white">Lista de Citas Agendadas</h6>
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
                            <span class="d-none d-sm-block">Agenda de citas</span>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="profile-tab-2" data-toggle="tab" href="#profile-2" role="tab"
                            aria-controls="profile-2" aria-selected="false">
                            <span class="d-block d-sm-none"><i class="fa fa-user"></i></span>
                            <span class="d-none d-sm-block">Lista de Citas</span>
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
                        <h6 class="mb-3 font-weight-bold text-primary">Registro de citas</h6>
                        <div class="row">
                            <div class="col-sm-12">
                                <table class="table table-sm table-striped" id="tbCitasAdmin" cellspacing="0" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Id</th>
                                            <th>Paciente</th>
                                            <th>Medico</th>
                                            <th>Cita</th>
                                            <th>Estado</th>
                                            <th>Registrado</th>
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
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">

    <script src="vendor/calen/moment.min.js"></script>
    <script src="vendor/calen/fullcalendar.min.js"></script>
    <script src="vendor/calen/es.js"></script>

    <script src="jsdev/PageCitas.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>
    <%--<script src="jsdev/PageCitas.js" type="text/javascript"></script>--%>
</asp:Content>
