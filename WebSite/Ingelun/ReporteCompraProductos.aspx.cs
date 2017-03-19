using IngelunEntidades;
using IngelunNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReporteCompraProductos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            refrescarCombo();
            ocultarLblMensaje();
            Session["reporteConFiltros"] = "NO";
            Session["dataSourceGrilla"] = null;
            ViewState["gwReporteVenta"] = "v.fecha";
            string orden = ViewState["gwReporteVenta"].ToString();
            List<VentaProducto> listaReporte = GestorVentas.getReporteVentaSinFiltro(orden);
            refrescarGrilla(listaReporte);
        }


    }

    private void ocultarLblMensaje()
    {
        lblMensaje.Visible = false;
    }

    private void mostrarLblMensaje()
    {
        lblMensaje.Visible = true;
    }

    public void refrescarCombo()
    {
        ddlCliente.DataTextField = "id_Cliente";
        ddlCliente.DataTextField = "Nombre";

        ddlCliente.DataSource = GestorVentas.ObtenerClientes();

        ddlCliente.DataBind();

        ddlCliente.Items.Insert(0, new ListItem("Elija un Cliente", "0"));
    }

    public void refrescarGrilla(List<VentaProducto> listaVentaReporte)
    {
        gwReporteVenta.DataSource = listaVentaReporte;
        gwReporteVenta.DataBind();
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (txtFecha.Text.CompareTo("") == 0 && txtCantidad.Text.CompareTo("") == 0 && ddlCliente.SelectedIndex == 0)
        {
            generarReporteSinFiltro();
        }
        else
        {
            Session["reporteConFiltros"] = "SI";
            ocultarLblMensaje();
            List<VentaProducto> listaVentaReporte = new List<VentaProducto>();
            if (txtFecha.Text.CompareTo("") == 0 && txtCantidad.Text.CompareTo("") == 0 && ddlCliente.SelectedIndex == 0)
            {
                //mostrarLblMensaje();
                MostrarMensajeCheto("Ingrese alguno de los parametros de consulta");
                //lblMensaje.Text = "Ingrese alguno de los parametros de consulta";

            }

            
            DateTime fechaReporte = new DateTime();
            int montoParcial = 0;
            string nombreClienteReporte = "";

            if (txtFecha.Text.CompareTo("") != 0)
            {
                
                fechaReporte = DateTime.Parse(txtFecha.Text);
            }

            if (txtCantidad.Text.CompareTo("") != 0)
            {

                montoParcial = int.Parse(txtCantidad.Text);
            }

            if (ddlCliente.SelectedIndex > 0)
            {
                
                nombreClienteReporte = ddlCliente.SelectedItem.Value;
            }


            listaVentaReporte = GestorVentas.getReporteVentasXFechaXCantidadXNombreCliente(fechaReporte, montoParcial, nombreClienteReporte);
            Session["dataSourceGrilla"] = listaVentaReporte;

            refrescarGrilla(listaVentaReporte);
        }

    }

    private void cargarGrillaOrdenada(string orden)
    {
        List<VentaProducto> listaReporte = (List<VentaProducto>)Session["dataSourceGrilla"];
        List<VentaProducto> listaOrdenada = new List<VentaProducto>();

        if (orden.CompareTo("v.fecha") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.fechaVenta).ToList();
        }
        if (orden.CompareTo("c.mail") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.mailCliente).ToList();
        }

        if (orden.CompareTo("c.nombre") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.nombreCliente).ToList();
        }

        if (orden.CompareTo("p.nombre") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.nombreProducto).ToList();
        }

        if (orden.CompareTo("pxv.cantidad") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.cantidad).ToList();
        }

        if (orden.CompareTo("v.montoTotal") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.montoTotal).ToList();
        }

        if (orden.CompareTo("pxv.montoParcial") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.montoParcial).ToList();
        }

        refrescarGrilla(listaOrdenada);
    }


    private void generarReporteSinFiltro()
    {
        Session["reporteConFiltros"] = "NO";
        string orden = ViewState["gwReporteVenta"].ToString();
        limpiarCampos();
        List<VentaProducto> listaReporte = GestorVentas.getReporteVentaSinFiltro(orden);
        refrescarGrilla(listaReporte);
        ocultarLblMensaje();
    }
    protected void gwReporteVenta_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gwReporteVenta.PageIndex = e.NewPageIndex;
        string orden = ViewState["gwReporteVenta"].ToString();
        if (Session["reporteConFiltros"].ToString().CompareTo("NO") == 0)
        {
            List<VentaProducto> listaReporte = GestorVentas.getReporteVentaSinFiltro(orden);
            refrescarGrilla(listaReporte);
        }
        else
        {
            cargarGrillaOrdenada(orden);
        }


    }
    protected void gwReporteVenta_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["gwReporteVenta"] = e.SortExpression;
        string orden = ViewState["gwReporteVenta"].ToString();
        if (Session["reporteConFiltros"].ToString().CompareTo("NO") == 0)
        {
            List<VentaProducto> listaReporte = GestorVentas.getReporteVentaSinFiltro(orden);
            refrescarGrilla(listaReporte);
        }
        else
        {
            cargarGrillaOrdenada(orden);
        }

    }
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        refrescarCombo();
        ocultarLblMensaje();
        ViewState["gwReporteVenta"] = "v.fecha";
        string orden = ViewState["gwReporteVenta"].ToString();
        List<VentaProducto> listaReporte = new List<VentaProducto>();
        refrescarGrilla(listaReporte);
        limpiarCampos();
        generarReporteSinFiltro();

    }

    private void limpiarCampos()
    {
        txtFecha.Text = "";
        txtCantidad.Text = "";
        ddlCliente.SelectedIndex = 0;
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