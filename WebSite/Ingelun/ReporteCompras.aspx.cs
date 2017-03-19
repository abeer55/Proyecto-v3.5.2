using IngelunEntidades;
using IngelunNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReporteCompras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           refrescarCombo();
           ocultarLblMensaje();
           Session["dataSourceGrilla"] = null;
           ViewState["gwReporteCompra"] = "c.fecha";
           Session["reporteConFiltros"] = "NO";
           string orden = ViewState["gwReporteCompra"].ToString();
           List<CompraReporte> listaReporte = GestorProveedores.getReporteCompraSinFiltro(orden);
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
        ddlProveedor.DataTextField = "id_Proveedor";
        ddlProveedor.DataTextField = "Nombre";

        ddlProveedor.DataSource = GestorProveedores.ObtenerProveedores();

        ddlProveedor.DataBind();

        ddlProveedor.Items.Insert(0, new ListItem("Elija un Proveedor", "0"));
    }

    public void refrescarGrilla(List<CompraReporte> listaCompraReporte)
    {
        gwReporteCompra.DataSource=listaCompraReporte;
        gwReporteCompra.DataBind();
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if(txtFecha.Text.CompareTo("")==0 && txtCantidad.Text.CompareTo("")==0 && ddlProveedor.SelectedIndex==0)
        {
            generarReporteSinFiltro();
        }
        else
        {
            Session["reporteConFiltros"] = "SI";
            ocultarLblMensaje();
            List<CompraReporte> listaCompraReporte = new List<CompraReporte>();
            if (txtFecha.Text.CompareTo("") == 0 && txtCantidad.Text.CompareTo("") == 0 && ddlProveedor.SelectedIndex == 0)
            {
                mostrarLblMensaje();
                lblMensaje.Text = "Ingrese alguno de los parametros de consulta";

            }

            DateTime fechaReporte = new DateTime();
            int cantidadReporte = 0;
            string nombreProveedorReporte = "";

            if (txtFecha.Text.CompareTo("") != 0)
            {
                fechaReporte = DateTime.Parse(txtFecha.Text);
            }

            if (txtCantidad.Text.CompareTo("") != 0)
            {
                cantidadReporte = int.Parse(txtCantidad.Text);
            }

            if (ddlProveedor.SelectedIndex > 0)
            {
                nombreProveedorReporte = ddlProveedor.SelectedItem.Value;
            }
        
                listaCompraReporte = GestorProveedores.getReporteCompraXFechaXCantidadXNombreProveedor(fechaReporte, cantidadReporte, nombreProveedorReporte);
                Session["dataSourceGrilla"] = listaCompraReporte;

            refrescarGrilla(listaCompraReporte);
        }
        
    }

    private void cargarGrillaOrdenada(string orden)
    {
        List<CompraReporte> listaReporte= (List<CompraReporte>)Session["dataSourceGrilla"];
        List<CompraReporte> listaOrdenada = new List<CompraReporte>();

        if (orden.CompareTo("c.fecha") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.fechaCompra).ToList();
        }

        if (orden.CompareTo("p.nombre") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.nombreProveedor).ToList();
        }

        if (orden.CompareTo("i.nombre") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.nombreInsumo).ToList();
        }

        if (orden.CompareTo("ixc.cantidad") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.cantidad).ToList();
        }

        if (orden.CompareTo("c.montoTotal") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.montoTotal).ToList();
        }

        if (orden.CompareTo("ixc.monto_Parcial") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.montoParcial).ToList();
        }

        refrescarGrilla(listaOrdenada);
    }


    private void generarReporteSinFiltro()
    {
        Session["reporteConFiltros"] = "NO";
        string orden = ViewState["gwReporteCompra"].ToString();
        limpiarCampos();
        List<CompraReporte> listaReporte = GestorProveedores.getReporteCompraSinFiltro(orden);
        refrescarGrilla(listaReporte);
        ocultarLblMensaje();
    }
    protected void gwReporteCompra_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gwReporteCompra.PageIndex = e.NewPageIndex;
        string orden = ViewState["gwReporteCompra"].ToString();
        if (Session["reporteConFiltros"].ToString().CompareTo("NO") == 0)
        {
            List<CompraReporte> listaReporte = GestorProveedores.getReporteCompraSinFiltro(orden);
            refrescarGrilla(listaReporte);
        }
        else
        {
            cargarGrillaOrdenada(orden);
        }
        
        
    }
    protected void gwReporteCompra_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["gwReporteCompra"] = e.SortExpression;
        string orden = ViewState["gwReporteCompra"].ToString();
        if (Session["reporteConFiltros"].ToString().CompareTo("NO") == 0)
        {
            List<CompraReporte> listaReporte = GestorProveedores.getReporteCompraSinFiltro(orden);
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
        ViewState["gwReporteCompra"] = "c.fecha";
        string orden = ViewState["gwReporteCompra"].ToString();
        List<CompraReporte> listaReporte = new List<CompraReporte>();
        refrescarGrilla(listaReporte);
        limpiarCampos();
       
    }

    private void limpiarCampos()
    {
        txtFecha.Text = "";
        txtCantidad.Text = "";
        ddlProveedor.SelectedIndex = 0;
    }
}