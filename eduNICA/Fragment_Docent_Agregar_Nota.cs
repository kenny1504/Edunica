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
    public class Fragment_Docent_Agregar_Nota : Fragment
    {
        Context context; ListView vlista;Button Btn_Guardar;TextView asignatura;
        Interface_Docente_Nota_Guardar _Docente_Nota_Guardar;
        Interface_Docente_Nota _Docente_Nota;
        Android.Support.V7.Widget.Toolbar toolbar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            Btn_Guardar = View.FindViewById<Button>(Resource.Id.button_Guardar_Nota);
            vlista = View.FindViewById<ListView>(Resource.Id.listView_Notas_Agregar);//vinculamos al listview del layout
            asignatura = View.FindViewById<TextView>(Resource.Id.asignatura_nota);
            asignatura.Text = Global.asignatura;
            Global._Notas.Clear();
            Global.notas_Estudiantes.Clear();
            Btn_Guardar.Click += Btn_Guardar_Click;
            if (Global.notas_Estudiantes.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                _Docente_Nota = RestService.For<Interface_Docente_Nota>("http://www.edunica.somee.com/api/NotasWS");

                VerNotaDocente VerNotaDocente = new VerNotaDocente();
                VerNotaDocente.cedula = Global.u.Cedula;
                VerNotaDocente.idMateria = Global.idasignatura;
                VerNotaDocente.id_detalle_Nota = Global.iddetallenota;

                List<Notas_Estudiante> N_E_lista = await _Docente_Nota.VerNotaDocente(VerNotaDocente);

                for (int i = 0; i < N_E_lista.Count; i++)
                {
                    Lista_Estudiante_Nota W = new Lista_Estudiante_Nota();
                    W.Nombre = N_E_lista[i].Nombre;
                    W.nota = N_E_lista[i].Nota.ToString();
                    W.id = N_E_lista[i].Id;
                    Global._Notas.Add(W);
                    //Notas_Estudiante W = new Notas_Estudiante();
                    //W.Nombre = N_E_lista[i].Nombre;
                    //W.Nota = N_E_lista[i].Nota;
                    //W.Id = N_E_lista[i].Id;
                    //Global.notas_Estudiantes.Add(W);
                }
                    vlista.Adapter = new Adapter_Docente_Nota_Agregar(Activity);
                    Esperar.Dismiss();//cerramos msj cargando
            }
            else
                vlista.Adapter = new Adapter_Docente_Nota_Agregar(Activity);
        }

        private async void Btn_Guardar_Click(object sender, EventArgs e)
        {
            // Global._Notas.Clear();
            bool band = false;
            List<Lista_Estudiante_Nota> notas = Global._Notas;
            int total=0,total_N=0;
            for (int i = 0; i < notas.Count; i++)//contar numero de edittext con nota
            {
                if (notas[i].nota != null)//cantidad de edittext con nota
                    total++;
            }
            if(total==notas.Count)//cantidad de notas ingresadas sea igual al numero de estudiantes de la lista
            {
                int a = 0;
                while (a<notas.Count && !band)//recorremos valor de la nota
                {
                    if (Convert.ToInt32(notas[a].nota) >= 0 && Convert.ToInt32(notas[a].nota) <= 100)//verificar que la nota sea valida, para guardarlas
                    {
                        band = false;
                        total_N++;
                    }
                    else//mensaje del estudiante que tenga una nota incorrecta
                    {
                        Toast.MakeText(Activity, "Estudiante ''" + notas[a].Nombre + "'' posee nota incorrecta", ToastLength.Short).Show();
                        band = true;
                    }
                    a++;
                }               
            }
            else
                Toast.MakeText(Activity, "Faltan Notas que Ingresar", ToastLength.Short).Show();//no ingreso todas las notas

            if(!band && total_N==notas.Count)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                   .SetContext(context)
                   .SetMessage("Guardando ...")
                   .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                _Docente_Nota_Guardar = RestService.For<Interface_Docente_Nota_Guardar>("http://www.edunica.somee.com/api/NotasWS");
                NotasD Notas_D = null;
                for (int i = 0; i < notas.Count; i++)//ingresamos a la clase para guardarla
                {
                    Notas_D = new NotasD();
                    Notas_D.Nota[i] = int.Parse(notas[i].nota);
                    Notas_D.IdNota[i] = notas[i].id;
                }
                int save = await _Docente_Nota_Guardar.AgregarNotaDocente(Notas_D);
                Esperar.Dismiss();
                if (save == 1)
                {
                    Toast.MakeText(Activity, "Notas Guardadas con Exito...!", ToastLength.Short).Show();

                }
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Docent_Nota_Agregar, container, false);
        }
    }
}