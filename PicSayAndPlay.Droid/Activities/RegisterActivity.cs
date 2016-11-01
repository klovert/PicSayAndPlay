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

namespace PicSayAndPlay.Droid
{
    [Activity(Label = "RegisterActivity", Theme = "@style/Base.Theme.Design")]
    public class RegisterActivity : Activity, DatePickerDialog.IOnDateSetListener
    {
        Button datePickerBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);

            datePickerBtn = FindViewById<Button>(Resource.Id.datepickerBtn);

            datePickerBtn.Click += (s, e) => 
            {
                var datePickerDialog = new DatePickerDialog(this, 0, this, 1997, 5, 12);
                
                //  Don't allow dates after today
                var now = DateTime.Now.Subtract(new DateTime(1970, 1, 1));
                datePickerDialog.DatePicker.MaxDate = (long)now.TotalMilliseconds;

                datePickerDialog.Show();
            };
        }

        #region IOnDateSetInterfaceImplementation

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            Console.WriteLine($"Date picked {dayOfMonth}/{monthOfYear}/{year}");
        }

        #endregion IOnDateSetInterfaceImplementation
    }
}