using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngelunEntidades
{
    [Serializable()]
    public class VentaProducto
    {
        public string nombreCliente { get; set; }
        public DateTime fechaVenta { get; set; }
        public int montoTotal { get; set; }
        public string mailCliente { get; set; }
        public int cantidad { get; set; }
        public int id_Venta { get; set; }
        public int  montoParcial { get; set; }
        public string nombreProducto { get; set; }

    }
}
