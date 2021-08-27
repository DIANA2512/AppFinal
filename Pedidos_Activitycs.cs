using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using cafeteriaPiedad;
using Android.Views;
using System.Collections.Generic;
using System.Data;
using System;
using static Android.Widget.AdapterView;
using Android.Content;

namespace cafeteriaPiedad
{
    [Activity(Label = "Registro de Pedido", Theme = "@style/AppTheme")]
    public class Pedidos_Activitycs:AppCompatActivity
    {
        Button btnEntrar;
        Button btnSalir;
        Button btnanular;
        

        ImageButton btnagregar;
        ImageButton btnbuscar;
        ImageButton btnnuevo;
        ImageButton btnbuscarcliente;
        ImageButton btnnuevocliente;

        EditText edtnumPedido;
        EditText edtpass;
        EditText edtnumprod;
        EditText edtnombreCliente;
        EditText edittextcliente;
        TextView txttotal;
        TextView txttiempo;
        TextView txtcliente;

        Spinner spnproducto;
        Spinner spncategoria;

        ListView lvdocumento;
        List<Documento> _doucmento = new List<Documento>();
        string txtbusqueda;

        private List<KeyValuePair<string, string>> planets;
        private WS_Conexion.Service_Bdd servicioBase = new WS_Conexion.Service_Bdd();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Pedido);

            btnEntrar = FindViewById<Button>(Resource.Id.btningresar);
            btnSalir = FindViewById<Button>(Resource.Id.btnsalir);
            btnagregar = FindViewById<ImageButton>(Resource.Id.btnagregar);
            btnbuscar = FindViewById<ImageButton>(Resource.Id.btnbuscar);
            btnnuevo = FindViewById<ImageButton>(Resource.Id.btnnuevo);
            btnbuscarcliente = FindViewById<ImageButton>(Resource.Id.btnbuscarcliente);
            btnanular = FindViewById<Button>(Resource.Id.btnanular);
            btnnuevocliente = FindViewById<ImageButton>(Resource.Id.btnnuevocliente);

            lvdocumento = FindViewById<ListView>(Resource.Id.lvdocumento);

            edtnumPedido = FindViewById<EditText>(Resource.Id.edtnumPedido);
            edtpass = FindViewById<EditText>(Resource.Id.edtpass);
            edtnumprod= FindViewById<EditText>(Resource.Id.edtnumprod);
            edtnombreCliente = FindViewById<EditText>(Resource.Id.edtnombreCliente);
            edittextcliente = FindViewById<EditText>(Resource.Id.edittextcliente);

            txttotal = FindViewById<TextView>(Resource.Id.txtusuario2);
            txttiempo = FindViewById<TextView>(Resource.Id.txttiempo);
            txtcliente = FindViewById<TextView>(Resource.Id.txtcliente);
            spnproducto = FindViewById<Spinner>(Resource.Id.spnproducto);
            spncategoria = FindViewById<Spinner>(Resource.Id.spncategoria);
            


            btnEntrar.Click += BtnEntrar_Click;
            btnSalir.Click += BtnSalir_Click;
            btnagregar.Click += Btnagregar_Click;
            btnbuscar.Click += Btnbuscar_Click;
            btnnuevo.Click += Btnnuevo_Click;
            btnanular.Click += Btnanular_Click;
            btnnuevocliente.Click += Btnnuevocliente_Click;  
            btnbuscarcliente.Click += Btnbuscarcliente_Click;
            lvdocumento.ItemLongClick += Lvdocumento_ItemLongClick;


            spncategoria.ItemSelected += Spncategoria_ItemSelected;

            numeroPedido();

