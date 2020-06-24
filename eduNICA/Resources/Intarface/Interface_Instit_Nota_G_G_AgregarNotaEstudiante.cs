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
using Refit;
using eduNICA.Resources.Model;

namespace eduNICA.Resources.Intarface
{
    public interface Interface_Instit_Nota_G_G_AgregarNotaEstudiante
    {
        [Post("/AgregarNotas_Estudiantes")]
        Task<int> AgregarNota([Body] AgregarNota dato);
    }
}