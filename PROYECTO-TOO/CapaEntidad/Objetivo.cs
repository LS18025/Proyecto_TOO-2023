using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Objetivo
    {
        public int idObjetivo { get; set; }
        public Proyecto idPro { get; set; }
        public string descripcionObj { get; set; }
    }
}
