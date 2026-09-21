using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using Monitoreo_Invernadero_1.ViewsCoordinador;
using Monitoreo_Invernadero_1.ViewsDirec;
using System.Threading.Tasks;


namespace Monitoreo_Invernadero_1

{
    public partial class MainPage : ContentPage
    {
       
        private readonly FirebaseAuthService _authService = new FirebaseAuthService();

        public MainPage()
        {
            InitializeComponent();
        }


        private async void OnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                var email = EmailEntry.Text?.Trim();
                var password = PasswordEntry.Text;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    await DisplayAlert("Error", "Por favor ingrese usuario y contraseña", "OK");
                    return;
                }

                var usuario = await _authService.Login(email, password);

                if (usuario == null)
                {
                    await DisplayAlert("Error", "Credenciales inválidas o usuario no encontrado", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(usuario.Roles.Nombre))
                {
                    await DisplayAlert("Error", "El usuario no tiene un rol asignado", "OK");
                    return;
                }

                SesionActual.UsuarioLogueado = usuario;

                if (usuario.Roles.Nombre == "Coordinador")
                    await Navigation.PushAsync(new MenuCoordinadorPage());
                else if (usuario.Roles.Nombre == "Directivo")
                    await Navigation.PushAsync(new MenuMaestroPage());
                else
                    await DisplayAlert("Error", "Usuario sin rol válido", "OK");
            }
            catch (Exception ex)
            {
                MessageLabel.Text = "Error: " + ex.Message;
            }
        }



    }
}
