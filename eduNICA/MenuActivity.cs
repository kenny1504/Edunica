using System;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace eduNICA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MenuActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);


            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            View headerView = navigationView.GetHeaderView(0); 

            //Intancias para establecer el tipo de usuario y nombre de Usuario
            TextView navUsername = (TextView)headerView.FindViewById(Resource.Id.NombreUsuario);
            TextView navUserTpo = (TextView)headerView.FindViewById(Resource.Id.TipoUsuario);

            toolbar.Title = Global.u.Institucion; //estable la institucion a la que pertenece el usuario


            navUsername.Text = Global.u.Nombre; //establce el nombre del usuario

            //Define y pone el tipo de usuario que esta en session
                navUserTpo.Text = "Administrador";
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.login_out)
            {
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);
            }

            return true;
        }
        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.usuario)
            {
                Intent i = new Intent(this, typeof(Activity_Admin_Usuario));
                StartActivity(i);
            }
            else if (id == Resource.Id.nota)
            {
                Intent i = new Intent(this, typeof(Activity_Admin_Nota));
                StartActivity(i);
            }
            else if (id == Resource.Id.matricula)
            {
                Intent i = new Intent(this, typeof(Activity_Admin_Matricula));
                StartActivity(i);
            }
            else if (id == Resource.Id.estudiante)
            {
                Intent i = new Intent(this, typeof(Activity_Admin_Estudiante));
                StartActivity(i);
            }
            else if (id == Resource.Id.docente)
            {
                Intent i = new Intent(this, typeof(Activity_Admin_Docente));
                StartActivity(i);
            }
            else if (id == Resource.Id.asignatura)
            {
                Intent i = new Intent(this, typeof(Activity_Admin_Asignatura));
                StartActivity(i);
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

