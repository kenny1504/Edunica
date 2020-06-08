﻿using System;
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
using eduNICA.Resources.Intarface;
using eduNICA.Resources.Model;
using Refit;

namespace eduNICA
{
    public class Fragment_Instit_Matricula_Grado : Fragment
    {
        ListView vlista;
        Instit_Matricula_Grados grados;
        Android.Support.V7.Widget.Toolbar toolbar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            Global.Lista_Grad.Clear();
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            // Create your fragment here
            vlista = View.FindViewById<ListView>(Resource.Id.w1listView1);

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
            vlista.Adapter = new Adapter_Lista_Grado(Activity);
            vlista.ItemClick += Vlista_ItemClick;
        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
            toolbar.Title = "Grupos";
            Fragment_Instit_Matricula_Grado_Grupo grupo = new Fragment_Instit_Matricula_Grado_Grupo();
            Estudiantes_grados modulo = Global.Lista_Grad[e.Position];
            Global.grado = modulo.Grado;

            ft.Replace(Resource.Id.relativeLayoutMenu, grupo).Commit();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.Instit_Matricula_Grado, container, false);
        }
    }
}