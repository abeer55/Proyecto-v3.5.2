using IngelunNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Compra : System.Web.UI.Page
{
    DataTable tablaDetalle;
    double totalCompra=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtFecha.Text = DateTime.Today.ToShortDateString();
            refrescarCombo();
            refrescarGrillaInsumos();
            Session["tablaDetalle"] = null;
            Session["totalCompra"] = 0;
            Session["idInsumo"] = 0;
            crearTablaDetalle();
          
        }
     
    }

    private void crearTablaDetalle()
    {
        tablaDetalle = new DataTable();
        if (tablaDetalle.Columns.Count == 0)
        {
            tablaDetalle.Columns.Add("id_Insumo", typeof(string));
            tablaDetalle.Columns.Add("nombre", typeof(string));
            tablaDetalle.Columns.Add("cantidad", typeof(string));
            tablaDetalle.Columns.Add("costo", typeof(string));
            tablaDetalle.Columns.Add("subtotal", typeof(string));
        }
        Session["tablaDetalle"] = tablaDetalle; 
    }

    private void ocultarLblMensaje()
    {
        lblMensaje.Visible = false;
    }

    private void mostrarLblMensaje()
    {
        lblMensaje.Visible = true;
    }


    public void refrescarGrillaInsumos()
    {
        DataTable tabla = GestorInsumos.ObtenerInsumosParaCompra();
        gwInsumos.DataSource = tabla;
        gwInsumos.DataBind();
    }
     public void refrescarGrillaDetalleCompra(DataTable tablaDetalle)
     {
         gwDetalleCompra.DataSource = tablaDetalle;
         gwDetalleCompra.DataBind();
     }

    public void refrescarCombo()
    {
        ddlProveedor.DataTextField = "id_Proveedor";
        ddlProveedor.DataTextField = "Nombre";

        ddlProveedor.DataSource = GestorProveedores.ObtenerProveedores();

        ddlProveedor.DataBind();

        ddlProveedor.Items.Insert(0, new ListItem("Elija un Proveedor", "0"));
    }
    protected void btnSeleccionarInsumo_Click(object sender, EventArgs e)
    {
        if (gwInsumos.SelectedValue == null)
        {
            mostrarLblMensaje();
            lblMensaje.Text = "Primero Seleccione algún insumo";
            return;
        }
        ocultarLblMensaje();

        int idInsumo = (int)gwInsumos.SelectedValue;

        try
        {
            IngelunEntidades.Insumo insumo = GestorInsumos.buscarPorId(idInsumo);
            Session["idInsumo"] = insumo.id_Insumo;
            txtCosto.Text = insumo.costo.ToString();
            txtInsumo.Text = insumo.nombre;

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    public void calcularTotalCompra(double subTotal)
    {
        totalCompra = double.Parse(Session["totalCompra"].ToString());
        totalCompra += subTotal;
        Session["totalCompra"] = totalCompra;
        lblTotalCompra.Visible = true;
        lblTotalCompra.Text = totalCompra.ToString();
        
    }
    protected void btnAgregarADetalle_Click(object sender, EventArgs e)
    {
        int cantidad = 0;
        if (txtCantidad.Text.CompareTo("") == 0)
        {
            lblMensajeAgregarDetalle.Text = "Ingrese la cantidad solicitada del insumo";
            lblMensajeAgregarDetalle.Visible=true;
            return;
        }
        else
        {
            lblMensajeAgregarDetalle.Visible = false;
        }

        try
        {
            cantidad = int.Parse(txtCantidad.Text);
        }
        catch (Exception)
        {
            lblMensajeAgregarDetalle.Text = "Error al ingresar cantidad solicitada";
            lblMensajeAgregarDetalle.Visible = true;
            
        }

        DataTable tabla = (DataTable)Session["tablaDetalle"];
        double subtotal = double.Parse(txtCosto.Text) * cantidad;
        tabla.Rows.Add(Session["idInsumo"].ToString(),txtInsumo.Text, txtCantidad.Text, txtCosto.Text, subtotal.ToString());
        
        Session["tablaDetalle"] = tabla;

        refrescarGrillaDetalleCompra(tabla);
        calcularTotalCompra(subtotal);
        limpiarCamposInsumo();

    }

    public void limpiarCamposInsumo()
    {
        txtCantidad.Text = "";
        txtCosto.Text = "";
        txtInsumo.Text = "";
    }
    protected void btnConfirmarCompra_Click(object sender, EventArgs e)
    {
        int idProveedor=0;
        if (ddlProveedor.SelectedIndex > 0)
        {
            lblMensajeConfirmarCompra.Visible = false;
            idProveedor = GestorProveedores.BuscarPorNombre(ddlProveedor.SelectedItem.Value, "p.nombre")[0].id_Proveedor;
        }
        else
        {
            lblMensajeConfirmarCompra.Visible = true;
            lblMensajeConfirmarCompra.Text = "Seleccione proveedor para imputarle la compra";
            return;
        }

        if (GestorTransaccionCompra.generarCompra((DataTable)Session["tablaDetalle"], (double)Session["totalCompra"], idProveedor, DateTime.Parse(txtFecha.Text)))
        {
            MostrarMensajeCheto("Compra registrada", "Se ha registrador la compra con éxito", lblAyuda, updPanelAyuda);
        }
        else
        {
            MostrarMensajeCheto("Error", "Error al generar transacción", lblAyuda, updPanelAyuda);
        }

        
       limpiarTodosLosCampos();

    }

    private void limpiarTodosLosCampos()
    {
        txtCantidad.Text = "";
        txtCosto.Text = "";
        txtInsumo.Text = "";
        ddlProveedor.SelectedIndex = 0;
        crearTablaDetalle();
        refrescarGrillaInsumos();
        refrescarGrillaDetalleCompra((DataTable)Session["tablaDetalle"]);
        Session["totalCompra"] = 0;
        totalCompra = 0;
        lblTotalCompra.Text = "";
        lblTotalCompra.Visible = false;
    }

    public void MostrarMensajeCheto(String titulo, String mensajeAyuda, Label labelAyuda, System.Web.UI.UpdatePanel contenedor)
    {
        string script = "$('#ventanaAyuda').modal('show');";
        labelAyuda.Text = crearAyudaModal(titulo, mensajeAyuda, script);
        labelAyuda.Visible = true;
        System.Web.UI.ScriptManager.RegisterStartupScript(contenedor, contenedor.GetType(), Guid.NewGuid().ToString(), script, true);
    }

    public String crearAyudaModal(String titulo, String mensajeAyuda, String scriptClose, String id = "ventanaAyuda")
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<div class='modal' id='" + (id.Equals("") ? "ventanaAyuda" : id) + "' role='dialog' style='overflow:hidden;'>");
        sb.AppendLine(" <div class='modal-dialog modal-lg'>");
        sb.AppendLine("  <div class='modal-content'>");
        sb.AppendLine("   <div class='modal-header header-ayuda'>");
        sb.Append("    <button type='button' class='close' data-dismiss='modal' "); sb.Append(scriptClose.Equals("") ? "" : "onclick=" + scriptClose); sb.AppendLine(">&times;</button>");
        sb.Append("    <h4 class='modal-title'>"); sb.Append(titulo); sb.AppendLine("</h4>");
        sb.AppendLine("   </div>");
        sb.Append("   <div class='modal-body'>"); sb.AppendLine(mensajeAyuda);
        sb.AppendLine("   </div>");
        sb.AppendLine("   <div class='modal-footer padding-vertical-5 no-margin-top'>");
        sb.Append("    <button type='button' class='btn btn-primary btn-md' data-dismiss='modal' "); sb.Append(scriptClose.Equals("") ? "" : "onclick=" + scriptClose); sb.AppendLine(">Cerrar</button>");
        sb.AppendLine("   </div>");
        sb.AppendLine("  </div>");
        sb.AppendLine(" </div>");
        sb.AppendLine("</div>");

        return sb.ToString();
    }

    
}