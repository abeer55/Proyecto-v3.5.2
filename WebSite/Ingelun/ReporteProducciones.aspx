<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteProducciones.aspx.cs" Inherits="ReporteProducciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="row">

            <div class="col-xs-12">
                <asp:Panel runat="server" ID="panelReporteProducciones" CssClass="panel panel-default" Visible="true">
                    
                    <div class="panel-body container">
                        <div>
                              <h1 class="page-header">
                          Reporte Producciones
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
                                    <asp:Label runat="server" ID="lblCantidad" CssClass="labelInput" Text="Nombre Producto:"></asp:Label>
                                    <asp:TextBox class="form-control" ID="txtNombreProducto" runat="server" data-bv-trigger="keyup" CausesValidation="True"  ></asp:TextBox>
                                </div>
                        </div>
                       <div class='col-sm-3 col-md-3 col-lg-3'>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label3" CssClass="labelInput" Text="Tipo Producto:"></asp:Label>
                                    <asp:DropDownList ID="ddlTipoProducto" CssClass="form-control"  runat="server"></asp:DropDownList>
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
                            <asp:GridView ID="gwReporteProduccion" runat="server" CssClass="table table-hover" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" Height="136px" Width="1000px" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gwReporteCompra_PageIndexChanging" OnSorting="gwReporteCompra_Sorting" AllowSorting="True" OnSelectedIndexChanged="gwReporteProduccion_SelectedIndexChanged">
                               <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="id_Produccion" HeaderText="ID Prod." SortExpression="p.id_Produccion" />
                                        <asp:BoundField DataField="fechaProduccion" HeaderText="Fecha Produccion " SortExpression="p.fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="nombreProducto" HeaderText="Nombre Prod." SortExpression="pr.nombre" />
                                        <asp:BoundField DataField="nombretipoProducto" HeaderText="Tipo Prod." SortExpression="tp.nombre" />
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" SortExpression="pxp.cantidad" />
                                      <%--  <asp:BoundField DataField="montoTotal" HeaderText="Total" SortExpression="c.montoTotal" DataFormatString="{0:C2}" />--%>
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

