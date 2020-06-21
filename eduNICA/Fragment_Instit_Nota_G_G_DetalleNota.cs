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
    public class Fragment_Instit_Nota_G_G_DetalleNota : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Instit_Nota_G_G_detallenota detallenota;
        Android.Support.V7.Widget.Toolbar toolbar;
        public async override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            vlista = View.FindViewById<ListView>(Resource.Id.listView1_D_N);
            if (Global.detallenotas.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                detallenota = RestService.For<Interface_Instit_Nota_G_G_detallenota>("http://www.edunica.somee.com/api/NotasWS");

                //hacemos peticion mediante el metodo de la interface 
                List<Detallenota> D_N_lista = await detallenota.Detallenota();
                for (int i = 0; i < D_N_lista.Count; i++)
                {
                    Detallenota W = new Detallenota();
                    W.Descripcion = D_N_lista[i].Descripcion;
                    W.Id = D_N_lista[i].Id;
                    W.Orden = D_N_lista[i].Orden;
                    Global.detallenotas.Add(W);
                }
                vlista.Adapter = new Adapter_Instit_Lista_DetalleNota(Activity);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
                vlista.Adapter = new Adapter_Instit_Lista_DetalleNota(Activity);
            vlista.ItemClick += Vlista_ItemClick;
        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();

            Fragment_Instit_Nota_G_G_DetalleNota_VerNotaEstudiante verNotaEstudiante = new Fragment_Instit_Nota_G_G_DetalleNota_VerNotaEstudiante();
            Detallenota modulo = Global.detallenotas[e.Position];
            Global.iddetallenota = modulo.Id;
            toolbar.Title = modulo.Descripcion;
            ft.Replace(Resource.Id.relativeLayoutMenu, verNotaEstudiante).DisallowAddToBackStack().Commit();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Instit_Nota_G_G_DetalleNota, container, false);
        }
    }
}