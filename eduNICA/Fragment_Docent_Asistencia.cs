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
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;

namespace eduNICA
{
    public class Fragment_Docent_Asistencia : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            BottomNavigationView navigation = View.FindViewById<BottomNavigationView>(Resource.Id.navigation_asistencia);
            seSetupDrawerContent(navigation);
            cargarinicio();
        }
        public void seSetupDrawerContent(BottomNavigationView navigation)
        {
            navigation.NavigationItemSelected += (sender, e) =>
            {
                //e.Item.SetChecked(true);
                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                switch (e.Item.ItemId)
                {
                    case Resource.Id.asistencia_dashboard:
                        Fragment_Docent_Asistencia_Ver _Docent_Asistencia_Ver = new Fragment_Docent_Asistencia_Ver();
                        ft.Replace(Resource.Id.linearLayout_docent_Asignatura, _Docent_Asistencia_Ver);
                        ft.DisallowAddToBackStack();
                        break;
                    case Resource.Id.addasistencia_dashboard:
                        Fragment_Docent_Asistencia_ListaEstudiante _Docent_Asistencia_ListaEstudiante = new Fragment_Docent_Asistencia_ListaEstudiante();
                        ft.Replace(Resource.Id.linearLayout_docent_Asignatura, _Docent_Asistencia_ListaEstudiante);
                        ft.DisallowAddToBackStack();
                        break;
                }
                ft.Commit();
            };
        }
        public void cargarinicio()
        {
            FragmentTransaction ft = this.FragmentManager.BeginTransaction();
            Fragment_Docent_Asistencia_Ver _Docent_Asistencia_Ver = new Fragment_Docent_Asistencia_Ver();
            ft.Replace(Resource.Id.linearLayout_docent_Asignatura, _Docent_Asistencia_Ver);
            ft.DisallowAddToBackStack();
            ft.Commit();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.Docent_Asistencia, container, false);
        }
    }
}