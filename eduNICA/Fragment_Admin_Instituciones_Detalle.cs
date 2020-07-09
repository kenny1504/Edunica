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
    public class Fragment_Admin_Instituciones_Detalle : Fragment
    {
        Interface_Admin_Instituciones_Detalle _Instituciones_Detalle;
        Context context;
        TextView txtnombre, txtusuario, txtcant_doc, txtcant_Estud, txtcant_Matri, txtdireccion;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            txtnombre = View.FindViewById<TextView>(Resource.Id.Admin_nombreinstitucion);
            txtusuario = View.FindViewById<TextView>(Resource.Id.Admin_usuario_institucion);
            txtdireccion = View.FindViewById<TextView>(Resource.Id.Admin_direccion_institucion);
            txtcant_doc = View.FindViewById<TextView>(Resource.Id.Admin_cant_docent_Instit);
            txtcant_Estud = View.FindViewById<TextView>(Resource.Id.Admin_cant_estud_Instit);
            txtcant_Matri = View.FindViewById<TextView>(Resource.Id.Admin_cant_matric_Instit);

            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                .SetContext(context)
                .SetMessage("Cargando ...")
                .Build();
            Esperar.Show();

            _Instituciones_Detalle = RestService.For<Interface_Admin_Instituciones_Detalle>("http://www.edunica.somee.com/api/UsuariosWS");

            Busqueda Busqueda = new Busqueda();
            Busqueda.Id = Global.id_Usuariosinstituciones;

            UsuarioInstitucion Edatos = await _Instituciones_Detalle.DatosInstituciones(Busqueda);
            DasboardWS wS = await _Instituciones_Detalle.DatosInstitucion(Busqueda);
            Global.ws = wS;
            Global.UsuarioInstitucion = Edatos;

            txtnombre.Text = Global.UsuarioInstitucion.Institucion;
            txtusuario.Text = Global.UsuarioInstitucion.Usuario;
            txtdireccion.Text = Global.UsuarioInstitucion.Direcccion;
            txtcant_doc.Text = Global.ws.Docentes.ToString();
            txtcant_Estud.Text = Global.ws.Estudiantes.ToString();
            txtcant_Matri.Text = Global.ws.Matriculas.ToString();
            Esperar.Dismiss();//cerrar mensaje
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Admin_Institucion_Detalle, container, false);
        }
    }
}