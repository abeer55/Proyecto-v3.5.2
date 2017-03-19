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
    public class GestorProducciones
    {
        public static List<DTOProduccionReporte> getReporteProduccionSinFiltro(string orden)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<DTOProduccionReporte> listaReporte = new List<DTOProduccionReporte>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                //cmd.CommandText = "SELECT  p.fecha, p.id_Produccion, pr.nombre as nombreProducto, pxp.cantidad, tp.nombre as tipoProducto FROM Produccion p INNER JOIN Producto pr ON p.id_Producto= pr.id_Producto INNER JOIN ProdcutosXproduccion pxp ON pxp.id_Produccion = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo order by " + orden;
                String jquery = "";
                jquery += " SELECT pr.nombre AS nombreProducto, pxp.id_Produccion, tp.nombre AS nombreTipoProducto, p.fecha AS fechaProduccion, pxp.cantidad";
                jquery += " FROM Producto pr INNER JOIN";
                jquery += " ProductosXproduccion pxp ON pr.id_Producto = pxp.id_Producto INNER JOIN";
                jquery += " TipoProducto tp ON pr.id_Tipo_Producto = tp.id_Tipo_Producto INNER JOIN";
                jquery += "  Produccion p ON pxp.id_Produccion = p.id_Produccion order by " + orden;
                cmd.CommandText = jquery;
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    DTOProduccionReporte dto = new DTOProduccionReporte();

                    dto.fechaProduccion = (DateTime)dr["fechaProduccion"];
                    dto.cantidad = (int)dr["cantidad"];
                    dto.nombreProducto = dr["nombreProducto"].ToString();
                    dto.id_Produccion = (int)dr["id_Produccion"];
                    dto.nombreTipoProducto = dr["nombreTipoProducto"].ToString();

                    listaReporte.Add(dto);

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
            return listaReporte;
        }

        public static List<DTOProduccionReporte> getReporteProduccionXFechaXTipoProductoXNombreProducto(DateTime fecha, int tipoProducto, string nombreProducto)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<DTOProduccionReporte> listaReporte = new List<DTOProduccionReporte>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                String jquery = "";
                jquery += " SELECT pr.nombre AS nombreProducto, pxp.id_Produccion, tp.nombre AS nombreTipoProducto, p.fecha AS fechaProduccion, pxp.cantidad";
                jquery += " FROM Producto pr INNER JOIN";
                jquery += " ProductosXproduccion pxp ON pr.id_Producto = pxp.id_Producto INNER JOIN";
                jquery += " TipoProducto tp ON pr.id_Tipo_Producto = tp.id_Tipo_Producto INNER JOIN";
                jquery += "  Produccion p ON pxp.id_Produccion = p.id_Produccion";
                jquery += " WHERE p.fecha = @fecha and tp.id_Tipo_Producto= @tipoProducto and pr.nombre like @Contiene";
                cmd.CommandText = jquery;
                //cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE c.fecha = @fecha and ixc.cantidad= @cantidad and p.nombre= @nombreProveedor";
                cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                cmd.Parameters.Add(new SqlParameter("@Contiene", "%" + nombreProducto + "%"));
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    DTOProduccionReporte dto = new DTOProduccionReporte();

                    dto.fechaProduccion = (DateTime)dr["fechaProduccion"];
                    dto.cantidad = (int)dr["cantidad"];
                    dto.nombreProducto = dr["nombreProducto"].ToString();
                    dto.id_Produccion = (int)dr["id_Produccion"];
                    dto.nombreTipoProducto = dr["nombreTipoProducto"].ToString();

                    listaReporte.Add(dto);

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
            return listaReporte;
        }

        public static List<DTOProduccionReporte> getReporteProduccionXNombreProducto(string nombreProducto)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<DTOProduccionReporte> listaReporte = new List<DTOProduccionReporte>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                String jquery = "";
                jquery += " SELECT pr.nombre AS nombreProducto, pxp.id_Produccion, tp.nombre AS nombreTipoProducto, p.fecha AS fechaProduccion, pxp.cantidad";
                jquery += " FROM Producto pr INNER JOIN";
                jquery += " ProductosXproduccion pxp ON pr.id_Producto = pxp.id_Producto INNER JOIN";
                jquery += " TipoProducto tp ON pr.id_Tipo_Producto = tp.id_Tipo_Producto INNER JOIN";
                jquery += "  Produccion p ON pxp.id_Produccion = p.id_Produccion";
                jquery += " WHERE pr.nombre like @Contiene";
                cmd.CommandText = jquery;
                cmd.Parameters.Add(new SqlParameter("@Contiene", "%" + nombreProducto + "%"));
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    DTOProduccionReporte dto = new DTOProduccionReporte();

                    dto.fechaProduccion = (DateTime)dr["fechaProduccion"];
                    dto.cantidad = (int)dr["cantidad"];
                    dto.nombreProducto = dr["nombreProducto"].ToString();
                    dto.id_Produccion = (int)dr["id_Produccion"];
                    dto.nombreTipoProducto = dr["nombreTipoProducto"].ToString();

                    listaReporte.Add(dto);

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
            return listaReporte;
        }

        public static List<DTOProduccionReporte> getReporteProduccionXFecha(DateTime fecha)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<DTOProduccionReporte> listaReporte = new List<DTOProduccionReporte>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                String jquery = "";
                jquery += " SELECT pr.nombre AS nombreProducto, pxp.id_Produccion, tp.nombre AS nombreTipoProducto, p.fecha AS fechaProduccion, pxp.cantidad";
                jquery += " FROM Producto pr INNER JOIN";
                jquery += " ProductosXproduccion pxp ON pr.id_Producto = pxp.id_Producto INNER JOIN";
                jquery += " TipoProducto tp ON pr.id_Tipo_Producto = tp.id_Tipo_Producto INNER JOIN";
                jquery += "  Produccion p ON pxp.id_Produccion = p.id_Produccion";
                jquery += " WHERE p.fecha = @fecha";
                cmd.CommandText = jquery;
                //cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE c.fecha = @fecha and ixc.cantidad= @cantidad and p.nombre= @nombreProveedor";
                cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    DTOProduccionReporte dto = new DTOProduccionReporte();

                    dto.fechaProduccion = (DateTime)dr["fechaProduccion"];
                    dto.cantidad = (int)dr["cantidad"];
                    dto.nombreProducto = dr["nombreProducto"].ToString();
                    dto.id_Produccion = (int)dr["id_Produccion"];
                    dto.nombreTipoProducto = dr["nombreTipoProducto"].ToString();

                    listaReporte.Add(dto);

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
            return listaReporte;
        }

        public static List<DTOProduccionReporte> getReporteProduccionXTipoProducto(int tipoProducto)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<DTOProduccionReporte> listaReporte = new List<DTOProduccionReporte>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                String jquery = "";
                jquery += " SELECT pr.nombre AS nombreProducto, pxp.id_Produccion, tp.nombre AS nombreTipoProducto, p.fecha AS fechaProduccion, pxp.cantidad";
                jquery += " FROM Producto pr INNER JOIN";
                jquery += " ProductosXproduccion pxp ON pr.id_Producto = pxp.id_Producto INNER JOIN";
                jquery += " TipoProducto tp ON pr.id_Tipo_Producto = tp.id_Tipo_Producto INNER JOIN";
                jquery += "  Produccion p ON pxp.id_Produccion = p.id_Produccion";
                jquery += " WHERE  tp.id_Tipo_Producto = @tipoProducto";
                cmd.CommandText = jquery;
                cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                //cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE c.fecha = @fecha and ixc.cantidad= @cantidad and p.nombre= @nombreProveedor";                
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    DTOProduccionReporte dto = new DTOProduccionReporte();

                    dto.fechaProduccion = (DateTime)dr["fechaProduccion"];
                    dto.cantidad = (int)dr["cantidad"];
                    dto.nombreProducto = dr["nombreProducto"].ToString();
                    dto.id_Produccion = (int)dr["id_Produccion"];
                    dto.nombreTipoProducto = dr["nombreTipoProducto"].ToString();

                    listaReporte.Add(dto);

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
            return listaReporte;
        }

        public static List<DTOProduccionReporte> getReporteProduccionXFechaXTipoProducto(DateTime fecha, int tipoProducto)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<DTOProduccionReporte> listaReporte = new List<DTOProduccionReporte>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                String jquery = "";
                jquery += " SELECT pr.nombre AS nombreProducto, pxp.id_Produccion, tp.nombre AS nombreTipoProducto, p.fecha AS fechaProduccion, pxp.cantidad";
                jquery += " FROM Producto pr INNER JOIN";
                jquery += " ProductosXproduccion pxp ON pr.id_Producto = pxp.id_Producto INNER JOIN";
                jquery += " TipoProducto tp ON pr.id_Tipo_Producto = tp.id_Tipo_Producto INNER JOIN";
                jquery += "  Produccion p ON pxp.id_Produccion = p.id_Produccion";
                jquery += " WHERE p.fecha = @fecha and tp.id_Tipo_Producto= @tipoProducto";
                cmd.CommandText = jquery;
                //cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE c.fecha = @fecha and ixc.cantidad= @cantidad and p.nombre= @nombreProveedor";
                cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    DTOProduccionReporte dto = new DTOProduccionReporte();

                    dto.fechaProduccion = (DateTime)dr["fechaProduccion"];
                    dto.cantidad = (int)dr["cantidad"];
                    dto.nombreProducto = dr["nombreProducto"].ToString();
                    dto.id_Produccion = (int)dr["id_Produccion"];
                    dto.nombreTipoProducto = dr["nombreTipoProducto"].ToString();

                    listaReporte.Add(dto);

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
            return listaReporte;
        }

        public static List<DTOProduccionReporte> getReporteProduccionXFechaXNombreProducto(DateTime fecha, string nombreProducto)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<DTOProduccionReporte> listaReporte = new List<DTOProduccionReporte>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                String jquery = "";
                jquery += " SELECT pr.nombre AS nombreProducto, pxp.id_Produccion, tp.nombre AS nombreTipoProducto, p.fecha AS fechaProduccion, pxp.cantidad";
                jquery += " FROM Producto pr INNER JOIN";
                jquery += " ProductosXproduccion pxp ON pr.id_Producto = pxp.id_Producto INNER JOIN";
                jquery += " TipoProducto tp ON pr.id_Tipo_Producto = tp.id_Tipo_Producto INNER JOIN";
                jquery += "  Produccion p ON pxp.id_Produccion = p.id_Produccion";
                jquery += " WHERE p.fecha = @fecha and pr.nombre like @Contiene";
                cmd.CommandText = jquery;
                //cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE c.fecha = @fecha and ixc.cantidad= @cantidad and p.nombre= @nombreProveedor";
                cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                cmd.Parameters.Add(new SqlParameter("@Contiene", "%" + nombreProducto + "%"));
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    DTOProduccionReporte dto = new DTOProduccionReporte();

                    dto.fechaProduccion = (DateTime)dr["fechaProduccion"];
                    dto.cantidad = (int)dr["cantidad"];
                    dto.nombreProducto = dr["nombreProducto"].ToString();
                    dto.id_Produccion = (int)dr["id_Produccion"];
                    dto.nombreTipoProducto = dr["nombreTipoProducto"].ToString();

                    listaReporte.Add(dto);

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
            return listaReporte;
        }

        public static List<DTOProduccionReporte> getReporteProduccionXTipoProductoXNombreProducto(int tipoProducto, string nombreProducto)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<DTOProduccionReporte> listaReporte = new List<DTOProduccionReporte>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                String jquery = "";
                jquery += " SELECT pr.nombre AS nombreProducto, pxp.id_Produccion, tp.nombre AS nombreTipoProducto, p.fecha AS fechaProduccion, pxp.cantidad";
                jquery += " FROM Producto pr INNER JOIN";
                jquery += " ProductosXproduccion pxp ON pr.id_Producto = pxp.id_Producto INNER JOIN";
                jquery += " TipoProducto tp ON pr.id_Tipo_Producto = tp.id_Tipo_Producto INNER JOIN";
                jquery += "  Produccion p ON pxp.id_Produccion = p.id_Produccion";
                jquery += " WHERE tp.id_Tipo_Producto= @tipoProducto and pr.nombre like @Contiene";
                cmd.CommandText = jquery;
                //cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE c.fecha = @fecha and ixc.cantidad= @cantidad and p.nombre= @nombreProveedor";               
                cmd.Parameters.Add(new SqlParameter("@tipoProducto", tipoProducto));
                cmd.Parameters.Add(new SqlParameter("@Contiene", "%" + nombreProducto + "%"));
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    DTOProduccionReporte dto = new DTOProduccionReporte();

                    dto.fechaProduccion = (DateTime)dr["fechaProduccion"];
                    dto.cantidad = (int)dr["cantidad"];
                    dto.nombreProducto = dr["nombreProducto"].ToString();
                    dto.id_Produccion = (int)dr["id_Produccion"];
                    dto.nombreTipoProducto = dr["nombreTipoProducto"].ToString();

                    listaReporte.Add(dto);

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
            return listaReporte;
        }

        public static DataTable ObtenerProductosParaProduccion()
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            DataTable dt = new DataTable();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select id_Producto, nombre, precio from Producto ";
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
    }
}
