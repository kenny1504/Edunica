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
    public class Adapter_Docente_Asistencia_Fechas : BaseAdapter
    {
        Activity context;//definimos el origen del listview
        List<string> vlista;//para vinculamos listview

        public Adapter_Docente_Asistencia_Fechas(Activity context, List<string> vlista)
        {
            this.context = context;
            this.vlista = vlista;
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
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);//aplicamos el formato predefinido
            }
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item;
            return view;
        }
    }
}