using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class RegistrarFauna : ContentPage
{
    private FirebaseAuthService firebaseAuthService = new FirebaseAuthService();
    FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");

    public List<Tipo_Insecto> TipoInsecto { get; set; }

    public RegistrarFauna()
	{
		InitializeComponent();
        Task.Run(async () => await CargarTipo_Insecto());
        BindingContext = this;

    }

	public async Task CargarTipo_Insecto()
	{
        try
        {
            var tipoResult = await client.Child("Tipo_Insecto").OnceAsync<Tipo_Insecto>();
            TipoInsecto = tipoResult.Select(x=>x.Object).ToList();

            TipoPicker.ItemsSource = TipoInsecto;

        }
        catch (Exception ex)
        {
            await DisplayAlert($"Error", $"No se pudieron cargar los Estados: {ex.Message}", "Ok");
        }
    }

    public async void OnRegister_Clicked(object sender, EventArgs e)
    {
        Tipo_Insecto tipo_Insecto = TipoPicker.SelectedItem as Tipo_Insecto;

        var nombre = NombreEntry.Text;
        var nombreC = NombreCientificoEntry.Text;
        var descripcion = DescripcionEntry.Text;
        var metodo = MetodoEntry.Text;

        if (String.IsNullOrEmpty(nombre) ||
            String.IsNullOrEmpty(nombreC) ||
            String.IsNullOrEmpty(descripcion) ||
            String.IsNullOrEmpty(metodo) ||
            tipo_Insecto == null
            )
        {
            await DisplayAlert("Error", $"Llena todos los campos", "OK");
            return;
        }

        try
        {
            var Registro = await firebaseAuthService.RegistrarInsectos(nombre, nombreC, tipo_Insecto, descripcion, metodo);
            if (Registro)
            {
                await DisplayAlert($"Correcto", $"El proyecto se guardo de manera correcta", "OK");
                await Navigation.PushAsync(new FaunaPage());
            }
            else
            {
                await DisplayAlert($"Error", $"El proyecto no se guardo", $"Intente mas tarde", "OK");
                await Navigation.PushAsync(new FaunaPage());

            }
        }
        catch ( Exception ex )
        {
            await DisplayAlert($"ERROR", "{ex.Message}", "OK");
        }
    }

    public async void OnVolver_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FaunaPage());
    }
}