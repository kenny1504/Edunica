using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EDMTDialog;
using eduNICA.Resources.Intarface;
using eduNICA.Resources.Model;
using Refit;

namespace eduNICA
{
    public class Fragment_Instit_Usuario : Fragment
    {
        ListView vlista; Context context;
        Android.Support.V7.Widget.Toolbar toolbar;
        //declaramos variable tipo interface
        Interface_Instit_Lista_Usuario_Docente usuario_Docente;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetHasOptionsMenu(true);//Agrega Cambios al Menu Actual
        }
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            base.OnActivityCreated(savedInstanceState);
            vlista = View.FindViewById<ListView>(Resource.Id.listView_usuario);

            //verificar si no hay lista en la clase
            if (Global.usuariosWs.Count == 0)
            {
                //// Instancia para Mostrar "Aviso" mientras carga la consulta  al servidor
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                usuario_Docente = RestService.For<Interface_Instit_Lista_Usuario_Docente>("http://www.edunica.somee.com/api/UsuariosWS");
               
                Busqueda Busqueda = new Busqueda();
                Busqueda.Id = Global.u.Id_Institucion;

                //hacemos peticion mediante el metodo de la interface 
                List<usuariosWS> usuariosvie = await usuario_Docente.Usuarios_Docentes(Busqueda);
                for (int i = 0; i < usuariosvie.Count; i++)
                {
                    usuariosWS W = new usuariosWS();
                    W.Cedula = usuariosvie[i].Cedula;
                    W.Id = usuariosvie[i].Id;
                    W.Institucion = usuariosvie[i].Institucion;
                    W.Nombre = usuariosvie[i].Nombre;
                    W.NombreDeUsuario = usuariosvie[i].NombreDeUsuario;
                    W.tipo = usuariosvie[i].tipo;
                    Global.usuariosWs.Add(W);
                }
                vlista.Adapter = new Adapter_Instit_Lista_Usuario(Activity);
                Esperar.Dismiss();//Cerrar Mensaje Cargando
            }
            else
                vlista.Adapter = new Adapter_Instit_Lista_Usuario(Activity);
            vlista.ItemClick += Vlista_ItemClick;
        }
        public void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            

            FragmentTransaction ft =Activity.FragmentManager.BeginTransaction();
            toolbar.Title="Detalle Usuario";
            Fragment_Instit_Usuario_Detalle usuario_Detalle = new Fragment_Instit_Usuario_Detalle();
            usuariosWS modulo = Global.usuariosWs[e.Position];
            Global.cedula = modulo.Cedula;
            ft.Replace(Resource.Id.relativeLayoutMenu, usuario_Detalle).DisallowAddToBackStack().Commit();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Instit_Usuario, container, false);
        }

        //Metodo para mostrar Botton 
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.menu_main, menu);
            menu.FindItem(Resource.Id.add_user).SetVisible(true); //Establece propiedad True
            menu.FindItem(Resource.Id.login_out).SetVisible(false);
            menu.FindItem(Resource.Id.credenciales).SetVisible(false);
            base.OnCreateOptionsMenu(menu, menuInflater); //Agrega cambios al Menu 
        }
    }
}