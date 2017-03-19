using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngelunEntidades
{
    public class DTOProduccionReporte
    {
        public int id_Produccion { get; set; }
        public DateTime fechaProduccion { get; set; }
        public String nombreProducto { get; set; }
        public String nombreTipoProducto { get; set; }
        public int cantidad { get; set; }
    }
}
