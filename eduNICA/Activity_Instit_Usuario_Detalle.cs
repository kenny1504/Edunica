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
using eduNICA.Resources.Model;

namespace eduNICA
{
    [Activity(Label = "Activity_Instit_Usuario_Detalle")]
    public class Activity_Instit_Usuario_Detalle : Activity
    {
        usuariosWS usuario;
        TextView txtinstituto, txtnombre, txtcedula, txtuser, txttipo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Instit_Usuario_Detalle);
            int id = Intent.GetIntExtra("Id", 0);//recuperamos id del docente seleccionado
            usuario = Global.usuariosWs.Where(x => x.Id == id).FirstOrDefault();
            txtinstituto = FindViewById<TextView>(Resource.Id.textView_institucion);
            txtnombre = FindViewById<TextView>(Resource.Id.textView_nombredocente);
            txtcedula = FindViewById<TextView>(Resource.Id.textView_cedula);
            txtuser = FindViewById<TextView>(Resource.Id.textView_usuario);
            txttipo = FindViewById<TextView>(Resource.Id.textView_tipo);

            txtinstituto.Text = usuario.Institucion;
            txtcedula.Text = usuario.Cedula;
            txtnombre.Text = usuario.Nombre;
            txtuser.Text = usuario.NombreDeUsuario;
            txttipo.Text = usuario.tipo.ToString();
            // Create your application here
        }
    }
}