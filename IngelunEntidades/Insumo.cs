using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngelunEntidades
{
    public class Insumo
    {
        public int id_Insumo { get; set; }
        public String nombre { get; set; }
        public double costo { get; set; }
        public int id_Tipo_Insumo { get; set; }
        public double volumen { get; set; }
        public bool esNacional { get; set; }
        public string nombreInsumo { get; set; }
        public int numeroSerie { get; set; }
    }
}