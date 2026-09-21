using Firebase.Database;
using Firebase.Database.Query;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using Monitoreo_Invernadero_1.ViewsCoordinador;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Monitoreo_Invernadero_1;

public partial class RegisterPage : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");

    private readonly FirebaseAuthService _authService = new FirebaseAuthService();
    public List<Roles> Roles { get; set; }


    public RegisterPage()
	{
		InitializeComponent();
        Task.Run(async () => await CargarCargos());
        BindingContext = this;
    }
    public async Task CargarCargos()
    {
        try
        {
            var rolesResult = await client.Child("Roles").OnceAsync<Roles>();
            Roles = rolesResult.Select(x => x.Object).ToList();

            // Actualizar el picker
            rolesPicker.ItemsSource = Roles;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los roles: {ex.Message}", "OK");
        }
    }

    private async void OnRegisterFirebase_Clicked(object sender, EventArgs e)
    {
        var boton = sender as Button;

       


        Roles rolSeleccionado = rolesPicker.SelectedItem as Roles;

        string mensaje = "";
        bool esValido = true;
        var MensajeLabel = MenssageLabel;

        var nombre = NombreEntry.Text?.Trim();
        var p_apellido = P_ApellidoEntry.Text?.Trim();
        var s_apellido = S_ApellidoEntry.Text?.Trim();
        var correo = EmailEntry.Text?.Trim();
        var contraseña = PasswordEntry.Text?.Trim();
        var numero = NumeroEntry.Text?.Trim();

        // --- VALIDAR CAMPOS VACÍOS ---
        if (string.IsNullOrEmpty(nombre) ||
            string.IsNullOrEmpty(p_apellido) ||
            string.IsNullOrEmpty(s_apellido) ||
            rolSeleccionado == null ||
            string.IsNullOrEmpty(correo) ||
            string.IsNullOrEmpty(contraseña) ||
            string.IsNullOrEmpty(numero))
        {
            esValido = false;
            mensaje += "Completa todos los campos.\n";
        }

        // --- VALIDAR CORREO ---
        else if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            esValido = false;
            mensaje += "El correo no es válido.\n";
        }

        // --- VALIDAR CONTRASEÑA ---
        else if (contraseña.Length < 8 ||
                 !Regex.IsMatch(contraseña, @"^(?=.*[A-Za-z])(?=.*\d).{8,}$"))
        {
            esValido = false;
            mensaje += "La contraseña debe tener al menos 8 caracteres, con letras y números.\n";
        }

        // --- VALIDAR NÚMERO ---
        else if (!Regex.IsMatch(numero, @"^\d{1,12}$"))
        {
            esValido = false;
            mensaje += "El número debe contener solo dígitos y máximo 12 caracteres.\n";
        }

        // --- MOSTRAR RESULTADO ---
        if (!esValido)
        {
            await DisplayAlert("Error de validación", mensaje, "OK");
            return;
        }

        try
        {
            await _authService.RegistraUsuario(nombre, p_apellido, s_apellido, correo, contraseña, rolSeleccionado, numero);
            MensajeLabel.Text = "Usuario registrado exitosamente!";
            await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");
            await Navigation.PushAsync(new UsuariosPage());
        }
        catch (Exception ex)
        {
            MensajeLabel.Text = $"Error: {ex.Message}";
            await DisplayAlert("Error", "Error al registrar, inténtelo más tarde", "OK");
        }
        



    }

    private async void OnMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UsuariosPage());
    }

    private void TogglePasswordButton_Clicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;

        TogglePasswordButton.Text = PasswordEntry.IsPassword ? "👁" : "🚫";
    }


}