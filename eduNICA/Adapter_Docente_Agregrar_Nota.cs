using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using eduNICA.Resources.Model;
using Java.Lang;
using Java.Security;

namespace eduNICA
{
    public class Adapter_Docente_Agregrar_Nota : BaseAdapter<Lista_Estudiante_Nota>
    {
        Activity context;//definimos el origen del listview
        List<Lista_Estudiante_Nota> vlista;//para vinculamos listview

        public Adapter_Docente_Agregrar_Nota(Activity context)
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
            _Nota _nota = null;
            //if (view != null)
            //{
            //    //_Nota = view.Tag as _Nota;
            //    //_Nota.nota.Tag = position;
            //}
            if(view==null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.Plantilla_Docent_Nota, null);//aplicamos el formato predefinido

                int nPosition = (int)view.Tag;
                System.Diagnostics.Debug.WriteLine("position  " + nPosition);
                _nota = new _Nota();

                _nota.nota =view.FindViewById<EditText>(Resource.Id.editText_NotaEstudiante);
                _nota.codigo= view.FindViewById<TextView>(Resource.Id.Asignatura);
                //_nota.nota.Text = "0";
                //_nota.id1 = position;
                _nota.nota.Tag = position;//edittext nota
                view.Tag = _nota;
            }
            else
            {
                _nota = view.Tag as _Nota ;
                _nota.nota.Tag = position;
            }
            //Lista_Estudiante_Nota _Estudiante_Nota = this.vlista[position];
            view.FindViewById<TextView>(Resource.Id.Nombre_Estudiante_).Text = item.Nombre;
            view.FindViewById<TextView>(Resource.Id.Asignatura).Text ="Codigo: "+ item.CodigoEstudinte;
            view.FindViewById<EditText>(Resource.Id.editText_NotaEstudiante).Text = item.nota;

            //_nota.nota.AddTextChangedListener(new MyTextWatcher(this.context, _nota, vlista));
            _nota.nota.Text = "" +vlista[position].nota;
            _nota.nota.AfterTextChanged += (sender, args) =>
              {
                  if (_nota.codigo.Text == vlista[position].CodigoEstudinte)
                  {
                      vlista[position].nota =args.Editable.ToString();
                  }
              };

            //////////////////EditText text = view.FindViewById<EditText>(Resource.Id.editText_NotaEstudiante);
            //////////////////text.AfterTextChanged += (sender, args) =>
            //////////////////{
            //////////////////    Global.addnota(item.CodigoEstudinte, text);
            //////////////////};

            ///////////////////int positio = (int)_Nota.nota.Tag;
            /////////////////////_Nota.nota.Id = positio;

            /////////////////////_Nota.nota.AddTextChangedListener(new MyTextWatcher(this.context,vlista,_Nota));

            //_Nota.nota.TextChanged += (sender, args) =>
            //{
            //    //Lista_Estudiante_Nota lista = vlista[position];
            //    //lista.nota=
            //    vlista[positio].nota = (int)Integer.ValueOf(_Nota.nota.Text);
            //};

            //view.FindViewById<EditText>(Resource.Id.editText_NotaEstudiante).Text = item.nota.ToString();

            //_Nota.nota.AddTextChangedListener(new ITextWatcher()
            //{

            //});
            //guardar.Estado_Asist.SetOnCheckedChangeListener(null);

            //guardar.Estado_Asist.Checked = item.estado;
            //guardar.Estado_Asist.SetOnCheckedChangeListener(new CheckedChangeListener(this.context, vlista));

            //_Nota.nota.TextChanged += Nota_TextChanged;
            return view;
        }

        //private void Nota_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        //{
        //    if (vlista[positio)
        //    {

        //    }
        //    else
        //    {

        //    }
        //}
        //public class MyTextWatcher : Java.Lang.Object, ITextWatcher
        //{
        //    private Activity context;
        //    private _Nota nota;
        //    private List<Lista_Estudiante_Nota> vlista;

        //    public MyTextWatcher(Activity context, _Nota nota, List<Lista_Estudiante_Nota> vlista)
        //    {
        //        this.context = context;
        //        this.nota = nota;
        //        this.vlista = vlista;
        //    }

        //    public void AfterTextChanged(IEditable s) { }
        //    public void BeforeTextChanged(Java.Lang.ICharSequence arg0, int start, int count, int after) { }
        //    public void OnTextChanged(Java.Lang.ICharSequence arg0, int start, int before, int count)
        //    {
        //        int position =nota.nota.id1;
        //        Lista_Estudiante_Nota _Estudiante_Nota = vlista[position];
        //        _Estudiante_Nota.nota = Convert.ToInt32(nota.nota.Text);
        //        vlista[position].nota = Convert.ToInt32(nota.nota.Text);

        //    }
        //}

        public class _Nota : Java.Lang.Object
        {
            public EditText nota { get; set; }
            public TextView codigo { get; set; }
            public int id1 { get; set; }
    }
}
}