            spncategoriasCargar();
            cargarTotal(Convert.ToInt32(edtnumPedido.Text));
            edtnumPedido.Enabled = false;
            
        }

        private void Btnnuevocliente_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Cliente_Activity));
            StartActivity(intent);
        }

        private void Lvdocumento_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            Convert.ToInt32(edtnumPedido.Text);
            List<Documento> list = _doucmento;

            string product = list[e.Position].Producto.ToString();
            string cantidad = list[e.Position].Cantidad.ToString();

            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("Eliminar producto");
            alert.SetMessage("¿Desea eliminar producto?");
            alert.SetCancelable(false);
            alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
            {
                //procedimiento eliminar prodiucto
                servicioBase.EliminarProducto(Convert.ToInt32(edtnumPedido.Text),product,Convert.ToInt32(cantidad));
                ActualizarRegistros();
                cargarTotal(Convert.ToInt32(edtnumPedido.Text));
            });
            alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
            {
            });
            Dialog dialog = alert.Create();
            dialog.Show();


        }

        private void Btnbuscarcliente_Click(object sender, EventArgs e)
        {
            if (edittextcliente.Text == "")
            {
                var toast = Toast.MakeText(this, "Ingrese un Cliente", ToastLength.Short);
                toast.Show();
                return;
            }
            else 
            { 
                edtnombreCliente.Text = servicioBase.BusquedaClienteMovil(edittextcliente.Text);

                if (edtnombreCliente.Text == "")
                {
                    var toast = Toast.MakeText(this, "Cliente no registrado o inactivo", ToastLength.Short);
                    toast.Show();
                }

            }
            
        }

        private void Btnanular_Click(object sender, EventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("Anular pedido");
            alert.SetMessage("¿Desea anular pedido?");
            alert.SetCancelable(false);
            alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
            {
                servicioBase.EstadoPedido(Convert.ToInt32(edtnumPedido.Text), "ANULAR");
                ActualizarRegistros();
                cargarTotal(Convert.ToInt32(edtnumPedido.Text));
                btnagregar.Visibility =ViewStates.Invisible;
                btnEntrar.Visibility = ViewStates.Invisible;
                btnanular.Visibility = ViewStates.Invisible;

            });
            alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
            {
            });
            Dialog dialog = alert.Create();
            dialog.Show();

            mostrarTiempo(Convert.ToInt32(edtnumPedido.Text));
        }

        private void Btnnuevo_Click(object sender, EventArgs e)
        {
            numeroPedido();
            cargarTotal(Convert.ToInt32(edtnumPedido.Text));
            ActualizarRegistros();
            edittextcliente.Text = "";
            edtnombreCliente.Text = "";
            btnagregar.Visibility = ViewStates.Visible;
            btnEntrar.Visibility = ViewStates.Visible;
            btnanular.Visibility = ViewStates.Visible;
            txttiempo.Visibility = ViewStates.Invisible;
            edtnumPedido.Enabled = false;
            lvdocumento.Enabled = true;
        }

        public void cargarTotal(int idpedido) 
        {
            txttotal.Text= "Total: $ "+servicioBase.TotalPedido(idpedido);
        }

        public void mostrarTiempo(int pedido)
        {
            txttiempo.Text = "Tiempo: " + servicioBase.TiempoEspera(pedido);

            if (servicioBase.TiempoEspera(pedido) != "")
            {
                btnEntrar.Visibility = ViewStates.Invisible;
                btnanular.Visibility = ViewStates.Invisible;
                btnagregar.Visibility = ViewStates.Invisible;
                lvdocumento.Enabled = false;
            }
            else
            {
                btnEntrar.Visibility = ViewStates.Visible;
                btnanular.Visibility = ViewStates.Visible;
                btnagregar.Visibility = ViewStates.Visible;
                lvdocumento.Enabled = true;
            }

            txttiempo.Visibility = ViewStates.Visible;

        }
        public void spncategoriasCargar() 
        {
            //categorias spinner
            var adapter = new ArrayAdapter<string>(this,
             Android.Resource.Layout.SimpleSpinnerItem, servicioBase.ConsultaCategoria());
            spncategoria.Adapter = adapter;

            
            
        }

        private void Spncategoria_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //productos spinner
            var adapter = new ArrayAdapter<string>(this,
             Android.Resource.Layout.SimpleSpinnerItem, servicioBase.ConsultaProducto(spncategoria.SelectedItem.ToString()));
            spnproducto.Adapter = adapter;

        }

        

        private void Btnbuscar_Click(object sender, System.EventArgs e)
        {
            _doucmento.Clear();
            DataSet documento = servicioBase.BusquedaDocumento(Convert.ToInt32(edtnumPedido.Text));
            cargarTotal(Convert.ToInt32(edtnumPedido.Text));
            mostrarTiempo(Convert.ToInt32(edtnumPedido.Text));
            edtnumPedido.Enabled = true;

            if (documento.Tables[0].Rows.Count <= 0) 
            {
                var toast = Toast.MakeText(this, "Pedido no existente o Anulado", ToastLength.Short);
                toast.Show();

                btnagregar.Visibility = ViewStates.Invisible;
            }

            try
            {
                foreach (DataRow row in documento.Tables[0].Rows)
                {
                    Documento doc = new Documento();
                    doc.Producto = row["PRODUCTO"].ToString();
                    doc.Cantidad = Convert.ToInt32(row["UNIDAD"].ToString()); ;
                    edtnombreCliente.Text= row["CLIENTE"].ToString();
                    _doucmento.Add(doc);
                }
                lvdocumento.Adapter = new DocumentoAdapter(this, _doucmento);
            }
            catch (Exception ex)
            {
                var toast = Toast.MakeText(this, "No se encontro resultados", ToastLength.Short);
                toast.Show();
                
            }
        }

        private void ActualizarRegistros()
        {
            _doucmento.Clear();
            DataSet documento = servicioBase.BusquedaDocumento(Convert.ToInt32(edtnumPedido.Text));
            try
            {
                foreach (DataRow row in documento.Tables[0].Rows)
                {
                    Documento doc = new Documento();
                    doc.Producto = row["PRODUCTO"].ToString();
                    doc.Cantidad = Convert.ToInt32(row["UNIDAD"].ToString()); ;

                    _doucmento.Add(doc);
                }
                lvdocumento.Adapter = new DocumentoAdapter(this, _doucmento);

                edtnumprod.Text = "0";
            }
            catch (Exception ex)
            {
                var toast = Toast.MakeText(this, "No se encontro resultados", ToastLength.Short);
                toast.Show();

            }
        }

        private void Btnagregar_Click(object sender, System.EventArgs e)
        {
            if (edtnombreCliente.Text != "")
            {
                string[] cliente = edtnombreCliente.Text.Split("-");


                if (edtnumprod.Text != "" && edtnumprod.Text != "0")
                {
                    if (servicioBase.RegistroPedido(Convert.ToInt32(edtnumPedido.Text), spnproducto.SelectedItem.ToString(), Convert.ToInt32(edtnumprod.Text), Convert.ToInt32(cliente[0].ToString())) == "EXITO")
                    {
                        var toast = Toast.MakeText(this, "Producto Ingresado", ToastLength.Short);
                        toast.Show();

                        ActualizarRegistros();
                        cargarTotal(Convert.ToInt32(edtnumPedido.Text));
                    }
                    else
                    {
                        var toast = Toast.MakeText(this, "Producto No Ingresado", ToastLength.Short);
                        toast.Show();

                    }
                }
                else
                {
                    var toast = Toast.MakeText(this, "No ingreso cantidad del producto", ToastLength.Short);
                    toast.Show();

                }
            }
            else 
            {
                var toast = Toast.MakeText(this, "Cargue un cliente por favor", ToastLength.Short);
                toast.Show();
            }
            
            
        }

        private void BtnSalir_Click(object sender, System.EventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("Cerrar la Aplicación");
            alert.SetMessage("¿Desea salir de la Aplicación?");
            alert.SetCancelable(false);
            alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);

            });
            alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
            {    
            });
            Dialog dialog = alert.Create();
            dialog.Show();
        }
        private void BtnEntrar_Click(object sender, System.EventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("Solicitar Pedido");
            alert.SetMessage("¿Desea solicitar el pedido?");
            alert.SetCancelable(false);
            alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
            {
                servicioBase.EstadoPedido(Convert.ToInt32(edtnumPedido.Text), "ENTREGADO");
                ActualizarRegistros();
                cargarTotal(Convert.ToInt32(edtnumPedido.Text));
                btnagregar.Visibility = ViewStates.Invisible;
                btnanular.Visibility = ViewStates.Invisible;
                
            });
            alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
            {
            });
            Dialog dialog = alert.Create();
            dialog.Show();

            mostrarTiempo(Convert.ToInt32(edtnumPedido.Text));
            btnEntrar.Visibility = ViewStates.Invisible;
        }

        private void numeroPedido()
        {
            edtnumPedido.Text = servicioBase.numeroPedido();
        }

        
    }
}