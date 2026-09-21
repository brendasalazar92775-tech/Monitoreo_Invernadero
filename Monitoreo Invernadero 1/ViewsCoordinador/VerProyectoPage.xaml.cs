using Firebase.Database;
using Firebase.Database.Query;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Monitoreo_Invernadero_1.ViewsCoordinador;

public partial class VerProyectoPage : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
	private Proyectos proyectos;
	public VerProyectoPage(Proyectos proyectos)
	{
		InitializeComponent();
        BindingContext = this;
        CargarAlumnos();
		this.proyectos = proyectos;

        nombreLabelTitle.Text = proyectos.Nombre;
        nombreLabel.Text = proyectos.Nombre;
		descripcionLabel.Text = proyectos.Descripcion;
		f_inicioLabel.Text = proyectos.FechaInicio.ToString("dddd, dd MMMM yyyy");
        f_terminoLabel.Text = proyectos.FechaFibalizacion.ToString("dddd, dd MMMM yyyy");
        estadoProyectoLabel.Text = proyectos.Estado.Nombre_Estado;
    }
    public ObservableCollection<Alumnos> Lista { get; set; } = new ObservableCollection<Alumnos>();
    private bool _sinAlumnos;
    public bool SinAlumnos
    {
        get => _sinAlumnos;
        set
        {
            _sinAlumnos = value;
            OnPropertyChanged();
        }
    }

    public void CargarAlumnos()
    {
        Lista.Clear();
        client.Child("Alumnos").
            AsObservable<Alumnos>()
            .Where(u => u.Object != null &&
                        u.Object.Proyectos == proyectos.Key)
            .Subscribe((alumnos) =>
            {
                if(alumnos.Object != null)
                {
                    alumnos.Object.Key = alumnos.Key;
                    Lista.Add(alumnos.Object);
                }

                SinAlumnos = Lista.Count == 0;
            });

        SinAlumnos = true;
    }

    private async void OnVolver_Clicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new ProyectosPage());
       
    }
    private async void OnEliminarAlumno_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var alumno = button?.CommandParameter as Alumnos;

        if (alumno != null)
        {
            bool confirmar = await DisplayAlert("Confirmrmar", $"¿Eliminar Alumno: {alumno.Nombre}", "Si", "No");
            if(confirmar)
            {
                await client.Child("Alumnos")
                    .Child(alumno.Key)
                    .DeleteAsync();

                Lista.Remove(alumno);
            }
        }
    }

    private async void nuevoUs_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroAlumnos(proyectos));
    }
}