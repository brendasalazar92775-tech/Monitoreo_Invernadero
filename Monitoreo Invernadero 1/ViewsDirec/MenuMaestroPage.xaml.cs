using System.Threading.Tasks;

namespace Monitoreo_Invernadero_1.ViewsDirec;

public partial class MenuMaestroPage : ContentPage
{
	public MenuMaestroPage()
	{
		InitializeComponent();
	}

    private async void monitoreo_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MonitoreoPage());
    }

    private async void FloraFauna_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FloraFaunaPage());
    }

    private async void Proyectos_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProyectosPage());
    }

    private async void Login_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}