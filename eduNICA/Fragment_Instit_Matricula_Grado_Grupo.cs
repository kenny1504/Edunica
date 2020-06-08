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
        Instit_Matricula_Grados_Grupo grupos;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            //limpiamos lista para no duplicar datos
            Global.grupos.Clear();
            base.OnActivityCreated(savedInstanceState);
            int grad = Global.grado;//recuperamos grupo seleccionado
            vlista =View.FindViewById<ListView>(Resource.Id.listView_grupos);//vinculamos al listview del layout

            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                .SetContext(context)
                .SetMessage("Cargando ...")
                .Build();
            Esperar.Show();
            Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

            //Establecemos la concexion con el servicio web API REST
            grupos = RestService.For<Instit_Matricula_Grados_Grupo>("http://www.edunica.somee.com/api/EstudiantesWS");
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
                Global.grupos.Add(W);
            }
            vlista.Adapter = new Adapter_Lista_Grupo(Activity);
            Esperar.Dismiss();//Cerramos mensaje
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            // Definimos layout que se mostrara en layout
            return inflater.Inflate(Resource.Layout.Inst_Matricula_Grado_Grupo, container, false);

        }
    }
}