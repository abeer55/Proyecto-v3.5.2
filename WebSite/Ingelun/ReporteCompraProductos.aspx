<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteCompraProductos.aspx.cs" Inherits="ReporteCompraProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="//code.jquery.com/ui/1.11.3/themes/smoothness/jquery-ui.min.css" rel="stylesheet" type="text/css" />
   <script src="//code.jquery.com/jquery-2.2.3.min.js" type="text/javascript"></script>
     <script src="//code.jquery.com/ui/1.11.4/jquery-ui.min.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="dialogC" title="Mensaje" style="display:none">         
    </div>  
    <div class="row">

            <div class="col-xs-12">
                <asp:Panel runat="server" ID="panelReporteCompras" CssClass="panel panel-default" Visible="true">
                    
                    <div class="panel-body container">
                        <div>
                              <h1 class="page-header">
                          Reporte Compras Productos 
                        </h1>
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
                                    <asp:Label runat="server" ID="lblCantidad" CssClass="labelInput" Text="Monto Parcial mayor a: "></asp:Label>
                                    <asp:TextBox class="form-control" ID="txtCantidad" runat="server" data-bv-trigger="keyup" CausesValidation="True"  ></asp:TextBox>
                                </div>
                        </div>
                       <div class='col-sm-3 col-md-3 col-lg-3'>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label3" CssClass="labelInput" Text="Cliente:"></asp:Label>
                                    <asp:DropDownList ID="ddlCliente" CssClass="form-control"  runat="server"></asp:DropDownList>
                                </div>
                       </div>
                      
                 </div>
                 <div class="panel-body container">
                     <div >
                            <asp:Button ID="btnConsultar" runat="server" Text="Generar Reporte " class="btn btn-primary btn-md" OnClick="btnConsultar_Click" />
                          <asp:Button ID="btnLimpiar" runat="server" Text="Resetear valores" class="btn btn-primary btn-md" OnClick="btnLimpiar_Click"/>
                            <asp:Label ID="lblMensaje" runat="server" Text="Label" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                       </div>
                 </div>

                    <div class="panel-body container">
                            <asp:GridView ID="gwReporteVenta" runat="server" CssClass="table table-hover" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" Height="136px" Width="1000px" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gwReporteVenta_PageIndexChanging" OnSorting="gwReporteVenta_Sorting" AllowSorting="True">
                               <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="id_Venta" HeaderText="Id Venta" SortExpression="v.id_Venta" />
                                        <asp:BoundField DataField="fechaVenta" HeaderText="Fecha Venta " SortExpression="v.fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="nombreCliente" HeaderText="Nombre Cliente." SortExpression="c.nombre" />
                                        <asp:BoundField DataField="mailCliente" HeaderText="Mail " SortExpression="c.mail" />
                                        <asp:BoundField DataField="NombreProducto" HeaderText="Producto" SortExpression="p.nombre" />
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" SortExpression="pxv.cantidad" />
                                        <asp:BoundField DataField="montoTotal" HeaderText="Total" SortExpression="v.montoTotal" DataFormatString="{0:C2}" />
                                        <asp:BoundField DataField="montoParcial" HeaderText="Costo Parcial" SortExpression="pxv.montoParcial" DataFormatString="{0:C2}" />
                                    </Columns>

                                <PagerStyle cssClass="gridpager" HorizontalAlign="Center" />  
                                <HeaderStyle BackColor="Silver" />    
                                <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#33276A" />
                            </asp:GridView>
                    </div>
                </asp:Panel>
            </div>
    </div>
      

     <script src="js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">
        $(".date-picker").datepicker();

        $(".date-picker").on("change", function () {
            var id = $(this).attr("id");
            var val = $("label[for='" + id + "']").text();
            $("#msg").text(val + " changed");
        });



</script>
</asp:Content>



