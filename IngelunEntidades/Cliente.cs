using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngelunEntidades
{
    public class Cliente
    {

        public int id_Cliente { get; set; }
        public String nombre { get; set; }
        public String direccion { get; set; }
        public String mail { get; set; }
        public string ciudad { get; set; }
        public string provincia { get; set; }
        public string pais { get; set; }
        public int? id_tipo_dni { get; set; }
        public int? codigo_Postal { get; set; }       
        
        
    }
}
