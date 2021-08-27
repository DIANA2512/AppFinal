using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;


namespace cafeteriaPiedad
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnEntrar;
        Button btnSalir;

        EditText edtusuario;
        EditText edtpass;

        private WS_Conexion.Service_Bdd servicioBase = new WS_Conexion.Service_Bdd();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnEntrar = FindViewById<Button>(Resource.Id.btningresar);
            btnSalir = FindViewById<Button>(Resource.Id.btnsalir);

            edtusuario = FindViewById<EditText>(Resource.Id.edtusuario);
            edtpass = FindViewById<EditText>(Resource.Id.edtpass);

            btnEntrar.Click += BtnEntrar_Click;
            btnSalir.Click += BtnSalir_Click;

        }

        private void BtnSalir_Click(object sender, System.EventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("Cerrar la Aplicación");
            alert.SetMessage("¿Desea salir de la Aplicación?");
            alert.SetCancelable(false);
            alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
            {
                this.Finish();

            });
            alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
            {
            });
            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private void BtnEntrar_Click(object sender, System.EventArgs e)
        {
            if (edtusuario.Text.Equals("") || edtpass.Text.Equals("")) 
            {
                var toast = Toast.MakeText(this, "Campos Vacios", ToastLength.Short);
                toast.Show();
                return;
            }

            try 
            {
                if (servicioBase.ValidarUsuario(edtusuario.Text, edtpass.Text, 0).Equals("EXITO"))
                {
                    Intent intent = new Intent(this, typeof(Pedidos_Activitycs));
                    StartActivity(intent);
                }
                else 
                {
                    var toast = Toast.MakeText(this, "Usuario no Existe!!!", ToastLength.Short);
                    toast.Show();
                    return;
                }
            }
            catch (System.Exception ex) 
            {
                Toast.MakeText(this, " Error " + ex.Message, ToastLength.Short).Show();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    
    }
}