using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;


namespace cafeteriaPiedad
{
    [Activity(Label = "Registro de Clientes", Theme = "@style/AppTheme")]
    public class Cliente_Activity : AppCompatActivity
    {
        Button btnRegistrar;
        Button btnSalir;

        EditText edtNombreCliente;
        EditText edtApellidoCliente;
        EditText edtCedulaCliente;
        EditText edtCorreoCliente;
        EditText edtDireccionCliente;
        

        private WS_Conexion.Service_Bdd servicioBase = new WS_Conexion.Service_Bdd();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Cliente);

            btnRegistrar = FindViewById<Button>(Resource.Id.btnRegistrar);
            btnSalir = FindViewById<Button>(Resource.Id.btnsalir);

            edtNombreCliente = FindViewById<EditText>(Resource.Id.edtNombreCliente);
            edtApellidoCliente = FindViewById<EditText>(Resource.Id.edtApellidoCliente);
            edtCorreoCliente = FindViewById<EditText>(Resource.Id.edtCorreoCliente);
            edtCedulaCliente = FindViewById<EditText>(Resource.Id.edtCedulaCliente);
            edtDireccionCliente = FindViewById<EditText>(Resource.Id.edtDireccionCliente);
           

            btnRegistrar.Click += BtnRegistrar_Click; 
            btnSalir.Click += BtnSalir_Click;

        }

        private void BtnRegistrar_Click(object sender, System.EventArgs e)
        {
            if (edtNombreCliente.Text == "" || edtApellidoCliente.Text == "" || edtCorreoCliente.Text == "" || edtCedulaCliente.Text == "" || edtDireccionCliente.Text == "")
            {
                var toast = Toast.MakeText(this, "No se permiten campos vacíos", ToastLength.Short);
                toast.Show();
                return;
            }
            else 
            {
                if (servicioBase.guardarActualizarCliente("GUARDAR",0,edtNombreCliente.Text,edtApellidoCliente.Text,edtCedulaCliente.Text,edtCorreoCliente.Text,edtDireccionCliente.Text,1) > 0)
                {
                    var toast = Toast.MakeText(this, "Registro existoso", ToastLength.Short);
                    toast.Show();
                    edtNombreCliente.Text = "";
                    edtApellidoCliente.Text = "";
                    edtCedulaCliente.Text = "";
                    edtCorreoCliente.Text = "";
                    edtDireccionCliente.Text = "";
                }
                else 
                {
                    var toast = Toast.MakeText(this, "No registrado o cliente existente", ToastLength.Short);
                    toast.Show();
                }
            }

        }

        

        private void BtnSalir_Click(object sender, System.EventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("Cerrar la Aplicación");
            alert.SetMessage("¿Desea regresar al módulo pedido?");
            alert.SetCancelable(false);
            alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
            {
                Intent intent = new Intent(this, typeof(Pedidos_Activitycs));
                StartActivity(intent);

            });
            alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
            {
            });
            Dialog dialog = alert.Create();
            dialog.Show();
        }

       
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}