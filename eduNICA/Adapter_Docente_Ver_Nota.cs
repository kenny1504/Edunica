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
    public class Adapter_Docente_Ver_Nota : BaseAdapter
    {
        Activity context;//definimos el origen del listview
        List<Notas_Estudiante> vlista;//para vinculamos listview

        public Adapter_Docente_Ver_Nota(Activity context)
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
                view = context.LayoutInflater.Inflate(Resource.Layout.Plantilla_Docent_Nota, null);//aplicamos el formato predefinido
            }
            view.FindViewById<TextView>(Resource.Id.Nombre_Estudiante_).Text = item.Nombre;
            view.FindViewById<TextView>(Resource.Id.Asignatura).Text = Global.asignatura;
            view.FindViewById<TextView>(Resource.Id.Texto_Editable).Text = "";
            view.FindViewById<EditText>(Resource.Id.editText_NotaEstudiante).Text = item.Nota.ToString();
            view.FindViewById<EditText>(Resource.Id.editText_NotaEstudiante).Enabled = false;
            return view;
        }
    }
}