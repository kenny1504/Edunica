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
                    case Resource.Id.asistencia:
                        Global.DetalleStudent_DetalleAsistencia = 1;
                        Global.b_click = 1;//inicializamos a 1 para q al regresar a notas se muestre de inicio las notas
                        Global.Click = 1;//inicializacion de variable para hacer uso de grado y grupo tanto en matricula, nota y asistencia
                        Global.Lista_Estudi.Clear();//limpiamos lista estudiante
                        toolbar.Title = "Grados Academicos";
                        Fragment_Instit_Matricula_Grado int_grado_n1 = new Fragment_Instit_Matricula_Grado();
                        ft.Replace(Resource.Id.relativeLayoutMenu, int_grado_n1);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.docente:
                        //renombramos toolbal
                        toolbar.Title = "Lista Usuario Docente";
                        Global.DetalleStudent_DetalleAsistencia = 0;
                        Global.b_click = 1;//inicializamos a 1 para q al regresar a notas se muestre de inicio las notas
                        Global.Lista_Estudi.Clear();//limpiamos lista estudiante
                        //instaciamos el fragment a implementar
                        Fragment_Instit_Usuario int_user = new Fragment_Instit_Usuario();
                        ft.Replace(Resource.Id.relativeLayoutMenu, int_user);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.matricula:
                        Global.DetalleStudent_DetalleAsistencia = 0;
                        toolbar.Title = "Grados Academicos";
                        Global.Click = 1;//inicializacion de variable para hacer uso de grado y grupo tanto en matricula como en nota 
                        Global.Lista_Estudi.Clear();//limpiamos lista estudiante
                        Global.b_click = 1;//inicializamos a 1 para q al regresar a notas se muestre de inicio las notas

                        Fragment_Instit_Matricula_Grado int_grado = new Fragment_Instit_Matricula_Grado();
                        ft.Replace(Resource.Id.relativeLayoutMenu, int_grado);
                       ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.nota:
                        Global.DetalleStudent_DetalleAsistencia = 0;
                        Global.Click = 0;//inicializacion de variable para hacer uso de grado y grupo tanto en matricula como en nota 
                        Global.b_click = 1;//inicializamos a 1 para q al regresar a notas se muestre de inicio las notas
                        Global.Lista_Estudi.Clear();//limpiamos lista estudiante
                        toolbar.Title = "Grados Academicos";
                        Fragment_Instit_Matricula_Grado int_grado_n = new Fragment_Instit_Matricula_Grado();
                        ft.Replace(Resource.Id.relativeLayoutMenu, int_grado_n);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.asignatura:
                        //renombramos toolbal
                        Global.DetalleStudent_DetalleAsistencia = 0;
                        toolbar.Title = "Asignaturas";
                        Global.b_click = 1;//inicializamos a 1 para q al regresar a notas se muestre de inicio las notas
                        Global.Lista_Estudi.Clear();//limpiamos lista estudiante
                        //instaciamos el fragment a implementar
                        Fragment_Instit_Asignaturas _Instit_Asignaturas = new Fragment_Instit_Asignaturas();
                        ft.Replace(Resource.Id.relativeLayoutMenu, _Instit_Asignaturas);
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
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            
            //MATRICULA INSTIT ir a pantalla anterior Grados
            Fragment f = FragmentManager.FindFragmentById(Resource.Id.relativeLayoutMenu);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else if (f is Fragment_Instit_Matricula_Grado_Grupo)
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
            else if (f is Fragment_Instit_Matricula_Grado_Grupo_Estudiante_Detalle || f is Fragment_Docent_Asistencia_Estudiantes_Detalle)
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
            else if(f is Fragment_Instit_Usuario || f is Fragment_Instit_Matricula_Grado || f is Fragment_Instit_Asignaturas)
            {
                toolbar.Title = Global.u.Institucion;
                Global.usuariosWs.Clear();
                Global.materia.Clear();
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
            //ir de parcial hacia asignaturas-nota
            else if(f is Fragment_Instit_Nota_G_G_DetalleNota)
            {
                toolbar.Title = "Asignaturas";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                //Global.detallenotas.Clear();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Nota_G_G_Asignatura());
                fragment.DisallowAddToBackStack().Commit();
            }
            //regresar de agregar nota(lista de estudiantes) a menu de opciones de nota
            else if(f is Fragment_Instit_Nota_G_G_Estudiantes_Add_Nota)
            {
                toolbar.Title = "Parcial";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                Global.Lista_Estudi.Clear();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Nota_G_G_DetalleNota());
                fragment.DisallowAddToBackStack().Commit();
            }
            //ir de Asignatura hacia Grupos-nota
            else if (f is Fragment_Instit_Nota_G_G_Asignatura)
            {
                toolbar.Title = "Grupos";
                FragmentTransaction fragment = FragmentManager.BeginTransaction();
                Global.materia.Clear();
                fragment.Replace(Resource.Id.relativeLayoutMenu, new Fragment_Instit_Matricula_Grado_Grupo());
                fragment.DisallowAddToBackStack().Commit();
            }
            //salir de la app
            else if(f is Fragment_Instit_Home)
            {
                if(Salir)
                {
                    this.FinishAffinity();
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

                    Global.materia.Clear();//limpiar asignaturas
                    Global.detallenotas.Clear();//limpiar parciales
                    Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas
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

                //***************************************************************
                //***********agregar tambien a actualizar credenciales***********
                //***************************************************************
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

                Global.materia.Clear();//limpiar asignaturas
                Global.detallenotas.Clear();//limpiar parciales
                Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas

                StartActivity(i);
            }
            if(id == Resource.Id.add_user)
            {
                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                Fragment_Instit_Usuario_Add add = new Fragment_Instit_Usuario_Add();
                add.Show(ft, "dialog frament");
            }
            else if (id == Resource.Id.credenciales)
            {
                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                Fragment_Actualizar_Credenciales_Institucion add = new Fragment_Actualizar_Credenciales_Institucion();
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