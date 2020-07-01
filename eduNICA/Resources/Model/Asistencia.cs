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
    public class Asistencia
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public ulong Estado { get; set; }
        public int IdMatricula { get; set; }
    }
    public class Lista_Estudiante_Asistencia//lista para almacenar temporalmente los datos, para luego guardar asistencia
    {
        public string Nombre { get; set; }
        public string CodigoEstudinte { get; set; }
        public int IdMatricula { get; set; }
        public bool estado { get; set; }//checkbox asistencia
    }
    public class ListaAsistencia//listado de alumnos para almacenar asistencia
    {
        public int IdMatricula { get; set; }
        public string Nombre { get; set; }
        public string CodigoEstudinte { get; set; }
    }
    public class BuscarAsistencia
    {
        public int IdInstitucion { get; set; }
        public string Cedula { get; set; }
    }
}