using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EDMTDialog;
using eduNICA.Resources.Intarface;
using eduNICA.Resources.Model;
using Refit;

namespace eduNICA
{
    public class Fragment_Instit_Matricula_Grado_Grupo : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Instit_Matricula_Grados_Grupo grupos;
        Android.Support.V7.Widget.Toolbar toolbar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            int grad = Global.idgrado;//recuperamos grupo seleccionado
            vlista = View.FindViewById<ListView>(Resource.Id.listView_grupos);//vinculamos al listview del layout
            if (Global.grupos.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                //Establecemos la concexion con el servicio web API REST
                grupos = RestService.For<Interface_Instit_Matricula_Grados_Grupo>("http://www.edunica.somee.com/api/EstudiantesWS");
                Grupos_ws Grupos_ws = new Grupos_ws();
                Grupos_ws.Grado = grad;
                Grupos_ws.institucion = Global.u.Id_Institucion;

                //hacemos peticion mediante el metodo de la interface 
                List<grupos_grados> grupo_institucion = await grupos.Estudiante_Grado_Grupo(Grupos_ws);
                for (int i = 0; i < grupo_institucion.Count; i++)
                {
                    grupos_grados W = new grupos_grados();
                    W.Grupo = grupo_institucion[i].Grupo;
                    W.Cantidad = grupo_institucion[i].Cantidad;
                    W.Idgrupo = grupo_institucion[i].Idgrupo;
                    Global.grupos.Add(W);
                }
                vlista.Adapter = new Adapter_Instit_Lista_Grupo(Activity);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
                vlista.Adapter = new Adapter_Instit_Lista_Grupo(Activity);
            vlista.ItemClick += Vlista_ItemClick;
        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (Global.Click == 1)
            {
                FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
                toolbar.Title = "Estudiantes";
                Fragment_Instit_Matricula_Grado_Grupo_Estudiante estudiante = new Fragment_Instit_Matricula_Grado_Grupo_Estudiante();
                grupos_grados modulo = Global.grupos[e.Position];
                Global.idgrupo = modulo.Idgrupo;
                ft.Replace(Resource.Id.relativeLayoutMenu, estudiante).DisallowAddToBackStack().Commit();
            }
            else
            {
                FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
                toolbar.Title = "Asignaturas";
                Fragment_Instit_Nota_G_G_Asignatura asignatura = new Fragment_Instit_Nota_G_G_Asignatura();
                grupos_grados modulo = Global.grupos[e.Position];
                Global.idgrupo = modulo.Idgrupo;
                ft.Replace(Resource.Id.relativeLayoutMenu, asignatura).DisallowAddToBackStack().Commit();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            // Definimos layout que se mostrara
            return inflater.Inflate(Resource.Layout.Instit_Matricula_Grado_Grupo, container, false);

        }
    }
}