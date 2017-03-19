using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IngelunNegocio;
using IngelunEntidades;
using System.Text;

public partial class Proveedores : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["GwDatosOrden"] = "nombre";
            gwProveedores.AllowPaging = true;
            gwProveedores.AllowSorting = true;
            pnlAgregar.Visible = false;
            gwProveedores.PageSize = 5;
            refrescarGrilla();
            refrescarCombo();
        }

    }

    public void refrescarGrilla()
    {
        string orden = ViewState["GwDatosOrden"].ToString();
        gwProveedores.DataSource = GestorProveedores.BuscarPorNombre(txtNombre.Text, orden); 
        gwProveedores.DataBind();
        gwProveedores.SelectedIndex = -1;
    }

    public void refrescarCombo()
    {

        ddlProveedor.DataTextField = "id_tipo_Dni";
        ddlProveedor.DataTextField = "descripcion";

        ddlProveedor.DataSource = GestorProveedores.ObtenerDNI();
        
        ddlProveedor.DataBind();

        ddlProveedor.Items.Insert(0, new ListItem("Elija un Documento", "0"));

        //Cargo el combo para consultar

        ddlProveedorConsulta.DataTextField = "id_tipo_Dni";
        ddlProveedorConsulta.DataTextField = "descripcion";

        ddlProveedorConsulta.DataSource = GestorProveedores.ObtenerDNI();

        ddlProveedorConsulta.DataBind();

        ddlProveedorConsulta.Items.Insert(0, new ListItem("Elija un Documento", "0"));
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        refrescarGrilla();
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        limpiarTxt();
        Panel1.Visible = false;
        btnConfAgregar.Visible = true;
        pnlAgregar.Visible = true;
        
        try
        {
            txtIdProveedor.Text = "" + GestorProveedores.obtenerSiguienteID();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

        lblAccion.Text = " Agregando";
        btnConfElim.Visible = false;
        btnAgregar.Visible = true;
        btnConfModificar.Visible = false;

    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (gwProveedores.SelectedValue == null)
        {
            MostrarMensajeCheto("Seleccione una Fila antes de continuar :)");
            return;
        }
        Panel1.Visible = false;
        pnlAgregar.Visible = false;
        pnlConsultar.Visible = true;
        btnConfElimCon.Visible = true;
        btnConfModificar.Visible = false;
        btnConfAgregar.Visible = false;

        lblAccion.Text = " Eliminando";
        int idProveedor = (int)gwProveedores.SelectedValue;
        try
        {
            IngelunEntidades.Proveedor Prov = GestorProveedores.buscarPorId(idProveedor);
            txtIdProveedorConsulta.Text = Prov.id_Proveedor.ToString();
            txtNombreConsulta.Text = Prov.nombre;
            txtDireccionConsulta.Text = Prov.direccion.ToString();
            txtEmailConsulta.Text = Prov.mail.ToString();
            txtTelefonoConsulta.Text = Prov.telefono.ToString();
            txtNumeroDocumentoConsulta.Text = Prov.numeroDocumento.ToString();
            txtCodigoPostalConsulta.Text = Prov.codigo_Postal.ToString();
            if (Prov.fechaNac != null)
            {
                string fecha = Prov.fechaNac.ToString();
                DateTime fechaNacimiento = DateTime.Parse(fecha);
                txtFechaNacimientoConsulta.Text = fechaNacimiento.ToShortDateString();
            }
            else
            {
                txtFechaNacimientoConsulta.Text = "";
            }
            chBoxEfectivoConsulta.Checked = Prov.soloEfectivo;
            ddlProveedorConsulta.SelectedIndex = (int)Prov.id_tipo_dni;
           
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

        btnConfElim.Visible = true;
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        
        if (gwProveedores.SelectedValue == null)
        {
            MostrarMensajeCheto("Seleccione una Fila antes de continuar :)");
            return;
        }
        Panel1.Visible = false;
        pnlAgregar.Visible = false;
        pnlConsultar.Visible = true;
        btnConfElimCon.Visible = false;
        int idProveedor = (int)gwProveedores.SelectedValue;
        try
        {
            IngelunEntidades.Proveedor Prov = GestorProveedores.buscarPorId(idProveedor);
            txtIdProveedorConsulta.Text = Prov.id_Proveedor.ToString();
            txtNombreConsulta.Text = Prov.nombre;
            txtDireccionConsulta.Text = Prov.direccion.ToString();
            txtEmailConsulta.Text = Prov.mail.ToString();
            txtTelefonoConsulta.Text = Prov.telefono.ToString();
            txtNumeroDocumentoConsulta.Text = Prov.numeroDocumento.ToString();
            txtCodigoPostalConsulta.Text = Prov.codigo_Postal.ToString();
            if (Prov.fechaNac != null)
            {
                string fecha = Prov.fechaNac.ToString();
                DateTime fechaNacimiento = DateTime.Parse(fecha);
                txtFechaNacimientoConsulta.Text = fechaNacimiento.ToShortDateString();
            }
            else
            {
                txtFechaNacimientoConsulta.Text = "";
            }
            chBoxEfectivoConsulta.Checked = Prov.soloEfectivo;
            ddlProveedorConsulta.SelectedIndex = (int)Prov.id_tipo_dni;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

        lblAccion.Text = " Consultando";
        btnConfElim.Visible = false;
        btnConfAgregar.Visible = false;
        btnConfModificar.Visible = false;
    }
    protected void btnModificar_Click(object sender, EventArgs e)
    {

        if (gwProveedores.SelectedValue == null)
        {
            MostrarMensajeCheto("Seleccione una Fila antes de continuar :)");
            return;
        }
        int idProveedor = (int)gwProveedores.SelectedValue;
        try
        {
            IngelunEntidades.Proveedor Prov = GestorProveedores.buscarPorId(idProveedor);
            Session["proveedor"] = Prov;
            txtIdProveedor.Text = Prov.id_Proveedor.ToString();
            txtNombreEdicion.Text = Prov.nombre;
            txtDireccion.Text = Prov.direccion.ToString();
            txtEmail.Text = Prov.mail.ToString();
            txtTelefono.Text = Prov.telefono.ToString();
            txtNumeroDocumento.Text = Prov.numeroDocumento.ToString();
            txtCodigoPostal.Text = Prov.codigo_Postal.ToString();
            if (Prov.fechaNac != null)
            {
                string fecha = Prov.fechaNac.ToString();
                DateTime fechaNacimiento = DateTime.Parse(fecha);
                txtFechaNacimiento.Text = fechaNacimiento.ToShortDateString();
            }
            else
            {
                txtFechaNacimiento.Text = "";
            }
          
            chBoxEfectivo.Checked = Prov.soloEfectivo;
            ddlProveedor.SelectedIndex = (int)Prov.id_tipo_dni;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

        Panel1.Visible = false;
        pnlAgregar.Visible = true;
        pnlConsultar.Visible = false;
       // lblAccion.Text = " Editando";
        btnConfElim.Visible = false;
        btnConfAgregar.Visible = false;
        btnConfModificar.Visible = true;
        
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        limpiarTxt();
        pnlAgregar.Visible = false;
        pnlConsultar.Visible = false;
        Panel1.Visible = true;
        refrescarGrilla();
    }

   
    protected void btnConfModificar_Click(object sender, EventArgs e)
    {
        pnlAgregar.Visible = false;
        Panel1.Visible = true;
        if((int.Parse(txtNumeroDocumento.Text) == ((Proveedor)Session["proveedor"]).numeroDocumento))
        {
            agregarProveedor();
        }
        else if (!GestorProveedores.existeProveedor(ddlProveedor.SelectedIndex, int.Parse(txtNumeroDocumento.Text)))
        {
            agregarProveedor();
        }
        else
        {
            MostrarMensajeCheto("Ya existe un proveedor con ese número de documento");
        }

        
    }

    private void agregarProveedor()
    {
        try
        {
            GestorProveedores.editarProveedor(int.Parse(txtIdProveedor.Text), txtNombreEdicion.Text, txtDireccion.Text, txtEmail.Text, int.Parse(txtTelefono.Text), int.Parse(txtNumeroDocumento.Text), ddlProveedor.SelectedIndex, int.Parse(txtCodigoPostal.Text), DateTime.Parse(txtFechaNacimiento.Text), chBoxEfectivo.Checked);
        }
        catch (Exception ex)
        {
            MostrarMensajeCheto(ex.Message);
        }
        MostrarMensajeCheto("Registro Modificado Correctamente");
        refrescarGrilla();
    }
    protected void btnConfElim_Click(object sender, EventArgs e)
    {
        
        try
        {
            GestorProveedores.eliminarProveedor((int)gwProveedores.SelectedValue);

        }
        catch (Exception ex)
        {
            MostrarMensajeCheto("El registro no se puede eliminar por que esta siendo usado por otra tabla =D =D ");
        }
        refrescarGrilla();
        Panel1.Visible = true;
        pnlAgregar.Visible = false;
        pnlConsultar.Visible = false;
        MostrarMensajeCheto("Registro Eliminado Correctamente");
        btnConfElim.Visible = false;
        btnConfAgregar.Visible = true;
    }
    protected void btnConfAgregar_Click(object sender, EventArgs e)
    {
        pnlAgregar.Visible = false;
        Panel1.Visible = true;

        try
        {
            if(GestorProveedores.existeProveedor(ddlProveedor.SelectedIndex,int.Parse(txtNumeroDocumento.Text)))
            {
                MostrarMensajeCheto("El proveedor con Tipo de Documento: "+ddlProveedor.SelectedIndex.ToString() + " y Número de Documento: "+txtNumeroDocumento.Text+", ya existe.");
            }
            else
            {
                GestorProveedores.agregarProveedor(txtNombreEdicion.Text, txtDireccion.Text, txtEmail.Text, int.Parse(txtTelefono.Text), int.Parse(txtNumeroDocumento.Text), ddlProveedor.SelectedIndex, int.Parse(txtCodigoPostal.Text), DateTime.Parse(txtFechaNacimiento.Text), chBoxEfectivo.Checked);
            }
         

        }
        catch (Exception ex)
        {
            MostrarMensajeCheto("El registro no se pudo agregar por que ya existe un proveedor con ese Número de Documento");
        }
        MostrarMensajeCheto("Registro Agregado Correctamente");
        refrescarGrilla();  
    }

    private void limpiarTxt()
    {
        txtNumeroDocumento.Text = "";
        txtCodigoPostal.Text = "";
        txtDireccion.Text = "";
        txtEmail.Text = "";
        txtFechaNacimiento.Text = "";
        txtIdProveedor.Text = "";
        txtNombreEdicion.Text = "";
        txtTelefono.Text = "";
        txtNumeroDocumento.Text = "";
        ddlProveedor.SelectedIndex = -1;
        chBoxEfectivo.Checked = false;
        
    }

    protected void gwProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gwProveedores.PageIndex = e.NewPageIndex;
        refrescarGrilla();
    }
    protected void gwProveedores_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["GwDatosOrden"] = e.SortExpression;
        refrescarGrilla();
    }

    private void MostrarMensaje(string msj)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "msjServidor",
    @" 
            <script type='text/javascript'>
                $( function () {
                    $('#dialogC').html('" + msj + @"');   // escribo el msj
                    $('#dialogC').dialog(); //muestro el msj
                });
            </script>
            ");
    }

    private void MostrarMensajeCheto(string msj)
    {
        StringBuilder asd2 = new StringBuilder();

        asd2.AppendLine(
            "{modal: true,buttons:{ \"Cerrar\": function() {$(this).dialog(\"close\");}}}");

        StringBuilder asd = new StringBuilder();

        asd.AppendLine("<script type = 'text/javascript'>");
        asd.AppendLine("$(function() {");
        asd.AppendFormat("$('#dialogC').html('{0}');", msj);
        asd.AppendFormat("$('#dialogC').dialog({0})", asd2.ToString());
        asd.AppendLine("});");
        asd.AppendLine("</script>");

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "msjServidor", asd.ToString());
    }
}