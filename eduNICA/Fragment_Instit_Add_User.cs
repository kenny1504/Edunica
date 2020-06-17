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
    public class Fragment_Instit_Add_User : Fragment
    {
        Context context;
        Android.Support.V7.Widget.Toolbar toolbar;
        EditText txtcedula, txtuser, txtpassword;Button btnsave;
        //declaramos variable tipo interface
        Interface_Instit_Add_User Add_User;
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            base.OnActivityCreated(savedInstanceState);


            txtcedula = View.FindViewById<EditText>(Resource.Id.cedula15);
            txtuser = View.FindViewById<EditText>(Resource.Id.useradd15);
            txtpassword = View.FindViewById<EditText>(Resource.Id.password1);
            btnsave = View.FindViewById<Button>(Resource.Id.saveuser1);

            btnsave.Click += Btnsave_Click;
            
        }
        private async void Btnsave_Click(object sender, EventArgs e)
        {
            //// Instancia para Mostrar "Aviso" mientras carga la consulta  al servidor
            //Android.Support.V7.App.AlertDialog.Builder Alerta = new Android.Support.V7.App.AlertDialog.Builder(Activity);
            //Alerta.SetTitle("Desea Guardar Usuario");
            //Alerta.SetMessage("Haga Click");
            //Alerta.SetCancelable(true);
                

            Usuarios usuarios = new Usuarios();
            usuarios.Cedula = txtcedula.Text;
            usuarios.Usuario = txtuser.Text;
            usuarios.Contraseña = txtpassword.Text;
            usuarios.IdInstitucion = Global.u.Id_Institucion;

            Add_User = RestService.For<Interface_Instit_Add_User>("http://www.edunica.somee.com/api/UsuariosWS");

            usuariosWS user = await Add_User.Registro_Usuario_Docente(usuarios);
            if (user.Id == -1)
            {
                Toast.MakeText(Activity, "Docente ya existe", ToastLength.Long).Show();
            }
            else if (user.Id == 0)
            {
                Toast.MakeText(Activity, "Docente no existe", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(Activity, "Guardado con exito", ToastLength.Long).Show();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Instit_User_Add, container, false);
        }
    }
}