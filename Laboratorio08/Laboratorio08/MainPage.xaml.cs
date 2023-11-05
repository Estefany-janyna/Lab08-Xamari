using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Laboratorio08
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSpeakButtonClicked(object sender, EventArgs e)
        {
            string textToSpeak = textEntry.Text;
            DependencyService.Get<ITextToSpeech>().Speak(textToSpeak);
        }

        private void OnBattery(object sender, EventArgs e)
        {
            IBattery batteryService = DependencyService.Get<IBattery>();
            int remainingChargePercent = batteryService.RemainingChargePercent;
            BatteryStatus status = batteryService.Status;
            PowerSource powerSource = batteryService.PowerSource;

            string batteryInfo = $"Battery Status: {status}\nRemaining Charge: {remainingChargePercent}%\nPower Source: {powerSource}";

            DisplayAlert("Información de la Batería", batteryInfo, "OK");
        }

        [Obsolete]
        private async void OnScanQrCodeClicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushModalAsync(scan);

            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();

                    // Verificar si el resultado del escaneo es un enlace web
                    if (Uri.TryCreate(result.Text, UriKind.Absolute, out Uri uriResult)
                        && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                    {
                        // Abrir el enlace en el navegador del dispositivo
                        Device.OpenUri(uriResult);
                    }
                    else
                    {
                        // Mostrar una alerta si el escaneo no es un enlace web
                        await DisplayAlert("Valor QRCODE", result.Text, "OK");
                    }
                });
            };
        }
    }
}
