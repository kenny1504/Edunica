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
    public class Fragment_Docente_Estudiantes : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Docent_Estudiantes _Estudiantes;
        Android.Support.V7.Widget.Toolbar toolbar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            vlista = View.FindViewById<ListView>(Resource.Id.listView_admin_estudiante);
            if (Global.ListaAsistencias.Count == 0)
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
                    ListaAsistencia W = new ListaAsistencia();
                    W.Nombre = A_lista[i].Nombre;
                    W.IdMatricula = A_lista[i].IdMatricula;
                    W.CodigoEstudinte = A_lista[i].CodigoEstudinte;
                    Global.ListaAsistencias.Add(W);
                }
                vlista.Adapter = new Adapter_Docent_Asistencia_Estudiantes(Activity);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
                vlista.Adapter = new Adapter_Docent_Asistencia_Estudiantes(Activity);
            vlista.ItemClick += Vlista_ItemClick; ;
        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
            //toolbar.Title = "Detalle Estudiante";
            //Fragment_Docent_Asistencia_Estudiantes_Detalle _Estudiantes_Detalle = new Fragment_Docent_Asistencia_Estudiantes_Detalle();
            ListaAsistencia modulo = Global.ListaAsistencias[e.Position];
            Global.idmatricula = modulo.IdMatricula;
            //Global.Nombre = modulo.Nombre;
            //ft.Replace(Resource.Id.relativeLayoutMenu, _Estudiantes_Detalle).DisallowAddToBackStack().Commit();
            Toast.MakeText(Activity, "Proximamente Detalle Estudiante", ToastLength.Short).Show();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Admin_Estudiantes, container, false);
        }
    }
}