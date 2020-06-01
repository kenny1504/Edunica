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

namespace eduNICA
{
    [Activity(Label = "Activity_Asistencia_Estudiante_Docente")]
    public class Activity_Docente_Asistencia_Estudiante : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Docente_Asistencia_Estudiante);
            // Create your application here
        }
    }
}