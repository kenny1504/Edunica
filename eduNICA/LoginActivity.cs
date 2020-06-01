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
           
            try
            {
                // instancia de la clase user para enviar parametros a la peticion 
                userview userview = new userview();
                userview.password = pass.Text; 
                userview.username = user.Text;

                //hacemos peticion mediante el metodo de la interface 
                int result = await login.Autenticar(userview);

                Intent i = new Intent(this, typeof(SplashActivity));
                (i).PutExtra(SplashActivity.user, user.Text);
                StartActivity(i);


                Toast.MakeText(this, "" + result, ToastLength.Short).Show();

            }
            catch (System.Exception ex)
            {

                Toast.MakeText(this, "" + ex.Message, ToastLength.Short).Show();
            }
            
        }
        public override void OnBackPressed()
        {
            this.FinishAffinity();
        }
    }
}