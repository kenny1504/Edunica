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
    public class Fragment_Actualizar_Credenciales_Institucion : DialogFragment
    {
        EditText institucion, usuario, contraseña, newcontraseña, newcontraseña_V,direccion;
        Button Btn_Actualizar, Btn_Cancelar;
        Context context;
        Interface_Actualizar_Credenciales_Institucion _Actualizar_Credenciales_Institucion;
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            institucion = View.FindViewById<EditText>(Resource.Id.nombre_Institucion);
            usuario = View.FindViewById<EditText>(Resource.Id.nombre_user_Institucion);
            direccion = View.FindViewById<EditText>(Resource.Id.direccion_Institucion);
            contraseña = View.FindViewById<EditText>(Resource.Id.contraseña_actual_Institucion);
            newcontraseña = View.FindViewById<EditText>(Resource.Id.nueva_contraseña_Institucion);
            newcontraseña_V = View.FindViewById<EditText>(Resource.Id.verificar_nueva_contraseña_Institucion);
            Btn_Actualizar = View.FindViewById<Button>(Resource.Id.actualizar_credenciales_institucion);
            Btn_Cancelar = View.FindViewById<Button>(Resource.Id.cancelar_actualizar_credenciales_institucion);

            //desabilitar EditText de verificacion de contraseña
            newcontraseña_V.Enabled = false;
            newcontraseña.TextChanged += Newcontraseña_TextChanged;//evento para habilitar la verificacion de la contraseña


            institucion.Text = Global.u.Institucion;
            usuario.Text = Global.user;
            direccion.Text = Global.u.Cedula;//direccion de institucion

            Btn_Actualizar.Click += Btn_Actualizar_Click;
            Btn_Cancelar.Click += Btn_Cancelar_Click;
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
            if (institucion.Text != "" && usuario.Text != "" && direccion.Text != "" && contraseña.Text != "" && newcontraseña.Text == "" && newcontraseña_V.Text == "")
            {
                if (contraseña.Text == Global.passw)
                {
                    if (institucion.Text != Global.u.Institucion && usuario.Text == Global.user && direccion.Text == Global.u.Cedula/*&& contraseña.Text == Global.passw*/)
                    {
                        Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                .SetContext(context)
                                .SetMessage("Actualizando Nombre Institucion")
                                .Build();
                        Esperar.Show();
                        //********************************************************************************************************
                        //*************************Actualizar Nombre de Institucion***********************************************
                        //********************************************************************************************************
                        _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                        UsuarioInstitucion user = new UsuarioInstitucion();
                        user.Id = Global.u.Id_Institucion;
                        user.Institucion = institucion.Text;
                        user.Direcccion = direccion.Text;
                        user.Usuario = usuario.Text;
                        int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                        Esperar.Dismiss();
                        if (retorno == 1)
                        {
                            Global.u.Institucion = institucion.Text;//actualizamos el nombre de institucion almacenado en datos de usuario logueado
                            Toast.MakeText(Activity, "Nombre de Institucion Actualizado", ToastLength.Short).Show();
                            Dismiss();
                        }
                    }
                    else if (institucion.Text != Global.u.Institucion && usuario.Text != Global.user && direccion.Text == Global.u.Cedula /*&& contraseña.Text == Global.passw*/)
                    {
                        Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                .SetContext(context)
                                .SetMessage("Actualizando Nombre Intitucion y Usuario")
                                .Build();
                        Esperar.Show();
                        //********************************************************************************************************
                        //*************************Actualizar Nombre Intitucion y Usuario*****************************************
                        //********************************************************************************************************
                        _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                        UsuarioInstitucion user = new UsuarioInstitucion();
                        user.Id = Global.u.Id_Institucion;
                        user.Institucion = institucion.Text;
                        user.Direcccion = direccion.Text;
                        user.Usuario = usuario.Text;
                        int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                        Esperar.Dismiss();
                        if (retorno == 1)
                        {
                            Global.user = usuario.Text;//actualizamos el nombre de usuario almacenado en la variable Global
                            Global.u.Institucion = institucion.Text;//actualizamos el nombre de institucion almacenado en datos de usuario logueado

                            //cerramos sesion al cambiar credenciales de acceso
                            Intent i = new Intent(context, typeof(LoginActivity));
                            //limpiar lista de item Matricula
                            Global.Lista_Grad.Clear();
                            Global.grupos.Clear();
                            Global.Lista_Estudi.Clear();
                            Global.datos_E.Clear();

                            //Limpiar lista de item Docentes
                            Global.usuariosWs.Clear();
                            Global.usuariosWs_Datos.Clear();

                            //limpiar lista de datos de grafico
                            Global.Lista_Grad_Graf.Clear();

                            Global.materia.Clear();//limpiar asignaturas
                            Global.detallenotas.Clear();//limpiar parciales
                            Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas

                            StartActivity(i);
                            Toast.MakeText(Activity, "Nombre de Institucion y Nombre de Usuario Actualizado", ToastLength.Short).Show();
                            Dismiss();
                        }
                        else
                        {
                            AlertDialog alert = new AlertDialog.Builder(context).Create();//***********************************************************************************************************************
                            alert.SetTitle("Error!");
                            alert.SetMessage("Nombre de Usuario en Uso!");
                            alert.SetButton("Aceptar", (a, b) =>
                            {
                                alert.Dismiss();
                            });
                            alert.Show();
                        }
                    }
                    else if (institucion.Text != Global.u.Institucion && usuario.Text != Global.user && direccion.Text != Global.u.Cedula /*&& contraseña.Text == Global.passw*/)
                    {
                        Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                .SetContext(context)
                                .SetMessage("Actualizando Nombre Intitucion, Usuario y Direccion")
                                .Build();
                        Esperar.Show();
                        //********************************************************************************************************
                        //********************Actualizar Nombre Intitucion, Usuario y Direccion***********************************
                        //********************************************************************************************************
                        _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                        UsuarioInstitucion user = new UsuarioInstitucion();
                        user.Id = Global.u.Id_Institucion;
                        user.Institucion = institucion.Text;
                        user.Direcccion = direccion.Text;
                        user.Usuario = usuario.Text;
                        int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                        Esperar.Dismiss();
                        if (retorno == 1)
                        {
                            Global.user = usuario.Text;//actualizamos el nombre de usuario almacenado en la variable Global
                            Global.u.Institucion = institucion.Text;//actualizamos el nombre de institucion almacenado en datos de usuario logueado
                            Global.u.Cedula = direccion.Text;//actualizamos direccion de institucion

                            //cerramos sesion al cambiar credenciales de acceso
                            Intent i = new Intent(context, typeof(LoginActivity));
                            //limpiar lista de item Matricula
                            Global.Lista_Grad.Clear();
                            Global.grupos.Clear();
                            Global.Lista_Estudi.Clear();
                            Global.datos_E.Clear();

                            //Limpiar lista de item Docentes
                            Global.usuariosWs.Clear();
                            Global.usuariosWs_Datos.Clear();

                            //limpiar lista de datos de grafico
                            Global.Lista_Grad_Graf.Clear();

                            Global.materia.Clear();//limpiar asignaturas
                            Global.detallenotas.Clear();//limpiar parciales
                            Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas


                            Toast.MakeText(Activity, "Nombre de Institucion, Usuario y Direccion Actualizado", ToastLength.Short).Show();
                            Dismiss();
                        }
                        else
                        {
                            AlertDialog alert = new AlertDialog.Builder(context).Create();//************************************************************************************************
                            alert.SetTitle("Error!");
                            alert.SetMessage("Nombre de Usuario en Uso!");
                            alert.SetButton("Aceptar", (a, b) =>
                            {
                                alert.Dismiss();
                            });
                            alert.Show();
                        }
                    }
                    else if (institucion.Text != Global.u.Institucion && usuario.Text == Global.user && direccion.Text != Global.u.Cedula/*&& contraseña.Text == Global.passw*/)
                    {
                        Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                .SetContext(context)
                                .SetMessage("Actualizando Nombre Intitucion y Direccion")
                                .Build();
                        Esperar.Show();
                        //********************************************************************************************************
                        //********************Actualizar Nombre Intitucion y Direccion***********************************
                        //********************************************************************************************************
                        _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                        UsuarioInstitucion user = new UsuarioInstitucion();
                        user.Id = Global.u.Id_Institucion;
                        user.Institucion = institucion.Text;
                        user.Direcccion = direccion.Text;
                        user.Usuario = usuario.Text;
                        int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                        Esperar.Dismiss();
                        if (retorno == 1)
                        {
                            Global.u.Institucion = institucion.Text;//actualizamos el nombre de institucion almacenado en datos de usuario logueado
                            Global.u.Cedula = direccion.Text;//actualizamos direccion de institucion
                            Toast.MakeText(Activity, "Nombre de Institucion y Direccion Actualizado", ToastLength.Short).Show();
                            Dismiss();
                        }
                    }
                    else if (institucion.Text == Global.u.Institucion && usuario.Text != Global.user && direccion.Text == Global.u.Cedula /*&& contraseña.Text == Global.passw*/)
                    {
                        Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                .SetContext(context)
                                .SetMessage("Actualizando Usuario")
                                .Build();
                        Esperar.Show();
                        //********************************************************************************************************
                        //*************************Actualizar Nombre de Usuario***************************************************
                        //********************************************************************************************************
                        _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                        UsuarioInstitucion user = new UsuarioInstitucion();
                        user.Id = Global.u.Id_Institucion;
                        user.Institucion = institucion.Text;
                        user.Direcccion = direccion.Text;
                        user.Usuario = usuario.Text;
                        int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                        Esperar.Dismiss();
                        if (retorno == 1)
                        {
                            Global.user = usuario.Text;//actualizamos el nombre de usuario almacenado en la variable Global

                            //cerramos sesion al cambiar credenciales de acceso
                            Intent i = new Intent(context, typeof(LoginActivity));
                            //limpiar lista de item Matricula
                            Global.Lista_Grad.Clear();
                            Global.grupos.Clear();
                            Global.Lista_Estudi.Clear();
                            Global.datos_E.Clear();

                            //Limpiar lista de item Docentes
                            Global.usuariosWs.Clear();
                            Global.usuariosWs_Datos.Clear();

                            //limpiar lista de datos de grafico
                            Global.Lista_Grad_Graf.Clear();

                            Global.materia.Clear();//limpiar asignaturas
                            Global.detallenotas.Clear();//limpiar parciales
                            Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas


                            Toast.MakeText(Activity, "Nombre de Usuario Actualizado", ToastLength.Short).Show();
                            Dismiss();
                        }
                        else
                        {
                            AlertDialog alert = new AlertDialog.Builder(context).Create();//************************************************************************************************
                            alert.SetTitle("Error!");
                            alert.SetMessage("Nombre de Usuario en Uso!");
                            alert.SetButton("Aceptar", (a, b) =>
                            {
                                alert.Dismiss();
                            });
                            alert.Show();
                        }
                    }
                    else if (institucion.Text == Global.u.Institucion && usuario.Text != Global.user && direccion.Text != Global.u.Cedula/* && contraseña.Text == Global.passw*/)
                    {
                        Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                .SetContext(context)
                                .SetMessage("Actualizando Usuario y Direccion")
                                .Build();
                        Esperar.Show();
                        //********************************************************************************************************
                        //*************************Actualizar Usuario y Direccion*************************************************
                        //********************************************************************************************************

                        _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                        UsuarioInstitucion user = new UsuarioInstitucion();
                        user.Id = Global.u.Id_Institucion;
                        user.Institucion = institucion.Text;
                        user.Direcccion = direccion.Text;
                        user.Usuario = usuario.Text;
                        int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                        Esperar.Dismiss();
                        if (retorno == 1)
                        {
                            Global.user = usuario.Text;//actualizamos el nombre de usuario almacenado en la variable Global
                            Global.u.Cedula = direccion.Text;//actualizamos direccion de institucion

                            //cerramos sesion al cambiar credenciales de acceso
                            Intent i = new Intent(context, typeof(LoginActivity));
                            //limpiar lista de item Matricula
                            Global.Lista_Grad.Clear();
                            Global.grupos.Clear();
                            Global.Lista_Estudi.Clear();
                            Global.datos_E.Clear();

                            //Limpiar lista de item Docentes
                            Global.usuariosWs.Clear();
                            Global.usuariosWs_Datos.Clear();

                            //limpiar lista de datos de grafico
                            Global.Lista_Grad_Graf.Clear();

                            Global.materia.Clear();//limpiar asignaturas
                            Global.detallenotas.Clear();//limpiar parciales
                            Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas


                            Toast.MakeText(Activity, "Nombre de Usuario y Direccion Actualizado", ToastLength.Short).Show();
                            Dismiss();
                        }
                        else
                        {
                            AlertDialog alert = new AlertDialog.Builder(context).Create();//************************************************************************************************
                            alert.SetTitle("Error!");
                            alert.SetMessage("Nombre de Usuario en Uso!");
                            alert.SetButton("Aceptar", (a, b) =>
                            {
                                alert.Dismiss();
                            });
                            alert.Show();
                        }
                    }
                    else if (institucion.Text == Global.u.Institucion && usuario.Text == Global.user && direccion.Text != Global.u.Cedula /*&& contraseña.Text == Global.passw*/)
                    {
                        Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                .SetContext(context)
                                .SetMessage("Actualizando Direccion")
                                .Build();
                        Esperar.Show();
                        //********************************************************************************************************
                        //*************************Actualizar Direccion de Institucion***********************************************
                        //********************************************************************************************************
                        _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                        UsuarioInstitucion user = new UsuarioInstitucion();
                        user.Id = Global.u.Id_Institucion;
                        user.Institucion = institucion.Text;
                        user.Direcccion = direccion.Text;
                        user.Usuario = usuario.Text;
                        int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                        Esperar.Dismiss();
                        if (retorno == 1)
                        {
                            Global.u.Cedula = direccion.Text;//actualizamos direccion de institucion
                            Toast.MakeText(Activity, "Direccion de Institucion Actualizado", ToastLength.Short).Show();
                            Dismiss();
                        }
                    }

                    else if (institucion.Text == Global.u.Institucion && usuario.Text == Global.user && direccion.Text == Global.u.Cedula /*&& contraseña.Text == Global.passw*/)
                    {
                        AlertDialog alert = new AlertDialog.Builder(context).Create();
                        alert.SetTitle("Error!");
                        alert.SetMessage("No hay cambios nuevo ha actualizar!");
                        alert.SetButton("Aceptar", (a, b) =>
                        {
                            alert.Dismiss();
                        });
                        alert.Show();
                    }
                }
                else
                {
                    AlertDialog alert = new AlertDialog.Builder(context).Create();
                    alert.SetTitle("Error!");
                    alert.SetMessage("Contraseña Actual incorrecta!!");
                    alert.SetButton("Aceptar", (a, b) =>
                    {
                        contraseña.RequestFocus();
                        alert.Dismiss();
                    });
                    alert.Show();
                }
            }
            else if (institucion.Text != "" && usuario.Text != "" && direccion.Text != "" && contraseña.Text != "" && newcontraseña.Text != "" && newcontraseña_V.Text != "")
            {
                if (contraseña.Text == Global.passw)//si la contraseña ingresada el la de usuario para verificar y asi poder actualizar
                {
                    if (newcontraseña_V.Text == newcontraseña.Text)
                    {
                        if (institucion.Text == Global.u.Institucion && usuario.Text == Global.user && direccion.Text == Global.u.Cedula /*&& newcontraseña.Text == newcontraseña_V.Text*/)
                        {
                            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                    .SetContext(context)
                                    .SetMessage("Actualizando Contraseña")
                                    .Build();
                            Esperar.Show();
                            //********************************************************************************************************
                            //************************************Actualizar Contraseña***********************************************
                            //********************************************************************************************************
                            _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                            UsuarioInstitucion user = new UsuarioInstitucion();
                            user.Id = Global.u.Id_Institucion;
                            user.Institucion = institucion.Text;
                            user.Direcccion = direccion.Text;
                            user.Usuario = usuario.Text;
                            user.Contraseña = newcontraseña.Text;//enviamos la nueva contraseña a actualizar
                            int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                            Esperar.Dismiss();
                            if (retorno == 1)
                            {
                                Global.passw = newcontraseña.Text;

                                //cerramos sesion al cambiar credenciales de acceso
                                Intent i = new Intent(context, typeof(LoginActivity));
                                //limpiar lista de item Matricula
                                Global.Lista_Grad.Clear();
                                Global.grupos.Clear();
                                Global.Lista_Estudi.Clear();
                                Global.datos_E.Clear();

                                //Limpiar lista de item Docentes
                                Global.usuariosWs.Clear();
                                Global.usuariosWs_Datos.Clear();

                                //limpiar lista de datos de grafico
                                Global.Lista_Grad_Graf.Clear();

                                Global.materia.Clear();//limpiar asignaturas
                                Global.detallenotas.Clear();//limpiar parciales
                                Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas

                                Toast.MakeText(Activity, "Contraseña Actualizado", ToastLength.Short).Show();
                                Dismiss();
                            }
                        }
                        else if (institucion.Text != Global.u.Institucion && usuario.Text == Global.user && direccion.Text == Global.u.Cedula  /*&& newcontraseña.Text == newcontraseña_V.Text*/)
                        {
                            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                    .SetContext(context)
                                    .SetMessage("Actualizando Contraseña y Nombre de Instituto")
                                    .Build();
                            Esperar.Show();
                            //********************************************************************************************************
                            //*******************************Actualizar Contraseña y nombre de instituto******************************
                            //********************************************************************************************************
                            _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                            UsuarioInstitucion user = new UsuarioInstitucion();
                            user.Id = Global.u.Id_Institucion;
                            user.Institucion = institucion.Text;
                            user.Direcccion = direccion.Text;
                            user.Usuario = usuario.Text;
                            user.Contraseña = newcontraseña.Text;//enviamos la nueva contraseña a actualizar
                            int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                            Esperar.Dismiss();
                            if (retorno == 1)
                            {
                                Global.passw = newcontraseña.Text;
                                Global.u.Institucion = institucion.Text;//actualizamos el nombre de institucion almacenado en datos de usuario logueado

                                //cerramos sesion al cambiar credenciales de acceso
                                Intent i = new Intent(context, typeof(LoginActivity));
                                //limpiar lista de item Matricula
                                Global.Lista_Grad.Clear();
                                Global.grupos.Clear();
                                Global.Lista_Estudi.Clear();
                                Global.datos_E.Clear();

                                //Limpiar lista de item Docentes
                                Global.usuariosWs.Clear();
                                Global.usuariosWs_Datos.Clear();

                                //limpiar lista de datos de grafico
                                Global.Lista_Grad_Graf.Clear();

                                Global.materia.Clear();//limpiar asignaturas
                                Global.detallenotas.Clear();//limpiar parciales
                                Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas


                                Toast.MakeText(Activity, "Nombre de Institucion y Contraseña Actualizado", ToastLength.Short).Show();
                                Dismiss();
                            }
                        }
                        else if (institucion.Text != Global.u.Institucion && usuario.Text != Global.user && direccion.Text == Global.u.Cedula  /*&& newcontraseña.Text == newcontraseña_V.Text*/)
                        {
                            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                    .SetContext(context)
                                    .SetMessage("Actualizando Contraseña, Nombre de Instituto y Usuario")
                                    .Build();
                            Esperar.Show();
                            //********************************************************************************************************
                            //********************Actualizar Contraseña, nombre de instituto y nombre de Usuario**********************
                            //********************************************************************************************************
                            _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                            UsuarioInstitucion user = new UsuarioInstitucion();
                            user.Id = Global.u.Id_Institucion;
                            user.Institucion = institucion.Text;
                            user.Direcccion = direccion.Text;
                            user.Usuario = usuario.Text;
                            user.Contraseña = newcontraseña.Text;//enviamos la nueva contraseña a actualizar
                            int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                            Esperar.Dismiss();
                            if (retorno == 1)
                            {
                                Global.passw = newcontraseña.Text;
                                Global.user = usuario.Text;//actualizamos el nombre de usuario en la variable global
                                Global.u.Institucion = institucion.Text;//actualizamos el nombre de institucion almacenado en datos de usuario logueado


                                //cerramos sesion al cambiar credenciales de acceso
                                Intent i = new Intent(context, typeof(LoginActivity));
                                //limpiar lista de item Matricula
                                Global.Lista_Grad.Clear();
                                Global.grupos.Clear();
                                Global.Lista_Estudi.Clear();
                                Global.datos_E.Clear();

                                //Limpiar lista de item Docentes
                                Global.usuariosWs.Clear();
                                Global.usuariosWs_Datos.Clear();

                                //limpiar lista de datos de grafico
                                Global.Lista_Grad_Graf.Clear();

                                Global.materia.Clear();//limpiar asignaturas
                                Global.detallenotas.Clear();//limpiar parciales
                                Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas

                                Toast.MakeText(Activity, "Nombre de Institucion, Usuario y Contraseña Actualizado", ToastLength.Short).Show();
                                Dismiss();
                            }
                        }
                        else if (institucion.Text != Global.u.Institucion && usuario.Text != Global.user && direccion.Text != Global.u.Cedula /*&& newcontraseña.Text == newcontraseña_V.Text*/)
                        {
                            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                    .SetContext(context)
                                    .SetMessage("Actualizando Contraseña, Nombre de Instituto, Usuario y Direccion")
                                    .Build();
                            Esperar.Show();
                            //********************************************************************************************************
                            //***************Actualizar Contraseña, nombre de instituto, nombre de Usuario y direccion****************
                            //********************************************************************************************************
                            _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                            UsuarioInstitucion user = new UsuarioInstitucion();
                            user.Id = Global.u.Id_Institucion;
                            user.Institucion = institucion.Text;
                            user.Direcccion = direccion.Text;
                            user.Usuario = usuario.Text;
                            user.Contraseña = newcontraseña.Text;//enviamos la nueva contraseña a actualizar
                            int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                            Esperar.Dismiss();
                            if (retorno == 1)
                            {
                                Global.passw = newcontraseña.Text;
                                Global.user = usuario.Text;//actualizamos el nombre de usuario en la variable global
                                Global.u.Cedula = direccion.Text;//actualizamos variable de dato de usuario loguiado
                                Global.u.Institucion = institucion.Text;//actualizamos el nombre de institucion almacenado en datos de usuario logueado


                                //cerramos sesion al cambiar credenciales de acceso
                                Intent i = new Intent(context, typeof(LoginActivity));
                                //limpiar lista de item Matricula
                                Global.Lista_Grad.Clear();
                                Global.grupos.Clear();
                                Global.Lista_Estudi.Clear();
                                Global.datos_E.Clear();

                                //Limpiar lista de item Docentes
                                Global.usuariosWs.Clear();
                                Global.usuariosWs_Datos.Clear();

                                //limpiar lista de datos de grafico
                                Global.Lista_Grad_Graf.Clear();

                                Global.materia.Clear();//limpiar asignaturas
                                Global.detallenotas.Clear();//limpiar parciales
                                Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas


                                Toast.MakeText(Activity, "Nombre de Institucion, Usuario, Direccion y Contraseña Actualizado", ToastLength.Short).Show();
                                Dismiss();
                            }
                        }
                        else if (institucion.Text == Global.u.Institucion && usuario.Text != Global.user && direccion.Text == Global.u.Cedula  /*&& newcontraseña.Text == newcontraseña_V.Text*/)
                        {
                            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                    .SetContext(context)
                                    .SetMessage("Actualizando Contraseña y Usuario")
                                    .Build();
                            Esperar.Show();
                            //********************************************************************************************************
                            //************************************Actualizar Contraseña y Usuario*************************************
                            //********************************************************************************************************
                            _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                            UsuarioInstitucion user = new UsuarioInstitucion();
                            user.Id = Global.u.Id_Institucion;
                            user.Institucion = institucion.Text;
                            user.Direcccion = direccion.Text;
                            user.Usuario = usuario.Text;
                            user.Contraseña = newcontraseña.Text;//enviamos la nueva contraseña a actualizar
                            int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                            Esperar.Dismiss();
                            if (retorno == 1)
                            {
                                Global.passw = newcontraseña.Text;
                                Global.user = usuario.Text;//actualizamos el nombre de usuario en la variable global


                                //cerramos sesion al cambiar credenciales de acceso
                                Intent i = new Intent(context, typeof(LoginActivity));
                                //limpiar lista de item Matricula
                                Global.Lista_Grad.Clear();
                                Global.grupos.Clear();
                                Global.Lista_Estudi.Clear();
                                Global.datos_E.Clear();

                                //Limpiar lista de item Docentes
                                Global.usuariosWs.Clear();
                                Global.usuariosWs_Datos.Clear();

                                //limpiar lista de datos de grafico
                                Global.Lista_Grad_Graf.Clear();

                                Global.materia.Clear();//limpiar asignaturas
                                Global.detallenotas.Clear();//limpiar parciales
                                Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas


                                Toast.MakeText(Activity, "Nombre de Usuario y Contraseña Actualizado", ToastLength.Short).Show();
                                Dismiss();
                            }
                        }
                        else if (institucion.Text == Global.u.Institucion && usuario.Text == Global.user && direccion.Text != Global.u.Cedula /*&& newcontraseña.Text == newcontraseña_V.Text*/)
                        {
                            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                    .SetContext(context)
                                    .SetMessage("Actualizando Contraseña y Direccion")
                                    .Build();
                            Esperar.Show();
                            //********************************************************************************************************
                            //***********************************Actualizar Contraseña y Direccion************************************
                            //********************************************************************************************************
                            _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                            UsuarioInstitucion user = new UsuarioInstitucion();
                            user.Id = Global.u.Id_Institucion;
                            user.Institucion = institucion.Text;
                            user.Direcccion = direccion.Text;
                            user.Usuario = usuario.Text;
                            user.Contraseña = newcontraseña.Text;//enviamos la nueva contraseña a actualizar
                            int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                            Esperar.Dismiss();
                            if (retorno == 1)
                            {
                                Global.passw = newcontraseña.Text;
                                Global.u.Cedula = direccion.Text;//actualizamos direccion de institucion almacenado en datos de usuario logueado


                                //cerramos sesion al cambiar credenciales de acceso
                                Intent i = new Intent(context, typeof(LoginActivity));
                                //limpiar lista de item Matricula
                                Global.Lista_Grad.Clear();
                                Global.grupos.Clear();
                                Global.Lista_Estudi.Clear();
                                Global.datos_E.Clear();

                                //Limpiar lista de item Docentes
                                Global.usuariosWs.Clear();
                                Global.usuariosWs_Datos.Clear();

                                //limpiar lista de datos de grafico
                                Global.Lista_Grad_Graf.Clear();

                                Global.materia.Clear();//limpiar asignaturas
                                Global.detallenotas.Clear();//limpiar parciales
                                Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas

                                Toast.MakeText(Activity, "Direccion y Contraseña Actualizado", ToastLength.Short).Show();
                                Dismiss();
                            }
                        }
                        else if (institucion.Text == Global.u.Institucion && usuario.Text != Global.user && direccion.Text != Global.u.Cedula  /*&& newcontraseña.Text == newcontraseña_V.Text*/)
                        {
                            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                    .SetContext(context)
                                    .SetMessage("Actualizando Contraseña, usuario y Direccion")
                                    .Build();
                            Esperar.Show();
                            //********************************************************************************************************
                            //***************************Actualizar Contraseña, usuario y Direccion***********************************
                            //********************************************************************************************************
                            _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                            UsuarioInstitucion user = new UsuarioInstitucion();
                            user.Id = Global.u.Id_Institucion;
                            user.Institucion = institucion.Text;
                            user.Direcccion = direccion.Text;
                            user.Usuario = usuario.Text;
                            user.Contraseña = newcontraseña.Text;//enviamos la nueva contraseña a actualizar
                            int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                            Esperar.Dismiss();
                            if (retorno == 1)
                            {
                                Global.passw = newcontraseña.Text;
                                Global.user = usuario.Text;//actualizamos el nombre de usuario en la variable global
                                Global.u.Cedula = direccion.Text;//actualizamos direccion de institucion almacenado en datos de usuario logueado


                                //cerramos sesion al cambiar credenciales de acceso
                                Intent i = new Intent(context, typeof(LoginActivity));
                                //limpiar lista de item Matricula
                                Global.Lista_Grad.Clear();
                                Global.grupos.Clear();
                                Global.Lista_Estudi.Clear();
                                Global.datos_E.Clear();

                                //Limpiar lista de item Docentes
                                Global.usuariosWs.Clear();
                                Global.usuariosWs_Datos.Clear();

                                //limpiar lista de datos de grafico
                                Global.Lista_Grad_Graf.Clear();

                                Global.materia.Clear();//limpiar asignaturas
                                Global.detallenotas.Clear();//limpiar parciales
                                Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas

                                Toast.MakeText(Activity, "Nombre de Usuario, Direccion y Contraseña Actualizado", ToastLength.Short).Show();
                                Dismiss();
                            }
                        }
                        else if (institucion.Text != Global.u.Institucion && usuario.Text == Global.user && direccion.Text != Global.u.Cedula  /*&& newcontraseña.Text == newcontraseña_V.Text*/)
                        {
                            Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                                    .SetContext(context)
                                    .SetMessage("Actualizando Contraseña, Nombre de Intituto y Direccion")
                                    .Build();
                            Esperar.Show();
                            //********************************************************************************************************
                            //***************************Actualizar Contraseña, Instituto y Direccion*********************************
                            //********************************************************************************************************
                            _Actualizar_Credenciales_Institucion = RestService.For<Interface_Actualizar_Credenciales_Institucion>("http://www.edunica.somee.com/api/UsuariosWS");
                            UsuarioInstitucion user = new UsuarioInstitucion();
                            user.Id = Global.u.Id_Institucion;
                            user.Institucion = institucion.Text;
                            user.Direcccion = direccion.Text;
                            user.Usuario = usuario.Text;
                            user.Contraseña = newcontraseña.Text;//enviamos la nueva contraseña a actualizar
                            int retorno = await _Actualizar_Credenciales_Institucion.Editar_Institucion(user);
                            Esperar.Dismiss();
                            if (retorno == 1)
                            {
                                Global.passw = newcontraseña.Text;
                                Global.u.Cedula = direccion.Text;//actualizamos direccion de institucion
                                Global.u.Institucion = institucion.Text;//actualizamos el nombre de institucion almacenado en datos de usuario logueado


                                //cerramos sesion al cambiar credenciales de acceso
                                Intent i = new Intent(context, typeof(LoginActivity));
                                //limpiar lista de item Matricula
                                Global.Lista_Grad.Clear();
                                Global.grupos.Clear();
                                Global.Lista_Estudi.Clear();
                                Global.datos_E.Clear();

                                //Limpiar lista de item Docentes
                                Global.usuariosWs.Clear();
                                Global.usuariosWs_Datos.Clear();

                                //limpiar lista de datos de grafico
                                Global.Lista_Grad_Graf.Clear();

                                Global.materia.Clear();//limpiar asignaturas
                                Global.detallenotas.Clear();//limpiar parciales
                                Global.notas_Estudiantes.Clear();//limpiar estudiantes con notas

                                Toast.MakeText(Activity, "Nombre de Institucion, DIreccion y Contraseña Actualizado", ToastLength.Short).Show();
                                Dismiss();
                            }
                        }
                    }
                    else//verificar que la nueva contraseña coincidan, para asi actualizarla
                    {
                        AlertDialog alert = new AlertDialog.Builder(context).Create();
                        alert.SetTitle("Error!");
                        alert.SetMessage("La contraseña de verificacion no coincide!");
                        alert.SetButton("Aceptar", (a, b) =>
                        {
                            newcontraseña_V.RequestFocus();
                            alert.Dismiss();
                        });
                        alert.Show();
                    }
                }
                else//la contraseña actual ingresada no es la misma, por lo que no se puede actualizar los datos
                {
                    AlertDialog alert = new AlertDialog.Builder(context).Create();
                    alert.SetTitle("Error!");
                    alert.SetMessage("Contraseña Actual incorrecta!");
                    alert.SetButton("Aceptar", (a, b) =>
                    {
                        contraseña.RequestFocus();
                        alert.Dismiss();
                    });
                    alert.Show();
                }
            }

            //***********************************************************************************
            //*******************************No permitir Campos vacio****************************
            //***********************************************************************************
            else if (institucion.Text == "")
            {
                Toast.MakeText(Activity, "Porfavor ingrese nombre de Institucion", ToastLength.Short).Show();
                institucion.RequestFocus();
            }
            else if (usuario.Text == "" )
            {
                Toast.MakeText(Activity, "Porfavor ingrese nombre de Usuario", ToastLength.Short).Show();
                usuario.RequestFocus();
            }
            else if (direccion.Text == "" )
            {
                Toast.MakeText(Activity, "Porfavor ingrese direccion de Institucion", ToastLength.Short).Show();
                direccion.RequestFocus();
            }
            else if (contraseña.Text == "")
            {
                Toast.MakeText(Activity, "Porfavor ingrese la Contraseña", ToastLength.Short).Show();
                contraseña.RequestFocus();
            }
            //***********************************************************************************
            //****************Verificar que ingrese la contraseña de verificacion****************
            //***********************************************************************************
            else if (institucion.Text != "" && usuario.Text != "" && direccion.Text != "" && contraseña.Text != "" && newcontraseña.Text != "" && newcontraseña_V.Text == "")
            {
                Toast.MakeText(Activity, "Porfavor ingrese la verificacion de la nueva contraseña", ToastLength.Short).Show();
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
            return inflater.Inflate(Resource.Layout.Actualizar_Credenciales_Institucion, container, false);
        }
    }
}