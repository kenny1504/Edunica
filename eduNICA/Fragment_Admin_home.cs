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
    public class Fragment_Admin_home : Fragment
    {
        Interface_Admin_home admin_Home;
        ChartView chartView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            chartView = View.FindViewById<ChartView>(Resource.Id.Chart_Admin);

            admin_Home = RestService.For<Interface_Admin_home>("http://www.edunica.somee.com/api/DashboardWS");//peticion
            //hacemos peticion mediante el metodo de la interface 
            List<Estudiantes_grados_Admin> estudiantes_Grados_Admins = await admin_Home.Total_Grados();
            for (int i = 0; i < estudiantes_Grados_Admins.Count; i++)
            {
                Estudiantes_grados_Admin W = new Estudiantes_grados_Admin();
                W.Grado = estudiantes_Grados_Admins[i].Grado;
                W.cantidad = estudiantes_Grados_Admins[i].cantidad;
                Global.Lista_Grad_Admin.Add(W);
            }
            List<Entry> entries = new List<Entry>();
            
                for (int i = 0; i < Global.Lista_Grad_Admin.Count; i++)
                {
                Entry entry = new Entry(Global.Lista_Grad_Admin[i].cantidad)
                {
                    Color = SKColor.Parse("#35780B"),
                    Label = Global.Lista_Grad_Admin[i].Grado + " Grado",
                    ValueLabel = Global.Lista_Grad_Admin[i].cantidad.ToString(),
                    TextColor = SKColor.Parse("#000000")
                };
                entries.Add(entry);
                }
            chartView.Chart = new BarChart() { Entries = entries, LabelTextSize = 25 };
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.Admin_Home, container, false);
        }
    }
}