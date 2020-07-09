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
        Context context;
        TextView txtinstit, txtestudiant, txtmatric, txtdoc;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            txtdoc = View.FindViewById<TextView>(Resource.Id.Admin_cant_docent);
            txtestudiant = View.FindViewById<TextView>(Resource.Id.Admin_cant_estudiant);
            txtinstit = View.FindViewById<TextView>(Resource.Id.Admin_cant_instit);
            txtmatric = View.FindViewById<TextView>(Resource.Id.Admin_cant_matric);
            chartView = View.FindViewById<ChartView>(Resource.Id.Chart_Admin);
            List<Entry> entries = new List<Entry>();

            if (Global.Lista_Grad_Admin.Count == 0 || Global.ws==null)
            {
                Global.Lista_Grad_Admin.Clear();
                Global.ws = null;
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alert

                admin_Home = RestService.For<Interface_Admin_home>("http://www.edunica.somee.com/api/DashboardWS");//peticion

                //hacemos peticion mediante el metodo de la interface 
                List<Estudiantes_grados_Admin> estudiantes_Grados_Admins = await admin_Home.Total_Grados();
                DasboardWS ws = await admin_Home.DatosAdmin();


                Global.ws = ws;
                txtestudiant.Text = Global.ws.Estudiantes.ToString();
                txtinstit.Text = Global.ws.Instituciones.ToString();
                txtdoc.Text = Global.ws.Docentes.ToString();
                txtmatric.Text = Global.ws.Matriculas.ToString();


                for (int i = 0; i < estudiantes_Grados_Admins.Count; i++)
                {
                    Estudiantes_grados_Admin W = new Estudiantes_grados_Admin();
                    W.Grado = estudiantes_Grados_Admins[i].Grado;
                    W.cantidad = estudiantes_Grados_Admins[i].cantidad;
                    Global.Lista_Grad_Admin.Add(W);
                }


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
                Esperar.Dismiss();
            }
            else
            {
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
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Admin_Home, container, false);
        }
    }
}