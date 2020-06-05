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
using eduNICA.Resources.Model;

namespace eduNICA
{
    [Activity(Label = "Adapter_Lista_Grado")]
    public class Adapter_Lista_Grado : BaseAdapter
    {
        Activity context;//definimos el origen del listview
        List<Estudiantes_grados> lista1;//para vinculamos listview

        public Adapter_Lista_Grado(Activity context)
        {
            this.context = context;
            this.lista1 = Global.Lista_Grad;
        }

        public override int Count => lista1.Count;

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
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2,null);//aplicamos el formato predefinido
                view.FindViewById<TextView>(Resource.Id.text1).Text ="Grado "+item.Grado;
                view.FindViewById<TextView>(Resource.Id.text2).Text = "Cantidad de Estudiante: " + item.cantidad;
            }
            return view;
        }
    }
}