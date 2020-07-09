using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using eduNICA.Resources.Model;
using Refit;
using eduNICA.Resources.Intarface;
using EDMTDialog;

namespace eduNICA
{
    public class Fragment_Admin_Usuarios : Fragment
    {
        EditText filtro;Button buscar;
        ListView vlista; Context context; //Instalcia de context
        Interface_Admin_Usuarios _Usuarios;
        Android.Support.V7.Widget.Toolbar toolbar;
        FilterableAdapter _adapter;
        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            toolbar = Activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            filtro = View.FindViewById<EditText>(Resource.Id.editText_filtrar_Admin_user);
            buscar = View.FindViewById<Button>(Resource.Id.btn_filtrar_usuarioADMIN);
            vlista = View.FindViewById<ListView>(Resource.Id.LV_Admin_usuario);//vinculamos al listview del layout
            //_adapter= new FilterableAdapter(Activity, Android.Resource.Layout.SimpleListItem1, GetItems());
            vlista.Adapter = _adapter;
            //string[] GetItems()
            //{
            //    //string[] names = Global.usuariosADMINs[].Nombre;
            //    List<string> res = new List<string>();
            //    for (int i = 0; i < Global.usuariosADMINs.Count; i++)
            //    {
            //        res.Add(names[names.Length]);
            //    }
            //    return res.ToArray();
            //}
            filtro.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                // filter on text changed
                var searchTerm = filtro.Text;
                if (String.IsNullOrEmpty(searchTerm))
                {
                    _adapter.ResetSearch();
                }
                else
                {
                    _adapter.Filter.InvokeFilter(searchTerm);
                }
            };
            if (Global.usuariosADMINs.Count == 0)
            {
                Android.Support.V7.App.AlertDialog Esperar = new EDMTDialogBuilder()
                    .SetContext(context)
                    .SetMessage("Cargando ...")
                    .Build();
                Esperar.Show();
                //Establecemos la concexion con el servicio web API REST
                _Usuarios = RestService.For<Interface_Admin_Usuarios>("http://www.edunica.somee.com/api/UsuariosWS");

                //hacemos peticion mediante el metodo de la interface 
                List<UsuariosADMIN> E_lista = await _Usuarios.Usuarios();

                for (int i = 0; i < E_lista.Count; i++)
                {
                    UsuariosADMIN W = new UsuariosADMIN();
                    W.Nombre = E_lista[i].Nombre;
                    W.Id = E_lista[i].Id;
                    W.Institucion = E_lista[i].Institucion;
                    Global.usuariosADMINs.Add(W);
                }
                vlista.Adapter = new Adapter_Admin_Usuarios(Activity);
                Esperar.Dismiss();//Cerramos mensaje
            }
            else
                vlista.Adapter = new Adapter_Admin_Usuarios(Activity);
            vlista.ItemClick += Vlista_ItemClick;

        }

        private void Vlista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
            toolbar.Title = "Informacion de Usuario";
            Fragment_Admin_Usuarios_Detalle _Usuarios_Detalle = new Fragment_Admin_Usuarios_Detalle();
            UsuariosADMIN modulo = Global.usuariosADMINs[e.Position];
            Global.iddocente = modulo.Id;
            ft.Replace(Resource.Id.relativeLayoutMenu, _Usuarios_Detalle).DisallowAddToBackStack().Commit();
        }
        //**************************************************************************************************************
        //**************************************************************************************************************
        //**************************************************************************************************************
        public class FilterableAdapter : ArrayAdapter, IFilterable
        {
            LayoutInflater inflater;
            Filter filter;
            Activity context;
            public string[] AllItems;
            public string[] MatchItems;

            public FilterableAdapter(Activity context, int txtViewResourceId, string[] items) : base(context, txtViewResourceId, items)
            {
                inflater = context.LayoutInflater;
                filter = new SuggestionsFilter(this);
                AllItems = items;
                MatchItems = items;
            }
            public override int Count
            {
                get
                {
                    return MatchItems.Length;
                }
            }

            public override Java.Lang.Object GetItem(int position)
            {
                return MatchItems[position];
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = convertView;
                if (view == null)
                    view = context.LayoutInflater.Inflate(Resource.Layout.Plantilla_Listar_Usuario, null);
                view.FindViewById<TextView>(Resource.Id.Nombre_Usuario).Text = MatchItems[position];
                //view.FindViewById<TextView>(Resource.Id.Nombre_User1).Text = MatchItems[position];

                return view;
            }
            public override Filter Filter
            {
                get
                {
                    return filter;
                }
            }

            public void ResetSearch()
            {
                MatchItems = AllItems;
                NotifyDataSetChanged();
            }
            class SuggestionsFilter : Filter
            {
                readonly FilterableAdapter _adapter;

                public SuggestionsFilter(FilterableAdapter adapter) : base()
                {
                    _adapter = adapter;
                }

                protected override Filter.FilterResults PerformFiltering(Java.Lang.ICharSequence constraint)
                {
                    FilterResults results = new FilterResults();
                    if (!String.IsNullOrEmpty(constraint.ToString()))
                    {
                        var searchFor = constraint.ToString();
                        Console.WriteLine("searchFor:" + searchFor);
                        var matchList = new List<string>();

                        var matches =
                            from i in _adapter.AllItems
                            where i.IndexOf(searchFor, StringComparison.InvariantCultureIgnoreCase) >= 0
                            select i;

                        foreach (var match in matches)
                        {
                            matchList.Add(match);
                        }

                        _adapter.MatchItems = matchList.ToArray();
                        Console.WriteLine("resultCount:" + matchList.Count);

                        Java.Lang.Object[] matchObjects;
                        matchObjects = new Java.Lang.Object[matchList.Count];
                        for (int i = 0; i < matchList.Count; i++)
                        {
                            matchObjects[i] = new Java.Lang.String(matchList[i]);
                        }

                        results.Values = matchObjects;
                        results.Count = matchList.Count;
                    }
                    else
                    {
                        _adapter.ResetSearch();
                    }
                    return results;
                }

                protected override void PublishResults(Java.Lang.ICharSequence constraint, Filter.FilterResults results)
                {
                    _adapter.NotifyDataSetChanged();
                }
            }
        }
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            context = inflater.Context;
            return inflater.Inflate(Resource.Layout.Admin_Usuarios, container, false);
        }
    }
}