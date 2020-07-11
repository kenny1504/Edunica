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
    public class Fragment_Admin_Usuarios : Fragment
    {
        EditText user_search;
        ListView vlista; Context context; //Instalcia de context
        Interface_Admin_Usuarios _Usuarios;
        Android.Support.V7.Widget.Toolbar toolbar;
        List<UsuariosADMIN> Buscar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            user_search = View.FindViewById<EditText>(Resource.Id.editText_filtrar_Admin_user);
            vlista = View.FindViewById<ListView>(Resource.Id.LV_Admin_usuario);//vinculamos al listview del layout
            user_search.TextChanged += Filtro_TextChanged;
            if (Global.usuariosADMINs.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                //Establecemos la concexion con el servicio web API REST
                _Usuarios = RestService.For<Interface_Admin_Usuarios>("http://www.edunica.somee.com/api/UsuariosWS");

                //hacemos peticion mediante el metodo de la interface 
                List<UsuariosADMIN> E_lista = await _Usuarios.Usuarios();
                for (int i = 0; i < E_lista.Count; i++)
                {
                    UsuariosADMIN W = new UsuariosADMIN();
                    W.Nombre = E_lista[i].Nombre;
                    W.Id = E_lista[i].Id;
                    W.Institucion = E_lista[i].Institucion;
                    Global.usuariosADMINs.Add(W);
                }
                vlista.Adapter = new Adapter_Admin_Usuarios(Activity, Global.usuariosADMINs);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
                vlista.Adapter = new Adapter_Admin_Usuarios(Activity,Global.usuariosADMINs);
            vlista.ItemClick += Vlista_ItemClick;
        }
        private void Filtro_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            Buscar = (from Usuario in Global.usuariosADMINs where Usuario.Nombre.Contains(user_search.Text) select Usuario).ToList<UsuariosADMIN>();
            vlista.Adapter = new Adapter_Admin_Usuarios(Activity,Buscar);
            vlista.ItemClick += Vlista_ItemClick1;
        }

        private void Vlista_ItemClick1(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
            toolbar.Title = "Informacion de Usuario";
            Fragment_Admin_Usuarios_Detalle _Usuarios_Detalle = new Fragment_Admin_Usuarios_Detalle();
            UsuariosADMIN modulo = Buscar[e.Position];
            Global.iddocente = modulo.Id;
            ft.Replace(Resource.Id.relativeLayoutMenu, _Usuarios_Detalle).DisallowAddToBackStack().Commit();
        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
            toolbar.Title = "Informacion de Usuario";
            Fragment_Admin_Usuarios_Detalle _Usuarios_Detalle = new Fragment_Admin_Usuarios_Detalle();
            UsuariosADMIN modulo = Global.usuariosADMINs[e.Position];
            Global.iddocente = modulo.Id;
            ft.Replace(Resource.Id.relativeLayoutMenu, _Usuarios_Detalle).DisallowAddToBackStack().Commit();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Admin_Usuarios, container, false);
        }
    }
}