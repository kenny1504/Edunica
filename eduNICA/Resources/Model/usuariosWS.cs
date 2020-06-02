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
    public class Busqueda//Recive el id de la isntitucion
    {
        public int Id { get; set; }
    }
    public class usuariosWS //Manejo de datos Usuarios en el  WS
    {
        public string NombreDeUsuario { get; set; }
        public string Nombre { get; set; }
        public int Id { get; set; }
        public int tipo { get; set; }
        public String Cedula { get; set; }
        public String Institucion { get; set; }
    }
}