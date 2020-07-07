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
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;

namespace eduNICA
{
    public class Fragment_Docent_DetalleNota : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Instit_Nota_G_G_detallenota detallenota;
        Android.Support.V7.Widget.Toolbar toolbar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            Global.b_click = 1;//inicializamos en uno b_click para q al dar click sobre parcial muestre nota

            BottomNavigationView navigation = View.FindViewById<BottomNavigationView>(Resource.Id.navigation_detalle);
            seSetupDrawerContent(navigation);


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
            if (Global.b_click == 1)//ver notas
            {
                FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
                Fragment_Docent_Ver_Nota _Ver_Nota = new Fragment_Docent_Ver_Nota();
                Detallenota modulo = Global.detallenotas[e.Position];
                Global.iddetallenota = modulo.Id;
                toolbar.Title = modulo.Descripcion;
                ft.Replace(Resource.Id.relativeLayoutMenu, _Ver_Nota).DisallowAddToBackStack().Commit();
            }
            else if (Global.b_click == -1)//agregar notas
            {
                FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
                Fragment_Docent_Agregar_Nota _Agregar_Nota = new Fragment_Docent_Agregar_Nota();
                Detallenota modulo = Global.detallenotas[e.Position];
                Global.iddetallenota = modulo.Id;
                toolbar.Title = modulo.Descripcion;
                ft.Replace(Resource.Id.relativeLayoutMenu, _Agregar_Nota).DisallowAddToBackStack().Commit();
            }
        }
        public void seSetupDrawerContent(BottomNavigationView navigation)
        {
            navigation.NavigationItemSelected += (sender, e) =>
            {
                //e.Item.SetChecked(true);
                switch (e.Item.ItemId)
                {
                    case Resource.Id.nota_dashboard:
                        Global.b_click = 1;//al dar click sobre ver notas inicializamos en 1 para ver nota
                        break;
                    case Resource.Id.addnota_dashboard:
                        Global.b_click = -1;//al dar click sobre agregar notas inicializamos en -1 para agregar nota
                        break;
                }
            };
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Instit_Nota_G_G_DetalleNota, container, false);
        }
    }
}