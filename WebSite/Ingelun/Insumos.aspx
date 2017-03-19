<%@ Page  Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Insumos.aspx.cs" Inherits="Insumos" UnobtrusiveValidationMode="None"  UICulture="en-US" Culture="en-US"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="//code.jquery.com/ui/1.11.3/themes/smoothness/jquery-ui.min.css" rel="stylesheet" type="text/css" />
   <!--  <script src="Scripts/jquery-2.2.3.min.js"></script>
     <script src="Scripts/jquery-ui-1.11.4.js"></script> -->
    
    
     <script src="//code.jquery.com/jquery-2.2.3.min.js" type="text/javascript"></script>
     <script src="//code.jquery.com/ui/1.11.4/jquery-ui.min.js" type="text/javascript"></script> 
 
    
    <script type="text/javascript">
        function validarMotivo(src, args) {
            if (args.Value === "0") {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

    </script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Division donde se encuentra el dialog -->
    <div id="dialogC" title="Mensaje" style="display:none">         
    </div>         
    
                <div class="row">
                    <div class="col-md-10">
                        <h1  class="page-header"  style="text-align: center">
                          Insumos
                        </h1>
                       
                    
<asp:Panel ID="pnlConsulta" runat="server" Height="500px" Width="900px">
    
    <div style="text-align: center">
    <div class="col-md-10"> 
    <asp:Label ID="lblMensaje" runat="server" Text="Label" Font-Size="Large"></asp:Label></div>    
    <br />
    <div class="col-md-4"> 
    <asp:Label ID="lblText" runat="server" Text="Ingrese Nombre para buscar" Font-Size="Large" ></asp:Label></div>
    <div class="col-md-3"> 
    <asp:TextBox ID="txtNombreBuscar" CssClass="form-control" runat="server"></asp:TextBox></div>         
    <div class="col-md-4"> 
    <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" class="btn btn-primary btn-md" Text="Buscar Nombre" /></div>
    <br />
    <br />    
    <br />
    </div>
    
    
    <asp:GridView ID="gwDatos" runat="server" CssClass="table  table-hover" DataKeyNames="id_Insumo"  BackColor="White" GridLines="None"  BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" OnSelectedIndexChanged="gwDatos_SelectedIndexChanged"  AutoGenerateColumns="False" OnPageIndexChanging="gwDatos_PageIndexChanging1" OnSorting="gwDatos_Sorting1" Height="180px" Width="899px">
        <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
            <asp:BoundField DataField="id_Insumo" HeaderText="idInsumo" SortExpression="id_Insumo" />
            <asp:BoundField DataField="nombre" HeaderText="Descripcion" SortExpression="nombre" />
            <asp:BoundField DataField="numeroSerie" HeaderText="Numero de Serie" SortExpression="numeroSerie" />
            <asp:BoundField DataField="costo" HeaderText="Costo" SortExpression="costo" DataFormatString="{0:C}" />
            <asp:BoundField DataField="volumen" HeaderText="Volumen" SortExpression="volumen" />
            <asp:BoundField DataField="nombreInsumo" HeaderText="Tipo Insumo" SortExpression="nombreInsumo" />
            <asp:CheckBoxField DataField="esNacional" HeaderText="Origen" Text="Nacional" />
        </Columns>
        <HeaderStyle BackColor="Silver" />        
         <SelectedRowStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" /> 
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
        <PagerStyle cssClass="gridpager" HorizontalAlign="Center" />          
    </asp:GridView>
        
    <br />
    <div style="text-align: center">
    <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" class="btn btn-primary btn-md" Text="Agregar Insumo" />
   
    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Insumo" class="btn btn-primary btn-md" OnClick="btnEliminar_Click" />
    
    <asp:Button ID="btnConsultar" runat="server" Text="Consultar Datos" class="btn btn-primary btn-md" OnClick="btnConsultar_Click" />
    
    <asp:Button ID="btnModificar" runat="server" Text="Modificar Insumo" class="btn btn-primary btn-md" OnClick="btnModificar_Click" />
    </div>
</asp:Panel>

                        </div>
                </div>
                <!-- /.row -->

     <asp:Panel ID="pnlAgregar" class="panel panel-default" runat="server">
         <div class="container">
            <div class="row">
                <div class="col-sm-6 col-sm-offset-3">
                    <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Datos de Insumos</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Id Insumo: " ></asp:Label>               
                        <asp:TextBox ID="txtIdInsumo" runat="server" CssClass="form-control" placeholder="IdInsumo"></asp:TextBox>                            
                        
        </div>
       <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Descripcion:" ></asp:Label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Descripcion" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="valDescripcionRequerida" CssClass="text-danger" runat="server" ErrorMessage="Debe ingresar nombre" ControlToValidate="txtDescripcion" ></asp:RequiredFieldValidator>
                 
       </div>
                        <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Numero de Serie:" ></asp:Label>
                <asp:TextBox ID="txtNumeroSerie" runat="server" CssClass="form-control" placeholder="Numero de Serie" TextMode="Number" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="text-danger" runat="server" ErrorMessage="Debe ingresar un Numero de Serie" ControlToValidate="txtNumeroSerie" ></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Debe ingresar solo numeros" Type="Integer" ControlToValidate="txtNumeroSerie" Operator="DataTypeCheck"></asp:CompareValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtNumeroSerie" runat="server" ErrorMessage="Solo puede agregar 4 digitos" ValidationExpression="^[\s\S]{0,25}$"></asp:RegularExpressionValidator>
       </div>
                        <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Çosto:" ></asp:Label>        
                 <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control" placeholder="Costo" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-danger" runat="server" ErrorMessage="Debe ingresar un Costo" ControlToValidate="txtCosto"  ></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1"  CssClass="text-danger" runat="server" ErrorMessage="Debe ingresar un valor numerico que puede tener decimales" ControlToValidate="txtCosto" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                <asp:RangeValidator ID="RangeValidator2" CssClass="text-danger" Type="Double" runat="server" ErrorMessage="Debe ingresar valores positivos" ControlToValidate="txtCosto" MinimumValue="0" MaximumValue="999999"></asp:RangeValidator> 
       </div>
                        <div class="form-group">
                <asp:Label ID="Label11" runat="server" Text="Vomunen:"></asp:Label>            
                 <asp:TextBox ID="txtVolumen" runat="server" CssClass="form-control" placeholder="Volumen" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-danger" runat="server" ErrorMessage="Debe ingresar un Volumen" ControlToValidate="txtVolumen" ></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="text-danger"  ErrorMessage="Debe ingresar un valor numerico que puede tener decimales" ControlToValidate="txtVolumen" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                 <asp:RangeValidator ID="RangeValidator1" CssClass="text-danger" Type="Double" runat="server" ErrorMessage="Debe ingresar valores positivos" ControlToValidate="txtVolumen" MinimumValue="0" MaximumValue="999999"></asp:RangeValidator>
        </div>
             <div class="form-group">
                <asp:Label ID="Label9" runat="server" Text="Tipo Producto:" AssociatedControlID="comboBoxTipoInsumo" ></asp:Label>             
                 <asp:DropDownList ID="comboBoxTipoInsumo" runat="server" CssClass="form-control"  ></asp:DropDownList>
            <asp:CustomValidator ID="valComboBox" runat="server" CssClass="text-danger" ErrorMessage="Debe seleccionar tipo de Insumo" ControlToValidate="comboBoxTipoInsumo" OnServerValidate="valComboBox_ServerValidate" ClientValidationFunction="validarMotivo" ></asp:CustomValidator>
        </div>
              <div class="form-group">
                <asp:Label ID="Label10" runat="server" Text="Origen:"></asp:Label>
                 <asp:CheckBox ID="chBoxOrigen" runat="server" Text="Es Nacional" CssClass="form-control" />
        </div>
                        
                        <div class="row">
                 <div  class="col-md-8" >
                   </div>
                </div>                 
                
                             
                     <asp:Button ID="btnConfAgregar" runat="server" class="btn btn-primary btn-md" OnClick="btnConfAgregar_Click" Text="Agregar Insumo" />
                     <asp:Button ID="btnCancelar" runat="server" class="btn btn-primary btn-md" OnClick="btnCancelar_Click" Text="Cancelar" CausesValidation="false"  />
                     <asp:Button ID="btnConfElim" runat="server" class="btn btn-primary btn-md" Text="Confirmar Eliminacion" OnClick="btnConfElim_Click"  />
                     <asp:Button ID="btnConfModificar" runat="server" Text="Confirmar Modificar" class="btn btn-primary btn-md" OnClick="btnConfModificar_Click"  />
                                                 
                </div>                 
                   
            </div>
        </div>
    </div> 
             </div>        
    </asp:Panel>
</asp:Content>