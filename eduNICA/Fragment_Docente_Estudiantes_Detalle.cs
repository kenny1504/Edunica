using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using eduNICA.Resources.Model;
using Refit;
using eduNICA.Resources.Intarface;
using EDMTDialog;

namespace eduNICA
{
    public class Fragment_Docente_Estudiantes_Detalle : Fragment
    {
        Interface_Docent_Estudiantes_Detalle _Estudiantes_Detalle;
        Context context;
        TextView txtnombre, txtcodigo, txtsexo, txtfecha, txttutor, txtdireccion, txttelefonotutor;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            txtcodigo = View.FindViewById<TextView>(Resource.Id.textView_codigo_estudiante);
            txtnombre = View.FindViewById<TextView>(Resource.Id.textView_nombreestudiante);
            txttutor = View.FindViewById<TextView>(Resource.Id.textView_nombre_tutor);
            txtdireccion = View.FindViewById<TextView>(Resource.Id.textView_direccionE);
            txtfecha = View.FindViewById<TextView>(Resource.Id.textView_fechanacimientoE);
            txtsexo = View.FindViewById<TextView>(Resource.Id.textView_sexoE);
            txttelefonotutor = View.FindViewById<TextView>(Resource.Id.textView_telefono_tutor);

            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                .SetContext(context)
                .SetMessage("Cargando ...")
                .Build();
            Esperar.Show();
            Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

            //Establecemos la concexion con el servicio web API REST
            _Estudiantes_Detalle = RestService.For<Interface_Docent_Estudiantes_Detalle>("http://www.edunica.somee.com/api/EstudiantesWS");

            Busqueda Busqueda = new Busqueda();
            Busqueda.Id = Global.idmatricula;

            //hacemos peticion mediante el metodo de la interface 
            DatosEstudiantesADMIN Edatos = await _Estudiantes_Detalle.DatosEstudianteDocente(Busqueda);
            Global.datosEstudiantesADMINs = Edatos;
            txtnombre.Text = Global.datosEstudiantesADMINs.Nombre;
            txtcodigo.Text = Global.datosEstudiantesADMINs.CodigoEstudiante;
            txtsexo.Text = Global.datosEstudiantesADMINs.Sexo;
            txtfecha.Text = Global.datosEstudiantesADMINs.FechaNacimiento.ToString();
            txtdireccion.Text = Global.datosEstudiantesADMINs.Direccion;
            txttutor.Text = Global.datosEstudiantesADMINs.Tutor;
            txttelefonotutor.Text = Global.datosEstudiantesADMINs.TelefonoTutor.ToString();
            Esperar.Dismiss();//cerrar mensaje
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Instit_Matricula_Grado_Grupo_Estudiante_Detalle, container, false);
        }
    }
}