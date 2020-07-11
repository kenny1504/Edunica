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
    public class Fragment_Docent_Agregar_Nota : Fragment
    {
        Context context; ListView vlista;Button Btn_Guardar;TextView asignatura;
        Interface_Docent_Estudiantes _Docent_Estudiantes;
        Android.Support.V7.Widget.Toolbar toolbar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            Btn_Guardar = View.FindViewById<Button>(Resource.Id.button_Guardar_Nota);
            vlista = View.FindViewById<ListView>(Resource.Id.listView_Notas_Agregar);//vinculamos al listview del layout
            asignatura = View.FindViewById<TextView>(Resource.Id.asignatura_nota);
            asignatura.Text = Global.asignatura;
            Btn_Guardar.Click += Btn_Guardar_Click;
            if (Global.notas_Estudiantes.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                _Docent_Estudiantes = RestService.For<Interface_Docent_Estudiantes>("http://www.edunica.somee.com/api/AsistenciaWS");

                BuscarAsistencia BuscarAsistencia = new BuscarAsistencia();
                BuscarAsistencia.IdInstitucion = Global.u.Id_Institucion;
                BuscarAsistencia.Cedula = Global.u.Cedula;

                List<ListaAsistencia> N_E_lista = await _Docent_Estudiantes.Listar_Asistencia(BuscarAsistencia);

                    for (int i = 0; i < N_E_lista.Count; i++)
                    {
                        Lista_Estudiante_Nota W = new Lista_Estudiante_Nota();
                        W.Nombre = N_E_lista[i].Nombre;
                        W.IdMatricula = N_E_lista[i].IdMatricula;
                        W.CodigoEstudinte = N_E_lista[i].CodigoEstudinte;
                        Global._Notas.Add(W);
                    }
                    vlista.Adapter = new Adapter_Docente_Nota_Agregar(Activity);
                    Esperar.Dismiss();//cerramos msj cargando
            }
            else
                vlista.Adapter = new Adapter_Docente_Nota_Agregar(Activity);
        }

        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
           // Global._Notas.Clear();
            List<Lista_Estudiante_Nota> notas = Global._Notas;
            int total=0;
            for (int i = 0; i < notas.Count; i++)//contar numero de edittext con nota
            {
                if (notas[i].nota != null)//cantidad de edittext con nota
                    total++;
            }
            if(total==notas.Count)//cantidad de notas ingresadas sea igual al numero de estudiantes de la lista
            {
                int a = 0;bool band = false;
                while (a<notas.Count && !band)//recorremos valor de la nota
                {
                    if(Convert.ToInt32( notas[a].nota)>=0 && Convert.ToInt32(notas[a].nota) <= 100)//verificar que la nota sea valida, para guardarlas
                    {
                        for (int i = 0; i < notas.Count; i++)//ingresamos a la clase para guardarla
                        {
                            AgregarNota agregarNota = new AgregarNota();
                            agregarNota.Nota = Convert.ToInt32(notas[i].nota);
                        }
                        Toast.MakeText(Activity, "Notas Guardadas con Exito(sin servicio todavia)...!", ToastLength.Short).Show();
                    }
                    else//mensaje del estudiante que tenga una nota incorrecta
                    {
                        Toast.MakeText(Activity, "Estudiante ''"+notas[a].Nombre+"'' posee nota incorrecta", ToastLength.Short).Show();
                        band = true;
                    }
                    a++;
                }               
            }
            else
                Toast.MakeText(Activity, "Faltan Notas que Ingresar", ToastLength.Short).Show();//no ingreso todas las notas
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Docent_Nota_Agregar, container, false);
        }
    }
}