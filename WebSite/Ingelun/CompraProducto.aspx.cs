using IngelunNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class CompraProducto : System.Web.UI.Page
{
    DataTable tablaDetalle;
    double totalCompra = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtFecha.Text = DateTime.Today.ToShortDateString();
            refrescarCombo();
            refrescarGrillaInsumos();
            Session["tablaDetalle"] = null;
            Session["totalCompra"] = 0;
            Session["id_Producto"] = 0;
            crearTablaDetalle();

        }

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
        DataTable tabla = GestorTransaccionVenta.ObtenerProductosParaComprar();
        gwProducto.DataSource = tabla;
        gwProducto.DataBind();
    }
    public void refrescarGrillaDetalleCompra(DataTable tablaDetalle)
    {
        gwDetalleCompra.DataSource = tablaDetalle;
        gwDetalleCompra.DataBind();
    }

    public void refrescarCombo()
    {
        ddlCliente.DataTextField = "id_Cliente";
        ddlCliente.DataTextField = "Nombre";

         ddlCliente.DataSource = GestorVentas.ObtenerClientes();
        //ddlCliente.DataSource = GestorProveedores.ObtenerProveedores();

        ddlCliente.DataBind();

        ddlCliente.Items.Insert(0, new ListItem("Elija un Cliente", "0"));
    }
    protected void btnSeleccionarProducto_Click(object sender, EventArgs e)
    {
        if (gwProducto.SelectedValue == null)
        {
            mostrarLblMensaje();
            lblMensaje.Text = "Primero Seleccione algún Producto";
            return;
        }
        ocultarLblMensaje();

        int idProducto = (int)gwProducto.SelectedValue;

        try
        {
            IngelunEntidades.Producto producto = GestorProductos.buscarPorId(idProducto);
            Session["id_Producto"] = producto.id_Producto;
            txtPrecio.Text = producto.precio.ToString();
            txtProducto.Text = producto.nombre;

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    public void calcularTotalCompra(int subTotal)
    {
        totalCompra = int.Parse(Session["totalCompra"].ToString());
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
            lblMensajeAgregarDetalle.Text = "Error al ingresar cantidad solicitada";
            lblMensajeAgregarDetalle.Visible = true;
        }

        if (verificarStock((int)Session["id_Producto"], int.Parse(txtCantidad.Text)))
        {
              DataTable tabla = (DataTable)Session["tablaDetalle"];
              int subtotal = int.Parse(txtPrecio.Text) * cantidad;
              tabla.Rows.Add(Session["id_Producto"].ToString(), txtProducto.Text, txtCantidad.Text, txtPrecio.Text, subtotal.ToString());

              Session["tablaDetalle"] = tabla;

              refrescarGrillaDetalleCompra(tabla);
              calcularTotalCompra(subtotal);
              limpiarCamposInsumo();
        }
        else
        {
            string mensaje= "Las unidades solicitadas superan al stock actual";
            MostrarMensajeCheto("Stock Insuficiente", mensaje, lblAyuda, updPanelAyuda);
        }

      

    }

    private bool verificarStock(int idProducto, int cantidadPedida)
    {
        int stockActual = GestorProductos.getStock(idProducto);
        if (stockActual >= cantidadPedida)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void limpiarCamposInsumo()
    {
        txtCantidad.Text = "";
        txtPrecio.Text = "";
        txtProducto.Text = "";
    }
    protected void btnConfirmarCompra_Click(object sender, EventArgs e)
    {
        int idCliente = 0;
        if (ddlCliente.SelectedIndex > 0)
        {
            lblMensajeConfirmarCompra.Visible = false;
            idCliente = GestorTransaccionVenta.BuscarPorNombre(ddlCliente.SelectedItem.Value, "nombre")[0].id_Cliente;
            //idCliente = GestorProveedores.BuscarPorNombre(ddlCliente.SelectedItem.Value, "p.nombre")[0].id_Proveedor;
        }
        else
        {
            lblMensajeConfirmarCompra.Visible = true;
            lblMensajeConfirmarCompra.Text = "Seleccione el Cliente para imputarle la compra";
            return;
        }

        if (GestorTransaccionVenta.generarVentaProducto((DataTable)Session["tablaDetalle"], int.Parse(Session["totalCompra"].ToString()), idCliente, DateTime.Parse(txtFecha.Text)))
        {
            MostrarMensajeCheto("Compra registrada", "Se ha registrado la compra con éxito", lblAyuda, updPanelAyuda);
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
        txtPrecio.Text = "";
        txtProducto.Text = "";
        ddlCliente.SelectedIndex = 0;
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