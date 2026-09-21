using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Firebase.Database;
using System.Linq;
using Microsoft.Maui.Dispatching;
using Firebase.Database.Query;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;

namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class FloraPage : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
    private readonly FirebaseAuthService _authService = new FirebaseAuthService();

    public ObservableCollection<Flora> Lista { get; set; } = new ObservableCollection<Flora>();
    public ObservableCollection<Flora> ListaArbol { get; set; } = new ObservableCollection<Flora>();
    public ObservableCollection<Flora> ListaHortaliza { get; set; } = new ObservableCollection<Flora>();
    public ObservableCollection<Flora> ListaFruta { get; set; } = new ObservableCollection<Flora>();
    public ObservableCollection<Flora> ListaChiles { get; set; } = new ObservableCollection<Flora>();
    public ObservableCollection<Flora> ListaCultivos { get; set; } = new ObservableCollection<Flora>();



    public FloraPage()
	{
		InitializeComponent();
        BindingContext = this;

        CargarFloraTipoArbol();
        CargarFloraTipoHortaliza();
        CargarFloraTipoFruta();
        CargarFloraTipoChiles();
        CargarFloraTipoCultivo();

    }




    public async Task CargarFloraTipoArbol()
    {
        ListaArbol.Clear();

        var snapshot = await client.Child("Flora").OnceAsync<Flora>();
        var listaFiltrada = snapshot
            .Where(f => f.Object?.tipoFlor != null
                     && f.Object.tipoFlor.Any(t => t.Nombre == "Arbol"))
            .Select(f =>
            {
                f.Object.key = f.Key;
                return f.Object;
            })
            .ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var flora in listaFiltrada)
                ListaArbol.Add(flora);
        });
    }


    public async Task CargarFloraTipoHortaliza()
    {
        ListaHortaliza.Clear();

        var snapshot = await client.Child("Flora").OnceAsync<Flora>();
        var listaFiltrada = snapshot
            .Where(f => f.Object?.tipoFlor != null
                     && f.Object.tipoFlor.Any(t => t.Nombre == "Hortaliza"))
            .Select(f =>
            {
                f.Object.key = f.Key;
                return f.Object;
            })
            .ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var flora in listaFiltrada)
                ListaHortaliza.Add(flora);
        });
    }

    public async Task CargarFloraTipoFruta()
    {
        ListaFruta.Clear();

        var snapshot = await client.Child("Flora").OnceAsync<Flora>();
        var listaFiltrada = snapshot
            .Where(f => f.Object?.tipoFlor != null
                     && f.Object.tipoFlor.Any(t => t.Nombre == "Fruta"))
            .Select(f =>
            {
                f.Object.key = f.Key;
                return f.Object;
            })
            .ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var flora in listaFiltrada)
                ListaFruta.Add(flora);
        });
    }

    public async Task CargarFloraTipoChiles()
    {
        ListaChiles.Clear();

        var snapshot = await client.Child("Flora").OnceAsync<Flora>();
        var listaFiltrada = snapshot
            .Where(f => f.Object?.tipoFlor != null
                     && f.Object.tipoFlor.Any(t => t.Nombre == "Chiles"))
            .Select(f =>
            {
                f.Object.key = f.Key;
                return f.Object;
            })
            .ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var flora in listaFiltrada)
                ListaChiles.Add(flora);
        });
    }

    public async Task CargarFloraTipoCultivo()
    {
        ListaCultivos.Clear();

        var snapshot = await client.Child("Flora").OnceAsync<Flora>();
        var listaFiltrada = snapshot
            .Where(f => f.Object?.tipoFlor != null
                     && f.Object.tipoFlor.Any(t => t.Nombre == "Cultivo"))
            .Select(f =>
            {
                f.Object.key = f.Key;
                return f.Object;
            })
            .ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var flora in listaFiltrada)
                ListaCultivos.Add(flora);
        });
    }




    private async void OnMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FloraYFaunaPage());
    }





    private async void OnEliminarFlora_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var flora = button?.CommandParameter as Flora;

        if (flora != null)
        {
            bool confirm = await DisplayAlert("Confirmar", $"¿Eliminar Proyecto: {flora.Nombre}?", "Si", "No");

            if (confirm)
            {
                await client.Child("Flora")
                    .Child(flora.key)
                    .DeleteAsync();

                ListaArbol.Remove(flora);
                ListaHortaliza.Remove(flora);
                ListaFruta.Remove(flora);
                ListaChiles.Remove(flora);
                ListaCultivos.Remove(flora);

            }
        }
    }

    private void OnEditarFlora_Clicked(object sender, EventArgs e)
    {

    }

    private async void nuevaFl_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroFlora());
    }
}