<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Proveedores.aspx.cs" Inherits="Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <script src="//code.jquery.com/jquery-2.2.3.min.js" type="text/javascript"></script>
     <script src="//code.jquery.com/ui/1.11.4/jquery-ui.min.js" type="text/javascript"></script> 
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="dialogC" title="Mensaje" style="display:none"> </div>  
     <!-- Page Heading -->

                        <h1 class="page-header">
                          Proveedor
                        </h1>

<asp:Panel ID="Panel1" runat="server" Height="550px" Width="1000px">
   
     <asp:Label ID="lblAccion" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblMensaje" runat="server" Text="Label" Font-Size="Large" Visible="False"></asp:Label>
    <br />

    <asp:Label ID="lblTexto" runat="server" Text="Ingrese Nombre de Proveedor"></asp:Label>
    &nbsp;
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    &nbsp;
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Visible="True" class="btn btn-primary btn-md" OnClick="btnBuscar_Click" />
     <br />
    <br />
    
    <asp:GridView ID="gwProveedores" runat="server" CssClass="table table-hover" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" DataKeyNames="id_Proveedor" GridLines="None" Height="136px" Width="1000px" AutoGenerateColumns="False" OnPageIndexChanging="gwProveedores_PageIndexChanging" OnSorting="gwProveedores_Sorting">
        <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="id_Proveedor" HeaderText="Id " SortExpression="id_Proveedor" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
            <asp:BoundField DataField="direccion" HeaderText="Direccion" SortExpression="direccion" />
            <asp:BoundField DataField="mail" HeaderText="E-mail" SortExpression="mail" />
            <asp:BoundField DataField="telefono" HeaderText="Telefono" SortExpression="telefono" />
            <asp:BoundField DataField="descripcion" HeaderText="Tipo Documento" SortExpression="descripcion" />
            <asp:BoundField DataField="codigo_Postal" HeaderText="Codigo Postal" SortExpression="codigo_Postal" />
            <asp:BoundField DataField="fechaNac" HeaderText="Fecha Nacimiento" SortExpression="fechaNac" DataFormatString="{0:d}" />
            <asp:CheckBoxField DataField="soloEfectivo" HeaderText="Efectivo" SortExpression="soloEfectivo" Text="Si" />
             <asp:BoundField DataField="numeroDocumento" HeaderText="Numero Documento" SortExpression="numeroDocumento" />
        </Columns>

        <PagerStyle cssClass="gridpager" HorizontalAlign="Center" />  
         <HeaderStyle BackColor="Silver" />    
         <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    
    <br />
     &nbsp;

    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Proveedor" class="btn btn-primary btn-md"  OnClick="btnAgregar_Click" />
    &nbsp;&nbsp;
    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Proveedor" class="btn btn-primary btn-md"  OnClick="btnEliminar_Click"  />
    &nbsp;&nbsp;
    <asp:Button ID="btnConsultar" runat="server" Text="Consultar Proveedor" class="btn btn-primary btn-md"  OnClick="btnConsultar_Click"  />
    &nbsp;&nbsp;
    <asp:Button ID="btnModificar" runat="server" Text="Modificar Proveedor" class="btn btn-primary btn-md"  OnClick="btnModificar_Click"  />

</asp:Panel>

