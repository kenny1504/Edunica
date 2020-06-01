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

namespace eduNICA
{
    [Activity(Label = "SplashActivity")]
    public class SplashActivity : Activity
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        public static readonly string user;
        string tipouser;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.splash);
            tipouser = Intent.GetStringExtra(user);
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            // Create your application here
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            RunOnUiThread(() => { abrir(); });
            timer.Dispose();
        }
        void abrir()
        {
            if (tipouser == "Admin")
            {
                Intent i = new Intent(this, typeof(MenuActivity));
                StartActivity(i);
            }
            else
            {
                Intent i = new Intent(this, typeof(Activity_Docente));
                StartActivity(i);
            }

        }
    }
}