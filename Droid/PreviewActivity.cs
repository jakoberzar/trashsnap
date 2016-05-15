
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
using Android.Graphics.Drawables;
using Android.Graphics;
using Java.Interop;
using Android.Locations;

namespace TrashSnap.Droid
{
	[Activity (Label = "PreviewActivity,", Icon = "@mipmap/icon", Theme = "@style/MyTheme2")]			
	public class PreviewActivity : Activity
	{
		private Bitmap bm;
		LocationManager locMgr;
		float latitude;
		float longitude;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Preview);
			bm = Entry.photo;
			ImageView first = (ImageView) FindViewById(Resource.Id.imageView1);
			locMgr = GetSystemService (Context.LocationService) as LocationManager;

			first.SetImageBitmap (bm);
		}


		public void OnLocationChanged (Location location)
		{
			latitude = (float) location.Latitude;
			longitude = (float) location.Longitude;
			Console.WriteLine ("lat " +latitude);
		}


		[Export("Send")]
		public void Send(View v) {
			


			EditText et = (EditText)FindViewById (Resource.Id.editText1);
			Entry entry = new Entry(et.Text, latitude, longitude, bm.ToByteArray());
			API.UploadPhoto(entry);
			Toast.MakeText (this, "Slika uspešno poslana!", ToastLength.Long).Show ();
			var intent = new Intent(this, typeof(MainActivity)).SetFlags(ActivityFlags.ReorderToFront);
			StartActivity(intent);
		}
	}
}

