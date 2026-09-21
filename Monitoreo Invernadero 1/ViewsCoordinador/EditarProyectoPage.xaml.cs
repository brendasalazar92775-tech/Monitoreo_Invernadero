using Firebase.Database;
using Firebase.Database.Query;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using System.Threading.Tasks;

namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class EditarProyectoPage : ContentPage
{
    FirebaseClient client1 = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
    private FirebaseAuthService client = new FirebaseAuthService();
    private Proyectos proyectos;

    public List<Estado> EstadoProyectos { get; set; }

    public EditarProyectoPage(Proyectos proyectos)
	{
		InitializeComponent();
        Task.Run(async () => await CargarEstados());
        BindingContext = this;
        this.proyectos = proyectos;

        NombreEntry.Text = proyectos.Nombre;
        DescripcionEntry.Text = proyectos.Descripcion;
        f_InicioDatePicker.Date = proyectos.FechaInicio;
        f_TerminoDatePicker.Date = proyectos.FechaFibalizacion;
        estadoPicker.SelectedItem = proyectos.Estado;

    }

    public async Task CargarEstados()
    {
        try
        {
            var estadoResult = await client1.Child("Estados_Proy").OnceAsync<Estado>();
            EstadoProyectos = estadoResult.Select(x => x.Object).ToList();

            estadoPicker.ItemsSource = EstadoProyectos;

        }
        catch (Exception ex)
        {
            await DisplayAlert($"Error", $"No se pudieron cargar los Estados: {ex.Message}", "Ok");
        }
    }

    private async void OnEditarProyecto_Clicked(object sender, EventArgs e)
    {
        var mensaje = MenssageLabel; 


        Estado estadoProyecto = estadoPicker.SelectedItem as Estado;

        proyectos.Nombre = NombreEntry.Text;
        proyectos.Descripcion = DescripcionEntry.Text;
        proyectos.FechaInicio = f_InicioDatePicker.Date;
        proyectos.FechaFibalizacion = f_TerminoDatePicker.Date;
        proyectos.Estado = estadoProyecto;

        bool confirma = await DisplayAlert("Confirmar", $"¿Desea actualizar a {proyectos.Nombre ?? "este proyecto"}?", "Si", "No");

        if (confirma)
        {
            await client.ActualizarProyecto(proyectos.Key, proyectos);
            await DisplayAlert("Listo", "Usuario Actualizado", "OK");

                await Navigation.PushAsync(new ProyectosPage());
        }
        
        }
    

    private async void OnMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProyectosPage());
    }
}