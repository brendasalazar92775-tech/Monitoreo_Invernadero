using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Dispatching;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class FaunaPage : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
    private readonly FirebaseAuthService _authService = new FirebaseAuthService();

    public ObservableCollection<Insectos> ListaInvasora { get; set; } = new ObservableCollection<Insectos>();
    public ObservableCollection<Insectos> ListaAutoctona { get; set; } = new ObservableCollection<Insectos>();

    public FaunaPage()
	{
		InitializeComponent();
        BindingContext = this;
        CargarFaunaAutoctona();
        CargarFaunaInvasora();
    }
    public async Task CargarFaunaAutoctona()
    {
        ListaAutoctona.Clear();

        var snapshot = await client.Child("Fauna").OnceAsync<Insectos>();
        var listaFiltrada = snapshot
            .Where(f => f.Object?.Tipo != null
                     && f.Object.Tipo.Nombre == "Fauna autóctona (nativa)")
            .Select(f =>
            {
                f.Object.Key = f.Key;
                return f.Object;
            })
            .ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var fauna in listaFiltrada)
                ListaAutoctona.Add(fauna);
        });
    }

    public async Task CargarFaunaInvasora()
    {
        ListaInvasora.Clear();

        var snapshot = await client.Child("Fauna").OnceAsync<Insectos>();
        var listaFiltrada = snapshot
            .Where(f => f.Object?.Tipo != null
                     && f.Object.Tipo.Nombre == "Fauna invasora")
            .Select(f =>
            {
                f.Object.Key = f.Key;
                return f.Object;
            })
            .ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var fauna in listaFiltrada)
                ListaInvasora.Add(fauna);
        });
    }




    private void OnEditarFauna_Clicked(object sender, EventArgs e)
    {

    }

    private async void OnEliminarFauna_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var insectos = button?.CommandParameter as Insectos;

        if (insectos != null)
        {
            bool confirm = await DisplayAlert("Confirmar", $"¿Eliminar Proyecto: {insectos.Nombre}?", "Si", "No");

            if (confirm)
            {
                await client.Child("Flora")
                    .Child(insectos.Key)
                    .DeleteAsync();

                ListaInvasora.Remove(insectos);
                ListaAutoctona.Remove(insectos);


            }
        }

    }


    private async void OnRegistrar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrarFauna());
    }

    private async void OnMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FloraYFaunaPage());
    }
}