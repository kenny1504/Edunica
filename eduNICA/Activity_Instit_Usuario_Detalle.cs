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
using eduNICA.Resources.Model;
using Refit;
using eduNICA.Resources.Intarface;

namespace eduNICA
{
    [Activity(Label = "Activity_Instit_Usuario_Detalle")]
    public class Activity_Instit_Usuario_Detalle : Activity
    {
        Admin_Lista_Usuario_Docente_Detalle Persona12;
        Personas usuario;
        TextView txtcedula, txtsexo, txtcorreo, txtdireccion, txttelefono, txtfecha,txtnombre;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Instit_Usuario_Detalle);
            string cedula = Intent.GetStringExtra("Cedula");//recuperamos id del docente seleccionado
            
            txtcedula = FindViewById<TextView>(Resource.Id.textView_cedula);
            txtnombre = FindViewById<TextView>(Resource.Id.textView_nombredocente);
            txtcorreo = FindViewById<TextView>(Resource.Id.textView_correo);
            txtdireccion = FindViewById<TextView>(Resource.Id.textView_direccion);
            txtfecha = FindViewById<TextView>(Resource.Id.textView_fechanacimiento);
            txtsexo = FindViewById<TextView>(Resource.Id.textView_sexo);
            txttelefono = FindViewById<TextView>(Resource.Id.textView_telefono);

            //Establecemos la concexion con el servicio web API REST
            Persona12 = RestService.For<Admin_Lista_Usuario_Docente_Detalle>("http://www.edunica.somee.com/api/UsuariosWS");

            BusquedaUD BusquedaUD = new BusquedaUD();
            BusquedaUD.Cedula = cedula;

            //hacemos peticion mediante el metodo de la interface 
            List<Personas> usuariosdata = await Persona12.Datos_Docente(BusquedaUD);
            for (int i = 0; i < usuariosdata.Count; i++)
            {
                Personas W = new Personas();
                W.Cedula = usuariosdata[i].Cedula;
                W.Nombre = usuariosdata[i].Nombre;
                W.Apellido1 = usuariosdata[i].Apellido1;
                W.Apellido2 = usuariosdata[i].Apellido2;
                W.Sexo = usuariosdata[i].Sexo;
                W.Direccion = usuariosdata[i].Direccion;
                W.Correo = usuariosdata[i].Correo;
                W.Telefono = usuariosdata[i].Telefono;
                W.FechaNacimiento = usuariosdata[i].FechaNacimiento;
                Global.usuariosWs_Datos.Add(W);
            }
            usuario = Global.usuariosWs_Datos.Where(x => x.Cedula == cedula).FirstOrDefault();
            txtnombre.Text = usuario.Nombre+' '+usuario.Apellido1+' '+usuario.Apellido2;
            txtcedula.Text = usuario.Cedula;
            txtsexo.Text = usuario.Sexo.ToString();
            txtfecha.Text = usuario.FechaNacimiento.ToString();
            txtdireccion.Text = usuario.Direccion;
            txtcorreo.Text = usuario.Correo;
            txttelefono.Text = usuario.Telefono.ToString();
            // Create your application here
        }
    }
}