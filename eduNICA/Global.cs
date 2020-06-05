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
using eduNICA.Resources.Model;

namespace eduNICA
{
    public class Global //clase global para guardar usuario en session 
    {
        public static usuariosview u;
        public static List<usuariosWS> usuariosWs = new List<usuariosWS>();
        public static List<Personas> usuariosWs_Datos = new List<Personas>();
        public static List<Estudiantes_grados> Lista_Grad = new List<Estudiantes_grados>();
        public static List<grupos_grados> grupos = new List<grupos_grados>();
    }

}