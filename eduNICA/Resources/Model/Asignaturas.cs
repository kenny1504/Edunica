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
    public class Asignaturas//clase para listar asignatura Inst_N_G_G_Asignatura
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class Asignaturasdocente
    {
        public int Idgrupo { get; set; }
        public int Idgrado { get; set; }
        public int Idasignaturas { get; set; }
        public string Nombre { get; set; }

    }
}