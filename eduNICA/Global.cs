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
        public static usuariosview u;//datos usuarios logueado
        public static string user;//variable para almacenar nombre de usuario para usarlo al actualizar las credenciales
        public static string passw;//variable para alamcenar contraseña de usuario logueado, para verificarla al actualizar las credenciales

        public static int Click;//variable para reutilizar grado, grupo y luego mostrar ya sea(nota,matricula o asistencia)

        public static string Direccion_Instit;//variable para almacenar la direccion de institucion para editarla

        public static string cedula;//cedula para mostrar detalle usuario
        public static int idgrado;//variable global donde se alamcena el id del grado seleccionado
        public static int idgrupo;//variable global donde se almacena el id del grupo seleccionado
        public static int idestudiante;//variable global donde se almacena el id del grupo seleccionado
        public static int idasignatura;//variable global donde se almacena el id de la asignatura seleccionado
        public static int iddetallenota;//variable global donde se almacena el id del parcial seleccionado
        public static int idmatricula;//variable global donde se almacena el id de la matricula del estudiante seleccionado

        public static int b_click;//variable para saber que opcion eligio(ver nota o agregar nota)
        public static string parcial;//guardar el parcial al agregar una nota para mostrarlo;
        public static string nombre_E_N;//guardar nombre de estudiante para mostrarlo al agregar nota;
        public static string asignatura;//guardar nombre de asignatura para mostrarlo en agregar nota;

        public static List<usuariosWS> usuariosWs = new List<usuariosWS>();//instancia de datos de usuario logueado
        public static List<Personas> usuariosWs_Datos = new List<Personas>();
        public static List<Estudiantes_grados> Lista_Grad = new List<Estudiantes_grados>();//instancia de grado con cantidad de estudiantes
        public static List<Estudiantes_grados_grafico> Lista_Grad_Graf = new List<Estudiantes_grados_grafico>();
        public static List<ListaEstudiantesWS> Lista_Estudi = new List<ListaEstudiantesWS>(); //instancia de lista estudiantes segun grupo-institucion
        public static List<DatosWS> datos_E = new List<DatosWS>();//instancia de datos personales de un estudiante
        public static List<Estudiantes_grados_Admin> Lista_Grad_Admin = new List<Estudiantes_grados_Admin>();//instancia de cantidad de estudiantes por Grado para grafico de administrador
        public static List<grupos_grados> grupos = new List<grupos_grados>();//instancia de lista de grupos

        public static List<Asignaturas> materia = new List<Asignaturas>();//instancia de cargar las asignaturas
        public static List<Detallenota> detallenotas = new List<Detallenota>();//instancia de parciales de nota
        public static List<Notas_Estudiante> notas_Estudiantes = new List<Notas_Estudiante>();//instancia de lista estudiante con nota

        public static DasboardWS ws;//instancia de total de intituciones,docente,estudiante y matriculas

        public static List<Asistencia> asistencias = new List<Asistencia>();//instancia para guardar asistencia

        public static List<Lista_Estudiante_Asistencia> _Asistencias = new List<Lista_Estudiante_Asistencia>();//lista temporal para guardar la asistencia
        public static List<Lista_Estudiante_Nota> _Notas = new List<Lista_Estudiante_Nota>();//lista temporal para guardar la nota

        public static List<ListaAsistencia> ListaAsistencias = new List<ListaAsistencia>();//listar estudiante de cada docente para asistencia

        public static List<Asignaturasdocente> Asignaturasdocentes = new List<Asignaturasdocente>();//lista de asignaturas de cada docente
    }
}