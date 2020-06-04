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

namespace eduNICA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        Button btnEntrar;
        EditText user,pass;

        LoginInterface login;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login);

            //Establecemos la concexion con el servicio web API REST
            login = RestService.For<LoginInterface>("http://www.edunica.somee.com/api/AutenticarWS");

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
                        StartActivity(i);
                    }
                    else //si no encontro registros muestra toast
                    {
                        Toast.MakeText(this, "Favor verifique las credenciales", ToastLength.Short).Show();
                    }


                }
                catch (System.Exception ex) //captura algun error en la session
                {

                    Toast.MakeText(this, "" + ex.Message, ToastLength.Short).Show();
                }

            }
        }
        public override void OnBackPressed()
        {
            this.FinishAffinity();
        }
    }
}