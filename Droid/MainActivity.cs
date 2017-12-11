using Android.App;
using Android.Widget;
using Android.OS;
using Android.Nfc;
using Android.Content;
using System;
using Android.Nfc.Tech;
using FelicaCardReader.Droid.NfcServices;
using FelicaCardReader.RirekiService;

namespace FelicaCardReader.Droid
{
    [Activity(Label = "FelicaCardReader", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private NfcAdapter _nfcAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _nfcAdapter = NfcAdapter.GetDefaultAdapter(this);

        }

        protected override void OnResume()
        {
            base.OnResume();


            var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var filters = new[] { tagDetected };

            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            _nfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);

        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            if (intent is null)
            {
                return;
            }
            var tag = (Tag)intent.GetParcelableExtra(NfcAdapter.ExtraTag);

            if (tag != null)
            {
                var textView = FindViewById<TextView>(Resource.Id.textView1);
                var result = NfcReader.Read(tag);
                foreach (var item in result.blocks)
                {
                    var rireki = new RirekiParse(item);
                    textView.Text += rireki.ToString();
                }
            }

        }
    }
}
