using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngelunEntidades
{
    public class Proveedor
    {
        public int id_Proveedor { get; set; }
        public String nombre { get; set; }
        public String direccion { get; set; }
        public String mail { get; set; }
        public int? telefono { get; set; }
        public int? id_tipo_dni { get; set; }
        public int? codigo_Postal { get; set; }
        public DateTime? fechaNac { get; set; }
        public bool soloEfectivo { get; set; }
        public int numeroDocumento { get; set; }
        public String descripcion { get; set; }

    }
}
