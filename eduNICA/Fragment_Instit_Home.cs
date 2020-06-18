using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using Microcharts.Droid;

using Microcharts;
using SkiaSharp;
using Entry = Microcharts.Entry;
using System.Reflection.Emit;

namespace eduNICA
{
    public class Fragment_Instit_Home : Fragment
    {
        Interface_Instit_home Instit_Home;
        ChartView chartView;
        Context context;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            chartView = View.FindViewById<ChartView>(Resource.Id.Chart_Inst);
            List<Entry> entries = new List<Entry>();
            if (Global.Lista_Grad_Graf.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alert
                Instit_Home = RestService.For<Interface_Instit_home>("http://www.edunica.somee.com/api/DashboardWS");//peticion
                Busqueda Busqueda = new Busqueda();
                Busqueda.Id = Global.u.Id_Institucion;

                //hacemos peticion mediante el metodo de la interface 
                List<Estudiantes_grados_grafico> estudiantes_Grados = await Instit_Home.Total_Grados(Busqueda);
                for (int i = 0; i < estudiantes_Grados.Count; i++)
                {
                    Estudiantes_grados_grafico W = new Estudiantes_grados_grafico();
                    W.Grado = estudiantes_Grados[i].Grado;
                    W.Cantidad = estudiantes_Grados[i].Cantidad;
                    Global.Lista_Grad_Graf.Add(W);
                }


                for (int i = 0; i < Global.Lista_Grad_Graf.Count; i++)
                {
                    Entry entry = new Entry(Global.Lista_Grad_Graf[i].Cantidad)
                    {
                        Color = SKColor.Parse("#35780B"),
                        Label = Global.Lista_Grad_Graf[i].Grado + " Grado",
                        ValueLabel = Global.Lista_Grad_Graf[i].Cantidad.ToString(),
                        TextColor = SKColor.Parse("#000000")
                    };
                    entries.Add(entry);
                }
                chartView.Chart = new BarChart() { Entries = entries, LabelTextSize = 25 };
                Esperar.Dismiss();
            }
            else
            {
                for (int i = 0; i < Global.Lista_Grad_Graf.Count; i++)
                {
                    Entry entry = new Entry(Global.Lista_Grad_Graf[i].Cantidad)
                    {
                        Color = SKColor.Parse("#35780B"),
                        Label = Global.Lista_Grad_Graf[i].Grado + " Grado",
                        ValueLabel = Global.Lista_Grad_Graf[i].Cantidad.ToString(),
                        TextColor = SKColor.Parse("#000000")
                    };
                    entries.Add(entry);
                }
                chartView.Chart = new BarChart() { Entries = entries, LabelTextSize = 25 };
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Instit_Home, container, false);
        }
    }
}