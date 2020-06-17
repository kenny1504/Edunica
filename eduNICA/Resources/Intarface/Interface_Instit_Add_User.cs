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
    public interface Interface_Instit_Add_User
    {        
        [Post("/usuarios/AgregarDocente")]
        Task<usuariosWS> Registro_Usuario_Docente([Body] Usuarios docent);
    }
}