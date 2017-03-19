using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngelunEntidades
{
    public class CompraReporte
    {
        public DateTime fechaCompra { get; set; }
        
        public int montoTotal { get; set; }
        public int montoParcial { get; set; }
        public string nombreProveedor { get; set; }
        public int cantidad { get; set; }
        public string nombreInsumo { get; set; }
    }
}
