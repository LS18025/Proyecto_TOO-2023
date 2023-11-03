using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Tarea
    {
        public int idTarea { get; set; }
        public Actividad idAct{ get; set; }
        public string nombreTarea { get; set; }
        public string descripcionT { get; set; }
        public DateTime fechaIniT { get; set; }
        public DateTime fechaFinT { get; set; }
    }
}
