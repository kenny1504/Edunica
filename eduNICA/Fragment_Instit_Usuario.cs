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
using eduNICA.Resources.Intarface;
using eduNICA.Resources.Model;
using Refit;

namespace eduNICA
{
    public class Fragment_Instit_Usuario : Fragment
    {
        ListView vlista;
        Android.Support.V7.Widget.Toolbar toolbar;
        //declaramos variable tipo interface
        Admin_Lista_Usuario_Docente usuario_Docente;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            Global.usuariosWs.Clear();
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            base.OnActivityCreated(savedInstanceState);
            //base.OnCreate(savedInstanceState);
            // Create your fragment here
            usuario_Docente = RestService.For<Admin_Lista_Usuario_Docente>("http://www.edunica.somee.com/api/UsuariosWS");
            vlista = View.FindViewById<ListView>(Resource.Id.listView1);
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
            vlista.Adapter = new Adapter_Lista_Usuario(Activity);
            vlista.ItemClick += Vlista_ItemClick;
        }
        public void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            

            FragmentTransaction ft =Activity.FragmentManager.BeginTransaction();
            toolbar.Title="Detalle Usuario";
            Fragment_Instit_Usuario_Detalle usuario_Detalle = new Fragment_Instit_Usuario_Detalle();
            usuariosWS modulo = Global.usuariosWs[e.Position];
            Global.cedula = modulo.Cedula;

            ft.Replace(Resource.Id.relativeLayoutMenu, usuario_Detalle).AddToBackStack(null).Commit();

            
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.Instit_Usuario, container, false);
        }
    }
}