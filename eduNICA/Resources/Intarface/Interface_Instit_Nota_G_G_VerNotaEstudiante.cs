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
    public interface Interface_Instit_Nota_G_G_VerNotaEstudiante
    {
        [Get("/VerNotas_Estudiantes")]
        Task<List<Notas_Estudiante>> notas_Estudiantes([Body] BusquedaNota dato);
    }
}