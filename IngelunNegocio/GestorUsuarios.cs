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

    public class GestorUsuarios
    {

        public static bool VerificarUsuarioClave(string usuario, string clave)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "select count(*) from usuario u where u.nombre = @usuario and u.clave = @clave";
                cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                cmd.Parameters.Add(new SqlParameter("@clave", clave));
                if ((int)cmd.ExecuteScalar() > 0) return true;

            }

            catch (Exception)
            {
                return false;
            }

            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return false;

        }

        public static string[] ObtenerRoles(string usuario)
        {

            String cadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT esAdmin from Usuario where nombre=@nombre";
                cmd.Parameters.Add(new SqlParameter("@nombre", usuario));
                cmd.CommandType = CommandType.Text;
                if ((bool)cmd.ExecuteScalar()) return new string[] { "administradores" };
                else
                {
                    return new string[] { "clientes" };
                }


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

        }

        public static string[] ObtenerRolesConBD()
        {
            bool? esAdministrador = false;

            if (esAdministrador == null)
                return new string[] { "" };
            else if (esAdministrador == true)
                return new string[] { "adminstrador" };
            else
                return new string[] { "clientes" };
        }

    }

}



