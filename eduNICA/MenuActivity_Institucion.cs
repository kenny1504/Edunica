﻿using System;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using EDMTDialog;
using Java.Lang;

namespace eduNICA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MenuActivity_Institucion : AppCompatActivity//, NavigationView.IOnNavigationItemSelectedListener
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        DrawerLayout drawer;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main_Institucion);
            

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            //navigationView.SetNavigationItemSelectedListener(this);
            SetupDrawerContent(navigationView);


            View headerView = navigationView.GetHeaderView(0);
            //Intancias para establecer el tipo de usuario y nombre de Usuario
            TextView navUsername = (TextView)headerView.FindViewById(Resource.Id.NombreUsuario);
            TextView navUserTpo = (TextView)headerView.FindViewById(Resource.Id.TipoUsuario);

            toolbar.Title = Global.u.Institucion; //estable la institucion a la que pertenece el usuario


            navUsername.Text = Global.u.Nombre; //establce el nombre del usuario
            //tipo de usuario
                navUserTpo.Text = "Institucion";
        }
        //al dar click sobre un item del menu lateral
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
                    case Resource.Id.usuario:
                        //renombramos toolbal
                        toolbar.Title = "Lista Usuario Docente";
                        //instaciamos el fragment a implementar
                        Fragment_Instit_Usuario int_user = new Fragment_Instit_Usuario();
                        ft.Replace(Resource.Id.relativeLayoutMenu, int_user);
                        break;
                    case Resource.Id.matricula:
                        toolbar.Title = "Lista Usuario Docente";

                        toolbar.Title = "Grados Academicos";
                        Fragment_Instit_Matricula_Grado int_grado = new Fragment_Instit_Matricula_Grado();
                        ft.Replace(Resource.Id.relativeLayoutMenu, int_grado).AddToBackStack(null);
                        break;
                }
                //lanzamiento de fragment
                ft.Commit();
                drawer.CloseDrawers();
                
            };

        }
        public override void OnBackPressed()
        {
            /////////////////////////
            //////aqui estoy viendo lo de ir atras
            ///////////////////////
            ////////////////////////
            //BottomNavigationView navigation = (BottomNavigationView)FindViewById(Resource.Id.nav_view);
            //if (navigation.SelectedItemId==Resource.Id.usuario)
            //{
            //    //Process.KillProcess(Process.MyPid());
            //    //drawer.CloseDrawer(GravityCompat.Start);
            //}
            //else
            //{
            //    navigation.SelectedItemId = Resource.Id.matricula;
            //}
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
        //cerrar sesion
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
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}