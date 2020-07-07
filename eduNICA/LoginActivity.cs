using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using eduNICA.Resources.Intarface;
using Android.Util;
using Refit;
using eduNICA.Resources.Model;
using System.Collections.Generic;
using EDMTDialog;

namespace eduNICA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        Button btnEntrar;
        EditText user,pass;

        Interface_LoginInterface login;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login);

            //Establecemos la concexion con el servicio web API REST
            login = RestService.For<Interface_LoginInterface>("http://www.edunica.somee.com/api/AutenticarWS");

            //instalcias
            btnEntrar = FindViewById<Button>(Resource.Id.button1);
            user = FindViewById<EditText>(Resource.Id.user);
            pass = FindViewById<EditText>(Resource.Id.pass);
            btnEntrar.Click += BtnEntrar_Click; //Evento click 


        }

        private async void BtnEntrar_Click(object sender, System.EventArgs e)
        {

            if (pass.Text == "" && user.Text == "") //VALIDAMOS si los campos estan vacios manda mensaje
                Toast.MakeText(this, "Favor Complete los campos", ToastLength.Short).Show();
            else
            {
                //// Instancia para Mostrar "Aviso" mientras carga la consulta  al servidor
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(this)
                    .SetMessage("verificando ...")
                    .Build();

                if (!Esperar.IsShowing) //Mostramos mensaje 
                    Esperar.Show();
               Esperar.Window.SetLayout(1000,800); //aplica tamaño a la alerta

                // instancia de la clase user para enviar parametros a la peticion 
                userview userview = new userview();
                userview.password = pass.Text;
                userview.username = user.Text;

                try
                {
                    //hacemos peticion mediante el metodo de la interface 
                    List<usuariosview> usuariosview = await login.Autenticar(userview);


                    if (usuariosview.Count>0) //si la consulta retorna una coincidencia 
                    {
                        Global.u = usuariosview[0];
                        Intent i = new Intent(this, typeof(SplashActivity));
                        Global.user = user.Text;//para actualizar credenciales
                        Global.passw = pass.Text;//para actualizar credenciales
                        StartActivity(i);
                    }
                    else //si no encontro registros muestra toast
                    {
                        Toast.MakeText(this, "Favor verifique las credenciales", ToastLength.Short).Show();
                    }
                    if (Esperar.IsShowing) //Cerramos mensaje 
                        Esperar.Dismiss();

                }
                catch (System.Exception ex) //captura algun error en la session
                {

                    Toast.MakeText(this, "" + ex.Message, ToastLength.Short).Show();
                    if (Esperar.IsShowing) //Cerramos mensaje 
                        Esperar.Dismiss();
                }

            }
        }
        public override void OnBackPressed()
        {
            this.FinishAffinity();
        }
    }
}