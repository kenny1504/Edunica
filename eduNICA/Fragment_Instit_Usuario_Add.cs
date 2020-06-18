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
using Refit;
using eduNICA.Resources.Intarface;
using EDMTDialog;


namespace eduNICA
{
    class Fragment_Instit_Usuario_Add:DialogFragment
    {
        Context context;
        EditText txtcedula, txtuser, txtpassword; Button btnsave,btncancelar;
        //declaramos variable tipo interface
        Interface_Instit_Add_User Add_User;
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            txtcedula = View.FindViewById<EditText>(Resource.Id.cedula_new_user);
            txtuser = View.FindViewById<EditText>(Resource.Id.user_new_user);
            txtpassword = View.FindViewById<EditText>(Resource.Id.password_new_user);
            btnsave = View.FindViewById<Button>(Resource.Id.save_new_user);
            btncancelar = View.FindViewById<Button>(Resource.Id.cancelar_add_user);

            btncancelar.Click += Btncancelar_Click;
            btnsave.Click += Btnsave_Click;
        }

        private async void Btnsave_Click(object sender, EventArgs e)
        {
            if(txtcedula.Text!="" && txtpassword.Text!="" && txtuser.Text != "")
            {
                Usuarios usuarios = new Usuarios();
                usuarios.Cedula = txtcedula.Text;
                usuarios.Usuario = txtuser.Text;
                usuarios.Contraseña = txtpassword.Text;
                usuarios.IdInstitucion = Global.u.Id_Institucion;
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                        .SetContext(context)
                        .SetMessage("Verificando ...")
                        .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(700, 600); //aplica tamaño a la alert
                Add_User = RestService.For<Interface_Instit_Add_User>("http://www.edunica.somee.com/api/UsuariosWS");

                usuariosWS user = await Add_User.Registro_Usuario_Docente(usuarios);
                Esperar.Dismiss();
                if (user.Id == -1)
                {
                    Toast.MakeText(Activity, "Usuario ya existe", ToastLength.Long).Show();
                }
                else if (user.Id == 0)
                {
                    Toast.MakeText(Activity, "Docente no existe", ToastLength.Long).Show();
                }
                else
                {
                    Global.usuariosWs.Clear();
                    FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                    Fragment_Instit_Usuario int_user = new Fragment_Instit_Usuario();
                    ft.Replace(Resource.Id.relativeLayoutMenu, int_user);
                    ft.DisallowAddToBackStack();
                    ft.Commit();
                    Toast.MakeText(Activity, "Guardado con exito", ToastLength.Long).Show();
                    Dismiss();
                }
            }
            else
                Toast.MakeText(Activity, "Llene todos los campos, Porfavor", ToastLength.Long).Show();
        }

        private void Btncancelar_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            var view = inflater.Inflate(Resource.Layout.Instit_Usuario_Add, container, false);
            return view;
        }
       
    }
}