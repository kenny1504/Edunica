using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using eduNICA.Resources.Intarface;
using eduNICA.Resources.Model;
using Refit;

namespace eduNICA
{
    [Activity(Label = "Activity_Instit_Matricula_Grado")]
    public class Activity_Instit_Matricula_Grado : Activity
    {
        ListView vlista;
        Instit_Matricula_Grados grados;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Instit_Matricula_Grado);
            vlista = FindViewById<ListView>(Resource.Id.w1listView1);

            //Establecemos la concexion con el servicio web API REST
            grados = RestService.For<Instit_Matricula_Grados>("http://www.edunica.somee.com/api/EstudiantesWS");
            Busqueda Busqueda = new Busqueda();
            Busqueda.Id = Global.u.Id_Institucion;

            //hacemos peticion mediante el metodo de la interface 
            List<Estudiantes_grados> grado_institucion = await grados.Estudiante_Grado(Busqueda);
            for (int i = 0; i < grado_institucion.Count; i++)
            {
                Estudiantes_grados W = new Estudiantes_grados();
                W.Grado = grado_institucion[i].Grado;
                W.cantidad = grado_institucion[i].cantidad;
                Global.Lista_Grad.Add(W);
            }
            vlista.Adapter = new Adapter_Lista_Grado(this);
            vlista.ItemClick += Vlista_ItemClick;
        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent i = new Intent(this, typeof(Activity_Instit_Matricula_Grado_Estudiante));
            StartActivity(i);
        }
    }
}