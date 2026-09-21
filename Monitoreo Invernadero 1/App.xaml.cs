using Monitoreo_Invernadero_1.ViewsCoordinador;

namespace Monitoreo_Invernadero_1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MenuCoordinadorPage());
        }
    }
}