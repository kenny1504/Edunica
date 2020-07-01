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
    public class Fragment_Docent_Asignaturas : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Docent_Asignaturas _Asignaturas;
        Android.Support.V7.Widget.Toolbar toolbar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            vlista = View.FindViewById<ListView>(Resource.Id.listView_asignaturas);

            if (Global.Asignaturasdocentes.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                _Asignaturas = RestService.For<Interface_Docent_Asignaturas>("http://www.edunica.somee.com/api/NotasWS");

                BusquedaUD BusquedaUD = new BusquedaUD();
                BusquedaUD.Cedula = Global.u.Cedula;

                //hacemos peticion mediante el metodo de la interface 
                List<Asignaturasdocente> A_lista = await _Asignaturas.Materias_Docente(BusquedaUD);

                for (int i = 0; i < A_lista.Count; i++)
                {
                    Asignaturasdocente W = new Asignaturasdocente();
                    W.Nombre = A_lista[i].Nombre;
                    W.Idasignaturas = A_lista[i].Idasignaturas;
                    W.Idgrado = A_lista[i].Idgrado;
                    W.Idgrupo = A_lista[i].Idgrupo;
                    Global.Asignaturasdocentes.Add(W);
                }
                vlista.Adapter = new Adapter_Docent_Lista_Asignatura(Activity);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
                vlista.Adapter = new Adapter_Docent_Lista_Asignatura(Activity);
            vlista.ItemClick += Vlista_ItemClick;
        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
            toolbar.Title = "Parcial";
            Fragment_Docent_DetalleNota detalleNota = new Fragment_Docent_DetalleNota();
            Asignaturasdocente modulo = Global.Asignaturasdocentes[e.Position];
            Global.idasignatura = modulo.Idasignaturas;
            Global.idgrado = modulo.Idgrado;
            Global.idgrupo = modulo.Idgrupo;
            Global.asignatura = modulo.Nombre;
            ft.Replace(Resource.Id.relativeLayoutMenu, detalleNota).DisallowAddToBackStack().Commit();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Instit_Nota_G_G_Asignatura, container, false);
        }
    }
}