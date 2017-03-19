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
    public class GestorInsumos
    {

        public static Insumo buscarPorId(int id_Insumo)
        {
            // procedimiento almacenado
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            Insumo Ins = null;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Insumo where id_Insumo = @IdInsumo "; //nombre del procedimiento q debe ir a buscar
                cmd.Parameters.Add(new SqlParameter("@IdInsumo", id_Insumo));
                //cmd.CommandType = CommandType.Text; // es necesario setear esta propiedad el valor por defecto es  CommandType.Text
                SqlDataReader dr = cmd.ExecuteReader();
                // con el resultado cargar una entidad

                if (dr.Read())
                {
                    Ins = new Insumo();
                    if (dr["Id_Insumo"] != DBNull.Value)
                    {
                        Ins.id_Insumo = (int)dr["Id_Insumo"];
                    }
                    if (dr["nombre"] != DBNull.Value)
                    {
                        Ins.nombre = dr["nombre"].ToString();
                    }
                    Ins.costo = (double)dr["costo"];
                    if (dr["id_Tipo_insumo"] != DBNull.Value)
                    {
                        Ins.id_Tipo_Insumo = (int)dr["id_Tipo_insumo"];
                    }
                    if (dr["numeroSerie"] != DBNull.Value)
                    {
                        Ins.numeroSerie = (int)dr["numeroSerie"];
                    }
                    Ins.volumen = (double)dr["volumen"];
                    Ins.esNacional = (Boolean)dr["esNacional"];

                    // if(dr["ID_ProV"] != DBnull.Value)
                    // p2.idProvincia = (int)dr["ID_ProV"];

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

            return Ins;
        }

        public static List<Insumo> BuscarPorNombre(string nombre, string orden)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            //Insumo Ins = null;
            List<Insumo> Insumos = new List<Insumo>(); //lista de este subtipo de dato
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "select Insumo.id_Insumo, Insumo.nombre, Insumo.numeroSerie, Insumo.costo, Insumo.volumen, TipoInsumo.nombreInsumo, Insumo.esNacional from Insumo, TipoInsumo where Insumo.id_Tipo_Insumo = TipoInsumo.id_Tipo_Insumo and Insumo.nombre like @Contiene order by " + orden;
                cmd.Parameters.Add(new SqlParameter("@Contiene", "%" + nombre + "%"));
                SqlDataReader dr = cmd.ExecuteReader();
                //cmd.CommandType = CommandType.Text; // es necesario setear esta propiedad el valor por defecto es  CommandType.Text
                // con el resultado cargar una lista

                while (dr.Read())
                {
                    Insumo Ins2 = new Insumo();

                    if (dr["Id_Insumo"] != DBNull.Value)
                    {
                        Ins2.id_Insumo = (int)dr["Id_Insumo"];
                    }
                    if (dr["nombre"] != DBNull.Value)
                    {
                        Ins2.nombre = dr["nombre"].ToString();
                    }
                    Ins2.costo = (double)dr["costo"];
                    if (dr["numeroSerie"] != DBNull.Value)
                    {
                        Ins2.numeroSerie = (int)dr["numeroSerie"];
                    }
                    Ins2.nombreInsumo = dr["nombreInsumo"].ToString();
                    Ins2.volumen = (double)dr["volumen"];
                    Ins2.esNacional = (Boolean)dr["esNacional"];

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
                //cmd.CommandText = "SELECT MAX(id_Insumo) AS id FROM Insumo";
                cmd.CommandText = "select IDENT_CURRENT('Insumo')";
                numero = int.Parse(cmd.ExecuteScalar().ToString());
                numero++;
            }

            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return numero;
        }

        public static void agregarInsumo(String nombre, double costo, double volumen, int id_tipo, bool esNacional, int numeroSerie)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Insumo (nombre, costo, volumen, id_tipo_insumo, esNacional, numeroSerie) VALUES (@nombre, @costo, @volumen, @id_tipo_insumo, @esNacional,@numeroSerie ) "; //nombre del procedimiento q debe ir a buscar
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@numeroSerie", numeroSerie));
                cmd.Parameters.Add(new SqlParameter("@costo", costo));
                cmd.Parameters.Add(new SqlParameter("@volumen", volumen));
                cmd.Parameters.Add(new SqlParameter("@id_tipo_insumo", id_tipo));
                cmd.Parameters.Add(new SqlParameter("@esNacional", esNacional));
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

        public static void editarInsumo(int idInsumo, String nombre, double costo, double volumen, int itTipo, bool esNacional, int numeroSerie)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "update Insumo set nombre = @nombre,  costo = @costo, volumen = @volumen, esNacional = @esNacional, id_tipo_insumo = @id_tipo_insumo, numeroSerie = @numeroSerie where id_Insumo = (@idInsumo) "; //nombre del procedimiento q debe ir a buscar
                cmd.Parameters.Add(new SqlParameter("@numeroSerie", numeroSerie));
                cmd.Parameters.Add(new SqlParameter("@idInsumo", idInsumo));
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@costo", costo));
                cmd.Parameters.Add(new SqlParameter("@volumen", volumen));
                cmd.Parameters.Add(new SqlParameter("@id_tipo_insumo", itTipo));
                cmd.Parameters.Add(new SqlParameter("@esNacional", esNacional));


                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();

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
                cmd.CommandText = "select * from TipoInsumo ";
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

        public static DataTable ObtenerInsumosParaCompra()
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            DataTable dt = new DataTable();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select i.id_Insumo, i.nombre, i.costo, stk.cantidad from Insumo i inner join StockInsumo stk on stk.id_Insumo = i.id_Insumo";
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

        public static void eliminarInsumo(int id_Insumo)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from Insumo where id_Insumo = @IdInsumo";
                cmd.Parameters.Add(new SqlParameter("@IdInsumo", id_Insumo));
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

        public static int getStock(int idInsumo)
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
                cmd.CommandText = "SELECT cantidad FROM StockInsumo WHERE id_Insumo = @idInsumo";
                cmd.Parameters.Add(new SqlParameter("@idInsumo", idInsumo));
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


    }
}
