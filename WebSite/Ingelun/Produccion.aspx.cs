using IngelunNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Produccion : System.Web.UI.Page
{
    DataTable tablaDetalle;
    double totalProduccion = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtFecha.Text = DateTime.Today.ToShortDateString();
            refrescarGrillaProductos();
            Session["tablaDetalle"] = null;
            Session["totalProduccion"] = 0;
            Session["idProducto"] = 0;
            crearTablaDetalle();


        }
    }

    protected void btnAgregarADetalle_Click(object sender, EventArgs e)
    {
        int cantidad = 0;
        if (txtCantidad.Text.CompareTo("") == 0)
        {
            lblMensajeAgregarDetalle.Text = "Ingrese la cantidad solicitada del producto";
            lblMensajeAgregarDetalle.Visible = true;
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
            lblMensajeAgregarDetalle.Text = "Error al ingresar cantidad producto";
            lblMensajeAgregarDetalle.Visible = true;

        }

        DataTable tabla = (DataTable)Session["tablaDetalle"];
        double subtotal = double.Parse(txtPrecio.Text) * cantidad;
        tabla.Rows.Add(Session["idProducto"].ToString(), txtProducto.Text, txtCantidad.Text, txtPrecio.Text, subtotal.ToString());
        Session["tablaDetalle"] = tabla;
        refrescarGrillaDetalleProduccion(tabla);
        calcularTotalProduccion(subtotal);
        limpiarCamposProducto();
        gwProductos.SelectedIndex = -1;

    }

    public void limpiarCamposProducto()
    {
        txtCantidad.Text = "";
        txtPrecio.Text = "";
        txtProducto.Text = "";
    }

    public void refrescarGrillaDetalleProduccion(DataTable tablaDetalle)
    {
        gwDetalleProduccion.DataSource = tablaDetalle;
        gwDetalleProduccion.DataBind();
    }

    public void calcularTotalProduccion(double subTotal)
    {
        totalProduccion = double.Parse(Session["totalProduccion"].ToString());
        totalProduccion += subTotal;
        Session["totalProduccion"] = totalProduccion;
        lblTotalProduccion.Visible = true;
        lblTotalProduccion.Text = totalProduccion.ToString();

    }

    protected void btnConfirmarProduccion_Click(object sender, EventArgs e)
    {
        if (GestorTransaccionProduccion.generarProduccion((DataTable)Session["tablaDetalle"], (double)Session["totalProduccion"], DateTime.Parse(txtFecha.Text)))
        {
            MostrarMensajeCheto("Producción registrada", "Se ha registrado la producción con éxito", lblAyuda, updPanelAyuda);
        }
        else
        {
            MostrarMensajeCheto("Error", "Error al generar transacción, insumos insuficientes", lblAyuda, updPanelAyuda);
        }


        limpiarTodosLosCampos();
    }

    private void limpiarTodosLosCampos()
    {
        txtCantidad.Text = "";
        txtPrecio.Text = "";
        txtProducto.Text = "";
        crearTablaDetalle();
        refrescarGrillaProductos();
        refrescarGrillaDetalleProduccion((DataTable)Session["tablaDetalle"]);
        Session["totalProduccion"] = 0;
        totalProduccion = 0;
        lblTotalProduccion.Text = "";
        lblTotalProduccion.Visible = false;
    }

    public void MostrarMensajeCheto(String titulo, String mensajeAyuda, Label labelAyuda, System.Web.UI.UpdatePanel contenedor)
    {
        string script = "$('#ventanaAyuda').modal('show');";
        labelAyuda.Text = crearAyudaModal(titulo, mensajeAyuda, script);
        labelAyuda.Visible = true;
        System.Web.UI.ScriptManager.RegisterStartupScript(contenedor, contenedor.GetType(), Guid.NewGuid().ToString(), script, true);
    }

    protected void gwProductos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void refrescarGrillaProductos()
    {
        DataTable tabla = GestorProducciones.ObtenerProductosParaProduccion();
        gwProductos.DataSource = tabla;
        gwProductos.DataBind();
    }

    private void crearTablaDetalle()
    {
        tablaDetalle = new DataTable();
        if (tablaDetalle.Columns.Count == 0)
        {
            tablaDetalle.Columns.Add("id_Producto", typeof(string));
            tablaDetalle.Columns.Add("nombre", typeof(string));
            tablaDetalle.Columns.Add("cantidad", typeof(string));
            tablaDetalle.Columns.Add("precio", typeof(string));
            tablaDetalle.Columns.Add("subtotal", typeof(string));
        }
        Session["tablaDetalle"] = tablaDetalle;
    }

    protected void btnSeleccionarProducto_Click(object sender, EventArgs e)
    {
        if (gwProductos.SelectedValue == null)
        {
            mostrarLblMensaje();
            lblMensaje.Text = "Primero Seleccione algún producto";
            return;
        }
        ocultarLblMensaje();

        int idProducto = (int)gwProductos.SelectedValue;

        try
        {
            IngelunEntidades.Producto producto = GestorProductos.buscarPorId(idProducto);
            Session["idProducto"] = producto.id_Producto;
            txtPrecio.Text = producto.precio.ToString();
            txtProducto.Text = producto.nombre;

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    private void mostrarLblMensaje()
    {
        lblMensaje.Visible = true;
    }

    private void ocultarLblMensaje()
    {
        lblMensaje.Visible = false;
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