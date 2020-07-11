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
    public class Adapter_Admin_Usuarios : BaseAdapter
    {
        Activity context;//definimos el origen del listview
        List<UsuariosADMIN> vlista;//para vinculamos listview

        public Adapter_Admin_Usuarios(Activity context, List<UsuariosADMIN> vlista)
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
                view = context.LayoutInflater.Inflate(Resource.Layout.Plantilla_Listar_Usuario, null);//aplicamos el formato personalizado
            view.FindViewById<TextView>(Resource.Id.Nombre_Usuario).Text = item.Nombre;
            view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(Resource.Drawable.interf);
            view.FindViewById<TextView>(Resource.Id.Nombre_User1).TextSize = 15;
            view.FindViewById<TextView>(Resource.Id.Nombre_User1).Text = "Institucion: " + item.Institucion;
            return view;
        }
    }
}