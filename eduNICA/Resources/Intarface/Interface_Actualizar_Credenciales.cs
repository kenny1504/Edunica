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
    public interface Interface_Actualizar_Credenciales
    {
        [Put("/editarUsuario")]
        Task<int> Editar_Usuario([Body] Usuarios usuarios);
    }
}