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
    public class Fragment_Admin_Estudiantes : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Admin_Estudiantes _Estudiantes;
        Android.Support.V7.Widget.Toolbar toolbar;
        public async override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            vlista = View.FindViewById<ListView>(Resource.Id.listView_admin_estudiante);//vinculamos al listview del layout
            if(Global.estudiantesADMINs.Count==0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                //Establecemos la concexion con el servicio web API REST
                _Estudiantes = RestService.For<Interface_Admin_Estudiantes>("http://www.edunica.somee.com/api/EstudiantesWS");

                //hacemos peticion mediante el metodo de la interface 
                List<EstudiantesADMIN> E_lista = await _Estudiantes.Estudiantes();

                for (int i = 0; i < E_lista.Count; i++)
                {
                    EstudiantesADMIN W = new EstudiantesADMIN();
                    W.Nombre = E_lista[i].Nombre;
                    W.Id = E_lista[i].Id;
                    W.Institucion = E_lista[i].Institucion;
                    Global.estudiantesADMINs.Add(W);
                }
                vlista.Adapter = new Adapter_Admin_Estudiantes(Activity);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
                vlista.Adapter = new Adapter_Admin_Estudiantes(Activity);
            vlista.ItemClick += Vlista_ItemClick;
        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
            toolbar.Title = "Informacion de Estudiante";
            Fragment_Admin_Estudiantes_Detalle _Estudiantes_Detalle = new Fragment_Admin_Estudiantes_Detalle();
            EstudiantesADMIN modulo = Global.estudiantesADMINs[e.Position];
            Global.idestudiante = modulo.Id;
            ft.Replace(Resource.Id.relativeLayoutMenu, _Estudiantes_Detalle).DisallowAddToBackStack().Commit();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Admin_Estudiantes, container, false);
        }
    }
}