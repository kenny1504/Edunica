﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace eduNICA.Resources.Model
{
      public  class userview //clase usada para enviar parametros al servicio web (Logueo)
    {
        public string password { get; set; }
        public string username { get; set; }

    }
}