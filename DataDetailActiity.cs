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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using AndroidX.Core.Graphics;
using AndroidX.Core.Graphics.Drawable;

namespace AppClientesAzure
{
    [Activity(Label = "DataDetailActivity")]
   public class DataDetailActiity : Activity, IOnMapReadyCallback
    {
        TextView txtNombre, txtDomicilio, txtCorreo, txtSaldo, txtEdad;
        ImageView Imagen, Imagenfondo;
        GoogleMap googleMap;
        string correo, imagen, nombre, domicilio, imagenfondo;
        double saldo;
        double lat, lon;
        int edad;

        protected override void OnCreate(Bundle savedInstanceSate)
        {
            base.OnCreate(savedInstanceSate);
            SetContentView(Resource.Layout.DataDetail);
            try
            {
                correo = Intent.GetStringExtra("correo");
                imagen = Intent.GetStringExtra("imagen");
                imagenfondo = Intent.GetStringExtra("imagenfondo");
                nombre = Intent.GetStringExtra("nombre");
                edad = int.Parse(Intent.GetStringExtra("edad"));
                domicilio = Intent.GetStringExtra("domicilio");
                saldo = double.Parse(Intent.GetStringExtra("saldo"));
                lat = double.Parse(Intent.GetStringExtra("Latitud"));
                lon = double.Parse(Intent.GetStringExtra("Longitud"));
                Imagenfondo = FindViewById<ImageView>(Resource.Id.imagenFondodetalle);
                Imagen = FindViewById<ImageView>(Resource.Id.imagendetalle);
                txtNombre = FindViewById<TextView>(Resource.Id.txtName);
                txtDomicilio = FindViewById<TextView>(Resource.Id.txtAddress);
                txtCorreo = FindViewById<TextView>(Resource.Id.txtEmail);
                txtEdad = FindViewById<TextView>(Resource.Id.txtAge);
                txtSaldo = FindViewById<TextView>(Resource.Id.txtMoney);

                txtNombre.Text = nombre;
                txtDomicilio.Text = domicilio;
                txtCorreo.Text = correo;
                txtEdad.Text = edad.ToString();
                txtSaldo.Text = saldo.ToString();
                var typeface = Typeface.CreateFromAsset(this.Assets, "fonts/KGHolocene.ttf");
                txtNombre.SetTypeface(typeface, TypefaceStyle.Normal);
                var RutaImagen = System.IO.Path.Combine(System.Environment.
                    GetFolderPath(System.Environment.SpecialFolder.Personal),
                    imagen);
                var RutaImagenFondo = System.IO.Path.Combine(System.Environment.
               GetFolderPath(System.Environment.SpecialFolder.Personal),
               imagenfondo);
                var rutauriimagen = Android.Net.Uri.Parse(RutaImagen);
                var rutauriimagenfondo = Android.Net.Uri.Parse(RutaImagenFondo);
                Imagen.SetImageURI(rutauriimagen);
                Imagenfondo.SetImageURI(rutauriimagenfondo);

                var opciones = new BitmapFactory.Options();
                opciones.InPreferredConfig = Bitmap.Config.Argb8888;
                var bitmap = BitmapFactory.DecodeFile(RutaImagen, opciones);
                Imagen.SetImageDrawable(getRoundedCornerImage(bitmap, 20));

                var mapView = FindViewById<MapView>(Resource.Id.mapita);
                mapView.OnCreate(savedInstanceSate);
                mapView.GetMapAsync(this);
                MapsInitializer.Initialize(this);

            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this.googleMap = googleMap;
            var builder = CameraPosition.InvokeBuilder();
            builder.Target(new LatLng(lat, lon));
            builder.Zoom(17);
            var cameraPosition = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            this.googleMap.AnimateCamera(cameraUpdate);
        }

        public static RoundedBitmapDrawable
            
            getRoundedCornerImage
            (Bitmap image, int cornerRadius)
        {
            var corner = RoundedBitmapDrawableFactory.Create(null, image);
            corner.CornerRadius = cornerRadius;
            return corner;
        }
    }
}
