using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.App;
using Android.Graphics;
using System.Collections.Generic;
using AndroidX.Core.Graphics.Drawable;
namespace AppClientesAzure
{
    public class DataAdapter : BaseAdapter<ElementosdelaTabla>
    {
        List<ElementosdelaTabla> items;
        Activity context;

        public DataAdapter(Activity context, List<ElementosdelaTabla> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override ElementosdelaTabla this[int position]
        {
            get { return items[position]; }
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            view = context.LayoutInflater.Inflate(Resource.Layout.DataRow, null);
            view.FindViewById<TextView>(Resource.Id.txtCorreo).Text = item.Correo;
            var txtHead = view.FindViewById<TextView>(Resource.Id.txtNombre);
            txtHead.Text = item.Nombre;
            var typeface = Typeface.CreateFromAsset(context.Assets, "fonts/KGHolocene.ttf");
            txtHead.SetTypeface(typeface, TypefaceStyle.Normal);
            var path = System.IO.Path.Combine(System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal), item.Imagen);
            var pathback = System.IO.Path.Combine(System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal), item.ImagenFondo);
            var Image = BitmapFactory.DecodeFile(path);
            var ImageBack = BitmapFactory.DecodeFile(pathback);
            Image = ResizeBitmap(Image, 100, 100);
            view.FindViewById<ImageView>(Resource.Id.imagen).
                SetImageDrawable(getRoundedCornerImage(Image, 5));
            view.FindViewById<ImageView>(Resource.Id.imagenFondo).
                SetImageDrawable(getRoundedCornerImage(ImageBack, 2));
            return view;
        }

        public static RoundedBitmapDrawable getRoundedCornerImage
         (Bitmap image, int cornerRadius)
        {
            var corner = RoundedBitmapDrawableFactory.Create(null, image);
            corner.CornerRadius = cornerRadius;
            return corner;
        }

        private Bitmap ResizeBitmap(Bitmap imagenoriginal, int widthimagenoriginal,
       int heightimagenoriginal)
        {
            Bitmap resizedImage = Bitmap.CreateBitmap(widthimagenoriginal,
                heightimagenoriginal, Bitmap.Config.Argb8888);
            float Width = imagenoriginal.Width;
            float Height = imagenoriginal.Height;
            var canvas = new Canvas(resizedImage);
            var scala = widthimagenoriginal / Width;
            var xTranslation = 0.0f;
            var yTranslation = (heightimagenoriginal - Height * scala) / 2.0f;
            var transformacion = new Matrix();
            transformacion.PostTranslate(xTranslation, yTranslation);
            transformacion.PreScale(scala, scala);
            var paint = new Paint();
            paint.FilterBitmap = true;
            canvas.DrawBitmap(imagenoriginal, transformacion, paint);
            return resizedImage;

        }

    }


}




