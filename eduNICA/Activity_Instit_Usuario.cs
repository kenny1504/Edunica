using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using eduNICA.Resources.Intarface;
using eduNICA.Resources.Model;
using Refit;

namespace eduNICA
{
    [Activity(Label = "Activity_Instit_Usuario")]
    public class Activity_Instit_Usuario : Activity
    {
        ListView vlista;
        //declaramos variable tipo interface
        Admin_Lista_Usuario_Docente usuario_Docente;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Instit_Usuario);

            //Establecemos la concexion con el servicio web API REST
            usuario_Docente = RestService.For<Admin_Lista_Usuario_Docente>("http://www.edunica.somee.com/api/UsuariosWS");

            vlista = FindViewById<ListView>(Resource.Id.listView1);

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

            vlista.Adapter = new Adapter_Lista_Usuario(this);
            vlista.ItemClick += Vlista_ItemClick;//al dar click sobre un usuario, para mostrar detalle
        }
        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent i = new Intent(this, typeof(Activity_Instit_Usuario_Detalle));
            usuariosWS modulo = Global.usuariosWs[e.Position];
            i.PutExtra("Cedula", modulo.Cedula);//pasar id de un activity a otro
            StartActivity(i);
        }
    }
}