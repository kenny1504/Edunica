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
    public class Fragment_Docent_Ver_Nota : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Instit_Nota_G_G_VerNotaEstudiante verNotaEstudiante;
        Android.Support.V7.Widget.Toolbar toolbar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            vlista = View.FindViewById<ListView>(Resource.Id.listView_Notas);//vinculamos al listview del layout
            if (Global.notas_Estudiantes.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                verNotaEstudiante = RestService.For<Interface_Instit_Nota_G_G_VerNotaEstudiante>("http://www.edunica.somee.com/api/NotasWS");

                BusquedaNota BusquedaNota = new BusquedaNota();
                BusquedaNota.IdInstitucion = Global.u.Id_Institucion;
                BusquedaNota.IdGrado = Global.idgrado;
                BusquedaNota.IdGrupo = Global.idgrupo;
                BusquedaNota.IdAsignatura = Global.idasignatura;
                BusquedaNota.IdDetalleNota = Global.iddetallenota;

                List<Notas_Estudiante> N_E_lista = await verNotaEstudiante.notas_Estudiantes(BusquedaNota);

                if (N_E_lista.Count == 0)//si no poseen los estudiantes notas
                {
                    Toast.MakeText(Activity, "No Se Encontraron Registros", ToastLength.Long).Show();
                    FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
                    toolbar.Title = "Parcial";
                    Fragment_Docent_DetalleNota detalleNota = new Fragment_Docent_DetalleNota();
                    ft.Replace(Resource.Id.relativeLayoutMenu, detalleNota).DisallowAddToBackStack().Commit();
                    Esperar.Dismiss();//cerramos msj cargando
                }
                else//listamos estudiante con su respectiva nota
                {
                    for (int i = 0; i < N_E_lista.Count; i++)
                    {
                        Notas_Estudiante W = new Notas_Estudiante();
                        W.Nombre = N_E_lista[i].Nombre;
                        W.Id = N_E_lista[i].Id;
                        W.Nota = N_E_lista[i].Nota;
                        Global.notas_Estudiantes.Add(W);
                    }
                    vlista.Adapter = new Adapter_Docente_Ver_Nota(Activity);
                    Esperar.Dismiss();//cerramos msj cargando
                }
            }
            else
                vlista.Adapter = new Adapter_Docente_Ver_Nota(Activity);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Docent_Nota_Ver, container, false);
        }
    }
}