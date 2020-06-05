﻿using System;
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
namespace eduNICA
{
    [Activity(Label = "Adapter_Lista_Grupo")]
    public class Adapter_Lista_Grupo : BaseAdapter
    {
        Activity context;//definimos el origen del listview
        List<grupos_grados> lista1;//para vinculamos listview

        public Adapter_Lista_Grupo(Activity context)
        {
            this.context = context;
            this.lista1 = Global.grupos;
        }

        public override int Count => lista1.Count();

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = lista1[position];
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.Plantilla_Listar_Grado, null);//aplicamos el formato predefinido
            }
            view.FindViewById<TextView>(Resource.Id.Grado_Academico).Text = item.Grupo;
            view.FindViewById<TextView>(Resource.Id.Cantidad).Text = item.cantidad + " Estudiantes";
            return view;
        }
    }
}