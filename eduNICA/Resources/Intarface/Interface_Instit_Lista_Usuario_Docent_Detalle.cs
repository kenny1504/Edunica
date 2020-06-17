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
    public interface Interface_Instit_Lista_Usuario_Docente_Detalle
    {
        [Get("/usuarios/Docente")]
        Task<List<Personas>> Datos_Docente([Body] BusquedaUD u); //metodo a utilizar dentro de la APP
    }
}