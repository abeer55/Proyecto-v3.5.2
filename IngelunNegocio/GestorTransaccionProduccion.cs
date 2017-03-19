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
    public class GestorTransaccionProduccion
    {

        public static bool generarProduccion(DataTable detalle, double total, DateTime fecha)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            SqlTransaction transaction;
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            transaction = cn.BeginTransaction("Transaciton_Produccion");
            cmd.Connection = cn;
            cmd.Transaction = transaction;

            
           

            try
            {
                cmd.Parameters.Clear();
                //Primero, inserto en la tabla compra

                cmd.CommandText = "INSERT INTO Produccion (fecha) VALUES (@fecha)";
                cmd.Parameters.Add(new SqlParameter("@fecha", fecha));

                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Close();

                cmd.Parameters.Clear();

                //Segundo, recupero el id de la compra agregada
                cmd.CommandText = "SELECT MAX(id_Produccion) FROM Produccion";
                int idProduccion = Convert.ToInt32(cmd.ExecuteScalar());

                //Tercero, inserto en la tabla InsumoXCompra
                foreach (DataRow fila in detalle.Rows)
                {
                    cmd.CommandText = "INSERT INTO ProductosXproduccion  (id_Produccion, id_Producto, cantidad) VALUES (@id_Produccion, @id_Producto, @cantidad)";
                    cmd.Parameters.Add(new SqlParameter("@id_Produccion", idProduccion));
                    cmd.Parameters.Add(new SqlParameter("@id_Producto", int.Parse(fila[0].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@cantidad", int.Parse(fila[2].ToString())));
                    //cmd.Parameters.Add(new SqlParameter("@subTotal", int.Parse(fila[4].ToString())));

                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr2 = cmd.ExecuteReader();
                    dr2.Close();

                    cmd.Parameters.Clear();
                }



                
                //Cuarto, actualizo el stock de cada insumo.
                foreach (DataRow fila in detalle.Rows)
                {
                    cmd.CommandText = "SELECT id_Insumo, cantidad_Insumo FROM InsumoXproducto WHERE id_Producto=@id_Producto";
                    cmd.Parameters.Add(new SqlParameter("@id_Producto", int.Parse(fila[0].ToString())));
                    SqlDataReader dr3 = cmd.ExecuteReader();

                    while (dr3.Read())
                    {

                        int idInsumo= (int)dr3["id_Insumo"];
                        int cantidadInsumo = (int)dr3["cantidad_Insumo"];
                        SqlConnection cn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                        cn2.Open();
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = cn2;
                       // cmd2.Transaction = transaction;
                        cmd2.CommandText = "SELECT cantidad FROM StockInsumo WHERE id_Insumo=@id_Insumo";
                        cmd2.Parameters.Add(new SqlParameter("@id_Insumo", idInsumo));
                        int ultimStock = Convert.ToInt32(cmd2.ExecuteScalar());
                        cmd2.Parameters.Clear();

                       

                        cmd2.CommandText = "UPDATE StockInsumo set cantidad=@cantidad WHERE id_Insumo=@id_Insumo";
                        int stockNuevo2 = ultimStock - (int.Parse(fila[2].ToString())*cantidadInsumo);
                        cmd2.Parameters.Add(new SqlParameter("@cantidad", stockNuevo2));
                        cmd2.Parameters.Add(new SqlParameter("@id_Insumo", idInsumo));
                        if (stockNuevo2 < 0) throw new System.InvalidOperationException("Logfile cannot be read-only");
                        cmd2.ExecuteNonQuery();
                        cn2.Close();

                    }
                    dr3.Close();

                    //recupero su ultimo stock
                    cmd.CommandText = "SELECT cantidad FROM StockProducto WHERE id_Producto=@id_Producto";
                  //  cmd.Parameters.Add(new SqlParameter("@id_Producto", int.Parse(fila[0].ToString())));
                    int ultStock = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.Clear();

                    string jquery = "IF EXISTS (SELECT cantidad FROM StockProducto WHERE id_Producto=@id_Producto)";
                    jquery += " UPDATE StockProducto SET cantidad = @cantidad WHERE id_Producto = @id_Producto";
                    jquery += " ELSE ";
                    jquery += " INSERT INTO StockProducto(id_Producto, id_StockProducto, cantidad) VALUES(@id_Producto, @id_Producto , @cantidad)";

                    cmd.CommandText = jquery;
                        //"UPDATE StockProducto set cantidad=@cantidad WHERE id_Producto=@id_Producto";
                    int stockNuevo = ultStock + int.Parse(fila[2].ToString());
                    cmd.Parameters.Add(new SqlParameter("@cantidad", stockNuevo));
                    cmd.Parameters.Add(new SqlParameter("@id_Producto", int.Parse(fila[0].ToString())));


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
                    return false;
                }
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }


    }
}
