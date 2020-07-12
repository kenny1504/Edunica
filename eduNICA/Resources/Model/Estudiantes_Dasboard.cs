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
    //listar grado con cantidad de estudiantes-institucion
    public class Estudiantes_grados
    {
        public int Grado { get; set; }
        public int Cantidad { get; set; }
        public int Idgrado { get; set; }
    }
    public class Estudiantes_grados_grafico
    {
        public int Grado { get; set; }
        public int Cantidad { get; set; }
    }
    //listar grupos segun grado selecionado-institucion
    public class grupos_grados
    {
        public int Idgrupo { get; set; }
        public int Cantidad { get; set; }
        public string Grupo { get; set; }
        
    } 
    //objeto buscar para lista estudiante-institucion
    public class estudianteWS
    {
        public int IdInstitucion { get; set; }
        public int IdGrado { get; set; }
        public int IdGrupo { get; set; }
        public int IdEstudiante { get; set; }

    }
    //listar estudiantes segun grupo-institucion
    public class ListaEstudiantesWS
    {
        public int Idestudiante { get; set; }
        public int IdMatricula { get; set; }
        public int Idgrado { get; set; }
        public int idGrupo { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }

    }   
    //datos de estudiante matriculado-institucion
    public class DatosWS
    {
        public string Nombre { get; set; }
        public string CodigoEstudiante { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Tutor { get; set; }
        public int TelefonoTutor { get; set; }

    }
    public class Estudiantes_grados_Admin
    {
        public int Grado { get; set; }
        public int cantidad { get; set; }
    }  
    public class BusquedaNota//id para mostrar notas nivel institucion
    {
        public int IdInstitucion { get; set; }
        public int IdGrado { get; set; }
        public int IdGrupo { get; set; }
        public int IdAsignatura { get; set; }
        public int IdDetalleNota { get; set; }
    }

    public class DasboardWS
    {
        public int Instituciones { get; set; }
        public int Docentes { get; set; }
        public int Estudiantes { get; set; }
        public int Matriculas { get; set; }
    }


    //Clase para mostrar lista de estudiantes (ADMINISTRADOR)
    public class EstudiantesADMIN
    {
        public string Institucion { get; set; }
        public string Nombre { get; set; }
        public int Id { get; set; }
    }

    //Clase para mostrar Datos de estudiante (ADMINISTRADOR)
    public class DatosEstudiantesADMIN
    {
        public string Institucion { get; set; }
        public string Nombre { get; set; }
        public string CodigoEstudiante { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Tutor { get; set; }
        public int TelefonoTutor { get; set; }
        public int Grado { get; set; }
        public string Grupo { get; set; }
    }

    public class DasboarDocente//datos a mostrar en el dasboard docente
    {
        public string Institucione { get; set; }
        public string Grupo { get; set; }
        public string Grado { get; set; }
    }

}