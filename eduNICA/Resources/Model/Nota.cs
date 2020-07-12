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
    public class AgregarNota//agregar nota desde institucion
    {
        public int IdMatricula { get; set; }
        public int IdDetalle { get; set; }
        public int IdAsigntura { get; set; }
        public int Nota { get; set; }
    }
    public class Notas_Estudiante//mostrar notas nivel institucion
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public string Nombre { get; set; }
    }

    public class VerNotaDocente//parametros para jalar notas docente
    {
        public string cedula { get; set; }
        public int id_detalle_Nota { get; set; }
        public int idMateria { get; set; }
    }
    public class NotasD//notas enviar
    {
        public NotasD(int idNota, int nota)
        {
            IdNota = new int[idNota];
            Nota = new int[nota];
        }

        public int[] IdNota { get; set; }
        public int[] Nota { get; set; }
    }
}