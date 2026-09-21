using Firebase.Database;
using Firebase.Database.Query;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using System.Threading.Tasks;


namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class EditarUsuarioPage : ContentPage
{
    FirebaseClient client1 = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
    private FirebaseAuthService client = new FirebaseAuthService();
    private Usuarios usuarios;
    public List<Roles> Roles { get; set; }
    public EditarUsuarioPage(Usuarios usuarios)
	{
		InitializeComponent();
        Task.Run(async () => await CargarCargos());
        BindingContext = this;
        this.usuarios = usuarios;


        NombreEntry.Text = usuarios.Nombre;
        P_ApellidoEntry.Text = usuarios.p_apellido;
        S_ApellidoEntry.Text = usuarios.s_apellido;
        rolesPicker.SelectedItem = usuarios.Roles;
    }

    public async Task CargarCargos()
    {
        try
        {
            var rolesResult = await client1.Child("Roles").OnceAsync<Roles>();
            Roles = rolesResult.Select(x => x.Object).ToList();

            // Actualizar el picker
            rolesPicker.ItemsSource = Roles;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los roles: {ex.Message}", "OK");
        }
    }

    private async void OnEditarUsuario_Clicked(object sender, EventArgs e)
    {
        var mensaje = MenssageLabel;
        Roles rolSeleccionado = rolesPicker.SelectedItem as Roles;
        usuarios.Nombre = NombreEntry.Text;
        usuarios.p_apellido = P_ApellidoEntry.Text;
        usuarios.s_apellido = S_ApellidoEntry.Text;
        usuarios.Roles = rolSeleccionado;

        bool confirm = await DisplayAlert("Confirmar", $"¿Desea actualizar a {usuarios.NombreCompeto}?", "Si", "No");

        if (confirm)
        {

            await client.ActualizarUsuario(usuarios.Key, usuarios);
            bool volver = await DisplayAlert("Listp", $"Usuario Actualizado", "OK", "");
            if (volver)
            {
                await Navigation.PushAsync(new UsuariosPage());
            }
        }

    }

    private async void OnMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuCoordinadorPage());

    }
}