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

        public static int Click;

        public static string cedula;//cedula para mostrar detalle usuario
        public static int idgrado;//variable global donde se alamcena el id del grado seleccionado
        public static int idgrupo;//variable global donde se almacena el id del grupo seleccionado
        public static int idestudiante;//variable global donde se almacena el id del grupo seleccionado
        public static int idasignatura;//variable global donde se almacena el id de la asignatura seleccionado
        public static int iddetallenota;//variable global donde se almacena el id del parcial seleccionado
        

        public static List<usuariosWS> usuariosWs = new List<usuariosWS>();
        public static List<Personas> usuariosWs_Datos = new List<Personas>();
        public static List<Estudiantes_grados> Lista_Grad = new List<Estudiantes_grados>();
        public static List<Estudiantes_grados_grafico> Lista_Grad_Graf = new List<Estudiantes_grados_grafico>();
        public static List<ListaEstudiantesWS> Lista_Estudi = new List<ListaEstudiantesWS>();
        public static List<DatosWS> datos_E = new List<DatosWS>();
        public static List<Estudiantes_grados_Admin> Lista_Grad_Admin = new List<Estudiantes_grados_Admin>();
        public static List<grupos_grados> grupos = new List<grupos_grados>();

        public static List<Asignaturas> materia = new List<Asignaturas>();
        public static List<Detallenota> detallenotas = new List<Detallenota>();
        public static List<Notas_Estudiante> notas_Estudiantes = new List<Notas_Estudiante>();
    }
}