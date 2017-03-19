using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IngelunEntidades;

namespace IngelunNegocio
{
    public class GestorTransaccionVenta
    {

        public static bool generarVentaProducto(DataTable detalle, int total, int idCliente, DateTime fecha)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            SqlTransaction transaction;
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            transaction = cn.BeginTransaction("TransactionVenta");
            cmd.Connection = cn;
            cmd.Transaction = transaction;

            try
            {
                cmd.Parameters.Clear();

                //Primero, inserto en la tabla Venta
                cmd.CommandText = "INSERT INTO Venta (fecha, montoTotal, id_Cliente) VALUES (@fecha, @montoTotal, @id_Cliente)";
                cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                cmd.Parameters.Add(new SqlParameter("@montoTotal", total));
                cmd.Parameters.Add(new SqlParameter("@id_Cliente", idCliente));

                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Close();

                cmd.Parameters.Clear();

                //Segundo, recupero el id de la compra agregada
                cmd.CommandText = "SELECT MAX(id_Venta) FROM Venta";
                int idVenta = Convert.ToInt32(cmd.ExecuteScalar());

                //Tercero, inserto en la tabla ProductoXventa
                foreach (DataRow fila in detalle.Rows)
                {
                    cmd.CommandText = "INSERT INTO ProductoXventa (id_Venta, id_Producto, cantidad, montoParcial) VALUES (@idVenta, @idProducto, @cantidad, @subTotal)";
                    cmd.Parameters.Add(new SqlParameter("@idVenta", idVenta));
                    cmd.Parameters.Add(new SqlParameter("@idProducto", int.Parse(fila[0].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@cantidad", int.Parse(fila[2].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@subTotal", int.Parse(fila[4].ToString())));

                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr2 = cmd.ExecuteReader();
                    dr2.Close();

                    cmd.Parameters.Clear();
                }

                //Cuarto, actualizo el stock de cada producto.
                foreach (DataRow fila in detalle.Rows)
                {
                    //recupero su ultimo stock
                    cmd.CommandText = "SELECT cantidad FROM StockProducto WHERE id_Producto=@idProducto";
                    cmd.Parameters.Add(new SqlParameter("@idProducto", int.Parse(fila[0].ToString())));
                    int ultStock = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.Clear();

                    cmd.CommandText = "UPDATE StockProducto set cantidad=@cantidad WHERE id_Producto = @idProducto";
                    int stockNuevo = ultStock - int.Parse(fila[2].ToString());
                    cmd.Parameters.Add(new SqlParameter("@cantidad", stockNuevo));
                    cmd.Parameters.Add(new SqlParameter("@idProducto", int.Parse(fila[0].ToString())));


                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr2 = cmd.ExecuteReader();
                    dr2.Close();

                    cmd.Parameters.Clear();
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    return false;
                }
                catch (Exception ex2)
                {
                    // throw ex2;
                    return false;
                }
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public static DataTable ObtenerProductosParaComprar()
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            DataTable dt = new DataTable();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select Producto.id_Producto, Producto.nombre, Producto.precio, StockProducto.cantidad  from Producto, StockProducto where Producto.id_Producto = StockProducto.id_Producto ";
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

        public static List<Cliente> BuscarPorNombre(string nombre, string orden)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<Cliente> Clientes = new List<Cliente>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT id_Cliente, nombre, direccion, mail, ciudad, provincia, pais FROM Cliente WHERE nombre like @Contiene order by " + orden;
                cmd.Parameters.Add(new SqlParameter("@Contiene", "%" + nombre + "%"));
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Cliente cl = new Cliente();

                    cl.nombre = dr["nombre"].ToString();
                    cl.id_Cliente = (int)dr["id_Cliente"];
                    cl.direccion = dr["direccion"].ToString();
                    cl.mail = dr["mail"].ToString();
                    cl.ciudad = dr["ciudad"].ToString();
                    cl.provincia = dr["provincia"].ToString();
                    cl.pais = dr["pais"].ToString();

                    Clientes.Add(cl);

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
            return Clientes;
        }

    }
}
