using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;



namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class RegistroFlora : ContentPage
{

    private FirebaseAuthService firebaseAuthService = new FirebaseAuthService();

    FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
    public List<Ubicacion> Ubicacion { get; set; }
    public List<Estados_crecimiento> Crecimiento { get; set; }

    public ObservableCollection<Funcion> Funcion { get; set; } = new ObservableCollection<Funcion>();
    public ObservableCollection<TipoFlora> TipoFlora { get; set; } = new ObservableCollection<TipoFlora>();



    public RegistroFlora()
	{

		InitializeComponent();
        CargarTipoFlora();
        CargarFunciones();
        Task.Run(async () => await CargarUbicacion());
        Task.Run(async () => await CargarCrecimiento());
        BindingContext = this;
    }

    public async void CargarTipoFlora()
    {
        try
        {
            
            var tipoResultado = await client.Child("Tipo_flora").OnceAsync<TipoFlora>();
            TipoFlora.Clear();

            foreach(var t in tipoResultado)
            {
                TipoFlora.Add(new TipoFlora
                {
                    Key = t.Object.Key ?? t.Key,
                    Nombre = t.Object.Nombre,
                    Descripcion = t.Object.Descripcion

                });
            }

        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"No se pudieron cargar los tipos de flora: {ex.Message}", "OK");

        }
    }

    public async void CargarFunciones()
    {
        try
        {
            var funcionResultado = await client.Child("Funcion").OnceAsync<Funcion>();

            Funcion.Clear();

            foreach (var f in funcionResultado)
            {
                Funcion.Add(new Funcion
                {
                    Key = f.Object.Key ?? f.Key,
                    Nombre = f.Object.Nombre,
                    Descripcion = f.Object.Descripcion
                });
            }


        } catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"No se pudieron cargar las funciones: {ex.Message}", "OK");
        }
    }



    public async Task CargarUbicacion()
    {
        try
        {
            var uibicacionResultado = await client.Child("Ubicacion").OnceAsync<Ubicacion>();
            Ubicacion = uibicacionResultado.Select(x => x.Object).ToList();
            UbicacionPicker.ItemsSource = Ubicacion;
        }
        catch (Exception ex)
        {
            await DisplayAlert($"Error", $"No se pudieron carcar las Carreras: {ex.Message}", "OK");
        }
    }

    public async Task CargarCrecimiento()
    {
        try
        {
            var crecimientoResultado = await client.Child("Estados_crecimiento").OnceAsync<Estados_crecimiento>();
            Crecimiento = crecimientoResultado.Select(x => x.Object).ToList();
            CrecimientoPicker.ItemsSource = Crecimiento;
        }
        catch (Exception ex)
        {
            await DisplayAlert($"Error", $"No se pudieron carcar las Carreras: {ex.Message}", "OK");
        }
    }

    public async void OnRegister_Clicked(object sender, EventArgs e)
    {
        var mensaje = MenssageLabel; 
        Ubicacion ubicacion = UbicacionPicker.SelectedItem as Ubicacion ;
        Estados_crecimiento crecimiento = CrecimientoPicker.SelectedItem as Estados_crecimiento;

        var tipoflora = TipoFlora.Where(t => t.IsSelect).ToList();
        var funcion = Funcion.Where(f => f.IsSelect).ToList();

        var nombre = NombreEntry.Text;
        var nombreC = NombreCientificoEntry.Text;
        var descripcion = DescripcionEntry.Text;
        var temperatura = $"{TemEntry.Text}°";
        var humedad = $"{HumEntry.Text}%";

        var cantidad = (int)CantidadSlider.Value;


        if (String.IsNullOrEmpty(nombre) ||
            String.IsNullOrEmpty(nombreC) ||
            String.IsNullOrEmpty(descripcion) ||
            String.IsNullOrEmpty(temperatura) ||
            String.IsNullOrEmpty(humedad)||
            ubicacion == null ||
            crecimiento == null) 
        {
            mensaje.Text = "Selecciona todos los campos";
            return;
        }

        try
        {
            var Registro = await firebaseAuthService.RegistrarFlora(nombre, nombreC, descripcion, temperatura, humedad, cantidad, tipoflora, ubicacion, crecimiento, funcion);
            if (Registro)
            {
                mensaje.Text = "Registro Creado Correctamente";
                await DisplayAlert($"Correcto", $"El proyecto se guardo de manera correcta", "OK");
                await Navigation.PushAsync(new FloraPage());
            }
            else
            {
                mensaje.Text = "ERROR";
                await DisplayAlert($"Error", $"El proyecto no se guardo", $"Intente mas tarde", "OK");
                await Navigation.PushAsync(new FloraPage());

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert($"ERROR", "{ex.Message}", "OK");
        }




    }

    private void OnVolver_Clicked(object sender, EventArgs e)
    {

    }
}