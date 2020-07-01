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
    public class Detallenota//detalle de nota Inst_N_G_G_parcial
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }
}