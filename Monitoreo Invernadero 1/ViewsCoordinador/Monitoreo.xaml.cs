using Firebase.Database;
using Firebase.Database.Query;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class Monitoreo : TabbedPage
{
    // 🔹 Cliente para LED
    FirebaseClient clientLed = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");

    // 🔹 Servicio personalizado para Sensor
    FirebaseAuthService client = new FirebaseAuthService();

    // 🔹 Constructor

    public Monitoreo()
    {
        InitializeComponent();
        BindingContext = this;

        // Cargar datos al iniciar
        _ = CargarDatos();

        GetLedStatus();
    }

    private double humedad;
    public double Humedad
    {
        get => humedad;
        set
        {
            if (humedad != value)
            {
                humedad = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HumedadTexto));
            }
        }
    }

    private double temperatura;
    public double Temperatura
    {
        get => temperatura;
        set
        {
            if (temperatura != value)
            {
                temperatura = value;
                OnPropertyChanged();
            }
        }
    }

  
    public string HumedadTexto => $"{Humedad:F1}%";

    private async Task CargarDatos()
    {
        var datos = await client.ObtenerDatosSensor();

        if (datos != null)
        {
            Console.WriteLine($"[DOTNET] Recibí: {datos.Temperatura} / {datos.Humedad}");

            Humedad = datos.Humedad;
            Temperatura = datos.Temperatura;
        }
    }

    private async void OnActualizarClicked(object sender, EventArgs e)
    {
        await CargarDatos();
        await DisplayAlert("Actualizado", "Datos actualizados correctamente", "OK");
    }

    // ILUMINACION

    private async void GetLedStatus()
    {
        var resultado = await clientLed
            .Child("led")
            .Child("estado")
            .OnceSingleAsync<int>();

        LedSwitch.IsToggled = resultado == 1;
    }

    private async void LedSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        int valor = e.Value ? 1 : 0;

        await clientLed
            .Child("led")
            .Child("estado")
            .PutAsync(valor);

        await DisplayAlert("Firebase", $"LED actualizado a {valor}", "OK");
    }

    public new event PropertyChangedEventHandler PropertyChanged;

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}