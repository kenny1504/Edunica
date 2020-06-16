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
    public class Fragment_Instit_Matricula_Grado_Grupo_Estudiante_Detalle : Fragment
    {
        Interface_Instit_Matricula_Grados_Grupo_Estudiante_Detalle detalle;
        DatosWS dato;
        Context context;
        TextView txtnombre,txtcodigo, txtsexo, txtfecha, txttutor, txtdireccion, txttelefonotutor ;
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
            detalle = RestService.For<Interface_Instit_Matricula_Grados_Grupo_Estudiante_Detalle>("http://www.edunica.somee.com/api/EstudiantesWS");

            estudianteWS estudianteWS = new estudianteWS();
            estudianteWS.IdInstitucion = Global.u.Id_Institucion;
            estudianteWS.IdEstudiante = Global.idestudiante;
            estudianteWS.IdGrado = Global.idgrado;
            estudianteWS.IdGrupo = Global.idgrupo;

            //hacemos peticion mediante el metodo de la interface 
            List<DatosWS> Edatos = await detalle.Datos_Institucion(estudianteWS);

            for (int i = 0; i < Edatos.Count; i++)
            {
                DatosWS W = new DatosWS();
                W.CodigoEstudiante = Edatos[i].CodigoEstudiante;
                W.Nombre = Edatos[i].Nombre;
                W.Sexo = Edatos[i].Sexo;
                W.Direccion = Edatos[i].Direccion;
                W.Tutor = Edatos[i].Tutor;
                W.TelefonoTutor = Edatos[i].TelefonoTutor;
                W.FechaNacimiento = Edatos[i].FechaNacimiento;
                Global.datos_E.Add(W);
            }
            dato = Global.datos_E.FirstOrDefault();
            txtnombre.Text = dato.Nombre;
            txtcodigo.Text = dato.CodigoEstudiante;
            txtsexo.Text = dato.Sexo.ToString();
            txtfecha.Text = dato.FechaNacimiento.ToString();
            txtdireccion.Text = dato.Direccion;
            txttutor.Text = dato.Tutor;
            txttelefonotutor.Text = dato.TelefonoTutor.ToString();
            Esperar.Dismiss();//cerrar mensaje
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            // Definimos layout que se mostrara
            return inflater.Inflate(Resource.Layout.Instit_Matricula_Grado_Grupo_Estudiante_Detalle, container, false);
        }
    }
}