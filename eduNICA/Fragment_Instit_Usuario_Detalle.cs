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
    public class Fragment_Instit_Usuario_Detalle : Fragment
    {
        Admin_Lista_Usuario_Docente_Detalle Persona12;
        Personas usuario;
        string cedula; Context context;
        TextView txtcedula, txtsexo, txtcorreo, txtdireccion, txttelefono, txtfecha, txtnombre;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            //decula de usuario seleccionado para ver detalle
            cedula = Global.cedula;
            txtcedula = View.FindViewById<TextView>(Resource.Id.textView_cedula);
            txtnombre = View.FindViewById<TextView>(Resource.Id.textView_nombredocente);
            txtcorreo = View.FindViewById<TextView>(Resource.Id.textView_correo);
            txtdireccion = View.FindViewById<TextView>(Resource.Id.textView_direccion);
            txtfecha = View.FindViewById<TextView>(Resource.Id.textView_fechanacimiento);
            txtsexo = View.FindViewById<TextView>(Resource.Id.textView_sexo);
            txttelefono = View.FindViewById<TextView>(Resource.Id.textView_telefono);

            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                .SetContext(context)
                .SetMessage("Cargando ...")
                .Build();
            Esperar.Show();
            Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta

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
            txtnombre.Text = usuario.Nombre + ' ' + usuario.Apellido1 + ' ' + usuario.Apellido2;
            txtcedula.Text = usuario.Cedula;
            txtsexo.Text = usuario.Sexo.ToString();
            txtfecha.Text = usuario.FechaNacimiento.ToString();
            txtdireccion.Text = usuario.Direccion;
            txtcorreo.Text = usuario.Correo;
            txttelefono.Text = usuario.Telefono.ToString();
            Esperar.Dismiss();//cerrar mensaje
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            //definimos el layout que se mostrara en el fragment
            return inflater.Inflate(Resource.Layout.Instit_Usuario_Detalle, container, false);
        }
    }
}