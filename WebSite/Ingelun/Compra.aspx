<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Compra.aspx.cs" Inherits="Compra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container-fluid container-general">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <asp:UpdatePanel ID="updPanelAyuda" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
                <asp:Label ID="lblAyuda" runat="server" Text="" Visible="false"></asp:Label>
           </ContentTemplate>
       </asp:UpdatePanel>
       
      
       </div>
    
    <div class="row">
        <div class="col-xs-12">
             <asp:Panel runat="server" ID="panelCompra" CssClass="panel panel-default" Visible="true">
                   <div class="panel-body container">
                        <div>
                              <h1 class="page-header">Abastecimiento de Insumos</h1>
                        </div>
                        
                       <div class='col-sm-3 col-md-3 col-lg-3'>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblFecha" CssClass="labelInput" Text="Fecha:"> </asp:Label>
                                    <div class='input-group date' id='datetimepicker1'> 
                                        <asp:TextBox class="date-picker form-control" ID="txtFecha" data-date-format="dd/mm/yyyy" runat="server" data-bv-trigger="keyup" CausesValidation="True" ></asp:TextBox>
                                        <label for="txtFechaNacimiento" class="input-group-addon btn" disabled><span class="glyphicon glyphicon-calendar"></span></label>
                                    </div>
                                </div>
                         </div>
                                               
                       <div class='col-sm-3 col-md-3 col-lg-3'>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label3" CssClass="labelInput" Text="Proveedor:"></asp:Label>
                                    <asp:DropDownList ID="ddlProveedor" CssClass="form-control"  runat="server"></asp:DropDownList>
                                </div>
                       </div>

                       <asp:GridView ID="gwInsumos" runat="server" CssClass="table table-hover" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" DataKeyNames="id_Insumo" GridLines="None" Height="136px" Width="1000px" AutoGenerateColumns="False">
                         <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="id_Insumo" HeaderText="Id "/>
                            <asp:BoundField DataField="nombre" HeaderText="Insumo"/>
                            <asp:BoundField DataField="costo" HeaderText="Costo"/>
                            <asp:BoundField DataField="cantidad" HeaderText="Stock"/>
           
                        </Columns>

                        <PagerStyle cssClass="gridpager" HorizontalAlign="Center" />  
                          <HeaderStyle BackColor="Silver" />    
                          <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                          <SortedAscendingCellStyle BackColor="#F1F1F1" />
                          <SortedAscendingHeaderStyle BackColor="#594B9C" />
                          <SortedDescendingCellStyle BackColor="#CAC9C9" />
                          <SortedDescendingHeaderStyle BackColor="#33276A" />

                      </asp:GridView>

                       <div class="row">
                           <div class='col-sm-6 col-md-6 col-lg-6'>
                                <asp:Button ID="btnSeleccionarInsumo" runat="server" Text="Confirmar Insumo" class="btn btn-primary btn-md" OnClick="btnSeleccionarInsumo_Click"/>
                                <asp:Label ID="lblMensaje" runat="server" Text="Label" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                           </div>
                       </div>
                       <br />
                       <div class="row">
                              <div class='col-sm-3 col-md-3 col-lg-3'>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblNombreInsumo" CssClass="labelInput" Text="Insumo:"></asp:Label>
                                    <asp:TextBox class="form-control" ID="txtInsumo" runat="server" data-bv-trigger="keyup" CausesValidation="True"  readonly></asp:TextBox>
                                </div>
                              </div>
                              <div class='col-sm-3 col-md-3 col-lg-3'>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblCosto" CssClass="labelInput" Text="Costo"></asp:Label>
                                    <asp:TextBox class="form-control" ID="txtCosto" runat="server" data-bv-trigger="keyup" CausesValidation="True" readonly></asp:TextBox>
                                </div>
                              </div>
                              <div class='col-sm-3 col-md-3 col-lg-3'>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblCantidad" CssClass="labelInput" Text="Cantidad"></asp:Label>
                                    <asp:TextBox class="form-control" ID="txtCantidad" runat="server" data-bv-trigger="keyup" CausesValidation="True"  ></asp:TextBox>
                                </div>
                              </div>
                          
                       </div>
                       <div class="row">
                             <div class='col-sm-6 col-md-6 col-lg-6'>
                                <div class="form-group">
                                    <asp:Button ID="btnAgregarADetalle" runat="server" Text="Agregar Insumo" class="btn btn-primary btn-md" OnClick="btnAgregarADetalle_Click" />
                                    <asp:Label ID="lblMensajeAgregarDetalle" runat="server" Text="Label" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                </div>
                            </div>
                       </div>

                         <div>
                              <h2 class="page-header">Detalle de Abastecimiento</h2>
                        </div>

                      <%-- <div class="row">--%>
                            <asp:GridView ID="gwDetalleCompra" runat="server" Visible="true" CssClass="table table-hover" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" Height="136px" Width="1000px" AutoGenerateColumns="False">
                                <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="id_Insumo" HeaderText="Nro. Insumo"/>
                                    <asp:BoundField DataField="nombre" HeaderText="Insumo"/>
                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad"/>
                                    <asp:BoundField DataField="costo" HeaderText="Costo"/>
                                    <asp:BoundField DataField="subtotal" HeaderText="Subtotal"/>
                                 </Columns>

                                <PagerStyle cssClass="gridpager" HorizontalAlign="Center" />  
                                <HeaderStyle BackColor="Silver" />    
                                <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#33276A" />

                            </asp:GridView>
                      <%-- </div>--%>
                      <div class="row">
                            <div class='col-sm-3 col-md-3 col-lg-3'>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label1" CssClass="labelInput" Text="Importe Total:" Font-Bold="True" Font-Size="Large"></asp:Label>
                                    <asp:Label runat="server" ID="Label2" CssClass="labelInput" Text="$ "  Font-Bold="True" Font-Size="Large"></asp:Label>
                                    <asp:Label runat="server" ID="lblTotalCompra" CssClass="labelInput" Text=""  Font-Bold="True" Font-Size="Large"></asp:Label>
                                </div>
                              </div>
                      </div>
                       <div class="row">
                            <div class='col-sm-6 col-md-6 col-lg-6'>
                                <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra" class="btn btn-primary btn-md" OnClick="btnConfirmarCompra_Click"/>
                                <asp:Label ID="lblMensajeConfirmarCompra" runat="server" Text="Label" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                            </div>
                       </div>


                   </div>
             </asp:Panel>
        </div>
    </div>
</asp:Content>

