using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapaDatos;
using CapaEntidad;

namespace Gestion_Proyectos
{
    public class CN_Tarea
    {
        private GestionTareas objCapaDato = new GestionTareas();

        public List<Tarea> Listar() 
        {
            return objCapaDato.Listar();
        }

        public int RegistrarTarea(Tarea obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.idAct <= 0)
            {
                Mensaje = "El id de la actividad no puede quedar en blanco o ser menor o igual a 0";
            }
            else if (string.IsNullOrEmpty(obj.nombreTarea) || string.IsNullOrWhiteSpace(obj.nombreTarea))
            {
                Mensaje = "El nombre de la tarea no puede quedar vacio";
            }
            else if (string.IsNullOrEmpty(obj.descripcionT) || string.IsNullOrWhiteSpace(obj.descripcionT))
            {
                Mensaje = "La descripción de la tarea no puede quedar vacio";
            }
            else if (obj.fechaIniT < DateTime.UtcNow)
            {
                Mensaje = "La fecha de inicio de la tarea no puede quedar vacio o ser menor a este dia";
            }
            else if (obj.fechaFinT < DateTime.UtcNow)
            {
                Mensaje = "la fecha de finalización de la tarea no puede quedar vacio o ser menor a este dia";
            }

            if(string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.RegistrarTarea(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool EditarTarea(Tarea obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.idAct <= 0)
            {
                Mensaje = "El id de la actividad no puede ser menor o igual a 0";
            }
            else if (string.IsNullOrEmpty(obj.nombreTarea) || string.IsNullOrWhiteSpace(obj.nombreTarea))
            {
                Mensaje = "El nombre de la tarea no puede quedar vacio";
            }
            else if (string.IsNullOrEmpty(obj.descripcionT) || string.IsNullOrWhiteSpace(obj.descripcionT))
            {
                Mensaje = "La descripción de la tarea no puede quedar vacio";
            }
            else if (obj.fechaIniT < DateTime.UtcNow)
            {
                Mensaje = "La fecha de inicio de la tarea no puede quedar vacio o ser menor a este dia y debe escribirse utilizando un formato fecha con numeros y utilizando de separador los signos (/ o -)";
            }
            else if (obj.fechaFinT < DateTime.UtcNow)
            {
                Mensaje = "la fecha de finalización de la tarea no puede quedar vacio o ser menor a este dia y debe escribirse utilizando un formato fecha con numeros y utilizando de separador los signos (/ o -)";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.EditarTarea(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool EliminarTarea(int id, out string Mensaje)
        {
            return objCapaDato.EliminarTarea(id, out Mensaje);
        }
    }
}