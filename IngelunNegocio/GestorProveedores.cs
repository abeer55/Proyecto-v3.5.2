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
    public class GestorProveedores
    {
          public static Proveedor buscarPorId(int id_proveedor)
          {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            Proveedor p2 = null;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT p.id_Proveedor,p.nombre,p.direccion,p.mail,p.telefono, td.descripcion, p.codigo_Postal,p.fechaNac,p.soloEfectivo,p.numeroDocumento, p.id_tipo_dni FROM Proveedor p INNER JOIN TipoDocumento td ON p.id_tipo_dni=td.id_tipo_Dni WHERE id_Proveedor = @IdProveedor";
                cmd.Parameters.Add(new SqlParameter("@IdProveedor", id_proveedor));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    p2 = new Proveedor();
                                     
                    p2.id_Proveedor = (int)dr["id_Proveedor"];                    
                    p2.nombre = dr["nombre"].ToString();
                    p2.direccion = dr["direccion"].ToString();
                    p2.mail = dr["mail"].ToString();
                    if (dr["codigo_Postal"] != DBNull.Value)
                    {
                        p2.codigo_Postal = (int)dr["codigo_Postal"];
                    }
                    if (dr["telefono"] != DBNull.Value)
                    {
                        p2.telefono = (int)dr["telefono"];
                    }
                 
                    if (dr["fechaNac"] != DBNull.Value)
                    {
                        p2.fechaNac = (DateTime)dr["fechaNac"];
                    }
                    if (dr["soloEfectivo"] != DBNull.Value)
                    {
                        p2.soloEfectivo = (Boolean)dr["soloEfectivo"];
                    }
                    p2.numeroDocumento = (int)dr["numeroDocumento"];
                    p2.descripcion = dr["descripcion"].ToString();
                    p2.id_tipo_dni = (int)dr["id_tipo_dni"];

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

            return p2;
        }


          public static bool existeProveedor(int idTipoDoc, int numeroDoc)
          {
              bool aux = false;

              string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
              SqlConnection cn = new SqlConnection(CadenaConexion);

              List<Proveedor> Proveedores = new List<Proveedor>();
              try
              {
                  cn.Open();
                  SqlCommand cmd = new SqlCommand();
                  cmd.Connection = cn;
                  cmd.Parameters.Clear();
                 // cmd.CommandText = "SELECT id_Proveedor FROM Proveedor WHERE id_tipo_dni = @tipoDoc AND numeroDocumento= @numeroDoc";
                  cmd.CommandText = "SELECT id_Proveedor FROM Proveedor WHERE numeroDocumento= @numeroDoc";
                  //cmd.Parameters.Add(new SqlParameter("@tipoDoc", idTipoDoc));
                  cmd.Parameters.Add(new SqlParameter("@numeroDoc", numeroDoc));
                  DataSet ds = new DataSet();
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  da.Fill(ds, "proveedor");

                  if (ds.Tables[0].Rows.Count != 0)
                  {
                      aux = true;
                  }
                  else
                  {
                      aux = false;
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
              return aux;
          }

        public static List<Proveedor> BuscarPorNombre(string nombre, string orden)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<Proveedor> Proveedores = new List<Proveedor>(); 
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT  P.id_Proveedor,p.nombre,p.direccion,p.mail,p.telefono, td.descripcion, p.codigo_Postal,p.fechaNac,p.soloEfectivo,p.numeroDocumento FROM Proveedor p INNER JOIN TipoDocumento td ON p.id_tipo_dni=td.id_tipo_Dni WHERE nombre like @Contiene order by " + orden;
                cmd.Parameters.Add(new SqlParameter("@Contiene", "%" + nombre + "%"));
                SqlDataReader dr = cmd.ExecuteReader();
               

                while (dr.Read())
                {
                    Proveedor p2 = new Proveedor();

                    p2.id_Proveedor = (int)dr["id_Proveedor"];
                    p2.nombre = dr["nombre"].ToString();
                    p2.direccion = dr["direccion"].ToString();
                    p2.mail = dr["mail"].ToString();
                    if (dr["codigo_Postal"] != DBNull.Value)
                    {
                        p2.codigo_Postal = (int)dr["codigo_Postal"];
                    }
                    if (dr["telefono"] != DBNull.Value)
                    {
                        p2.telefono = (int)dr["telefono"];
                    }     
                    if (dr["fechaNac"] != DBNull.Value)
                    {
                        p2.fechaNac = (DateTime)dr["fechaNac"];
                    }
                    p2.soloEfectivo = (Boolean)dr["soloEfectivo"];
                    p2.numeroDocumento = (int)dr["numeroDocumento"];
                    p2.descripcion = dr["descripcion"].ToString();


                    Proveedores.Add(p2);

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
            return Proveedores;
        }

        public static DataTable ObtenerDNI()
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            DataTable dt = new DataTable();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT * FROM TipoDocumento ";
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

        public static DataTable ObtenerProveedores()
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            DataTable dt = new DataTable();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT * FROM Proveedor ";
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
                cmd.CommandText = "SELECT IDENT_CURRENT('Proveedor')";
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

        public static void agregarProveedor(String nombre, string direccion, string mail, int telefono, int numeroDocumento, int id_tipo_dni, int codigo_Postal, DateTime fechaNac, Boolean soloEfectivo)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Proveedor (nombre, direccion, mail, telefono, id_tipo_dni, codigo_Postal, fechaNac, soloEfectivo, numeroDocumento ) VALUES (@nombreProveedor, @direccionProveedor, @mailProveedor, @telefonoProveedor, @tipoDniProveedor, @codigoProveedor ,@fechaNacProveedor, @soloEfectivoProveedor, @numeroDocumentoProveedor )"; //nombre del procedimiento q debe ir a buscar
                cmd.Parameters.Add(new SqlParameter("@nombreProveedor", nombre));
                cmd.Parameters.Add(new SqlParameter("@direccionProveedor", direccion));
                cmd.Parameters.Add(new SqlParameter("@mailProveedor", mail));
                cmd.Parameters.Add(new SqlParameter("@telefonoProveedor", telefono));
                cmd.Parameters.Add(new SqlParameter("@tipoDniProveedor", id_tipo_dni));
                cmd.Parameters.Add(new SqlParameter("@codigoProveedor", codigo_Postal));
                cmd.Parameters.Add(new SqlParameter("@fechaNacProveedor", fechaNac));
                cmd.Parameters.Add(new SqlParameter("@soloEfectivoProveedor", soloEfectivo));
                cmd.Parameters.Add(new SqlParameter("@numeroDocumentoProveedor",numeroDocumento));
                cmd.CommandType = CommandType.Text; 
                SqlDataReader dr = cmd.ExecuteReader();
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
        }

        public static void editarProveedor(int idProv, String nombre, string direccion, string mail, int telefono, int numeroDocumento, int id_tipo_dni, int codigo_Postal, DateTime fechaNac, Boolean soloEfectivo)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "update Proveedor set nombre=@nombreProveedor, direccion=@direccionProveedor, mail=@mailProveedor, telefono=@telefonoProveedor, id_tipo_dni=@tipoDniProveedor, codigo_Postal=@codigoProveedor, fechaNac=@fechaNacProveedor, soloEfectivo=@soloEfectivoProveedor, numeroDocumento=@numeroDocumentoProveedor where id_Proveedor = (@idProv) "; //nombre del procedimiento q debe ir a buscar
                cmd.Parameters.Add(new SqlParameter("@idProv", idProv));
                cmd.Parameters.Add(new SqlParameter("@nombreProveedor", nombre));
                cmd.Parameters.Add(new SqlParameter("@direccionProveedor", direccion));
                cmd.Parameters.Add(new SqlParameter("@mailProveedor", mail));
                cmd.Parameters.Add(new SqlParameter("@telefonoProveedor", telefono));
                cmd.Parameters.Add(new SqlParameter("@tipoDniProveedor", id_tipo_dni));
                cmd.Parameters.Add(new SqlParameter("@codigoProveedor", codigo_Postal));
                cmd.Parameters.Add(new SqlParameter("@fechaNacProveedor", fechaNac));
                cmd.Parameters.Add(new SqlParameter("@soloEfectivoProveedor", soloEfectivo));
                cmd.Parameters.Add(new SqlParameter("@numeroDocumentoProveedor", numeroDocumento));
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


        public static void eliminarProveedor(int id_proveedor)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            SqlConnection cn = new SqlConnection(CadenaConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from Proveedor where id_Proveedor = @IdProveedor";
                cmd.Parameters.Add(new SqlParameter("@IdProveedor", id_proveedor));
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

        //Para reportes:
        public static List<CompraReporte> getReporteCompraSinFiltro(string orden)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<CompraReporte> compraReporte = new List<CompraReporte>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, ixc.monto_Parcial, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo order by " + orden;
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    CompraReporte cpr = new CompraReporte();

                    cpr.fechaCompra = (DateTime)dr["fecha"];
                    cpr.montoTotal = (int)dr["montoTotal"];
                    cpr.montoParcial = (int)dr["monto_Parcial"];
                    cpr.nombreProveedor = dr["NombreProveedor"].ToString();
                    cpr.cantidad = (int)dr["cantidad"];
                    cpr.nombreInsumo = dr["NombreInsumo"].ToString();

                    compraReporte.Add(cpr);

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
            return compraReporte;
        }


        public static List<CompraReporte> getReporteCompraXFechaXCantidadXNombreProveedor(DateTime fecha, int cantidad, string nombreProveedor)
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);

            List<CompraReporte> compraReporte = new List<CompraReporte>();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Parameters.Clear();
               
                //Consulto por todas las combinaciones posibles
                if (fecha.Year.ToString().CompareTo("1")!=0 && cantidad > 0 && nombreProveedor.CompareTo("") != 0)
                {
                    cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, ixc.monto_Parcial, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE c.fecha = @fecha and ixc.cantidad= @cantidad and p.nombre= @nombreProveedor";
                }

                if (fecha.Year.ToString().CompareTo("1") != 0 && cantidad == 0 && nombreProveedor.CompareTo("") == 0)
                {
                    cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, ixc.monto_Parcial, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE c.fecha = @fecha";
                }

                if (fecha.Year.ToString().CompareTo("1") != 0 && cantidad > 0 && nombreProveedor.CompareTo("") == 0)
                {
                    cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, ixc.monto_Parcial, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE c.fecha = @fecha and ixc.cantidad= @cantidad";
                }

                if (fecha.Year.ToString().CompareTo("1") != 0 && cantidad == 0 && nombreProveedor.CompareTo("") != 0)
                {
                    cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, ixc.monto_Parcial, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE c.fecha = @fecha and p.nombre= @nombreProveedor";
                }

                if (fecha.Year.ToString().CompareTo("1") == 0 && cantidad > 0 && nombreProveedor.CompareTo("") != 0)
                {
                    cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, ixc.monto_Parcial, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE ixc.cantidad= @cantidad and p.nombre= @nombreProveedor";
                }

                if (fecha.Year.ToString().CompareTo("1") == 0 && cantidad > 0 && nombreProveedor.CompareTo("") == 0)
                {
                    cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, ixc.monto_Parcial, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE ixc.cantidad= @cantidad";
                }

                if (fecha.Year.ToString().CompareTo("1") == 0 && cantidad == 0 && nombreProveedor.CompareTo("") != 0)
                {
                    cmd.CommandText = "SELECT  c.fecha, c.montoTotal, p.nombre as NombreProveedor, ixc.cantidad, ixc.monto_Parcial, i.nombre as NombreInsumo FROM Compra c INNER JOIN Proveedor p ON c.id_Proveedor= p.id_Proveedor INNER JOIN InsumoXCompra ixc ON ixc.codigo_Compra = c.codigo INNER JOIN Insumo i ON ixc.id_Insumo = i.id_Insumo WHERE p.nombre= @nombreProveedor";
                }

                if (fecha.Year.ToString().CompareTo("1") != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                }
                if (cantidad > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                }
                if (nombreProveedor.CompareTo("") != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@nombreProveedor", nombreProveedor));
                }
                
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    CompraReporte cpr = new CompraReporte();

                    cpr.fechaCompra = (DateTime)dr["fecha"];
                    cpr.montoTotal = (int)dr["montoTotal"];
                    cpr.montoParcial = (int)dr["monto_Parcial"];
                    cpr.nombreProveedor = dr["NombreProveedor"].ToString();
                    cpr.cantidad = (int)dr["cantidad"];
                    cpr.nombreInsumo = dr["NombreInsumo"].ToString();

                    compraReporte.Add(cpr);

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
            return compraReporte;
        }

    }

}

