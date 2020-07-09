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
    public class Fragment_Admin_Usuarios_Detalle : Fragment
    {
        Interface_Admin_Usuarios_Detalle _Usuarios_Detalle;
        Context context;
        TextView txtnombre, txtcedula, txtsexo, txtfecha, txtcorreo, txtdireccion, txttelefono, txtinstituto;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            txtcedula = View.FindViewById<TextView>(Resource.Id.Admin_cedulausuario);
            txtnombre = View.FindViewById<TextView>(Resource.Id.Admin_nombreusuario);
            txtcorreo = View.FindViewById<TextView>(Resource.Id.Admin_correousuario);
            txtdireccion = View.FindViewById<TextView>(Resource.Id.Admin_direccionusuario);
            txtfecha = View.FindViewById<TextView>(Resource.Id.Admin_fechanaciusuario);
            txtsexo = View.FindViewById<TextView>(Resource.Id.Admin_sexousuario);
            txttelefono = View.FindViewById<TextView>(Resource.Id.Admin_telefonousuario);
            txtinstituto = View.FindViewById<TextView>(Resource.Id.Admin_institucion_usuario);

            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                .SetContext(context)
                .SetMessage("Cargando ...")
                .Build();
            Esperar.Show();
            Esperar.Window.SetLayout(1000, 800); //aplica tamaño a la alerta
            //Establecemos la concexion con el servicio web API REST
            _Usuarios_Detalle = RestService.For<Interface_Admin_Usuarios_Detalle>("http://www.edunica.somee.com/api/UsuariosWS");

            Busqueda Busqueda = new Busqueda();
            Busqueda.Id = Global.iddocente;

            //hacemos peticion mediante el metodo de la interface 
            DatosUsuariosADMIN Edatos = await _Usuarios_Detalle.DatosUsuarios(Busqueda);
            Global.datosUsuariosADMIN = Edatos;

            txtnombre.Text = Global.datosUsuariosADMIN.Nombre;
            txtcedula.Text = Global.datosUsuariosADMIN.Cedula;
            txtsexo.Text = Global.datosUsuariosADMIN.Sexo;
            txtfecha.Text = Global.datosUsuariosADMIN.FechaNacimiento.ToString();
            txtdireccion.Text = Global.datosUsuariosADMIN.Direccion;
            txtcorreo.Text = Global.datosUsuariosADMIN.Correo;
            txttelefono.Text = Global.datosUsuariosADMIN.Telefono.ToString();
            txtinstituto.Text = Global.datosUsuariosADMIN.Institucion;
            Esperar.Dismiss();//cerrar mensaje
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Admin_Usuarios_Detalle, container, false);
        }
    }
}