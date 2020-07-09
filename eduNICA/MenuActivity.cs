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
        private bool Salir_Admin = false;
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
                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.admin_usuario:
                        Global.estudiantesADMINs.Clear();
                        Global.usuarioInstitucions.Clear();
                        //renombramos toolbal
                        Global.ws = null;
                        toolbar.Title = "Lista Usuarios";
                        //instaciamos el fragment a implementar
                        Fragment_Admin_Usuarios _Usuarios = new Fragment_Admin_Usuarios();
                        ft.Replace(Resource.Id.relativeLayoutMenu, _Usuarios);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.admin_estudiante:
                        Global.usuarioInstitucions.Clear();
                        Global.usuariosADMINs.Clear();
                        Global.ws = null;
                        //renombramos toolbal
                        toolbar.Title = "Lista Estudiantes";                      
                        //instaciamos el fragment a implementar
                        Fragment_Admin_Estudiantes _Estudiantes = new Fragment_Admin_Estudiantes();
                        ft.Replace(Resource.Id.relativeLayoutMenu, _Estudiantes);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.admin_institucion:
                        Global.usuariosADMINs.Clear();
                        Global.estudiantesADMINs.Clear();
                        Global.ws = null;
                        toolbar.Title = "Lista Intituciones";
                        //instaciamos el fragment a implementar
                        Fragment_Admin_Instituciones _Instituciones = new Fragment_Admin_Instituciones();
                        ft.Replace(Resource.Id.relativeLayoutMenu, _Instituciones);
                        ft.DisallowAddToBackStack();
                        break;
                }
                ft.Commit();
                drawer.CloseDrawers();
            };
        }
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            Fragment f = FragmentManager.FindFragmentById(Resource.Id.relativeLayoutMenu);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else if(f is Fragment_Admin_Estudiantes || f is Fragment_Admin_Usuarios || f is Fragment_Admin_Instituciones)
            {
                toolbar.Title = Global.u.Institucion;
                Global.estudiantesADMINs.Clear();
                Global.usuariosADMINs.Clear();
                Global.usuarioInstitucions.Clear();
                Global.ws = null;
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Admin_home());
                fragment.DisallowAddToBackStack().Commit();
            }
            else if (f is Fragment_Admin_Instituciones_Detalle)
            {
                toolbar.Title = "Lista Intituciones";
                Global.ws = null;
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Admin_Instituciones());
                fragment.DisallowAddToBackStack().Commit();
            }
            else if(f is Fragment_Admin_Estudiantes_Detalle)
            {
                toolbar.Title = "Lista Estudiante";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Admin_Estudiantes());
                fragment.DisallowAddToBackStack().Commit();
            }
            else if (f is Fragment_Admin_Usuarios_Detalle)
            {
                toolbar.Title = "Lista Usuarios";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Admin_Usuarios());
                fragment.DisallowAddToBackStack().Commit();
            }
            else if (f is Fragment_Admin_home)
            {
                //salir de la app
                if (Salir_Admin)
                {
                    this.FinishAffinity();
                    //limpiamos listas al salir
                    Global.Lista_Grad_Admin.Clear();
                    Global.estudiantesADMINs.Clear();
                    Global.usuariosADMINs.Clear();
                    Global.usuarioInstitucions.Clear();
                }
                Salir_Admin = true;

                Toast.MakeText(this, "Click nuevamente ATRÁS para salir", ToastLength.Short).Show();

                var handler = new Handler();

                handler.PostDelayed(() => { Salir_Admin = false; }, 1800);
                
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

                //limpiamos listar al cerrar sesion 
                Global.estudiantesADMINs.Clear();
                Global.Lista_Grad_Admin.Clear();
                Global.usuariosADMINs.Clear();
                Global.usuarioInstitucions.Clear();
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

