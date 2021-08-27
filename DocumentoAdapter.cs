using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cafeteriaPiedad
{
    class DocumentoAdapter : BaseAdapter<Documento>
    {
        private static View convertView;
        List<Documento> items;
        Activity context;
        View view = convertView;

        TextView tvdocProducto;
        TextView tvdocCant;

        public DocumentoAdapter(Activity context, List<Documento> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override Documento this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            try
            {
                var item = items[position];

                if (convertView == null)
                {
                    convertView = context.LayoutInflater.Inflate(Resource.Layout.ListaItem, null);
                }

                tvdocProducto = convertView.FindViewById<TextView>(Resource.Id.tvProducto);
                tvdocProducto.Text = item.Producto;
                tvdocCant = convertView.FindViewById<TextView>(Resource.Id.tvCant);
                tvdocCant.Text = Convert.ToString(item.Cantidad);

                return convertView;
            }
            catch (Exception ex)
            {

                return convertView;
            }

        }
    }
}