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
    public class Estudiantes_grados
    {
        public int Grado { get; set; }
        public int cantidad { get; set; }
    }
    public class grupos_grados
    {
        public string Grupo { get; set; }
        public int cantidad { get; set; }
    }
    public class Grupos_ws
    {
        public int id_grado { get; set; }
        public int id_intituto { get; set; }
    }
}