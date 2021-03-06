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
    public interface Interface_Instit_home
    {
        [Get("/Dashboard/Institucion")]
        Task<List<Estudiantes_grados_grafico>> Total_Grados([Body] Busqueda inst); //metodo a utilizar dentro de la APP

        [Get("/Dashboard/Datos_Institucion")]
        Task<DasboardWS> DatosInstitucion([Body] Busqueda inst); //metodo a utilizar dentro de la APP
    }
}