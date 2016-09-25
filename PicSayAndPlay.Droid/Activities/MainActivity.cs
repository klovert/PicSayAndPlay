﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Plugin.Media;
using System;

namespace PicSayAndPlay.Droid
{
    [Activity(Label = "Pic, Say and Play", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button takePhotoBtn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            takePhotoBtn = FindViewById<Button>(Resource.Id.TakePic);

            //  EventHandlers
            takePhotoBtn.Click += TakePhotoBtn_Click;
        }

        private async void TakePhotoBtn_Click(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                /* TODO: Change to snackbar*/
                Toast.MakeText(this.ApplicationContext, "La cámara no está disponible :(", ToastLength.Long).Show();
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                Directory = "PicSay",
                Name = "image.jpg",
                SaveToAlbum = true
            });

            if (file == null)
                return;

            Intent i = new Intent(this, typeof(ResultActivity));
            i.PutExtra("Image", file.Path);
            StartActivity(i);
        }
    }
}