using System;
using Android;
using Android.App;
using Android.Content;
using Android.Opengl;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Webkit;
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

        private bool Salir = false;

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
            SetupDrawerContent(navigationView);

            //BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation_56);
            //navigation.Enabled = false;

            View headerView = navigationView.GetHeaderView(0);
            //Intancias para establecer el tipo de usuario y nombre de Usuario
            TextView navUsername = (TextView)headerView.FindViewById(Resource.Id.NombreUsuario);
            TextView navUserTpo = (TextView)headerView.FindViewById(Resource.Id.TipoUsuario);

            toolbar.Title = Global.u.Institucion; //estable la institucion a la que pertenece el usuario
            navUsername.Text = Global.u.Nombre; //establece el nombre del usuario
            //tipo de usuario
                navUserTpo.Text = "Institucion";
            inicio();
        }
        void inicio()
        {
            FragmentTransaction ft = this.FragmentManager.BeginTransaction();
            //FragmentManager manager= getSupportFragmentManager();
            Fragment_Instit_Home grafica = new Fragment_Instit_Home();
            ft.Replace(Resource.Id.relativeLayoutMenu, grafica);
            ft.DisallowAddToBackStack();
            ft.Commit();
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
                    case Resource.Id.docente:
                        //renombramos toolbal
                        toolbar.Title = "Lista Usuario Docente";
                        //instaciamos el fragment a implementar
                        Fragment_Instit_Usuario int_user = new Fragment_Instit_Usuario();
                        ft.Replace(Resource.Id.relativeLayoutMenu, int_user);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.matricula:
                        toolbar.Title = "Grados Academicos";
                        Global.Click = 1;
                        Fragment_Instit_Matricula_Grado int_grado = new Fragment_Instit_Matricula_Grado();
                        ft.Replace(Resource.Id.relativeLayoutMenu, int_grado);
                       ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.nota:
                        Global.Click = 0;
                        toolbar.Title = "Grados Academicos";
                        Fragment_Instit_Matricula_Grado int_grado_n = new Fragment_Instit_Matricula_Grado();
                        ft.Replace(Resource.Id.relativeLayoutMenu, int_grado_n);
                        ft.DisallowAddToBackStack();
                        break;
                }
                //lanzamiento de fragment
                ft.Commit();
                drawer.CloseDrawers();
                
            };

        }
        
        public override void OnBackPressed()
        {
            //MATRICULA INSTIT ir a pantalla anterior Grados
            Fragment f = FragmentManager.FindFragmentById(Resource.Id.relativeLayoutMenu);
            if (f is Fragment_Instit_Matricula_Grado_Grupo)
            {
                toolbar.Title = "Grados Academicos";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Matricula_Grado());
                Global.grupos.Clear();
                fragment.DisallowAddToBackStack().Commit();
            }
            //MATRICULA INSTIT  ir a pantalla anterior grupos
            else if (f is Fragment_Instit_Matricula_Grado_Grupo_Estudiante)
            {
                toolbar.Title = "Grupos";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Matricula_Grado_Grupo());
                Global.Lista_Estudi.Clear();
                fragment.DisallowAddToBackStack().Commit();
            }
            //MATRICULA INSTIT ir a pantalla anterior lista de estudiantes
            else if (f is Fragment_Instit_Matricula_Grado_Grupo_Estudiante_Detalle)
            {
                toolbar.Title = "Estudiantes";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                Global.datos_E.Clear();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Matricula_Grado_Grupo_Estudiante());
                fragment.DisallowAddToBackStack().Commit();
            }

            //ir a pantalla anterior de lista de usuarios
            else if (f is Fragment_Instit_Usuario_Detalle || f is Fragment_Instit_Usuario_Add)
            {
                toolbar.Title = "Lista Usuario Docente";
                Global.usuariosWs_Datos.Clear();
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Usuario());
                fragment.DisallowAddToBackStack().Commit();
            }
            //ir a home, si se encuentra en unos de los items
            else if(f is Fragment_Instit_Usuario || f is Fragment_Instit_Matricula_Grado)
            {
                toolbar.Title = Global.u.Institucion;
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Home());
                fragment.DisallowAddToBackStack().Commit();
            }

            //ir hacia parcial
            else if(f is Fragment_Instit_Nota_G_G_DetalleNota_VerNotaEstudiante)
            {
                toolbar.Title = "Parcial";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                Global.notas_Estudiantes.Clear();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Nota_G_G_DetalleNota());
                fragment.DisallowAddToBackStack().Commit();
            }
            //ir de parcial hacia asignaturas
            else if(f is Fragment_Instit_Nota_G_G_DetalleNota)
            {
                toolbar.Title = "Asignaturas";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                //Global.detallenotas.Clear();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Nota_G_G_Asignatura());
                fragment.DisallowAddToBackStack().Commit();
            }
            else if(f is Fragment_Instit_Nota_G_G_Asignatura)
            {
                toolbar.Title = "Grupos";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                Global.detallenotas.Clear();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Matricula_Grado_Grupo());
                fragment.DisallowAddToBackStack().Commit();
            }
            //salir de la app
            else if(f is Fragment_Instit_Home)
            {
                if(Salir)
                {
                    this.FinishAffinity();
                    Global.Lista_Grad_Graf.Clear();
                }
                Salir = true;

                Toast.MakeText(this, "Click nuevamente ATRÁS para salir", ToastLength.Short).Show();

                var handler = new Handler();

                handler.PostDelayed(() => { Salir = false; }, 1800);
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
                //limpiar lista de item Matricula
                Global.Lista_Grad.Clear();
                Global.grupos.Clear();
                Global.Lista_Estudi.Clear();                               
                Global.datos_E.Clear();

                //Limpiar lista de item Docentes
                Global.usuariosWs.Clear();
                Global.usuariosWs_Datos.Clear();

                //limpiar lista de datos de grafico
                Global.Lista_Grad_Graf.Clear();
                StartActivity(i);
            }
            if(id == Resource.Id.add_user)
            {
                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                Fragment_Instit_Usuario_Add add = new Fragment_Instit_Usuario_Add();
                add.Show(ft, "dialog frament");
            }
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}