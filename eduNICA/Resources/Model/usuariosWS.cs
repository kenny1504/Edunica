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
    public class BusquedaUD//Recive cedula de la persona
    {
        public string Cedula { get; set; }
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
    public class Personas
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public char Sexo { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public int Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int IdInstitucion { get; set; }
    }
    public class UsuarioInstitucion//usada en actualizar credenciales Institucion
    {
        public int Id { get; set; }
        public string Institucion { get; set; }
        public string Direcccion { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }

    }
}