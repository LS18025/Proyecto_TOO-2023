using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Asignacion_Tarea
    {
        public int idAsignacion { get; set; }
        public Usuario idUsuario { get; set; }
        public Tarea idTarea { get; set; }
    }
}
