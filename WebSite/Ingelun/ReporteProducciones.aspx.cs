using IngelunEntidades;
using IngelunNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReporteProducciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            refrescarCombo();
            ocultarLblMensaje();
            Session["dataSourceGrilla"] = null;
            Session["reporteConFiltros"] = "NO";
            ViewState["gwReporteProduccion"] = "p.fecha";
            string orden = ViewState["gwReporteProduccion"].ToString();
            List<DTOProduccionReporte> listaReporte = GestorProducciones.getReporteProduccionSinFiltro(orden);
            Session["dataSourceGrilla"] = listaReporte;
            refrescarGrilla(listaReporte);
        }
    }

    protected void gwReporteProduccion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gwReporteCompra_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gwReporteProduccion.PageIndex = e.NewPageIndex;
        List<DTOProduccionReporte> listaReporte = (List<DTOProduccionReporte>) Session["dataSourceGrilla"];
        refrescarGrilla(listaReporte);

    }

    protected void gwReporteCompra_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["gwReporteProduccion"] = e.SortExpression;
        string orden = ViewState["gwReporteProduccion"].ToString();
        if (Session["reporteConFiltros"].ToString().CompareTo("NO") == 0)
        {
            List<DTOProduccionReporte> listaReporte = GestorProducciones.getReporteProduccionSinFiltro(orden);
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
        ViewState["gwReporteProduccion"] = "p.fecha";
        string orden = ViewState["gwReporteProduccion"].ToString();
        List<DTOProduccionReporte> listaReporte = new List<DTOProduccionReporte>();
        listaReporte = GestorProducciones.getReporteProduccionSinFiltro(orden);
        refrescarGrilla(listaReporte);
        limpiarCampos();
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (txtFecha.Text.CompareTo("") == 0 && txtNombreProducto.Text.CompareTo("") == 0 && ddlTipoProducto.SelectedIndex == 0)
        {
            generarReporteSinFiltro();
        }
        else
        {
            Session["reporteConFiltros"] = "SI";
            ocultarLblMensaje();
            List<DTOProduccionReporte> listaProduccionReporte = new List<DTOProduccionReporte>();
            if (txtFecha.Text.CompareTo("") == 0 && txtNombreProducto.Text.CompareTo("") == 0 && ddlTipoProducto.SelectedIndex == 0)
            {
                mostrarLblMensaje();
                lblMensaje.Text = "Ingrese alguno de los parametros de consulta";

            }

            bool fecha = false;
            bool nombrePr = false;
            bool tipoPr = false;

            DateTime fechaReporte = new DateTime();
            String nombreProductoReporte = "";
            int tipoProductoReporte = 0;

            if (txtFecha.Text.CompareTo("") != 0)
            {
                fecha = true;
                fechaReporte = DateTime.Parse(txtFecha.Text);
            }

            if (txtNombreProducto.Text.CompareTo("") != 0)
            {
                nombrePr = true;
                nombreProductoReporte = txtNombreProducto.Text;
            }

            if (ddlTipoProducto.SelectedIndex > 0)
            {
                tipoPr = true;
                tipoProductoReporte = int.Parse(ddlTipoProducto.SelectedItem.Value.ToString());
            }


            if (fecha && nombrePr && tipoPr)
            {
                listaProduccionReporte = GestorProducciones.getReporteProduccionXFechaXTipoProductoXNombreProducto(fechaReporte, tipoProductoReporte, nombreProductoReporte);
                Session["dataSourceGrilla"] = listaProduccionReporte;
            }

            if (fecha && tipoPr == false && nombrePr == false)
            {
                listaProduccionReporte = GestorProducciones.getReporteProduccionXFecha(fechaReporte);
                Session["dataSourceGrilla"] = listaProduccionReporte;
            }

            if (fecha && tipoPr && nombrePr == false)
            {
                listaProduccionReporte = GestorProducciones.getReporteProduccionXFechaXTipoProducto(fechaReporte, tipoProductoReporte);
                Session["dataSourceGrilla"] = listaProduccionReporte;
            }

            if (fecha == false && tipoPr && nombrePr == false)
            {
                listaProduccionReporte = GestorProducciones.getReporteProduccionXTipoProducto(tipoProductoReporte);
                Session["dataSourceGrilla"] = listaProduccionReporte;
            }

            if (fecha && tipoPr == false && nombrePr)
            {
                listaProduccionReporte = GestorProducciones.getReporteProduccionXFechaXNombreProducto(fechaReporte, nombreProductoReporte);
                Session["dataSourceGrilla"] = listaProduccionReporte;
            }

            if (fecha == false && tipoPr && nombrePr)
            {
                listaProduccionReporte = GestorProducciones.getReporteProduccionXTipoProductoXNombreProducto(tipoProductoReporte, nombreProductoReporte);
                Session["dataSourceGrilla"] = listaProduccionReporte;
            }

            if (fecha == false && tipoPr == false && nombrePr)
            {
                listaProduccionReporte = GestorProducciones.getReporteProduccionXNombreProducto(nombreProductoReporte);
                Session["dataSourceGrilla"] = listaProduccionReporte;
            }

            refrescarGrilla(listaProduccionReporte);
        }
    }

    public void refrescarGrilla(List<DTOProduccionReporte> listaProduccionReporte)
    {
        gwReporteProduccion.DataSource = listaProduccionReporte;
        gwReporteProduccion.DataBind();
    }

    private void generarReporteSinFiltro()
    {
        Session["reporteConFiltros"] = "NO";
        string orden = ViewState["gwReporteProduccion"].ToString();
        limpiarCampos();
        List<DTOProduccionReporte> listaReporte = GestorProducciones.getReporteProduccionSinFiltro(orden);
        refrescarGrilla(listaReporte);
        ocultarLblMensaje();
    }

    private void limpiarCampos()
    {
        txtFecha.Text = "";
        txtNombreProducto.Text = "";
        ddlTipoProducto.SelectedIndex = 0;
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
        ddlTipoProducto.DataValueField = "id_Tipo_Producto";
        ddlTipoProducto.DataTextField = "Nombre";

        ddlTipoProducto.DataSource = GestorProductos.ObtenerTodas();

        ddlTipoProducto.DataBind();

        ddlTipoProducto.Items.Insert(0, new ListItem("Elija un Tipo Producto", "0"));
    }

    private void cargarGrillaOrdenada(string orden)
    {
        List<DTOProduccionReporte> listaReporte = (List<DTOProduccionReporte>)Session["dataSourceGrilla"];
        List<DTOProduccionReporte> listaOrdenada = new List<DTOProduccionReporte>();

        if (orden.CompareTo("p.fecha") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.fechaProduccion).ToList();
        }

        if (orden.CompareTo("pr.nombre") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.nombreProducto).ToList();
        }

        if (orden.CompareTo("tp.nombre") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.nombreTipoProducto).ToList();
        }

        if (orden.CompareTo("pxp.cantidad") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.cantidad).ToList();
        }

        if (orden.CompareTo("p.id_Produccion") == 0)
        {
            listaOrdenada = listaReporte.OrderBy(o => o.id_Produccion).ToList();
        }

        refrescarGrilla(listaOrdenada);
    }


}