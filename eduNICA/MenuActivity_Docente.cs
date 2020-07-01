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
using Android.Views;
using Android.Widget;
using EDMTDialog;
using Java.Lang;

namespace eduNICA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MenuActivity_Docente : AppCompatActivity
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        DrawerLayout drawer;
        private bool Salir_Docente = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main_Docente);


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

            toolbar.Title = Global.u.Institucion; //estable la institucion a la que pertenece el usuario


            navUsername.Text = Global.u.Nombre; //establce el nombre del usuario
            //tipo de usuario
                navUserTpo.Text = "Docente";
            inicio();
        }
        void inicio()
        {
            FragmentTransaction ft = this.FragmentManager.BeginTransaction();
            //FragmentManager manager= getSupportFragmentManager();
            Fragment_Docent_Home _Docent_Home = new Fragment_Docent_Home();
            ft.Replace(Resource.Id.relativeLayoutMenu, _Docent_Home);
            ft.DisallowAddToBackStack();
            ft.Commit();
        }
        //Acciones al dar click sobre el Boton Hacia Atras
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            Fragment f = FragmentManager.FindFragmentById(Resource.Id.relativeLayoutMenu);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else if(f is Fragment_Docent_Ver_Nota)//regresar de lista de estudiante con nota, a parciales
            {
                toolbar.Title = "Parcial";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Docent_DetalleNota());
                Global.notas_Estudiantes.Clear();
                fragment.DisallowAddToBackStack().Commit();
            }
            //regresar de parciales a lista de asignaturas
            else if(f is Fragment_Docent_DetalleNota)
            {
                toolbar.Title = "Asignaturas";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Docent_Asignaturas());
                fragment.DisallowAddToBackStack().Commit();
            }
            //regresar de lista de asignaturas de docente a home
            else if(f is Fragment_Docent_Asignaturas)
            {
                toolbar.Title = Global.u.Institucion;
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Docent_Home());
                fragment.DisallowAddToBackStack().Commit();
            }
            //salir de la app
            else if (f is Fragment_Docent_Home)
            {
                if (Salir_Docente)
                {
                    Global.Asignaturasdocentes.Clear();//limpiar lista de asignatura de docente
                    Global._Asistencias.Clear();//limpiar lista de asistencia(estudiante)
                    Global.detallenotas.Clear();//limpiar parciales de nota
                    Global.notas_Estudiantes.Clear();//limpiar lista estudiantes con notas
                    this.FinishAffinity();
                }
                Salir_Docente = true;

                Toast.MakeText(this, "Click nuevamente ATRÁS para salir", ToastLength.Short).Show();

                var handler = new Handler();

                handler.PostDelayed(() => { Salir_Docente = false; }, 1800);
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

                //limpiar listas al cerrar sesion
                Global.Asignaturasdocentes.Clear();//limpiar lista de asignatura de docente
                Global._Asistencias.Clear();//limpiar lista de asistencia(estudiante)
                Global.detallenotas.Clear();//limpiar parciales de nota
                Global.notas_Estudiantes.Clear();//limpiar lista estudiantes con notas

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
                    case Resource.Id.asistencia_doc:
                        toolbar.Title = "Asistencia";

                        Global.Asignaturasdocentes.Clear();//limpiar lista de asignatura de docente                       
                        Global.detallenotas.Clear();//limpiar parciales de nota
                        Global.notas_Estudiantes.Clear();//limpiar lista estudiantes con notas

                        Fragment_Docent_Asistencia_ListaEstudiante estudiante = new Fragment_Docent_Asistencia_ListaEstudiante();
                        ft.Replace(Resource.Id.relativeLayoutMenu, estudiante);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.nota_doc:
                        toolbar.Title = "Asignaturas";

                        Global._Asistencias.Clear();//limpiar lista de asistencia(estudiante)

                        Fragment_Docent_Asignaturas _Docent_Asignaturas = new Fragment_Docent_Asignaturas();
                        ft.Replace(Resource.Id.relativeLayoutMenu, _Docent_Asignaturas);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.estudiante_doc:
                        break;
                }
                //lanzamiento de fragment
                ft.Commit();
                drawer.CloseDrawers();

            };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}