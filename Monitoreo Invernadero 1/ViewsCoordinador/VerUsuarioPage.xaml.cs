using Firebase.Database;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Monitoreo_Invernadero_1.ViewsCoordinador;


public partial class VerUsuarioPage : ContentPage
{
    private Usuarios usuarios;


    public VerUsuarioPage(Usuarios usuarios)
    {
		InitializeComponent();

        this.usuarios = usuarios;

        NombreCEntry.Text = usuarios.NombreCompeto;
        nombreLabel.Text = usuarios.Nombre;
        p_apellioLabel.Text = usuarios.p_apellido;
        s_apellioLabel.Text = usuarios.s_apellido;
        correoLabel.Text = usuarios.correo??"No se pudieron cargar los datos";
        numeroLabel.Text = usuarios.Numero ?? "No se pudieron cargar los datos";
        rolesEntry.Text = usuarios.Roles.Nombre ?? "No se pudieron cargar los datos";

    }



    private async void OnVolver_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UsuariosPage());
    }
}