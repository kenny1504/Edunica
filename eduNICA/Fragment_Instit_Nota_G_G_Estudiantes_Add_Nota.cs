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
using eduNICA.Resources.Model;
using Refit;
using eduNICA.Resources.Intarface;
using EDMTDialog;

namespace eduNICA
{
    public class Fragment_Instit_Nota_G_G_Estudiantes_Add_Nota : Fragment
    {
        ListView vlista; Context context; //Instalcia de context
        Interface_Instit_Matricula_Grados_Grupo_Estudiante estudiantes;
        Android.Support.V7.Widget.Toolbar toolbar;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            int grad = Global.idgrado;
            int grup = Global.idgrupo;

            vlista = View.FindViewById<ListView>(Resource.Id.listView_Instit_asignaturas);//vinculamos al listview del layout
            if (Global.Lista_Estudi.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

                //Establecemos la concexion con el servicio web API REST
                estudiantes = RestService.For<Interface_Instit_Matricula_Grados_Grupo_Estudiante>("http://www.edunica.somee.com/api/EstudiantesWS");

                estudianteWS estudianteWS = new estudianteWS();
                estudianteWS.IdInstitucion = Global.u.Id_Institucion;
                estudianteWS.IdGrado = grad;
                estudianteWS.IdGrupo = grup;

                //hacemos peticion mediante el metodo de la interface 
                List<ListaEstudiantesWS> E_lista = await estudiantes.Estudiantes_Institucion(estudianteWS);

                for (int i = 0; i < E_lista.Count; i++)
                {
                    ListaEstudiantesWS W = new ListaEstudiantesWS();
                    W.Nombre = E_lista[i].Nombre;
                    W.Sexo = E_lista[i].Sexo;
                    W.Idestudiante = E_lista[i].Idestudiante;
                    W.IdMatricula = E_lista[i].IdMatricula;
                    Global.Lista_Estudi.Add(W);
                }
                vlista.Adapter = new Adapter_Instit_Lista_Estudiante(Activity, Global.Lista_Estudi);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
                vlista.Adapter = new Adapter_Instit_Lista_Estudiante(Activity, Global.Lista_Estudi);
            vlista.ItemClick += Vlista_ItemClick;
        }
        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                Fragment_Instit_Nota_Add add = new Fragment_Instit_Nota_Add();
                ListaEstudiantesWS modulo = Global.Lista_Estudi[e.Position];
                Global.idmatricula = modulo.IdMatricula;
                Global.nombre_E_N = modulo.Nombre;
                add.Show(ft, "dialog frament");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            // Definimos layout que se mostrara
            return inflater.Inflate(Resource.Layout.instit_Asignaturas, container, false);
        }
    }
}