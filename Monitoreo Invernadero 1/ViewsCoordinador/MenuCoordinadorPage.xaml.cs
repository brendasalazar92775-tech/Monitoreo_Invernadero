namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class MenuCoordinadorPage : ContentPage
{
	public MenuCoordinadorPage()
	{
		InitializeComponent();
	}



    private async void OnRegister_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UsuariosPage());
    }

    private async void OnMonitoreo_Clicked(object sender, EventArgs e)
    {        await Navigation.PushAsync(new Monitoreo());
    }


    private async void Login_btn_Clicked(object sender, EventArgs e)
    {
        string accion = await DisplayActionSheet(
            "¿Estas seguro de Cerrar Sesion?", "Si", "No"
        );

        switch (accion)
        {
            case "Si":
                // Insertar MainPage antes de la actual
                Navigation.InsertPageBefore(new MainPage(), this);
                // Vaciar la pila y navegar
                await Navigation.PopToRootAsync();
                break;

        }

    }

    private async void OnProyectos_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProyectosPage());
    }

    private async void OnFloraYFauna_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FloraYFaunaPage());
    }
}