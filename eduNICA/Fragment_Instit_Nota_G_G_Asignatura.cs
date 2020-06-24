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
using eduNICA.Resources.Model;
using Refit;
using eduNICA.Resources.Intarface;
using EDMTDialog;

namespace eduNICA
{
    public class Fragment_Instit_Nota_G_G_Asignatura : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Instit_Nota_G_G_Asignatura asignaturas;
        Android.Support.V7.Widget.Toolbar toolbar;
        public async override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            int grad = Global.idgrado;
            int grup = Global.idgrupo;
            vlista = View.FindViewById<ListView>(Resource.Id.listView_asignaturas);
            if (Global.materia.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                asignaturas = RestService.For<Interface_Instit_Nota_G_G_Asignatura>("http://www.edunica.somee.com/api/NotasWS");

                estudianteWS estudianteWS = new estudianteWS();
                estudianteWS.IdInstitucion = Global.u.Id_Institucion;
                estudianteWS.IdGrado = grad;
                estudianteWS.IdGrupo = grup;

                //hacemos peticion mediante el metodo de la interface 
                List<Asignaturas> A_lista = await asignaturas.Materias(estudianteWS);

                for (int i = 0; i < A_lista.Count; i++)
                {
                    Asignaturas W = new Asignaturas();
                    W.Nombre = A_lista[i].Nombre;
                    W.Id = A_lista[i].Id;
                    Global.materia.Add(W);
                }
                vlista.Adapter = new Adapter_Instit_Lista_Asignatura(Activity);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
                vlista.Adapter = new Adapter_Instit_Lista_Asignatura(Activity);
            vlista.ItemClick += Vlista_ItemClick;
        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
            toolbar.Title = "Parcial";
            Fragment_Instit_Nota_G_G_DetalleNota detalleNota = new Fragment_Instit_Nota_G_G_DetalleNota();
            Asignaturas modulo = Global.materia[e.Position];
            Global.idasignatura = modulo.Id;
            Global.asignatura = modulo.Nombre;
            ft.Replace(Resource.Id.relativeLayoutMenu, detalleNota).DisallowAddToBackStack().Commit();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Instit_Nota_G_G_Asignatura, container, false);
        }
    }
}