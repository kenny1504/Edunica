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
    public class Fragment_Docent_Home : Fragment
    {
        Interface_Docent_Home _Docent_Home;
        Context context;
        TextView txtinstituto, txtgrado, txtgrupo;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            txtinstituto = View.FindViewById<TextView>(Resource.Id.Docent_nombreinstitucion);
            txtgrado = View.FindViewById<TextView>(Resource.Id.Docent_grado);
            txtgrupo = View.FindViewById<TextView>(Resource.Id.Docent_grupo);
            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                .SetContext(context)
                .SetMessage("Cargando ...")
                .Build();
            Esperar.Show();
            _Docent_Home = RestService.For<Interface_Docent_Home>("http://www.edunica.somee.com/api/DashboardWS");//peticion

            BusquedaUD BusquedaUD = new BusquedaUD();
            BusquedaUD.Cedula = Global.u.Cedula;

            DasboarDocente ws = await _Docent_Home.DatosDocente(BusquedaUD);

            Global.Docente = ws;
            txtinstituto.Text = Global.Docente.Institucione;
            txtgrado.Text = Global.Docente.Grado;
            txtgrupo.Text = Global.Docente.Grupo;
            Esperar.Dismiss();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Docent_Home, container, false);
        }
    }
}