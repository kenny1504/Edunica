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
    [Activity(Label = "Adapter_Instit_Lista_VerNotasEstudiante")]
    public class Adapter_Instit_Lista_VerNotasEstudiante : BaseAdapter
    {
        Activity context;//definimos el origen del listview
        List<Notas_Estudiante> vlista;//para vinculamos listview

        public Adapter_Instit_Lista_VerNotasEstudiante(Activity context)
        {
            this.context = context;
            this.vlista = Global.notas_Estudiantes;
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
                view = context.LayoutInflater.Inflate(Resource.Layout.Plantilla_Listar_Grado, null);//aplicamos el formato predefinido
            }
            view.FindViewById<TextView>(Resource.Id.Titulo).Text = item.Nombre;
            view.FindViewById<TextView>(Resource.Id.Subtitulo).Text = "Nota de Parcial: "+item.Nota;
            view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(Resource.Drawable.lista_estudiante);
            return view;
        }
    }
}