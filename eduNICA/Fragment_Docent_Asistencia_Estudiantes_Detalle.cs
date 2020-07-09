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
    public class Fragment_Docent_Asistencia_Estudiantes_Detalle : Fragment
    {
        Interface_Docent_Asistencia_Ver _Docent_Asistencia_Ver;
        Context context;
        TextView txtpresente, txtausente, txtnombre;
        ListView vlista;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            txtausente = View.FindViewById<TextView>(Resource.Id.docent_Asistencia_Ausente);
            txtnombre = View.FindViewById<TextView>(Resource.Id.docent_nombre_estudiante);
            txtpresente = View.FindViewById<TextView>(Resource.Id.docent_Asistencia_Presente);
            vlista = View.FindViewById<ListView>(Resource.Id.listView_Fechas_Ausente);

            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                .SetContext(context)
                .SetMessage("Cargando ...")
                .Build();
            Esperar.Show();
            txtnombre.Text = Global.Nombre;
            _Docent_Asistencia_Ver = RestService.For<Interface_Docent_Asistencia_Ver>("http://www.edunica.somee.com/api/AsistenciaWS");

            Busqueda Busqueda = new Busqueda();
            Busqueda.Id = Global.idmatricula;

            List<int> Edatos = await _Docent_Asistencia_Ver.Ver_Asistencia(Busqueda);
            List<string> vs= await _Docent_Asistencia_Ver.Fechas(Busqueda);
            vlista.Adapter = new Adapter_Docente_Asistencia_Fechas(Activity, vs);
            txtausente.Text = Edatos[0].ToString();
            txtpresente.Text = Edatos[1].ToString();
            Esperar.Dismiss();//cerrar mensaje
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Docent_Asistencia_Ver_Detalle, container, false);
        }
    }
}