using System;
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
    //objeto buscar grupo segun grado e institucion
    public class Grupos_ws
    {
        public int institucion { get; set; }
        public int Grado { get; set; }
    }
}