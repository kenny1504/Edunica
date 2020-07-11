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
    public class Adapter_Docente_Nota_Agregar : BaseAdapter<Lista_Estudiante_Nota>
    {
        Activity context;//definimos el origen del listview
        List<Lista_Estudiante_Nota> vlista;//para vinculamos listview

        public Adapter_Docente_Nota_Agregar(Activity context)
        {
            this.context = context;
            this.vlista = Global._Notas;
        }
        public override Lista_Estudiante_Nota this[int position] { get { return vlista[position]; } }
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
            _Nota _nota = new _Nota();
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.Plantilla_Docent_Nota,null);//aplicamos el formato predefinido

                _nota.nota = view.FindViewById<EditText>(Resource.Id.editText_NotaEstudiante);
                _nota.codigo = view.FindViewById<TextView>(Resource.Id.Asignatura);
                _nota.nota.Tag = position;//edittext nota
                view.Tag = _nota;
            }
            else
            {
                _nota = view.Tag as _Nota;
                _nota.nota.Tag = position;
            }
            view.FindViewById<TextView>(Resource.Id.Nombre_Estudiante_).Text = item.Nombre;
            view.FindViewById<TextView>(Resource.Id.Asignatura).Text = item.CodigoEstudinte;
            view.FindViewById<EditText>(Resource.Id.editText_NotaEstudiante).Text = item.nota;


            _nota.nota.Text = "" + vlista[position].nota;
            _nota.nota.AfterTextChanged += (sender, args) =>
            {
                if (_nota.codigo.Text == vlista[position].CodigoEstudinte)
                {
                    vlista[position].nota = args.Editable.ToString();
                }
            };
            return view;
        }
        public class _Nota : Java.Lang.Object
        {
            public EditText nota { get; set; }
            public TextView codigo { get; set; }
            public int id1 { get; set; }
        }
    }
}