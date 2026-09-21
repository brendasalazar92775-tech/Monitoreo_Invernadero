using Firebase.Database;
using Firebase.Database.Query;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using System.Threading.Tasks;

namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class RegisterProyectos : ContentPage

{
    private FirebaseAuthService firebaseAuthService = new FirebaseAuthService();

    FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
    public List<Estado> EstadoProyectos { get; set; }
    public RegisterProyectos()
	{
		InitializeComponent();
        Task.Run(async () => await CargarEstado());
        BindingContext = this;
    }

    public async Task CargarEstado()
    {
        try
        {
            var estadoResult = await client.Child("Estados_Proy").OnceAsync<Estado>();
            EstadoProyectos = estadoResult.Select(x => x.Object).ToList();

            estadoPicker.ItemsSource = EstadoProyectos;

        }
        catch (Exception ex) {
            await DisplayAlert($"Error", $"No se pudieron cargar los Estados: {ex.Message}", "Ok");
        }
    }

    private async void OnMenu_Clicked(object sender, EventArgs e)
    { 
		await Navigation.PushAsync(new MenuCoordinadorPage());
    }

    private string FechaTermino;

    private void MiCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        f_TerminoDatePicker.IsEnabled = !e.Value;
        
    }


    public async void OnRegister_Clicked(object sender, EventArgs e)
    {
        Estado estadoSelecciomado = estadoPicker.SelectedItem as Estado;
        var mensaje = MenssageLabel;

        var nombre = NombreEntry.Text;
        var descripcion = DescripcionEntry.Text;
        var fechaInicio = f_InicioDatePicker.Date;
        var FechaTermino = f_TerminoDatePicker.Date;


        if (String.IsNullOrEmpty(nombre) ||
            String.IsNullOrEmpty(descripcion) ||
            estadoSelecciomado == null)
        {
            mensaje.Text = "Selecciona todos los campos";
            return;
        }

        try
        {
            var Registro = await firebaseAuthService.RegistrarProyectos(nombre, descripcion, fechaInicio, FechaTermino, estadoSelecciomado);

            if (Registro)
            {
                mensaje.Text = "Registro Creado Correctamente";
                await DisplayAlert($"Correcto", $"El proyecto se guardo de manera correcta", "OK");
                await Navigation.PushAsync(new ProyectosPage());
            } else
            {
                mensaje.Text = "ERROR";
                await DisplayAlert($"Error", $"El proyecto no se guardo", $"Intente mas tarde", "OK");
                await Navigation.PushAsync(new ProyectosPage());

            }
        }
        catch (Exception ex) { 
            await DisplayAlert($"ERROR", "{ex.Message}", "OK");   
        }

    }
}