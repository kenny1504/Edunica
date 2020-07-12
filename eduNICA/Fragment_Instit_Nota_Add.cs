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
using Java.Lang;

namespace eduNICA
{
    public class Fragment_Instit_Nota_Add : DialogFragment
    {
        Context context;
        Interface_Instit_Nota_G_G_AgregarNotaEstudiante _AgregarNotaEstudiante;
        EditText txtestudiante, txtparcial, txtnota,txtasignatura; Button btnguardar, btncancelar;
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            txtestudiante = View.FindViewById<EditText>(Resource.Id.nombre_estudiante_nota);
            txtparcial = View.FindViewById<EditText>(Resource.Id.parcial_nota);
            txtasignatura = View.FindViewById<EditText>(Resource.Id.materia_nota);
            txtnota = View.FindViewById<EditText>(Resource.Id.nueva_nota);
            btnguardar = View.FindViewById<Button>(Resource.Id.add_new_nota);
            btncancelar = View.FindViewById<Button>(Resource.Id.cancelar_add_nota);
            txtestudiante.Text = Global.nombre_E_N;
            txtparcial.Text = Global.parcial;
            txtasignatura.Text = Global.asignatura;
            btncancelar.Click += Btncancelar_Click;
            btnguardar.Click += Btnguardar_Click;
        }

        private async void Btnguardar_Click(object sender, EventArgs e)
        {
            
            if(txtnota.Text != "")
            {
                int new_nota = Integer.ParseInt(txtnota.Text);
                if (new_nota<=100 && new_nota>=0)
                {
                    
                    Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                            .SetContext(context)
                            .SetMessage("Verificando ...")
                            .Build();
                    Esperar.Show();
                    Esperar.Window.SetLayout(700, 600); //aplica tamaño a la alert

                    _AgregarNotaEstudiante = RestService.For<Interface_Instit_Nota_G_G_AgregarNotaEstudiante>("http://www.edunica.somee.com/api/NotasWS");

                    AgregarNota AgregarNota = new AgregarNota();
                    AgregarNota.IdAsigntura = Global.idasignatura;
                    AgregarNota.IdMatricula = Global.idmatricula;
                    AgregarNota.IdDetalle = Global.iddetallenota;
                    AgregarNota.Nota = new_nota;

                    int nota = await _AgregarNotaEstudiante.AgregarNota(AgregarNota);
                    Esperar.Dismiss();
                    if (nota == 1)
                    {
                        Toast.MakeText(Activity, "Guardado con Exito", ToastLength.Short).Show();
                        Dismiss();                       
                    }
                    else
                    {
                        Toast.MakeText(Activity, "Nota Actualizada con Exito", ToastLength.Short).Show();
                        Dismiss();
                    }                      
                }
                else
                    Toast.MakeText(Activity, "Nota Ingresada No Valida", ToastLength.Short).Show();
            }
            else
                Toast.MakeText(Activity, "Ingrese la Nota", ToastLength.Short).Show();
        }
        private void Btncancelar_Click(object sender, EventArgs e)
        {
            Dismiss();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            var view = inflater.Inflate(Resource.Layout.Instit_Nota_Add, container, false);
            return view;
        }
    }
}