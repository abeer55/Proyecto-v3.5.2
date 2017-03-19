using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngelunEntidades
{
    public class Producto
    {
        public int id_Producto { get; set; }
        public String nombre { get; set; }
        public double? precio { get; set; }
        public int? id_Tipo_Producto { get; set; }
        public String nombre_Tipo_Producto { get; set; }
        public DateTime? fecha_Construccion { get; set; }
    }
}
