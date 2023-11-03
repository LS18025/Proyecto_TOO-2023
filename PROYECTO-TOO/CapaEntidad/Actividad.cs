using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Actividad
    {
        public int idActividad { get; set; }
        public int idProyecto { get; set; }
        public Proyecto idProyect { get; set; }
        public string nombreAct { get; set; }
        public string descripcionAct { get; set; }
        
    }
}
