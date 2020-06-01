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
   public  interface LoginInterface
    {
        [Get("/login")] //tipo de peticion y nombre del metodo
        Task<int> Autenticar( [Body] userview user); //metodo a utilizar dentro de la app
    }
}