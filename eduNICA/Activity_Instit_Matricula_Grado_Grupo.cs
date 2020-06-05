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
    [Activity(Label = "Activity_Instit_Matricula_Grado_Grupo")]
    public class Activity_Instit_Matricula_Grado_Grupo : Activity
    {
        ListView vlista;
        Instit_Matricula_Grados_Grupo grupos;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Inst_Matricula_Grado_Grupo);
            int grad = Intent.GetIntExtra("Grado", 0);//recuperamos grupo seleccionado
            vlista = FindViewById<ListView>(Resource.Id.listView_grupos);//vinculamos al listview del layout

            //Establecemos la concexion con el servicio web API REST
            grupos = RestService.For<Instit_Matricula_Grados_Grupo>("http://www.edunica.somee.com/api/EstudiantesWS");
            Grupos_ws Grupos_ws = new Grupos_ws();
            Grupos_ws.id_grado = grad;
            Grupos_ws.id_intituto = Global.u.Id_Institucion;

            //hacemos peticion mediante el metodo de la interface 
            List<grupos_grados> grupo_institucion = await grupos.Estudiante_Grado_Grupo(Grupos_ws);
            for (int i = 0; i < grupo_institucion.Count; i++)
            {
                grupos_grados W = new grupos_grados();
                W.Grupo = grupo_institucion[i].Grupo;
                W.cantidad = grupo_institucion[i].cantidad;
                Global.grupos.Add(W);
            }
            vlista.Adapter = new Adapter_Lista_Grupo(this);
        }
    }
}