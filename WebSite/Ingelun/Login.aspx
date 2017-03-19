<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate"  LoginButtonStyle-CssClass="btn-primary" TextBoxStyle-CssClass="text-primary" TitleTextStyle-CssClass="text-primary" TitleTextStyle-ForeColor="Black" Width="600" Height="300"></asp:Login>--%>
   
      <asp:Login runat="server" OnAuthenticate="login_Authenticate" ID="loginControl" DisplayRememberMe="true">
                <LayoutTemplate>
                    
                    <div class="container">
                        <div class="col-sm-6 col-md-4 col-md-offset-2">
                    <%--<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">--%>
                        <div class="modal-dialog modal-dialog-login">
                            <div class="modal-content">
 
                                <div class="modal-header header-ayuda">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                    <h4 class="modal-title" id="myModalLabel">Inicio de Sesión</h4>
                                </div>
                                <!-- /.modal-header -->

                                <div class="modal-body">


                                    <form role="form">

                                        <div class="input-group margin-bottom-sm">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="UserName" placeholder="Usuario" />
                                          <span class="input-group-addon"><i class="fa fa-user fa-fw"></i></span>
                                        </div>
                                        <br />
                                        <div class="input-group margin-bottom-sm margin-top-10">
                                          <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="Password" placeholder="Contraseña" />
                                          <span class="input-group-addon"><i class="fa fa-lock fa-fw"></i></span>
                                        </div>
                                         <br />
                                   
                                        <!-- /.form-group -->

                                        <%--<div class="checkbox">
                                            <label>
                                                <input type="checkbox">
                                                Remember me
                                            </label>
                                        </div>
                                        <!-- /.checkbox -->--%>
                                    </form>


                                </div>
                                <!-- /.modal-body -->

                                <div class="modal-footer modal-footer-login">
                                    <asp:LinkButton ID="Login" runat="server" Text="Ingresar" CssClass="form-control btn btn-primary" 
                                        CommandName="Login" 
                                        CausesValidation="true" 
                                        ValidationGroup="loginGroupValidation">
                                    </asp:LinkButton>
                                    <%--<button class="form-control btn btn-primary">Ok</button>--%>
                                   
                                    <div class="progress">
                                        <div class="progress-bar progress-bar-primary" role="progressbar" aria-valuenow="1" aria-valuemin="1" aria-valuemax="100" style="width: 0%;">
                                            <span class="sr-only">progress</span>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.modal-footer -->
         
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    <%--</div>--%>
                    <!-- /.modal -->
                            </div>
                        </div>
                </LayoutTemplate>
            </asp:Login>

    <br />
 
      <div class="container">
                        <div class="col-sm-6 col-md-4 col-md-offset-4">
                              <div class="input-group margin-bottom-sm margin-top-10">
                               <asp:CheckBox ID="chkRecordarme" class="checkbox" Text="Recordarme en este sitio" runat="server" />
                                         
                              </div>
                        </div>
      </div>
                   

   
</asp:Content>

