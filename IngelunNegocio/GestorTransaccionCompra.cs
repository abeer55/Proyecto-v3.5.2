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
    public class GestorTransaccionCompra
    {
        public static bool generarCompra(DataTable detalle, double total, int idProveedor, DateTime fecha)
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection cn = new SqlConnection(CadenaConexion);
            SqlTransaction transaction;
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            transaction = cn.BeginTransaction("Transaciton_Compra");
            cmd.Connection = cn;
            cmd.Transaction = transaction;
                       
            try
            {
                cmd.Parameters.Clear();
                //Primero, inserto en la tabla compra

                cmd.CommandText = "INSERT INTO Compra (fecha, montoTotal, id_Proveedor) VALUES (@fecha, @montoTotal, @id_Proveedor)"; 
                cmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                cmd.Parameters.Add(new SqlParameter("@montoTotal", total));
                cmd.Parameters.Add(new SqlParameter("@id_Proveedor", idProveedor));

                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Close();

                cmd.Parameters.Clear();

                //Segundo, recupero el id de la compra agregada
                cmd.CommandText = "SELECT MAX(codigo) FROM Compra";
                int idCompra = Convert.ToInt32(cmd.ExecuteScalar());

                //Tercero, inserto en la tabla InsumoXCompra
                foreach (DataRow fila in detalle.Rows)
                {
                    cmd.CommandText = "INSERT INTO InsumoXcompra (codigo_Compra, id_Insumo, cantidad, monto_Parcial) VALUES (@idCompra, @idInsumo, @cantidad, @subTotal)";
                    cmd.Parameters.Add(new SqlParameter("@idCompra", idCompra));
                    cmd.Parameters.Add(new SqlParameter("@idInsumo", int.Parse(fila[0].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@cantidad", int.Parse(fila[2].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@subTotal", int.Parse(fila[4].ToString())));
                    
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr2 = cmd.ExecuteReader();
                    dr2.Close();

                    cmd.Parameters.Clear();
                }
                
                //Cuarto, actualizo el stock de cada insumo.
                foreach (DataRow fila in detalle.Rows)
                {
                    //recupero su ultimo stock
                    cmd.CommandText = "SELECT cantidad FROM StockInsumo WHERE id_Insumo=@idInsumo";
                    cmd.Parameters.Add(new SqlParameter("@idInsumo", int.Parse(fila[0].ToString())));
                    int ultStock = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.Clear();

                    cmd.CommandText = "UPDATE StockInsumo set cantidad=@cantidad WHERE id_Insumo = @idInsumo";
                    int stockNuevo = ultStock + int.Parse(fila[2].ToString());
                    cmd.Parameters.Add(new SqlParameter("@cantidad", stockNuevo));
                    cmd.Parameters.Add(new SqlParameter("@idInsumo", int.Parse(fila[0].ToString())));
                    

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
