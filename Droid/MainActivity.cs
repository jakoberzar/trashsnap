﻿using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Hardware;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;



namespace TrashSnap.Droid
{


	[Activity (Label = "TrashSnap", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/MyTheme")]
	public class MainActivity : Activity, TextureView.ISurfaceTextureListener
	{
		Android.Hardware.Camera _camera;
		TextureView _textureView;
		Button _takePicture;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			_textureView = FindViewById<TextureView> (Resource.Id.textureView1);
			_textureView.SurfaceTextureListener = this;

			_takePicture = FindViewById<Button> (Resource.Id.but2);

			_takePicture.Click += CaptureImage;


			_textureView.SurfaceTextureListener = this;

		}

		public void OnSurfaceTextureAvailable (Android.Graphics.SurfaceTexture surface, int w, int h)
		{
			_camera = Android.Hardware.Camera.Open ();

			var surfaceOrientation = WindowManager.DefaultDisplay.Rotation;
			Matrix transform = new Matrix();

			if (surfaceOrientation == SurfaceOrientation.Rotation0 || surfaceOrientation == SurfaceOrientation.Rotation180) {
				_camera.SetDisplayOrientation (90);
				_textureView.LayoutParameters = new FrameLayout.LayoutParams (w, h, GravityFlags.Center);
				transform.SetScale (-1, 1);
			} else {
				_textureView.LayoutParameters = new FrameLayout.LayoutParams (w, h, GravityFlags.Center);
			}

			try {
				_camera.SetPreviewTexture (surface);
				_camera.StartPreview ();
				//_textureView.SetTransform(transform);
			} catch (Java.IO.IOException ex) {
				Console.WriteLine (ex.Message);
			}
		}

		public bool OnSurfaceTextureDestroyed (Android.Graphics.SurfaceTexture surface)
		{
			_camera.StopPreview ();
			_camera.Release ();

			return true;
		}

		public void OnSurfaceTextureSizeChanged (Android.Graphics.SurfaceTexture surface, int width, int height)
		{
			// camera takes care of this
		}

		public void OnSurfaceTextureUpdated (Android.Graphics.SurfaceTexture surface)
		{

		}


		public void OnPictureTaken(byte[] data, Android.Hardware.Camera camera)
		{
			camera.StopPreview();
			Toast.MakeText(this, "Cheese", ToastLength.Short).Show();
			camera.StartPreview();
		}


		public void CaptureImage(object sender, EventArgs e) {
			Bitmap bm = _textureView.Bitmap;
		}


	}


	public class CameraSurfaceTextureListener : Java.Lang.Object, TextureView.ISurfaceTextureListener
	{
		private MainActivity Activity;
		public CameraSurfaceTextureListener(MainActivity oActivity)
		{
			Activity = oActivity;
		}
		public void OnSurfaceTextureAvailable (Android.Graphics.SurfaceTexture oSurface, int iWidth, int iHeight)
		{
			
		}
		public bool OnSurfaceTextureDestroyed (Android.Graphics.SurfaceTexture oSurface)
		{
			return true;
		}
		public void OnSurfaceTextureSizeChanged (Android.Graphics.SurfaceTexture oSurface, int iWidth, int iHeight)
		{
			
		}
		public void OnSurfaceTextureUpdated (Android.Graphics.SurfaceTexture oSurface)
		{
		}
	}

		

}


