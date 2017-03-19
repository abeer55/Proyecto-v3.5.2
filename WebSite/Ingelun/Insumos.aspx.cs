using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IngelunNegocio;
using IngelunEntidades;
using System.Text;

public partial class Insumos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblMensaje.Visible = false;
            refrescarComboBox();
            pnlAgregar.Visible = false;
            ViewState["GwDatosOrden"] = "nombre";
            gwDatos.AllowPaging = true;
            gwDatos.AllowSorting = true;
            gwDatos.PageSize = 4;
            refrescarGrilla();

        }
        lblMensaje.Visible = false;
    }



    protected void valComboBox_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (args.Value == "0")
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }

    }

    public void refrescarComboBox()
    {
        comboBoxTipoInsumo.DataTextField = "nombreInsumo";
        comboBoxTipoInsumo.DataValueField = "id_tipo_insumo";
        comboBoxTipoInsumo.DataSource = GestorInsumos.ObtenerTodas();
        comboBoxTipoInsumo.DataBind();
        comboBoxTipoInsumo.Items.Insert(0, new ListItem("Elija un tipo de Insumo", "0"));
    }

    protected void gwDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idInsumo = (int)gwDatos.SelectedValue;
    }

    public void refrescarGrilla()
    {
        string orden = ViewState["GwDatosOrden"].ToString();
        gwDatos.DataSource = GestorInsumos.BuscarPorNombre(txtNombreBuscar.Text, orden);
        gwDatos.DataBind();
        gwDatos.SelectedIndex = -1;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {

        refrescarGrilla();

    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        btnConfAgregar.Visible = true;
        pnlConsulta.Visible = false;
        pnlAgregar.Visible = true;
        txtCosto.Text = "";
        txtDescripcion.Text = "";
        txtVolumen.Text = "";
        txtNumeroSerie.Text = "";
        comboBoxTipoInsumo.SelectedIndex = -1;
        chBoxOrigen.Checked = false;

        try
        {
            txtIdInsumo.Text = "" + GestorInsumos.obtenerSiguienteID();
        }
        catch (Exception ex)
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = ex.Message;
        }

        txtIdInsumo.Enabled = false;
        txtCosto.Enabled = true;
        txtVolumen.Enabled = true;
        txtDescripcion.Enabled = true;
        txtNumeroSerie.Enabled = true;
        comboBoxTipoInsumo.Enabled = true;
        chBoxOrigen.Enabled = true;
        btnConfElim.Visible = false;
        btnAgregar.Visible = true;
        btnConfModificar.Visible = false;

    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {

        pnlAgregar.Visible = false;
        pnlConsulta.Visible = true;
        refrescarGrilla();
        txtCosto.Text = "";
        txtDescripcion.Text = "";
        txtVolumen.Text = "";
        txtNumeroSerie.Text = "";
        comboBoxTipoInsumo.SelectedIndex = -1;


    }
    protected void btnConfAgregar_Click(object sender, EventArgs e)
    {
        pnlAgregar.Visible = false;
        pnlConsulta.Visible = true;

        try
        {
            GestorInsumos.agregarInsumo(txtDescripcion.Text, Double.Parse(txtCosto.Text), Double.Parse(txtVolumen.Text), comboBoxTipoInsumo.SelectedIndex, chBoxOrigen.Checked, int.Parse(txtNumeroSerie.Text));
        }
        catch (Exception ex)
        {
            MostrarMensajeCheto("No se puede agregar un nuevo insumo por que el numero de serie: " + txtNumeroSerie.Text + ", ya existe, por favor intente otro");
            pnlAgregar.Visible = true;
            pnlConsulta.Visible = false;
        }

        MostrarMensajeCheto("Insumo agregado Satisfactoriamente");

        refrescarGrilla();


    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {

        if (gwDatos.SelectedValue == null)
        {
            MostrarMensajeCheto("Seleccione una Fila antes de continuar ");

            return;
        }
        pnlConsulta.Visible = false;
        pnlAgregar.Visible = true;
        btnConfElim.Visible = true;
        btnConfModificar.Visible = false;
        btnConfAgregar.Visible = false;


        int idInsumo = (int)gwDatos.SelectedValue;
        try
        {
            IngelunEntidades.Insumo Ins = GestorInsumos.buscarPorId(idInsumo);
            txtIdInsumo.Text = Ins.id_Insumo.ToString();
            txtDescripcion.Text = Ins.nombre;
            txtNumeroSerie.Text = Ins.numeroSerie.ToString();
            txtCosto.Text = Ins.costo.ToString();
            txtVolumen.Text = Ins.volumen.ToString();
            comboBoxTipoInsumo.SelectedIndex = (int)Ins.id_Tipo_Insumo;
            chBoxOrigen.Checked = Ins.esNacional;

        }
        catch (Exception ex)
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = ex.Message;
        }
        txtIdInsumo.Enabled = false;
        txtCosto.Enabled = false;
        txtVolumen.Enabled = false;
        txtDescripcion.Enabled = false;
        txtNumeroSerie.Enabled = false;
        comboBoxTipoInsumo.Enabled = false;
        chBoxOrigen.Enabled = false;
        btnConfElim.Visible = true;



    }
    protected void btnConfElim_Click(object sender, EventArgs e)
    {
        if (gwDatos.SelectedValue == null)
        {
            return;
        }

        int idInsumo = (int)gwDatos.SelectedValue;
        try
        {
            GestorInsumos.eliminarInsumo(idInsumo);

        }
        catch (Exception ex)
        {

            MostrarMensajeCheto("No se puede eliminar el insumo por que esta siendo utilizado en otra tabla.");
        }

        MostrarMensajeCheto("Registro Eliminado Satisfactoriamente ");
        refrescarGrilla();
        pnlConsulta.Visible = true;
        pnlAgregar.Visible = false;

        btnConfElim.Visible = false;
        btnConfAgregar.Visible = true;
    }
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        if (gwDatos.SelectedValue == null)
        {
            MostrarMensajeCheto("Seleccione una Fila antes de continuar ");

            return;
        }
        int idInsumos = (int)gwDatos.SelectedValue;
        try
        {
            IngelunEntidades.Insumo Ins = GestorInsumos.buscarPorId(idInsumos);
            txtIdInsumo.Text = Ins.id_Insumo.ToString();
            txtDescripcion.Text = Ins.nombre;
            txtCosto.Text = Ins.costo.ToString();
            txtNumeroSerie.Text = Ins.numeroSerie.ToString();
            txtVolumen.Text = Ins.volumen.ToString();
            comboBoxTipoInsumo.SelectedIndex = (int)Ins.id_Tipo_Insumo;
            chBoxOrigen.Checked = Ins.esNacional;
        }
        catch (Exception ex)
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = ex.Message;
        }
        txtIdInsumo.Enabled = false;
        txtCosto.Enabled = true;
        txtVolumen.Enabled = true;
        txtDescripcion.Enabled = true;
        txtNumeroSerie.Enabled = true;
        comboBoxTipoInsumo.Enabled = true;
        chBoxOrigen.Enabled = true;
        comboBoxTipoInsumo.Visible = true;
        pnlConsulta.Visible = false;
        pnlAgregar.Visible = true;
        txtIdInsumo.Enabled = false;
        btnConfElim.Visible = false;
        btnConfAgregar.Visible = false;
        btnConfModificar.Visible = true;

    }
    protected void btnConfModificar_Click(object sender, EventArgs e)
    {
        pnlAgregar.Visible = false;
        pnlConsulta.Visible = true;

        try
        {
            GestorInsumos.editarInsumo(Int16.Parse(txtIdInsumo.Text), txtDescripcion.Text, Double.Parse(txtCosto.Text), Double.Parse(txtVolumen.Text), comboBoxTipoInsumo.SelectedIndex, chBoxOrigen.Checked, int.Parse(txtNumeroSerie.Text));
        }
        catch (Exception ex)
        {
            MostrarMensajeCheto("No se puede modificar el insumo por que el numero de serie: " + txtNumeroSerie.Text + ", ya existe, por favor intente otro");
            pnlAgregar.Visible = true;
            pnlConsulta.Visible = false;
        }

        MostrarMensajeCheto("Registro Modificado Correctamente ");
        refrescarGrilla();


    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (gwDatos.SelectedValue == null)
        {
            MostrarMensajeCheto("Seleccione una Fila antes de continuar ");

            return;
        }
        pnlConsulta.Visible = false;
        pnlAgregar.Visible = true;
        int idInsumos = (int)gwDatos.SelectedValue;
        try
        {
            IngelunEntidades.Insumo Ins = GestorInsumos.buscarPorId(idInsumos);
            txtIdInsumo.Text = Ins.id_Insumo.ToString();
            txtDescripcion.Text = Ins.nombre;
            txtCosto.Text = Ins.costo.ToString();
            txtNumeroSerie.Text = Ins.numeroSerie.ToString();
            txtVolumen.Text = Ins.volumen.ToString();
            comboBoxTipoInsumo.SelectedIndex = (int)Ins.id_Tipo_Insumo;
            chBoxOrigen.Checked = Ins.esNacional;
        }
        catch (Exception ex)
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = ex.Message;
        }
        comboBoxTipoInsumo.Visible = true;
        txtIdInsumo.Enabled = false;
        txtCosto.Enabled = false;
        txtVolumen.Enabled = false;
        txtDescripcion.Enabled = false;
        txtNumeroSerie.Enabled = false;
        comboBoxTipoInsumo.Enabled = false;
        chBoxOrigen.Enabled = false;
        btnConfElim.Visible = false;
        btnConfAgregar.Visible = false;
        btnConfModificar.Visible = false;
    }
    protected void gwDatos_Sorting1(object sender, GridViewSortEventArgs e)
    {
        ViewState["GwDatosOrden"] = e.SortExpression;
        refrescarGrilla();
    }

    protected void gwDatos_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gwDatos.PageIndex = e.NewPageIndex;
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

