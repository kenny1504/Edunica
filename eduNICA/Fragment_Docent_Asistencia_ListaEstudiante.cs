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
    public class Fragment_Docent_Asistencia_ListaEstudiante : Fragment
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        ListView vlista; Context context; //Instalcia de context
        Button button_guardar;
        Interface_Docent_Estudiantes _Estudiantes;
        Interface_Docent_Asistencia_Add Docent_Asistencia_Add;
        TextView textfecha;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            button_guardar = View.FindViewById<Button>(Resource.Id.button_Guardar_Asistencia);
            button_guardar.Click += Button_guardar_Click;
            Global.ListaAsistencias.Clear();
            textfecha = View.FindViewById<TextView>(Resource.Id.textView5);
            textfecha.Text = DateTime.Today.ToShortDateString();
            vlista = View.FindViewById<ListView>(Resource.Id.listView_Asistencia_Estudi);//vinculamos al listview del layout
            if (Global.ListaAsistencias.Count == 0 && Global._Asistencia.Count==0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                //Establecemos la concexion con el servicio web API REST
                _Estudiantes = RestService.For<Interface_Docent_Estudiantes>("http://www.edunica.somee.com/api/AsistenciaWS");

                BuscarAsistencia BuscarAsistencia = new BuscarAsistencia();
                BuscarAsistencia.IdInstitucion = Global.u.Id_Institucion;
                BuscarAsistencia.Cedula = Global.u.Cedula;

                //hacemos peticion mediante el metodo de la interface 
                List<ListaAsistencia> A_lista = await _Estudiantes.Listar_Asistencia(BuscarAsistencia);

                for (int i = 0; i < A_lista.Count; i++)
                {
                    Lista_Estudiante_Asistencia W = new Lista_Estudiante_Asistencia();
                    W.Nombre = A_lista[i].Nombre;
                    W.IdMatricula = A_lista[i].IdMatricula;
                    W.CodigoEstudinte = A_lista[i].CodigoEstudinte;
                    Global._Asistencia.Add(W);
                }
                vlista.Adapter = new Adapter_Docent_Asistencia_Add(Activity);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
            {
                vlista.Adapter = new Adapter_Docent_Asistencia_Add(Activity);
            }                
        }       
        private async void Button_guardar_Click(object sender, EventArgs e)
        {
            Global.asistencias.Clear();
            List<Lista_Estudiante_Asistencia> asistencias = Global._Asistencia;
                    Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                   .SetContext(context)
                   .SetMessage("Guardando ...")
                   .Build();
                    Esperar.Show();
            //peticion
            Docent_Asistencia_Add = RestService.For<Interface_Docent_Asistencia_Add>("http://www.edunica.somee.com/api/AsistenciaWS");
                for (int i = 0; i < asistencias.Count; i++)
                {
                    Asistencia asistencia = new Asistencia();
                    asistencia.Fecha = DateTime.Today; //Convert.ToDateTime(textfecha.Text);//fecha actual
                    if (asistencias[i].estado == false)
                        asistencia.Estado = 0;
                    else
                        asistencia.Estado = 1;
                    asistencia.IdMatricula = asistencias[i].IdMatricula;

                    Global.asistencias.Add(asistencia);
                }
            int asis = await Docent_Asistencia_Add.Agregar_Asistencia(Global.asistencias);
            Esperar.Dismiss();
            if (asis == 1)
            {
                Toast.MakeText(Activity, "Guardado Con Exito", ToastLength.Short).Show();
                toolbar.Title = Global.u.Institucion;
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Docent_Home());
                fragment.DisallowAddToBackStack().Commit();
            }               
            else
            {
                AlertDialog alert = new AlertDialog.Builder(context).Create();
                alert.SetTitle("Aviso!");
                alert.SetIcon(Resource.Drawable.warning);
                alert.SetMessage("La Asistencia ya ha sido Realizada!");
                alert.SetButton("Aceptar", (a, b) =>
                {
                    alert.Dismiss();
                });
                alert.Show();
                //Toast.MakeText(Activity, "Asistencia ya Realizada", ToastLength.Short).Show();
                toolbar.Title = Global.u.Institucion;
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Docent_Home());
                fragment.DisallowAddToBackStack().Commit();
            }               
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Docent_Asistencia_Agregar, container, false);
        }
    }
}