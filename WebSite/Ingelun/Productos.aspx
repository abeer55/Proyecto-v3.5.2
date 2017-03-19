<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Productos.aspx.cs" Inherits="Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="//code.jquery.com/jquery-2.2.3.min.js" type="text/javascript"></script>
     <script src="//code.jquery.com/ui/1.11.4/jquery-ui.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div id="dialogC" title="Mensaje" style="display:none">    
    </div> 

    <!-- Page Heading -->
                <div class="row">
                    <div class="col-md-12">
                        <h1 class="page-header">
                          Productos
                        </h1>
                        <asp:Panel ID="pnlConsulta" runat="server" Height="500px" Width="990px">

                             <asp:Label ID="lblAccion" runat="server" Text="" Visible="false"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblMensaje" runat="server" Text="Label" Visible="false" CssClass="alert-danger" Font-Size="Large"></asp:Label>
                                    <br />
                                    &nbsp;
                                    <asp:Label ID="lblText" runat="server" Text="Ingrese Nombre para Buscar"></asp:Label>
                                    &nbsp; &nbsp;
                                    <asp:TextBox ID="txtNombreBuscar" runat="server"></asp:TextBox>
                                    &nbsp; &nbsp;
                                     <asp:Button ID="btnBuscarPorNombre" runat="server"  Text="Buscar" class="btn btn-primary btn-md" OnClick="btnBuscarPorNombre_Click" /> 
                                    <br />
                            <br />
                         <asp:GridView ID="grdProductos" runat="server" CssClass="table table-hover" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" DataKeyNames="id_Producto" GridLines="None" OnSelectedIndexChanged="grdProductos_SelectedIndexChanged" AutoGenerateColumns="False" OnPageIndexChanging="grdProductos_PageIndexChanging" OnSorting="grdProductos_Sorting">                              
                          <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />

                       
                             <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="id_Producto" HeaderText="ID Producto" SortExpression="id_Producto" />
                                <asp:BoundField DataField="nombre" HeaderText="Descripcion" SortExpression="Producto.nombre" />
                                <asp:BoundField DataField="precio" HeaderText="Costo" SortExpression="precio" />
                                <asp:BoundField DataField="nombre_Tipo_Producto" HeaderText="Tipo Producto" SortExpression="TipoProducto.nombre" />
                                <asp:BoundField DataField="fecha_Construccion" HeaderText="Fecha Construcción" SortExpression="fecha_Construccion" DataFormatString="{0:d}" />
                            </Columns>
                             <PagerStyle cssClass="gridpager" HorizontalAlign="Center" />  
                               <HeaderStyle BackColor="Silver" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#594B9C" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#33276A" />

                         </asp:GridView>

                        <asp:Button ID="btnAgregar" runat="server"  Text="Agregar Producto" class="btn btn-primary btn-md" OnClick="btnAgregar_Click" />                        
                             &nbsp;<asp:Button ID="btnConsultar" runat="server"  Text="Cosultar Producto" class="btn btn-primary btn-md" OnClick="btnConsultar_Click" />   
                              &nbsp;<asp:Button ID="btnEditar" runat="server"  Text="Modificar Producto" class="btn btn-primary btn-md" OnClick="btnEditar_Click" />   
                              &nbsp;<asp:Button ID="btnEliminar" runat="server" Text="Eliminar Producto" class="btn btn-primary btn-md" OnClick="btnEliminar_Click" />   
        
                            </asp:Panel>

                        
                    </div>
                </div>
                <!-- /.row -->

   
