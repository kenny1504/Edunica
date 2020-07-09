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
using eduNICA.Resources.Intarface;
using eduNICA.Resources.Model;
using Refit;
using EDMTDialog;

namespace eduNICA
{
    public class Fragment_Actualizar_Credenciales : DialogFragment
    {
        EditText usuario, contraseña, newcontraseña, newcontraseña_V;
        Button Btn_Actualizar,Btn_Cancelar;
        Context context;
        Interface_Actualizar_Credenciales _Actualizar_Credenciales;
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            usuario = View.FindViewById<EditText>(Resource.Id.nombre_user);
            contraseña = View.FindViewById<EditText>(Resource.Id.contraseña_actual);
            newcontraseña = View.FindViewById<EditText>(Resource.Id.nueva_contraseña);
            newcontraseña_V = View.FindViewById<EditText>(Resource.Id.verificar_nueva_contraseña);
            Btn_Actualizar = View.FindViewById<Button>(Resource.Id.actualizar_credenciales);
            Btn_Cancelar = View.FindViewById<Button>(Resource.Id.cancelar_actualizar_credenciales);

            usuario.Text = Global.user;
            Btn_Actualizar.Click += Btn_Actualizar_Click;
            Btn_Cancelar.Click += Btn_Cancelar_Click;

            //desabilitar EditText de verificacion de contraseña
            newcontraseña_V.Enabled = false;
            newcontraseña.TextChanged += Newcontraseña_TextChanged;//si el campo de nueva contraseña esta vacio, inhabilitamos el de nueva contraseña
        }
        private void Newcontraseña_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (newcontraseña.Text == "")
            {
                newcontraseña_V.Enabled = false;
                newcontraseña_V.Text = "";
            }
            else
                newcontraseña_V.Enabled = true;
        }
        private async void Btn_Actualizar_Click(object sender, EventArgs e)
        {
            if (usuario.Text != "" && contraseña.Text != "" && newcontraseña.Text == "" && newcontraseña_V.Text == "")
            {
                if (contraseña.Text == Global.passw && usuario.Text!=Global.user)//verificar si la contraseña es correcta y que haiga modificado el nuevo usuario, para asi actualizarlo
                {
                    Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                            .SetContext(context)
                            .SetMessage("Actualizando Usuario...")
                            .Build();
                    Esperar.Show();
                    //********************************************************************************************************
                    //*************************Actualizar Nombre de Usuario***************************************************
                    //********************************************************************************************************
                    _Actualizar_Credenciales = RestService.For<Interface_Actualizar_Credenciales>("http://www.edunica.somee.com/api/UsuariosWS");
                    Usuarios Usuarios = new Usuarios();
                    Usuarios.Usuario = usuario.Text;//se envia solo el nuevo usuario a actualizar
                    Usuarios.Cedula = Global.u.Cedula;
                    int retorno= await _Actualizar_Credenciales.Editar_Usuario(Usuarios);
                    Esperar.Dismiss();
                    if(retorno==1)
                    {
                        Global.user = usuario.Text;//actualizamos el nombre de usuario almacenado en la variable Global

                        //cerrar sesion al cambiar credenciales de acceso
                        Intent i = new Intent(context, typeof(LoginActivity));

                        //limpiar listas al cerrar sesion
                        Global.Asignaturasdocentes.Clear();//limpiar lista de asignatura de docente
                        Global._Asistencias.Clear();//limpiar lista de asistencia(estudiante)
                        Global.detallenotas.Clear();//limpiar parciales de nota
                        Global.notas_Estudiantes.Clear();//limpiar lista estudiantes con notas

                        Global.ListaAsistencias.Clear();//lista de estudiantes
                        Global.asistencias.Clear();//limpiar lista luego de guardar asistencia
                        Global._Notas.Clear();//limpiamos lista temporal de nota
                        Global.Asignaturasdocentes.Clear();//limpiamos asignatura de docente al cerrar sesion
                        StartActivity(i);

                        Toast.MakeText(Activity, "Nombre de Usuario Actualizado", ToastLength.Short).Show();
                        Dismiss();
                    }
                    else
                    {
                        AlertDialog alert = new AlertDialog.Builder(context).Create();
                        alert.SetTitle("Error!");
                        alert.SetMessage("Nombre de Usuario en Uso!");
                        alert.SetButton("Aceptar", (a, b) =>
                        {
                            usuario.RequestFocus();
                            alert.Dismiss();
                        });
                        alert.Show();
                    }
                }
                else if(usuario.Text==Global.user && contraseña.Text==Global.passw)//si no ha modificado el usuario, no se hace la peticion
                {
                    //Toast.MakeText(Activity, "El nombre de usuario es el mismo!", ToastLength.Short).Show();
                    AlertDialog alert = new AlertDialog.Builder(context).Create();
                    alert.SetTitle("Error!");
                    alert.SetMessage("El nombre de usuario es el mismo!");
                    alert.SetButton("Aceptar", (a, b) =>
                    {
                        alert.Dismiss();
                    });
                    alert.Show();
                }
                else//la contraseña ingresada no coincide con la del usuario, por lo que no se podra modificar, hasta q sea la misma
                {
                    //Toast.MakeText(Activity, "Contraseña Actual no correcta", ToastLength.Short).Show();
                    AlertDialog alert = new AlertDialog.Builder(context).Create();
                    alert.SetTitle("Error!");
                    alert.SetMessage("Contraseña Actual no correcta!");
                    alert.SetButton("Aceptar", (a, b) =>
                    {
                        contraseña.RequestFocus();
                        alert.Dismiss();
                    });
                    alert.Show();                   
                }
            }
            else if (usuario.Text != "" && contraseña.Text != "" && newcontraseña.Text != "" && newcontraseña_V.Text != "")
            {
                if(contraseña.Text==Global.passw)//si la contraseña ingresada el la de usuario para verificar y asi poder actualizar
                {
                    if (newcontraseña.Text == newcontraseña_V.Text && usuario.Text == Global.user)//verificacion de la contraseña nueva
                    {
                        Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                            .SetContext(context)
                            .SetMessage("Actualizando Contraseña...")
                            .Build();
                            Esperar.Show();
                        //********************************************************************************************
                        //*************************Actualizar Contraseña**********************************************
                        //********************************************************************************************
                        _Actualizar_Credenciales = RestService.For<Interface_Actualizar_Credenciales>("http://www.edunica.somee.com/api/UsuariosWS");
                        Usuarios Usuarios = new Usuarios();
                        Usuarios.Usuario = usuario.Text;
                        Usuarios.Contraseña = newcontraseña_V.Text;
                        Usuarios.Cedula = Global.u.Cedula;
                        int retorno = await _Actualizar_Credenciales.Editar_Usuario(Usuarios);
                        Esperar.Dismiss();
                        if (retorno == 1)
                        {
                            Toast.MakeText(Activity, "Contraseña Actualizada", ToastLength.Short).Show();

                            //cerrar sesion al cambiar credenciales de acceso
                            Intent i = new Intent(context, typeof(LoginActivity));
                            //limpiar listas al cerrar sesion
                            Global.Asignaturasdocentes.Clear();//limpiar lista de asignatura de docente
                            Global._Asistencias.Clear();//limpiar lista de asistencia(estudiante)
                            Global.detallenotas.Clear();//limpiar parciales de nota
                            Global.notas_Estudiantes.Clear();//limpiar lista estudiantes con notas

                            Global.ListaAsistencias.Clear();//lista de estudiantes
                            Global.asistencias.Clear();//limpiar lista luego de guardar asistencia
                            Global._Notas.Clear();//limpiamos lista temporal de nota
                            Global.Asignaturasdocentes.Clear();//limpiamos asignatura de docente al cerrar sesion
                            StartActivity(i);

                            Dismiss();
                        }
                    }
                    else if (newcontraseña.Text == newcontraseña_V.Text && usuario.Text != Global.user)//verificacion de la contraseña nueva
                    {
                        Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                            .SetContext(context)
                            .SetMessage("Actualizando Usuario Y Contraseña...")
                            .Build();
                        Esperar.Show();
                        //********************************************************************************************
                        //*********************Actualizar Nombre de Usuario y Contraseña******************************
                        //********************************************************************************************
                        _Actualizar_Credenciales = RestService.For<Interface_Actualizar_Credenciales>("http://www.edunica.somee.com/api/UsuariosWS");
                        Usuarios Usuarios = new Usuarios();
                        Usuarios.Contraseña =newcontraseña_V.Text;//enviamos nombre y contraseña del usuario a actualizar
                        Usuarios.Usuario = usuario.Text;
                        Usuarios.Cedula = Global.u.Cedula;
                        int retorno = await _Actualizar_Credenciales.Editar_Usuario(Usuarios);
                        Esperar.Dismiss();
                        if (retorno == 1)
                        {
                            Global.user = usuario.Text;
                            Toast.MakeText(Activity, "Contraseña y nombre de Usuario Actualizada", ToastLength.Short).Show();

                            //cerrar sesion al cambiar credenciales de acceso
                            Intent i = new Intent(context, typeof(LoginActivity));
                            //limpiar listas al cerrar sesion
                            Global.Asignaturasdocentes.Clear();//limpiar lista de asignatura de docente
                            Global._Asistencias.Clear();//limpiar lista de asistencia(estudiante)
                            Global.detallenotas.Clear();//limpiar parciales de nota
                            Global.notas_Estudiantes.Clear();//limpiar lista estudiantes con notas

                            Global.ListaAsistencias.Clear();//lista de estudiantes
                            Global.asistencias.Clear();//limpiar lista luego de guardar asistencia
                            Global._Notas.Clear();//limpiamos lista temporal de nota
                            Global.Asignaturasdocentes.Clear();//limpiamos asignatura de docente al cerrar sesion
                            StartActivity(i);

                            Dismiss();
                        }
                    }
                    else//verifiacar q la nueva contraseña coincidan, para asi actualizarla
                    {
                        AlertDialog alert = new AlertDialog.Builder(context).Create();
                        alert.SetTitle("Error!");
                        alert.SetMessage("La contraseña nueva no coincide!");
                        alert.SetButton("Aceptar", (a, b) =>
                        {
                            newcontraseña_V.RequestFocus();
                            alert.Dismiss();
                        });
                        alert.Show();
                        //Toast.MakeText(Activity, "La contraseña nueva no coincide", ToastLength.Short).Show();
                        //newcontraseña_V.RequestFocus();
                    }
                }
                else//la contraseña actual ingresada no es la misma, por lo que no se puede actualizar los datos
                {
                    AlertDialog alert = new AlertDialog.Builder(context).Create();
                    alert.SetTitle("Error!");
                    alert.SetMessage("Contraseña Actual no correcta!");
                    alert.SetButton("Aceptar", (a, b) =>
                    {
                        contraseña.RequestFocus();
                        alert.Dismiss();
                    });
                    alert.Show();
                }
            }
            //***********************************************************************************************************
            //*****************verificar q los campos se encuentre llenos al queres actualizar***************************
            //*****************************ubicamos el foco el el edittext vacio*****************************************
            //***********************************************************************************************************
            //no ingreso el nombre de usuario
            else if (usuario.Text == "")
            {
                Toast.MakeText(Activity, "Porfavor Ingrese Nombre de Usuario", ToastLength.Short).Show();
                usuario.RequestFocus();
            }
            //no ingreso la contraseña actual para actualizar datos
            else if (contraseña.Text == "")
            {
                Toast.MakeText(Activity, "Porfavor Ingrese la Contraseña", ToastLength.Short).Show();
                contraseña.RequestFocus();
            }
            //no ingreso la verificacion de la nueva contraseña
            else if (usuario.Text != "" && contraseña.Text != "" && newcontraseña.Text != "" && newcontraseña_V.Text == "")
            {
                Toast.MakeText(Activity, "Porfavor Ingrese la Verificacion la contraseña", ToastLength.Short).Show();
                newcontraseña_V.RequestFocus();
            }
        }
        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            Dismiss();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Actualizar_Credenciales, container, false);
        }
    }
}