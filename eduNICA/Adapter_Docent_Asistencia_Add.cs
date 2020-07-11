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
    [Activity(Label = "Adapter_Instit_Asistencia_Add")]
    public class Adapter_Docent_Asistencia_Add : BaseAdapter<Lista_Estudiante_Asistencia>
    {
        Activity context;//definimos el origen del listview
        List<Lista_Estudiante_Asistencia> vlista;//para vinculamos listview
        public Adapter_Docent_Asistencia_Add(Activity context)
        {
            this.context = context;
            this.vlista = Global._Asistencia;
        }
        public override Lista_Estudiante_Asistencia this[int position] { get { return vlista[position]; } }

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
            Guardar guardar = new Guardar();
            if (view != null)
            {
                guardar = view.Tag as Guardar;
                guardar.Estado_Asist.Tag = position;
            }
            else
            {               
                view = this.context.LayoutInflater.Inflate(Resource.Layout.Plantilla_Docent_Pasar_Asistencia, null);//aplicamos el formato predefinido

                guardar.Estado_Asist = view.FindViewById<CheckBox>(Resource.Id.cb_asistencia);
                guardar.Estado_Asist.Tag = position;//checkbox asistencia
                view.Tag = guardar;
            }
            Lista_Estudiante_Asistencia listaws = this.vlista[position];
            view.FindViewById<TextView>(Resource.Id.Nombre_Estudiante_Asistencia).Text = listaws.Nombre;
            view.FindViewById<CheckBox>(Resource.Id.cb_asistencia).Checked = listaws.estado;


            guardar.Estado_Asist.SetOnCheckedChangeListener(null);
            guardar.Estado_Asist.Checked = item.estado;
            guardar.Estado_Asist.SetOnCheckedChangeListener(new CheckedChangeListener(this.context, vlista));

            return view;
        }
        public class Guardar : Java.Lang.Object
        {
            public CheckBox Estado_Asist { get; set; }
            //public TextView nombre { get; set; }
        }
        private class CheckedChangeListener : Java.Lang.Object, CompoundButton.IOnCheckedChangeListener
        {
            private Activity activity;
            private List<Lista_Estudiante_Asistencia> list;
            public CheckedChangeListener(Activity activity, List<Lista_Estudiante_Asistencia> listaEstudiantes)
            {
                this.activity = activity;
                this.list = listaEstudiantes;
            }
            public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
            {
                if (isChecked)
                {
                    int position = (int)buttonView.Tag;
                    Lista_Estudiante_Asistencia item = list[position];
                    item.estado = true;
                    list[position].estado = true;
                }
                else
                {
                    int position = (int)buttonView.Tag;                   
                    list[position].estado = false;
                }
            }
        }
    }
}