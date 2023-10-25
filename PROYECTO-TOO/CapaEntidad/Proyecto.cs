using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Proyecto
    {
        public int  idProyecto { get; set; }
        public Usuario oUsuario { get; set; }
        public string  nombreProyecto { get; set; }
        public string  descripcion { get; set; }
        public DateTime  fechaIni { get; set; }
        public DateTime  fechaFin { get; set; }
    }
}
