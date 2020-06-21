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
using EDMTDialog;

namespace eduNICA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MenuActivity : AppCompatActivity
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        DrawerLayout drawer;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);


            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            SetupDrawerContent(navigationView);

            View headerView = navigationView.GetHeaderView(0); 

            //Intancias para establecer el tipo de usuario y nombre de Usuario
            TextView navUsername = (TextView)headerView.FindViewById(Resource.Id.NombreUsuario);
            TextView navUserTpo = (TextView)headerView.FindViewById(Resource.Id.TipoUsuario);

            toolbar.Title = Global.u.Institucion; //establece la institucion a la que pertenece el usuario
            inicio();

            navUsername.Text = Global.u.Nombre; //establece el nombre del usuario

            //Define y pone el tipo de usuario que esta en session
                navUserTpo.Text = "Administrador";
        }
        private void SetupDrawerContent(NavigationView navigationView)
        {
            //// Instancia para Mostrar "Aviso" mientras carga la consulta  al servidor
            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(this)
                    .SetMessage("Cargando ...")
                    .Build();

            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);
                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.usuario:
                        
                        break;
                    case Resource.Id.matricula:
                       
                        break;
                }
                drawer.CloseDrawers();
            };
        }
        public override void OnBackPressed()
        {
            Fragment f = FragmentManager.FindFragmentById(Resource.Id.relativeLayoutMenu);
            if (f is Fragment_Admin_home)
            {
                //salir de la app
                this.FinishAffinity();
                Global.Lista_Grad_Admin.Clear();
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
        void inicio()
        {
            FragmentTransaction ft = this.FragmentManager.BeginTransaction();
            //FragmentManager manager= getSupportFragmentManager();
            Fragment_Admin_home grafica = new Fragment_Admin_home();
            ft.Replace(Resource.Id.relativeLayoutMenu, grafica);
            ft.DisallowAddToBackStack();
            ft.Commit();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

