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
    class Documento
    {
        private string producto;
        private int cantidad;

        public string Producto { get => producto; set => producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
    }
}