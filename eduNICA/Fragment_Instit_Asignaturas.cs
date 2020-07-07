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
    public class Fragment_Instit_Asignaturas : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Instit_Asignaturas _Instit_Asignaturas;
        public async override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            vlista = View.FindViewById<ListView>(Resource.Id.listView_Instit_asignaturas);
            Global.materia.Clear();


            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
            Esperar.Show();
            Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

            _Instit_Asignaturas = RestService.For<Interface_Instit_Asignaturas>("http://www.edunica.somee.com/api/AsignaturasWS");

            Busqueda Busqueda = new Busqueda();
            Busqueda.Id = Global.u.Id_Institucion;

            //hacemos peticion mediante el metodo de la interface 
            List<Asignaturas> A_lista = await _Instit_Asignaturas.Asignaturas_Intitucion(Busqueda);

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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.instit_Asignaturas, container, false);
        }
    }
}