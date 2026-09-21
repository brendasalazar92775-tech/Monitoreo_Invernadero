namespace Monitoreo_Invernadero_1.ViewsCoordinador;

using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using LiteDB;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

public partial class UsuariosPage : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
    private readonly FirebaseAuthService _authService = new FirebaseAuthService();

    public ObservableCollection<Usuarios> Lista {  get; set; } = new ObservableCollection<Usuarios>();


    public UsuariosPage()
	{
		InitializeComponent();
        BindingContext = this;
        cargarUsuarios();
	}


    public void cargarUsuarios()
    {
        client.Child("Usuarios")
                .AsObservable<Usuarios>()
                .Subscribe((usuarios) =>
                {
                    if (usuarios.Object != null)
                    {
                        usuarios.Object.Key = usuarios.Key;
                        Lista.Add(usuarios.Object);
                    }
                });
    }
    
    private async void nuevoUs_btn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new RegisterPage());
    }


    private void filtroEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        string filtro = filtroEntry.Text.ToLower();

        if (filtro.Length > 0)
        {
            ListaCollection.ItemsSource = Lista.Where(x => x.Nombre.ToLower().Contains(filtro));
        }
        else
        {
            ListaCollection.ItemsSource = Lista;
        }
    }

    private async void OnMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuCoordinadorPage());
    }

    private async void OnEliminarUsuario_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var usuario = button?.CommandParameter as Usuarios;

        if(usuario != null)
        {
            var usuarioActual = SesionActual.UsuarioLogueado;

            bool confirm = await DisplayAlert("Conformar", $"¿Eliminar a {usuario.NombreCompeto}?", "Si", "No");

            if (confirm) 
            {

                await client.Child("Usuarios")
                    .Child(usuario.Key)
                    .DeleteAsync();


                Lista.Remove(usuario);
            }
        }
    }

    private async void OnEditarUsuario_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var usuario = button?.BindingContext as Usuarios;


        if (usuario != null)
        {
            await Navigation.PushAsync(new EditarUsuarioPage(usuario));
        }

    }


    private async void VerUusuario_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var usuario = button?.BindingContext as Usuarios;

        if (usuario != null)
        {
            await Navigation.PushAsync(new VerUsuarioPage(usuario));
        }
    }
}