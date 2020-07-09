﻿using System;
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
    public interface Interface_Admin_Usuarios
    {
        [Get("/UsuariosADMIN")]//tipo de peticion y nombre del metodo
        Task<List<UsuariosADMIN>> Usuarios(); //metodo a utilizar dentro de la APP
    }
}