<asp:Panel ID="pnlAgregar" class="panel panel-default" Height="1050px" runat="server" >
    <div class="container">
   <br />
           <div class="row">
     <div class="col-md-5">
         <div class="panel panel-default">
      <div class="panel-heading">Datos del producto:</div>
      <div class="panel-body">
    <div class="container">
        <div class="row"> 
            <div class="col-md-2"> 
                <br />
                <asp:Label ID="Label1" runat="server" Text="ID:"></asp:Label></div>
             <div class="col-md-3"> 
                 <br />
                 <asp:TextBox ID="txtId" runat="server" Enabled="False" CssClass="form-control" Width="180px"></asp:TextBox>  </div>                               
        </div>

         <div class="row"> 
            <div class="col-md-2"> 
                <br />
                <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label></div>
             <div class="col-md-3"> 
                 <br />
                 <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Width="180px"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNombre" CssClass="text-danger" ErrorMessage="Debe ingresar un Nombre"></asp:RequiredFieldValidator>
             </div>
			 
        </div>
        <div class="row"> 
            <div class="col-md-2"> 
                <br />
                <asp:Label ID="Label3" runat="server" Text="Precio:"></asp:Label></div>
             <div class="col-md-3"> 
                 <br />
                 <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" Width="180px" OnTextChanged="txtPrecio_TextChanged"></asp:TextBox>
                 <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPrecio" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar un valor numerico que puede tener decimales" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPrecio" CssClass="text-danger" ErrorMessage="Debe ingresar un Precio"></asp:RequiredFieldValidator>
            </div>
       
        </div>

        <div class="row"> 
            <div class="col-md-2"> 
                <br />
                <asp:Label ID="Label4" runat="server" Text="Tipo Producto:"></asp:Label></div>
             <div class="col-md-3"> 
                 <br />
                 <asp:DropDownList ID="comboBoxTipoProducto" runat="server" CssClass="form-control" Width="180px"></asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="comboBoxTipoProducto" CssClass="text-danger" ErrorMessage="Debe seleccionar un tipo" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
			 
        </div>

        <div class="row"> 
            <div class="col-md-2"> 
                <br />
                <asp:Label ID="Label5" runat="server" Text="Fecha de fabricacion:"></asp:Label></div>
             <div class="col-md-3"> 
                 <br />
               <%--   <div class='input-group date' id='datetimepicker1'> --%>
                
                  
                  <div class='input-group date' id='datetimepicker1'>
                       <div class="input-append">
                <asp:TextBox class="date-picker form-control" ID="txtFechaConstruccion" data-date-format="dd/mm/yyyy"  runat="server" data-bv-trigger="keyup" CausesValidation="True" Width="140px" Height="30px"></asp:TextBox>
                 <label for="txtFechaConstruccion" class="input-group-addon btn"><span class="glyphicon glyphicon-calendar"></span>
                </label>
                      </div>
                    
                   </div>
                  
                      <%--<label for="txtFechaNacimiento" class="input-group-addon btn"><span class="glyphicon glyphicon-calendar"></span>--%>

                <%--<asp:TextBox ID="txtFechaConstruccion" runat="server"></asp:TextBox>--%>
                
                 
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="text-danger"  runat="server" ErrorMessage="Ingrese una fecha de construccion" ControlToValidate="txtFechaConstruccion"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="valFechaValida" runat="server" ErrorMessage="Ingrese una fecha válida" ControlToValidate="txtFechaConstruccion" 
                                Operator="DataTypeCheck" Type="Date" Display="Dynamic" CssClass="text-danger" ></asp:CompareValidator>
                       
                 <br />
                 <br />
            </div>
          
        </div>

    </div>
    </div>
             </div>
              
    
</div>
    
    <div  class="col-md-6"> 
         
        <div class="row">
         <div class="panel panel-default">
      <div class="panel-heading">Insumos que lo componen:</div>
      <div class="panel-body">
          <asp:GridView ID="grdInsumoXProducto" runat="server"  CssClass="table table-hover" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" AutoGenerateColumns="False" OnSelectedIndexChanged="grdInsumoXProducto_SelectedIndexChanged" >
        <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
               <Columns> 
                   <asp:BoundField DataField="id_Insumo" HeaderText="Id" SortExpression="id_Insumo" />
                   <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                     <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="5">
            <ItemTemplate>
                <asp:TextBox ID="cantidad" runat="server" Text='<%# Eval("cantidad") %>' />
            </ItemTemplate>
                         <ControlStyle Width="60px" />
                         
                         <ItemStyle Width="5px" />
                         
        </asp:TemplateField>
                   <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
               </Columns>
                       <HeaderStyle BackColor="Silver" />    
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
          </asp:GridView>
            <asp:Button ID="agregarInsumo" runat="server"  Text="Agregar Insumos" class="btn btn-primary btn-md" OnClick="agregarInsumo_Click" CausesValidation="False"/> 

             &nbsp;<asp:Button ID="guardarCambiosInsumo" runat="server"  Text="Guardar Insumos"  class="btn btn-primary btn-md" OnClick="guardarCambiosInsumo_Click"/> 
      </div>
    </div>
            </div>


        <asp:Panel ID="pnlInsumosDisponibles" runat="server"   >
         <div class="row"> 
              <div class="panel panel-default"> 
      <div class="panel-heading">Insumos disponibles:</div>
      <div class="panel-body"> 

          <asp:GridView ID="grdInsumosDisponibles" runat="server"  CssClass="table table-hover" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" AutoGenerateColumns="False" OnSelectedIndexChanged="grdInsumosDisponibles_SelectedIndexChanged">
        <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
 
              <Columns> 
                   <asp:CommandField SelectText="Agregar Insumo" ShowSelectButton="True" />
                   <asp:BoundField DataField="id_Insumo" HeaderText="Insumo" SortExpression="id_Insumo" />
                   
                   <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                   
               </Columns>
              <HeaderStyle BackColor="Silver" />    
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
          </asp:GridView>
          <!-- DESDE ACA -->

          

         <!-- HASTA ACA -->
      </div>
                  </div>
         </div>
            </asp:Panel>
    </div>

             </div>

      <div class="row">
          <div  class="col-md-11" > 
              <br />
     <asp:Button ID="btnConfAgregar" runat="server"  Text="Agregar Producto" class="btn btn-primary btn-md" OnClick="btnConfAgregar_Click" />                        
                          <asp:Button ID="btnCancelar" runat="server"  Text="Cancelar" class="btn btn-primary btn-md" OnClick="btnCancelar_Click" CausesValidation="false" />   
                              <asp:Button ID="btnConfElim" runat="server"  Text="Confirmar Eliminacion" class="btn btn-primary btn-md" OnClick="btnConfElim_Click" CausesValidation="False" />   
                              <asp:Button ID="btnConfModificar" runat="server" Text="Confirmar Modificar" class="btn btn-primary btn-md" OnClick="btnConfModificar_Click" />  
              </div>
          </div>
         
        </div>
    </asp:Panel>

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


