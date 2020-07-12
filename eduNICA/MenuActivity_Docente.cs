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
            else if ( f is Fragment_Docent_Agregar_Nota)//regresar de lista de estudiante con nota, a parciales
            {
                toolbar.Title = "Parcial";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Docent_DetalleNota());
                Global.notas_Estudiantes.Clear();
                Global._Notas.Clear();
                fragment.DisallowAddToBackStack().Commit();
            }
            //regresar de parciales a lista de asignaturas
            else if (f is Fragment_Docent_DetalleNota)
            {
                toolbar.Title = "Asignaturas";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Docent_Asignaturas());
                fragment.DisallowAddToBackStack().Commit();
            }
            //regresar de lista de asignaturas de docente a home
            else if (f is Fragment_Docent_Asignaturas || f is Fragment_Docent_Asistencia || f is Fragment_Docente_Estudiantes)///////////////////pendiente ver
            {
                toolbar.Title = Global.u.Institucion;
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Docent_Home());

                Global.Asignaturasdocentes.Clear();
                Global.ListaAsistencias.Clear();
                Global.asistencias.Clear();

                fragment.DisallowAddToBackStack().Commit();
            }
            else if(f is Fragment_Docente_Estudiantes_Detalle)
            {
                toolbar.Title = "Estudiantes";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Docente_Estudiantes());
                fragment.DisallowAddToBackStack().Commit();
            }
            else if(f is Fragment_Docent_Asistencia_Estudiantes_Detalle)
            {

                toolbar.Title = "Asistencia";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Docent_Asistencia());
                fragment.DisallowAddToBackStack().Commit();
            }
            //salir de la app
            else if (f is Fragment_Docent_Home)
            {
                if (Salir_Docente)
                {
                    Global.Asignaturasdocentes.Clear();//limpiar lista de asignatura de docente
                    Global._Asistencia.Clear();//limpiar lista de asistencia(estudiante)
                    Global.detallenotas.Clear();//limpiar parciales de nota
                    Global.notas_Estudiantes.Clear();//limpiar lista estudiantes con notas

                    Global.ListaAsistencias.Clear();//lista de estudiantes
                    Global.asistencias.Clear();//limpiar lista luego de guardar asistencia
                    Global._Notas.Clear();//limpiamos lista temporal de nota
                    Global.Asignaturasdocentes.Clear();//limpiamos asignatura de docente al cerrar sesion
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

                //recordar agregar listas a cambiar credenciales
                //limpiar listas al cerrar sesion
                Global.Asignaturasdocentes.Clear();//limpiar lista de asignatura de docente
                Global._Asistencia.Clear();//limpiar lista de asistencia(estudiante)
                Global.detallenotas.Clear();//limpiar parciales de nota
                Global.notas_Estudiantes.Clear();//limpiar lista estudiantes con notas

                Global.ListaAsistencias.Clear();//lista de estudiantes
                Global.asistencias.Clear();//limpiar lista luego de guardar asistencia
                Global._Notas.Clear();//limpiamos lista temporal de nota
                Global.Asignaturasdocentes.Clear();//limpiamos asignatura de docente al cerrar sesion

                StartActivity(i);
            }
            else if(id ==Resource.Id.credenciales)
            {
                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                Fragment_Actualizar_Credenciales add = new Fragment_Actualizar_Credenciales();
                add.Show(ft, "dialog frament");
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
                        Global._Asistencia.Clear();//limpiar lista de asistencia(estudiante)
                        Fragment_Docent_Asistencia _Asistencia = new Fragment_Docent_Asistencia();
                        ft.Replace(Resource.Id.relativeLayoutMenu, _Asistencia);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.nota_doc:
                        toolbar.Title = "Asignaturas";

                        Global._Asistencia.Clear();//limpiar lista de asistencia(estudiante)

                        Fragment_Docent_Asignaturas _Docent_Asignaturas = new Fragment_Docent_Asignaturas();
                        ft.Replace(Resource.Id.relativeLayoutMenu, _Docent_Asignaturas);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.estudiante_doc:
                        toolbar.Title = "Estudiantes";
                        Global._Asistencia.Clear();//limpiar lista de asistencia(estudiante)
                        Global.ListaAsistencias.Clear();
                        Fragment_Docente_Estudiantes _Docente_Estudiantes = new Fragment_Docente_Estudiantes();
                        ft.Replace(Resource.Id.relativeLayoutMenu, _Docente_Estudiantes);
                        ft.DisallowAddToBackStack();
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