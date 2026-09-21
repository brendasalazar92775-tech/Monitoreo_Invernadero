using Monitoreo_Invernadero_1.ViewsCoordinador;

namespace Monitoreo_Invernadero_1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Rutas

            Routing.RegisterRoute(nameof(VerUsuarioPage), typeof(VerUsuarioPage));
        }
    }
}
