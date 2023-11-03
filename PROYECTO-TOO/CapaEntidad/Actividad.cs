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
/*
     <form method="post" action="@Url.Action("crearActividad", "Proyecto")">
        <div class="card-body">

            <div class="form-group">
                <label for="tituloAct">Nombre</label>
                <input type="text" class="form-control" id="nombreAct" name="nombreAct" placeholder="Ingrese el nombre de la actividad.">
            </div>

            <div class="form-group">
                <label for="descripcion">descripcion</label>
                <input type="text" class="form-control" id="descripcionAct" name="descripcionAct" placeholder="Ingrese una descripcion de la actividad">
            </div>

            <div class="form-group">
                <label for="proyecto">proyecto al que pertenece</label>
                <input type="text" class="form-control" id="proyectoAct" name="proyectoAct" placeholder="ingrese el nombre del proyecto">
            </div>
        </div>
        <!-- /.card-body -->

        <div class="card-footer">
            <button type="submit" class="btn btn-success">Guardar</button>

        </div>
    </form>
 */