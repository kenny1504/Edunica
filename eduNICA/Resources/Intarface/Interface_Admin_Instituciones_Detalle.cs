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
    public interface Interface_Admin_Instituciones_Detalle
    {
        [Get("/DatosInstitucionADMIN")]//tipo de peticion y nombre del metodo
        Task<UsuarioInstitucion> DatosInstituciones([Body] Busqueda dato); //metodo a utilizar dentro de la APP

        [Get("/DatosInstitucionADMIN2")]//tipo de peticion y nombre del metodo
        Task<DasboardWS> DatosInstitucion([Body] Busqueda inst); //metodo a utilizar dentro de la APP
    }
}