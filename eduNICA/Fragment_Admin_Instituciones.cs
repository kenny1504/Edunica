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
    public class Fragment_Admin_Instituciones : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Admin_Instituciones _Instituciones;
        Android.Support.V7.Widget.Toolbar toolbar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            vlista = View.FindViewById<ListView>(Resource.Id.LV_Admin_instituciones);//vinculamos al listview del layout
            if (Global.usuarioInstitucions.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                //Establecemos la concexion con el servicio web API REST
                _Instituciones = RestService.For<Interface_Admin_Instituciones>("http://www.edunica.somee.com/api/UsuariosWS");

                //hacemos peticion mediante el metodo de la interface 
                List<Usuariosinstituciones> E_lista = await _Instituciones.INstituciones();

                for (int i = 0; i < E_lista.Count; i++)
                {
                    Usuariosinstituciones W = new Usuariosinstituciones();
                    W.Usuario = E_lista[i].Usuario;
                    W.Contraseña = E_lista[i].Contraseña;
                    W.Id = E_lista[i].Id;
                    W.IdInstitucion = E_lista[i].IdInstitucion;
                    Global.usuarioInstitucions.Add(W);
                }
                vlista.Adapter = new Adapter_Admin_Instituciones(Activity);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
                vlista.Adapter = new Adapter_Admin_Instituciones(Activity);
            vlista.ItemClick += Vlista_ItemClick;
        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
            toolbar.Title = "Informacion de Institucion";
            Fragment_Admin_Instituciones_Detalle _Admin_Instituciones_Detalle = new Fragment_Admin_Instituciones_Detalle();
            Usuariosinstituciones modulo = Global.usuarioInstitucions[e.Position];
            Global.id_Usuariosinstituciones = modulo.Id;
            ft.Replace(Resource.Id.relativeLayoutMenu, _Admin_Instituciones_Detalle).DisallowAddToBackStack().Commit();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Admin_Institucion, container, false);
        }
    }
}