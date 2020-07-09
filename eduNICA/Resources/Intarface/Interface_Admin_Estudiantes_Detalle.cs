using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using eduNICA.Resources.Model;
using Refit;

namespace eduNICA.Resources.Intarface
{
    public interface Interface_Admin_Estudiantes_Detalle
    {
        [Get("/DatosEstudiantes")]//tipo de peticion y nombre del metodo
        Task<DatosEstudiantesADMIN> Estudiantes([Body] Busqueda id); //metodo a utilizar dentro de la APP
    }
}