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
    [Activity(Label = "Activity_Instit_Matricula_Grado_Estudiante")]
    public class Activity_Instit_Matricula_Grado_Estudiante : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Instit_Matricula_Grado_Estudiante);
            // Create your application here
        }
    }
}