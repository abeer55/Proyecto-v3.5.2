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
    public class GestorProductos
    {

        public static Producto buscarPorId(int id_producto)
        {
            // procedimiento almacenado
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            Producto p2 = null;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Producto where id_Producto = @IdProducto"; //nombre del procedimiento q debe ir a buscar
                cmd.Parameters.Add(new SqlParameter("@IdProducto", id_producto));
                //cmd.CommandType = CommandType.Text; // es necesario setear esta propiedad el valor por defecto es  CommandType.Text
                SqlDataReader dr = cmd.ExecuteReader();
                // con el resultado cargar una entidad

                if (dr.Read())
                {
                    p2 = new Producto();
                    if (dr["Id_Producto"] != DBNull.Value)
                    {
                        p2.id_Producto = (int)dr["Id_Producto"];
                    }
                    p2.nombre = dr["nombre"].ToString();
                    p2.precio = (double)dr["precio"];
                    p2.id_Tipo_Producto = (int)dr["Id_Tipo_Producto"];
                    p2.fecha_Construccion = (DateTime)dr["fecha_Construccion"];
                }
                dr.Close();
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

            return p2;
        }

        public static int getStock(int idProducto)
        {
            int ultStock; 
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT cantidad FROM StockProducto WHERE id_Producto = @idProducto";
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                ultStock = Convert.ToInt32(cmd.ExecuteScalar());
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
            return ultStock;
        }

        public static List<Producto> BuscarPorNombre(string nombre, string orden)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            //Producto p2 = null;
            List<Producto> Productos = new List<Producto>(); //lista de este subtipo de dato

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                string jquery = "SELECT Producto.Id_Producto, Producto.nombre AS nombre_Producto, Producto.precio, Producto.Id_Tipo_Producto, Producto.fecha_Construccion, TipoProducto.nombre AS nombre_Tipo_Producto";
                jquery += " FROM Producto INNER JOIN";
                jquery += " TipoProducto ON Producto.id_Tipo_Producto = TipoProducto.id_Tipo_Producto";
                jquery += " WHERE Producto.nombre like @Contiene ORDER BY " + orden;
                //order by " + orden;
                cmd.CommandText = jquery;
                cmd.Parameters.Add(new SqlParameter("@Contiene", "%" + nombre + "%"));
                SqlDataReader dr = cmd.ExecuteReader();
                //cmd.CommandType = CommandType.Text; // es necesario setear esta propiedad el valor por defecto es  CommandType.Text
                // con el resultado cargar una lista

                while (dr.Read())
                {
                    Producto p = new Producto();
                    if (dr["Id_Producto"] != DBNull.Value)
                    {
                        p.id_Producto = (int)dr["Id_Producto"];
                    } //idCliente nombre del campo de la consulta
                    p.nombre = dr["nombre_Producto"].ToString();
                    p.precio = (double)dr["precio"];
                    p.id_Tipo_Producto = (int)dr["Id_Tipo_Producto"];
                    string fecha = dr["fecha_Construccion"].ToString();
                    p.fecha_Construccion = Convert.ToDateTime(dr["fecha_Construccion"].ToString());
                    p.nombre_Tipo_Producto = dr["nombre_Tipo_Producto"].ToString();
                    Productos.Add(p);
                }
                dr.Close();
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
            return Productos;
        }

        public static List<DTOInsumoxProducto> agregarInsumo(DTOInsumoxProducto insumoDTO, List<DTOInsumoxProducto> lista)
        {
            bool existe = false;
            try
            {
                foreach (DTOInsumoxProducto item in lista)
                {
                    if (insumoDTO.id_Insumo == item.id_Insumo)
                    {
                        existe = true;
                    }
                }

            }
            catch (Exception es)
            {

                throw;
            }
            finally
            {
                if (!existe)
                {
                    lista.Add(insumoDTO);
                }
            }
            return lista;
        }

        public static List<DTOInsumoxProducto> quitarInsumo(DTOInsumoxProducto insumoDTO, List<DTOInsumoxProducto> lista)
        {
            bool existe = false;
            int indice = 0;
            try
            {
                foreach (DTOInsumoxProducto item in lista)
                {
                    if (insumoDTO.id_Insumo == item.id_Insumo)
                    {
                        existe = true;
                        lista.RemoveAt(indice);
                        break;

                    }
                    indice++;
                }

            }
            catch (Exception es)
            {

                throw;
            }
            finally
            {
                //if (existe)
                //{
                //    lista.RemoveAt(indice);
                //}
            }
            return lista;
        }

        public static int obtenerSiguienteID()
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            int numero;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "select IDENT_CURRENT( 'Producto' )";
                numero = int.Parse(cmd.ExecuteScalar().ToString());
                numero++;
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
            return numero;
        }

        public static void agregarProducto(String nombre, Double precio, int id_tipo, DateTime fechaConstruccion)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Producto (nombre, precio, id_Tipo_Producto, fecha_Construccion) VALUES (@nombreProducto, @precio, @id_tipo, @fechaConstruccion)"; //nombre del procedimiento q debe ir a buscar
                cmd.Parameters.Add(new SqlParameter("@nombreProducto", nombre));
                cmd.Parameters.Add(new SqlParameter("@precio", precio));
                cmd.Parameters.Add(new SqlParameter("@id_tipo", id_tipo));
                cmd.Parameters.Add(new SqlParameter("@fechaConstruccion", fechaConstruccion));
                cmd.CommandType = CommandType.Text; // es necesario setear esta propiedad el valor por defecto es  CommandType.Text
                SqlDataReader dr = cmd.ExecuteReader();
                // con el resultado cargar una entidad
                dr.Close();
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
        }

        public static void editarProducto(int idProducto, String nombre, double Precio, int id_tipo, DateTime fecha_Construccion)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "update Producto set nombre = @nombre,  precio = @precio, fecha_Construccion = @fecha_Construccion, id_tipo_producto = @id_tipo_producto where id_Producto = (@idProducto) "; //nombre del procedimiento q debe ir a buscar
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@precio", Precio));
                cmd.Parameters.Add(new SqlParameter("@fecha_Construccion", fecha_Construccion));
                cmd.Parameters.Add(new SqlParameter("@id_tipo_producto", id_tipo));


                cmd.CommandType = CommandType.Text; // es necesario setear esta propiedad el valor por defecto es  CommandType.Text
                SqlDataReader dr = cmd.ExecuteReader();
                // con el resultado cargar una entidad
                dr.Close();
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
        }

        public static void eliminarProducto(int id_Producto)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from Producto where id_Producto = @IdProducto";
                cmd.Parameters.Add(new SqlParameter("@IdProducto", id_Producto));
                cmd.CommandType = CommandType.Text;
                int filasafectadas = cmd.ExecuteNonQuery();
                if (filasafectadas == 0)
                {
                    throw new Exception("El registro ya no esta en la BD");

                }
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
        }

        public static DataTable ObtenerTodas()
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            DataTable dt = new DataTable();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select * from TipoProducto ";
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

        public static List<DTOInsumoxProducto> BuscarInsumosPorProducto(int idProducto)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            //Insumo Ins = null;
            List<DTOInsumoxProducto> Insumos = new List<DTOInsumoxProducto>(); //lista de este subtipo de dato
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                String jquery = "SELECT        Insumo.nombre as nombre, InsumoXproducto.cantidad_Insumo as cantidad, InsumoXproducto.id_Insumo AS IdInsumo";
                jquery += " FROM            Insumo INNER JOIN";
                jquery += " InsumoXproducto ON Insumo.id_Insumo = InsumoXproducto.id_Insumo INNER JOIN";
                jquery += " Producto ON InsumoXproducto.id_Producto = Producto.id_Producto";
                jquery += " WHERE InsumoXProducto.id_Producto=@IdProducto";
                cmd.CommandText = jquery;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdProducto", idProducto));
                SqlDataReader dr = cmd.ExecuteReader();
                //cmd.CommandType = CommandType.Text; // es necesario setear esta propiedad el valor por defecto es  CommandType.Text
                // con el resultado cargar una lista

                while (dr.Read())
                {
                    DTOInsumoxProducto Ins2 = new DTOInsumoxProducto();

                    if (dr["IdInsumo"] != DBNull.Value)
                    {
                        Ins2.id_Insumo = int.Parse(dr["IdInsumo"].ToString());

                    }

                    if (dr["nombre"] != DBNull.Value)
                    {
                        Ins2.nombre = dr["nombre"].ToString();
                    }

                    if (dr["cantidad"] != DBNull.Value)
                    {
                        Ins2.cantidad = int.Parse(dr["cantidad"].ToString());
                    }


                    Insumos.Add(Ins2);
                }
                dr.Close();
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
            return Insumos;
        }

        public static void agregarInsumoPorProducto(int idInsumo, int idProducto, int cantidad)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                string jquery = "IF EXISTS (SELECT * FROM insumoxproducto WHERE id_Producto = @idProducto AND id_Insumo = @idInsumo)";
                jquery += " UPDATE insumoxproducto SET cantidad_Insumo = @cantidad WHERE id_Producto = @idProducto AND id_Insumo = @idInsumo";
                jquery += " ELSE ";
                jquery += " INSERT INTO insumoxproducto(id_Producto, id_Insumo, cantidad_Insumo) VALUES(@idProducto, @idInsumo, @cantidad)";
                cmd.CommandText = jquery;
                cmd.Parameters.Add(new SqlParameter("@idInsumo", idInsumo));
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                cmd.CommandType = CommandType.Text; // es necesario setear esta propiedad el valor por defecto es  CommandType.Text
                cmd.ExecuteNonQuery();
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
        }

        public static void borrarTodosLosInsumosXProducto(int idProducto)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                string jquery = "DELETE FROM InsumoXproducto WHERE id_Producto = @idProducto";
                cmd.CommandText = jquery;
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                cmd.CommandType = CommandType.Text; // es necesario setear esta propiedad el valor por defecto es  CommandType.Text
                cmd.ExecuteNonQuery();
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
        }



    }
}
