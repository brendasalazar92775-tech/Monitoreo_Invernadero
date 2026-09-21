using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class RegistroAlumnos : ContentPage
{
	private FirebaseAuthService firebaseAuthService = new FirebaseAuthService();

	FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
	public List<Carrera> carreras {  get; set; }
	public List<Grupo> grupos { get; set; }

    private Proyectos proyectoActual;
    public RegistroAlumnos(Proyectos proyectos)
	{
		InitializeComponent();
		Task.Run(async () => await CargarCarreras()); 
		Task.Run(async() => await CargarGrupos());
		BindingContext = this;
        proyectoActual = proyectos;
    }

	public async Task CargarCarreras()
	{
		try
		{
			var carrerasResultado = await client.Child("Carrera").OnceAsync<Carrera>();
			carreras = carrerasResultado.Select(x => x.Object).ToList();
			carreraPicker.ItemsSource = carreras;
		}
		catch (Exception ex)
		{
			await DisplayAlert($"Error", $"No se pudieron carcar las Carreras: {ex.Message}", "OK");
		}
	}

	public async Task CargarGrupos()
	{
		try
		{
			var gruposResult = await client.Child("Grupos").OnceAsync<Grupo>();
			grupos = gruposResult.Select(x => x.Object).ToList();
			grupoPicker.ItemsSource = grupos;
		}
		catch (Exception ex)
		{
			await DisplayAlert($"Error", $"No se pudieron cargar los grupos: {ex.Message}", "OK");
		}
	}

    private async void OnRegister_Clicked(object sender, EventArgs e)
    {
			var mensaje = MenssageLabel;

		var nombre = NombreEntry.Text;
		var p_apellido = P_ApellidoEntry.Text;
		var s_apellido = S_ApellidoEntry.Text;
		var telefono = NumeroEntry.Text;
		var correo = EmailEntry.Text;
		var proyectoID = proyectoActual.Key;

        Carrera carreraSeleccionado = carreraPicker.SelectedItem as Carrera;
        Grupo grupoSeleccionado = grupoPicker.SelectedItem as Grupo;

		if (String.IsNullOrEmpty(nombre)||
			String.IsNullOrEmpty(p_apellido)||
			String.IsNullOrEmpty(s_apellido)||
				carreraSeleccionado ==null||
				grupoSeleccionado == null)
		{
			await DisplayAlert("Error", "Selecciona Todos los campos", "OK");
		}

		try
		{
			var Registro = await firebaseAuthService.RegistrarAlumno(nombre, p_apellido, s_apellido, telefono, correo, carreraSeleccionado, grupoSeleccionado, proyectoID);
		}
		catch (Exception ex)
		{

		}
    }

    private async void volver_Clicked(object sender, EventArgs e)
    {
        // Insertar MainPage antes de la actual
        Navigation.InsertPageBefore(new MenuCoordinadorPage(), this);
        // Vaciar la pila y navegar
        await Navigation.PopToRootAsync();
        
    }
}