<asp:Panel ID="pnlAgregar" runat="server" class="panel panel-default"  Height="1100px" Visible="False">
   
  <br /> 
         <div class="col-xs-12 col-sm-6 col-md-6 box register" runat="server">
                    <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Datos de Proveedores</h3>
                    </div>
                    <div class="panel-body">

			  <h3 class="title">Información de registro</h3>
			  <div class="form-group">
				<label>Id:</label>
                <asp:TextBox class="form-control" ID="txtIdProveedor" runat="server" data-bv-trigger="keyup" readonly></asp:TextBox>
              </div>

			  <div class="form-group">
				<label>Nombre:</label>
                  <asp:TextBox class="form-control" ID="txtNombreEdicion" runat="server" data-bv-trigger="keyup" CausesValidation="True"  ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-danger" runat="server" ErrorMessage="Debe ingresar un Nombre" ControlToValidate="txtNombreEdicion"></asp:RequiredFieldValidator>
			  </div>
			
			 <div class="form-group">
				<label>Dirección:</label>
                  <asp:TextBox class="form-control" ID="txtDireccion" runat="server" data-bv-trigger="keyup" CausesValidation="True"  ></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-danger" runat="server" ErrorMessage="Debe ingresar una direccion" ControlToValidate="txtDireccion"></asp:RequiredFieldValidator>
			  </div>
			  
			  <div class="form-group">
				<label>E-mail:</label>
                  <asp:TextBox class="form-control" ID="txtEmail" runat="server" data-bv-trigger="keyup" CausesValidation="True" ></asp:TextBox>
				<asp:RequiredFieldValidator ID="valEmailRequerido"  runat="server" ErrorMessage="Debe ingresar un email" 
                                ControlToValidate="txtEmail" CssClass="text-danger" Display="Dynamic" ></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="valEmailValido" runat="server" ErrorMessage="Debe ingresar un email válido" 
                                CssClass="text-danger" ControlToValidate="txtEmail" ValidationExpression="^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$" Display="Dynamic"></asp:RegularExpressionValidator>
                       
			  </div>
			   
			   <div class="form-group">
				<label>Telefono:</label>
                  <asp:TextBox class="form-control" ID="txtTelefono" runat="server" data-bv-trigger="keyup" CausesValidation="True"  ></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="text-danger" runat="server" ErrorMessage="Ingrese un telefono" ControlToValidate="txtTelefono"></asp:RequiredFieldValidator>
                   <asp:CompareValidator ID="CompareValidator1" CssClass="text-danger" runat="server" ErrorMessage="Ingrese solo numeros" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtTelefono"></asp:CompareValidator>
			  </div>
			  
			   <div class="form-group">
				<label>Tipo Dni:</label>
               <asp:DropDownList ID="ddlProveedor" CssClass="form-control"  runat="server"></asp:DropDownList>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="text-danger" runat="server" ErrorMessage="Debe seleccionar un tipo de documento" ControlToValidate="ddlProveedor" InitialValue="0"></asp:RequiredFieldValidator>
			  </div>
			  
			    <div class="form-group">
				<label>Número de Documento:</label>
                  <asp:TextBox class="form-control" ID="txtNumeroDocumento" runat="server" data-bv-trigger="keyup" CausesValidation="True"  ></asp:TextBox>
				  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="text-danger" runat="server" ControlToValidate="txtNumeroDocumento" ErrorMessage="Debe agregar un DNI"></asp:RequiredFieldValidator>
                <asp:CompareValidator ControlToValidate="txtNumeroDocumento" CssClass="text-danger" ID="CompareValidator2" runat="server" ErrorMessage="Deben ser solo Numeros" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" CssClass="text-danger" ErrorMessage="Debe ser entre 1.000.000 y 100.000.000" ControlToValidate="txtNumeroDocumento" MaximumValue="100000000" MinimumValue="1000000" Type="Integer"></asp:RangeValidator>
            
			  </div>
			  
			    <div class="form-group">
				<label>Código Postal:</label>
                  <asp:TextBox class="form-control" ID="txtCodigoPostal" runat="server" data-bv-trigger="keyup" CausesValidation="True"  ></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="text-danger" runat="server" ControlToValidate="txtCodigoPostal" ErrorMessage="Debe ingresar un codigo Postal"></asp:RequiredFieldValidator>
                <asp:CompareValidator ControlToValidate="txtCodigoPostal" CssClass="text-danger" ID="CompareValidator3" runat="server" ErrorMessage="Deben ser solo Numeros" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                
			  </div>
			  <div class="form-group">
				<label>Fecha Nacimiento: <span class="required">*</span></label>
				 <div class='input-group date' id='datetimepicker1'> 
                    <asp:TextBox class="date-picker form-control" ID="txtFechaNacimiento" data-date-format="dd/mm/yyyy" runat="server" data-bv-trigger="keyup" CausesValidation="True" ></asp:TextBox>
                    <label for="txtFechaNacimiento" class="input-group-addon btn" disabled><span class="glyphicon glyphicon-calendar"></span></label>
                  </div>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="text-danger"  runat="server" ErrorMessage="Ingrese una fecha de nacimiento" ControlToValidate="txtFechaNacimiento"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="valFechaValida" runat="server" ErrorMessage="Ingrese una fecha válida" ControlToValidate="txtFechaNacimiento" 
                                Operator="DataTypeCheck" Type="Date" Display="Dynamic" CssClass="text-danger" ></asp:CompareValidator>
   			  </div>
			  
			    <div class="form-group">
				<label>Efectivo:</label>
                  <asp:CheckBox ID="chBoxEfectivo" runat="server" />
				
			  </div>

               <div class="form-group">
			  <asp:Button ID="btnConfAgregar" runat="server"  Text="Agregar Proveedor" class="btn btn-primary btn-md"   OnClick="btnConfAgregar_Click" />
                     &nbsp;&nbsp;<asp:Button ID="btnConfElim" runat="server" class="btn btn-primary btn-md" OnClick="btnConfElim_Click" Text="Confirmar Eliminacion" />
                     &nbsp;<asp:Button ID="btnConfModificar" runat="server" class="btn btn-primary btn-md" OnClick="btnConfModificar_Click" Text="Confirmar Modificar" />
                     &nbsp;<asp:Button ID="btnCancelar" runat="server"  Text="Cancelar" class="btn btn-primary btn-md"  CausesValidation="false" OnClick="btnCancelar_Click" />
				
			  </div>

		  </div>
        </div>

	  </div>
       
     </asp:Panel>
    <asp:Panel ID="pnlConsultar" runat="server" class="panel panel-default"  Height="1050px" Visible="False">
   
  <br /> 
         <div class="col-xs-12 col-sm-6 col-md-6 box register" runat="server">
                    <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Datos de Proveedores</h3>
                    </div>
                    <div class="panel-body">

			  <h3 class="title">Información de registro</h3>
			  <div class="form-group">
				<label>Id:</label>
                <asp:TextBox class="form-control" ID="txtIdProveedorConsulta" runat="server" data-bv-trigger="keyup" readonly></asp:TextBox>
              </div>

			  <div class="form-group">
				<label>Nombre:</label>
                  <asp:TextBox class="form-control" ID="txtNombreConsulta" runat="server" data-bv-trigger="keyup" CausesValidation="True"  readonly></asp:TextBox>
			  </div>
			
			 <div class="form-group">
				<label>Dirección:</label>
                  <asp:TextBox class="form-control" ID="txtDireccionConsulta" runat="server" data-bv-trigger="keyup" CausesValidation="True"  readonly></asp:TextBox>
        	  </div>
			  
			  <div class="form-group">
				<label>E-mail:</label>
                  <asp:TextBox class="form-control" ID="txtEmailConsulta" runat="server" data-bv-trigger="keyup" CausesValidation="True" readonly></asp:TextBox>
			  </div>
			   
			   <div class="form-group">
				<label>Telefono:</label>
                  <asp:TextBox class="form-control" ID="txtTelefonoConsulta" runat="server" data-bv-trigger="keyup" CausesValidation="True"  readonly></asp:TextBox>
			  </div>
			  
			   <div class="form-group">
				<label>Tipo Dni:</label>
               <asp:DropDownList ID="ddlProveedorConsulta" CssClass="form-control"  runat="server" disabled=""></asp:DropDownList>
			  </div>
			  
			    <div class="form-group">
				<label>Número de Documento:</label>
                  <asp:TextBox class="form-control" ID="txtNumeroDocumentoConsulta" runat="server" data-bv-trigger="keyup" CausesValidation="True"  readonly></asp:TextBox>
			  </div>
			  
			    <div class="form-group">
				<label>Código Postal:</label>
                  <asp:TextBox class="form-control" ID="txtCodigoPostalConsulta" runat="server" data-bv-trigger="keyup" CausesValidation="True"  readonly></asp:TextBox>
		 	  </div>
			  <div class="form-group">
				<label>Fecha Nacimiento: <span class="required">*</span></label>
                <asp:TextBox class="date-picker form-control" ID="txtFechaNacimientoConsulta" data-date-format="dd/mm/yyyy" runat="server" data-bv-trigger="keyup" CausesValidation="True" readonly></asp:TextBox>
               </div>
   			  </div>

			  <div class="form-group">
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label>Efectivo:</label>
                <asp:CheckBox ID="chBoxEfectivoConsulta" runat="server" Enabled="false"/>
			  </div>

               <div class="form-group">
                   &nbsp;<asp:Button ID="btnConfElimCon" runat="server" class="btn btn-primary btn-md" OnClick="btnConfElim_Click" Text="Confirmar Eliminacion" />
			       &nbsp;<asp:Button ID="Button4" runat="server"  Text="Cancelar" class="btn btn-primary btn-md"  CausesValidation="false" OnClick="btnCancelar_Click" />
			  </div>

		  </div>
        </div>

       
     </asp:Panel>
     
      <!-- DatePicker -->
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
