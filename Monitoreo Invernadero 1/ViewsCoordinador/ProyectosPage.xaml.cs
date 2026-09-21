using Firebase.Database;
using Firebase.Database.Query;
using LiteDB;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Monitoreo_Invernadero_1.ViewsCoordinador;


public partial class ProyectosPage : ContentPage
{

    FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");

    public ObservableCollection<Proyectos> Lista { get; set; } = new ObservableCollection<Proyectos>();

    public ProyectosPage()
    {
        InitializeComponent();
        BindingContext = this;
        cargarProyectos();
    }

    public void cargarProyectos()
    {
        client.Child("Proyectos")
            .AsObservable<Proyectos>()
            .Subscribe((proyectos_p) =>
            {
                if (proyectos_p.Object != null)
                {
                    proyectos_p.Object.Key = proyectos_p.Key;
                    Lista.Add(proyectos_p.Object);
                }

            });
    }

    private void filtroEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

        string filtro = filtroEntry.Text.ToLower();

        if (filtro.Length > 0)
        {
            ListaCollection.ItemsSource = Lista.Where(X => X.Nombre.ToLower().Contains(filtro));
        }
        else
        {
            ListaCollection.ItemsSource = Lista;
        }
    }

    private async void nuevoUs_btn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new RegisterProyectos());
    }

    private async void OnMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuCoordinadorPage());
    }

    private async void OnEliminarProyecto_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var proyecto = button?.CommandParameter as Proyectos;

        if (proyecto != null) 
        {
            bool confirm = await DisplayAlert("Confirmar", $"¿Eliminar Proyecto: {proyecto.Nombre}?", "Si", "No");

            if (confirm)
            {
                await client.Child("Proyectos")
                    .Child(proyecto.Key)
                    .DeleteAsync();

                Lista.Remove(proyecto);
            }
        }
    }

    private async void OnEditarProyecto_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var proyectos = button?.BindingContext as Proyectos;


        if (proyectos != null)
        {
            await Navigation.PushAsync(new EditarProyectoPage(proyectos));
        }
    }

    private async void VerProyecto_Clicked(object sender, EventArgs e)
    {
        var boton = sender as Button;
        var proyecto = boton?.BindingContext as Proyectos;

        if (proyecto != null)
        {
            await Navigation.PushAsync(new VerProyectoPage(proyecto));
        }
    }
}