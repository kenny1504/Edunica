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
    [Activity(Label = "Adapter_Instit_Lista_Asignatura")]
    public class Adapter_Instit_Lista_Asignatura : BaseAdapter
    {
        Activity context;//definimos el origen del listview
        List<Asignaturas> vlista;//para vinculamos listview

        public Adapter_Instit_Lista_Asignatura(Activity context)
        {
            this.context = context;
            this.vlista = Global.materia;
        }

        public override int Count => vlista.Count;

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
            var item = vlista[position];
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.Plantilla_Instit_Nota_Asignatura, null);//aplicamos el formato predefinido
            }
            view.FindViewById<TextView>(Resource.Id.Titulo2).Text = item.Nombre;
            return view;
        }
    }
}