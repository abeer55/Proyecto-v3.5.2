using IngelunEntidades;
using IngelunNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Productos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            refrescarComboBox();
            pnlAgregar.Visible = false;
            pnlInsumosDisponibles.Visible = false;
            ViewState["GrdProductosOrden"] = "Producto.nombre";
            grdProductos.AllowPaging = true;
            grdProductos.AllowSorting = true;
            grdProductos.PageSize = 5;
            refrescarGrilla();
            guardarCambiosInsumo.Enabled = true;
            lblAccion.Text = "";
            lblMensaje.Text = "";

        }

    }

    public void refrescarComboBox()
    {
        comboBoxTipoProducto.DataTextField = "nombre";
        comboBoxTipoProducto.DataValueField = "id_tipo_producto";
        comboBoxTipoProducto.DataSource = GestorProductos.ObtenerTodas();
        comboBoxTipoProducto.DataBind();
        comboBoxTipoProducto.Items.Insert(0, new ListItem("Elija un tipo de Producto", "0"));
    }

    public void refrescarGrilla()
    {
        string orden = ViewState["GrdProductosOrden"].ToString();
        grdProductos.DataSource = GestorProductos.BuscarPorNombre(txtNombreBuscar.Text, orden);
        grdProductos.DataBind();
        grdProductos.SelectedIndex = -1;
    }

    public void cargarGrillaInsumosXProducto()
    {

    }

    protected void grdProductos_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["GrdProductosOrden"] = e.SortExpression;
        refrescarGrilla();
    }

    protected void grdProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProductos.PageIndex = e.NewPageIndex;
        refrescarGrilla();

    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        pnlConsulta.Visible = false;
        pnlAgregar.Visible = true;
        btnConfAgregar.Visible = true;
        agregarInsumo.Visible = true;
        guardarCambiosInsumo.Visible = true;
        pnlInsumosDisponibles.Visible = false;
        List<DTOInsumoxProducto> lista = new List<DTOInsumoxProducto>();
        grdInsumoXProducto.DataSource = lista;
        grdInsumoXProducto.DataBind();
        Session["ListaInsumosXProducto"] = lista;
        txtNombre.Text = "";
        txtPrecio.Text = "";
        comboBoxTipoProducto.SelectedIndex = -1;
        txtFechaConstruccion.Text = "";

        try
        {
            txtId.Text = "" + GestorProductos.obtenerSiguienteID();
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

    protected void grdProductos_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int idProducto = (int)grdProductos.SelectedValue;
    }

    protected void agregarInsumo_Click(object sender, EventArgs e)
    {
        grdInsumosDisponibles.DataSource = GestorInsumos.BuscarPorNombre("", "Id_Insumo");
        grdInsumosDisponibles.DataBind();
        grdInsumosDisponibles.SelectedIndex = -1;
        pnlInsumosDisponibles.Visible = true;
        guardarCambiosInsumo.Enabled = true;
        guardarCambiosInsumo.Visible = true;
        agregarInsumo.Visible = false;
    }

    protected void btnBuscarPorNombre_Click(object sender, EventArgs e)
    {
        refrescarGrilla();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        pnlAgregar.Visible = false;
        pnlConsulta.Visible = true;
        pnlInsumosDisponibles.Visible = false;
        refrescarGrilla();
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {

        if (grdProductos.SelectedValue == null)
        {
            MostrarMensajeCheto("Seleccione una Fila antes de continuar");
            return;
        }
        pnlConsulta.Visible = false;
        pnlAgregar.Visible = true;
        agregarInsumo.Visible = false;
        guardarCambiosInsumo.Visible = true;
        int idProducto = (int)grdProductos.SelectedValue;
        try
        {
            IngelunEntidades.Producto Pro = GestorProductos.buscarPorId(idProducto);
            txtId.Text = Pro.id_Producto.ToString();
            txtNombre.Text = Pro.nombre;
            txtPrecio.Text = Pro.precio.ToString();
            txtFechaConstruccion.Text = Pro.fecha_Construccion.ToString();
            comboBoxTipoProducto.SelectedIndex = (int)Pro.id_Tipo_Producto;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        comboBoxTipoProducto.Visible = true;

        lblAccion.Text = " Consultando";
        btnConfElim.Visible = false;
        btnConfAgregar.Visible = false;
        btnConfModificar.Visible = false;
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        if (grdProductos.SelectedValue == null)
        {
            MostrarMensajeCheto("Seleccione una Fila antes de continuar");
            return;
        }
        int idProducto = (int)grdProductos.SelectedValue;
        try
        {
            IngelunEntidades.Producto Pro = GestorProductos.buscarPorId(idProducto);
            txtId.Text = Pro.id_Producto.ToString();
            txtNombre.Text = Pro.nombre;
            txtPrecio.Text = Pro.precio.ToString();

            if (Pro.fecha_Construccion != null)
            {
                txtFechaConstruccion.Text = Pro.fecha_Construccion.ToString().Substring(0, 10);
            }
            //txtFechaConstruccion.Text = Pro.fecha_Construccion.ToString();
            comboBoxTipoProducto.SelectedIndex = (int)Pro.id_Tipo_Producto;
            List<DTOInsumoxProducto> lista = GestorProductos.BuscarInsumosPorProducto(idProducto);
            Session["ListaInsumosXProducto"] = lista;
            grdInsumoXProducto.DataSource = lista;
            grdInsumoXProducto.DataBind();

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        comboBoxTipoProducto.Visible = true;
        pnlConsulta.Visible = false;
        pnlAgregar.Visible = true;
        lblAccion.Text = " Editando";
        btnConfElim.Visible = false;
        btnConfAgregar.Visible = false;
        btnConfModificar.Visible = true;
        btnConfModificar.Visible = true;
        agregarInsumo.Visible = true;
        guardarCambiosInsumo.Visible = true;
    }

    protected void btnConfModificar_Click(object sender, EventArgs e)
    {
        pnlAgregar.Visible = false;
        pnlConsulta.Visible = true;
        double anDouble;
        anDouble = Convert.ToDouble(txtPrecio.Text);
        DateTime nDate;
        nDate = Convert.ToDateTime(txtFechaConstruccion.Text);
        int anInteger3;
        anInteger3 = Convert.ToInt32(txtId.Text);

        try
        {
            GestorProductos.editarProducto(anInteger3, txtNombre.Text, anDouble, comboBoxTipoProducto.SelectedIndex, DateTime.Parse(txtFechaConstruccion.Text));
        }
        catch (Exception ex)
        {
            //   lblMensaje.Text = ex.Message;
            MostrarMensajeCheto("No se puede modificar el registro por que ya existe un producto con ese nombre, por favor intente otro");


        }

        MostrarMensajeCheto("Registro Modificado Correctamente");
        refrescarGrilla();
        //txtDescripcion.Text = "";
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {

        if (grdProductos.SelectedValue == null)
        {
            MostrarMensajeCheto("Seleccione una Fila antes de continuar");
            return;
        }
        pnlConsulta.Visible = true;
        pnlAgregar.Visible = true;
        btnConfElim.Visible = true;
        btnConfModificar.Visible = false;
        btnConfAgregar.Visible = false;
        agregarInsumo.Visible = false;
        guardarCambiosInsumo.Visible = true;

        lblAccion.Text = " Eliminando";
        int idProducto = (int)grdProductos.SelectedValue;
        try
        {
            IngelunEntidades.Producto Pro = GestorProductos.buscarPorId(idProducto);
            txtId.Text = Pro.id_Producto.ToString();
            txtNombre.Text = Pro.nombre;
            txtPrecio.Text = Pro.precio.ToString();
            txtFechaConstruccion.Text = Pro.fecha_Construccion.ToString();
            comboBoxTipoProducto.SelectedIndex = (int)Pro.id_Tipo_Producto;

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

        btnConfElim.Visible = true;
        pnlConsulta.Visible = false;
        pnlAgregar.Visible = true;


    }

    protected void btnConfElim_Click(object sender, EventArgs e)
    {
        if (grdProductos.SelectedValue == null)
        {
            lblMensaje.Visible = true;

            return;
        }

        int idProducto = (int)grdProductos.SelectedValue;
        try
        {
            GestorProductos.eliminarProducto(idProducto);

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }


        refrescarGrilla();
        pnlConsulta.Visible = true;
        pnlAgregar.Visible = false;
        MostrarMensajeCheto("Registro Eliminado Satisfactoriamente");
        btnConfElim.Visible = false;
        btnConfAgregar.Visible = true;
    }

    protected void btnConfAgregar_Click(object sender, EventArgs e)
    {
        pnlAgregar.Visible = false;
        pnlConsulta.Visible = true;

        double Precio;
        Precio = Double.Parse(txtPrecio.Text);
        DateTime fechaConstruccion;
        fechaConstruccion = DateTime.Parse(txtFechaConstruccion.Text);

        try
        {
            GestorProductos.agregarProducto(txtNombre.Text, Precio, comboBoxTipoProducto.SelectedIndex, fechaConstruccion);
        }
        catch (Exception ex)
        {
            //    lblMensaje.Text = ex.Message;
            MostrarMensajeCheto("No se puede agregar un nuevo registro por que ya existe un producto con ese nombre, por favor intente otro");

        }
        MostrarMensajeCheto("Registro agregado Satisfactoriamente");
        refrescarGrilla();
        txtNombre.Text = "";

    }

    protected void grdInsumosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idInsumo = int.Parse(grdInsumosDisponibles.SelectedRow.Cells[1].Text);
        Insumo insumo = GestorInsumos.buscarPorId(idInsumo);
        DTOInsumoxProducto insumoDTO = new DTOInsumoxProducto();
        insumoDTO.id_Insumo = insumo.id_Insumo;
        insumoDTO.nombre = insumo.nombre;
        insumoDTO.cantidad = 1;
        if (Session["ListaInsumosXProducto"] != null)
        {
            List<DTOInsumoxProducto> lista = (List<DTOInsumoxProducto>)Session["ListaInsumosXProducto"];
            lista = GestorProductos.agregarInsumo(insumoDTO, lista);
            grdInsumoXProducto.DataSource = lista;
            grdInsumoXProducto.DataBind();
            Session["ListaInsumosXProducto"] = lista;
        }
        grdInsumosDisponibles.SelectedIndex = -1;
        guardarCambiosInsumo.Enabled = true;

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

    protected void txtPrecio_TextChanged(object sender, EventArgs e)
    {

    }


    protected void guardarCambiosInsumo_Click(object sender, EventArgs e)
    {
        GestorProductos.borrarTodosLosInsumosXProducto(int.Parse(txtId.Text));
        foreach (GridViewRow row in grdInsumoXProducto.Rows)
        {
            TextBox cantidad = (TextBox)row.FindControl("cantidad");
            int r = 3;
            if (!int.TryParse(cantidad.Text, out r))
            {
                cantidad.Text = "1";
            }
            GestorProductos.agregarInsumoPorProducto(int.Parse(row.Cells[0].Text), int.Parse(txtId.Text), int.Parse(cantidad.Text));
        }
        if (grdInsumoXProducto.Rows.Count == 0)
            GestorProductos.borrarTodosLosInsumosXProducto(int.Parse(txtId.Text));

        List<DTOInsumoxProducto> lista = GestorProductos.BuscarInsumosPorProducto(int.Parse(txtId.Text));
        Session["ListaInsumosXProducto"] = lista;
        grdInsumoXProducto.DataSource = lista;
        grdInsumoXProducto.DataBind();
        guardarCambiosInsumo.Enabled = true;
    }

    protected void grdInsumoXProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idInsumo = int.Parse(grdInsumoXProducto.SelectedRow.Cells[0].Text);
        Insumo insumo = GestorInsumos.buscarPorId(idInsumo);
        DTOInsumoxProducto insumoDTO = new DTOInsumoxProducto();
        insumoDTO.id_Insumo = insumo.id_Insumo;
        insumoDTO.nombre = insumo.nombre;
        insumoDTO.cantidad = 1;
        if (Session["ListaInsumosXProducto"] != null)
        {
            List<DTOInsumoxProducto> lista = (List<DTOInsumoxProducto>)Session["ListaInsumosXProducto"];
            lista = GestorProductos.quitarInsumo(insumoDTO, lista);
            grdInsumoXProducto.DataSource = lista;
            grdInsumoXProducto.DataBind();
            Session["ListaInsumosXProducto"] = lista;
        }
        grdInsumoXProducto.SelectedIndex = -1;
        guardarCambiosInsumo.Enabled = true;
    }
}