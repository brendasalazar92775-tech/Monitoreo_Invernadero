namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class FloraYFaunaPage : ContentPage
{
	public FloraYFaunaPage()
	{
		InitializeComponent();
	}

    private async void OnFlora_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FloraPage());
    }

    private async void OnFauna_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FaunaPage());
    }

    private async void OnMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuCoordinadorPage());
    }
}