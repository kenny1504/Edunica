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
    [Activity(Label = "Adapter_Lista_Usuario")]
    public class Adapter_Instit_Lista_Usuario : BaseAdapter
    {
        Activity context;//definimos el origen del listview
        List<usuariosWS> lista1;//para vinculamos listview
        //constructor
        public Adapter_Instit_Lista_Usuario(Activity context, List<usuariosWS> lista1)
        {
            this.context = context;
            this.lista1 = lista1;
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
            if(view==null)
                view = context.LayoutInflater.Inflate(Resource.Layout.Plantilla_Listar_Usuario, null);//aplicamos el formato predefinido
            view.FindViewById<TextView>(Resource.Id.Nombre_Usuario).Text = item.Nombre;
            view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(Resource.Drawable.interf);
            view.FindViewById<TextView>(Resource.Id.Nombre_User1).Text = "Nombre de Usuario: " + item.NombreDeUsuario;
            return view;
        }
    }
}