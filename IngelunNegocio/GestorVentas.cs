using IngelunEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngelunNegocio
{
    public class GestorVentas
    {
        public static DataTable ObtenerClientes()
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            DataTable dt = new DataTable();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT * FROM Cliente ";
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return dt;
        }

        public static List<VentaProducto> getReporteVentaSinFiltro(string orden)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<VentaProducto> ventaReporte = new List<VentaProducto>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT  c.nombre, c.mail, p.nombre as NombreProducto, pxv.cantidad, pxv.montoParcial,v.id_Venta, v.fecha, v.montoTotal FROM Cliente c INNER JOIN Venta v ON c.id_Cliente = v.id_Cliente INNER JOIN ProductoXventa pxv ON pxv.id_Venta = v.id_Venta INNER JOIN Producto p ON pxv.id_Producto = p.id_Producto order by " + orden;
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    VentaProducto Vp = new VentaProducto();
                    Vp.id_Venta = (int)dr["id_Venta"];
                    Vp.fechaVenta = (DateTime)dr["fecha"];
                    Vp.montoTotal = (int)dr["montoTotal"];
                    Vp.nombreCliente = dr["nombre"].ToString();
                    Vp.mailCliente = dr["mail"].ToString();
                    Vp.cantidad = (int)dr["cantidad"];
                    Vp.montoParcial = (int)dr["montoParcial"];
                    Vp.nombreProducto = dr["NombreProducto"].ToString();

                    ventaReporte.Add(Vp);

                }
                dr.Close();
            }

            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return ventaReporte;
        }

        


        public static List<VentaProducto> getReporteVentasXFechaXCantidadXNombreCliente(DateTime fecha, int montoParcial, string nombreCliente)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<VentaProducto> ventaReporte = new List<VentaProducto>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();

                //Consulto por todas las combinaciones posibles
                if (fecha.Year.ToString().CompareTo("1") != 0 && montoParcial > 0 && nombreCliente.CompareTo("") != 0)
                {
                    cmd.CommandText = "SELECT  c.nombre, c.mail, p.nombre as NombreProducto, pxv.cantidad, pxv.montoParcial,v.id_Venta, v.fecha, v.montoTotal FROM Cliente c INNER JOIN Venta v ON c.id_Cliente = v.id_Cliente INNER JOIN ProductoXventa pxv ON pxv.id_Venta = v.id_Venta INNER JOIN Producto p ON pxv.id_Producto = p.id_Producto WHERE v.fecha = @fecha and pxv.montoParcial>= @montoParcial and c.nombre= @nombreCliente ";
                }

                if (fecha.Year.ToString().CompareTo("1") != 0 && montoParcial == 0 && nombreCliente.CompareTo("") == 0)
                {
                    cmd.CommandText = "SELECT  c.nombre, c.mail, p.nombre as NombreProducto, pxv.cantidad, pxv.montoParcial,v.id_Venta, v.fecha, v.montoTotal FROM Cliente c INNER JOIN Venta v ON c.id_Cliente = v.id_Cliente INNER JOIN ProductoXventa pxv ON pxv.id_Venta = v.id_Venta INNER JOIN Producto p ON pxv.id_Producto = p.id_Producto WHERE v.fecha = @fecha ";
                }

                if (fecha.Year.ToString().CompareTo("1") != 0 && montoParcial > 0 && nombreCliente.CompareTo("") == 0)
                {
                    cmd.CommandText = "SELECT  c.nombre, c.mail, p.nombre as NombreProducto, pxv.cantidad, pxv.montoParcial,v.id_Venta, v.fecha, v.montoTotal FROM Cliente c INNER JOIN Venta v ON c.id_Cliente = v.id_Cliente INNER JOIN ProductoXventa pxv ON pxv.id_Venta = v.id_Venta INNER JOIN Producto p ON pxv.id_Producto = p.id_Producto WHERE v.fecha = @fecha and pxv.montoParcial>= @montoParcial  ";
                }

                if (fecha.Year.ToString().CompareTo("1") != 0 && montoParcial == 0 && nombreCliente.CompareTo("") != 0)
                {
                    cmd.CommandText = "SELECT  c.nombre, c.mail, p.nombre as NombreProducto, pxv.cantidad, pxv.montoParcial,v.id_Venta, v.fecha, v.montoTotal FROM Cliente c INNER JOIN Venta v ON c.id_Cliente = v.id_Cliente INNER JOIN ProductoXventa pxv ON pxv.id_Venta = v.id_Venta INNER JOIN Producto p ON pxv.id_Producto = p.id_Producto WHERE v.fecha = @fecha  and c.nombre= @nombreCliente ";
                }

                if (fecha.Year.ToString().CompareTo("1") == 0 && montoParcial > 0 && nombreCliente.CompareTo("") != 0)
                {
                    cmd.CommandText = "SELECT  c.nombre, c.mail, p.nombre as NombreProducto, pxv.cantidad, pxv.montoParcial,v.id_Venta, v.fecha, v.montoTotal FROM Cliente c INNER JOIN Venta v ON c.id_Cliente = v.id_Cliente INNER JOIN ProductoXventa pxv ON pxv.id_Venta = v.id_Venta INNER JOIN Producto p ON pxv.id_Producto = p.id_Producto WHERE pxv.montoParcial>= @montoParcial and c.nombre= @nombreCliente ";
                }

                if (fecha.Year.ToString().CompareTo("1") == 0 && montoParcial > 0 && nombreCliente.CompareTo("") == 0)
                {
                    cmd.CommandText = "SELECT  c.nombre, c.mail, p.nombre as NombreProducto, pxv.cantidad, pxv.montoParcial,v.id_Venta, v.fecha, v.montoTotal FROM Cliente c INNER JOIN Venta v ON c.id_Cliente = v.id_Cliente INNER JOIN ProductoXventa pxv ON pxv.id_Venta = v.id_Venta INNER JOIN Producto p ON pxv.id_Producto = p.id_Producto WHERE pxv.montoParcial >= @montoParcial ";
                }

                if (fecha.Year.ToString().CompareTo("1") == 0 && montoParcial == 0 && nombreCliente.CompareTo("") != 0)
                {
                    cmd.CommandText = "SELECT  c.nombre, c.mail, p.nombre as NombreProducto, pxv.cantidad, pxv.montoParcial,v.id_Venta, v.fecha, v.montoTotal FROM Cliente c INNER JOIN Venta v ON c.id_Cliente = v.id_Cliente INNER JOIN ProductoXventa pxv ON pxv.id_Venta = v.id_Venta INNER JOIN Producto p ON pxv.id_Producto = p.id_Producto WHERE c.nombre= @nombreCliente ";
                }

                if (fecha.Year.ToString().CompareTo("1") != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                }
                if (montoParcial > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@montoParcial", montoParcial));
                }
                if (nombreCliente.CompareTo("") != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@nombreCliente", nombreCliente));
                }

                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    VentaProducto Vp = new VentaProducto();

                    Vp.id_Venta = (int)dr["id_Venta"];
                    Vp.fechaVenta = (DateTime)dr["fecha"];
                    Vp.montoTotal = (int)dr["montoTotal"];
                    Vp.nombreCliente = dr["nombre"].ToString();
                    Vp.mailCliente = dr["mail"].ToString();
                    Vp.cantidad = (int)dr["cantidad"];
                    Vp.montoParcial = (int)dr["montoParcial"];
                    Vp.nombreProducto = dr["NombreProducto"].ToString();

                    ventaReporte.Add(Vp);

                }
                dr.Close();
            }

            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return ventaReporte;
        }

    }
}
