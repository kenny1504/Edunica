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
    public interface Interface_Admin_home
    {
        [Get("/Dashboard/Admin")]
        Task<List<Estudiantes_grados_Admin>> Total_Grados(); //metodo a utilizar dentro de la APP

        [Get("/Dashboard/Datos_Admin")]
        Task<DasboardWS> DatosAdmin(); //metodo a utilizar dentro de la APP
    }
}