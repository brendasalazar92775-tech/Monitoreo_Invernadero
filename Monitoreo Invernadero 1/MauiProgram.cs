using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Logging;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.Services;
using CommunityToolkit.Maui;

namespace Monitoreo_Invernadero_1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit() 
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Oswald-Bold.ttf", "OswaldB");
                    fonts.AddFont("Roboto.ttf", "Roboto");

                });

#if DEBUG
            builder.Logging.AddDebug();
#endif      
            RegistrarEstado_Crecimiento();
            RegistrarFunciones();
            RegistrarUbicacion();
            RegistrarTipo_flora();
            RegistrarGrupos();
            RegistrarCarrera();
            RegistrarRoles();
            RegistrarTipo_Insectos();
            return builder.Build();
        }

        //Agregar Cargos
        public static void RegistrarRoles()
        {
            FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
            var Roles = client.Child("Roles").OnceAsync<Roles>();
            if (Roles.Result.Count == 0)
            {
                client.Child("Roles").PostAsync(new Roles { Nombre = "Coordinador" });
                client.Child("Roles").PostAsync(new Roles { Nombre = "Directivo" });
            }
        }

        //Agregar Grupos
        public static void RegistrarGrupos()
        {
            FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
            var Grupo = client.Child("Grupos").OnceAsync<Grupo>();
            if (Grupo.Result.Count == 0)
            {
                client.Child("Grupos").PostAsync(new Roles { Nombre = "1125IMEC" });
                client.Child("Grupos").PostAsync(new Roles { Nombre = "2125IMEC" });
                client.Child("Grupos").PostAsync(new Roles { Nombre = "3125IMEC" });
                client.Child("Grupos").PostAsync(new Roles { Nombre = "4125IMEC" });
                client.Child("Grupos").PostAsync(new Roles { Nombre = "5125IMEC" });
                client.Child("Grupos").PostAsync(new Roles { Nombre = "6125IMEC" });
            }
        }

        //Agregar Carrera
        public static void RegistrarCarrera()
        {
            FirebaseClient client = new FirebaseClient("--");
            var Carrera = client.Child("Carrera").OnceAsync<Carrera>();
            if (Carrera.Result.Count == 0)
            {
                client.Child("Carrera").PostAsync(new Carrera { Nombre = "Ingenieria en Software" });
                client.Child("Carrera").PostAsync(new Carrera { Nombre = "Ingeniería en Manufactura Avanzada" });
                client.Child("Carrera").PostAsync(new Carrera { Nombre = "Licenciatura en Ingeniería Financiera" });
                client.Child("Carrera").PostAsync(new Carrera { Nombre = "Licenciatura en Ingeniería en Tecnologías de la Información e Innovación Digital" });
                client.Child("Carrera").PostAsync(new Carrera { Nombre = "Licenciatura en Ingeniería Mecánica Automotriz" });
                client.Child("Carrera").PostAsync(new Carrera { Nombre = "Licenciatura en Comercio Internacional y Aduanas" });
            }
        }


        public static void RegistrarEstadoProyecto()
        {
            FirebaseClient client = new FirebaseClient("");
            var EstadoProyecto = client.Child("Estado_Proy").OnceAsync<Estado>();
            if (EstadoProyecto.Result.Count == 0)
            {
                client.Child("Estado_Proy").PostAsync(new Estado { Nombre_Estado = "Idea / Concepción" });
                client.Child("Estado_Proy").PostAsync(new Estado { Nombre_Estado = "Planificación / Definición" });
                client.Child("Estado_Proy").PostAsync(new Estado { Nombre_Estado = "Prototipo / Diseño" });
                client.Child("Estado_Proy").PostAsync(new Estado { Nombre_Estado = "Inicio / Ejecución" });
                client.Child("Estado_Proy").PostAsync(new Estado { Nombre_Estado = "Seguimiento / Control" });
                client.Child("Estado_Proy").PostAsync(new Estado { Nombre_Estado = "Cierre / Conclusión" });
            }

        }

        //CARGAR FK DE TABLA DE FLORA 

        public static void RegistrarTipo_flora()
        {
            FirebaseClient client = new FirebaseClient("--");

            var Tipo_flora = client.Child("Tipo_flora").OnceAsync<TipoFlora>();
            if (Tipo_flora.Result.Count == 0)
            {
                client.Child("Tipo_flora").PostAsync(new TipoFlora { Nombre = "Arbol", Descripcion = "Plantas leñosas perennes que alcanzan gran tamaño." });

                client.Child("Tipo_flora").PostAsync(new TipoFlora { Nombre = "Hortaliza", Descripcion = "Plantas cultivadas para consumo alimenticio, principalmente herbáceas." });

                client.Child("Tipo_flora").PostAsync(new TipoFlora { Nombre = "Cultivo", Descripcion = "Plantas sembradas para producción agrícola a gran escala." });

                client.Child("Tipo_flora").PostAsync(new TipoFlora { Nombre = "Chiles", Descripcion = "Plantas del género Capsicum, productoras de frutos picantes o dulces." });

                client.Child("Tipo_flora").PostAsync(new TipoFlora { Nombre = "Fruta", Descripcion = "Plantas que producen frutos comestibles, generalmente dulces." });

                client.Child("Tipo_flora").PostAsync(new TipoFlora { Nombre = "Leguminosa", Descripcion = "Planta con flores que produce un fruto que encierra una o varias semillas." });

            }

        }

        public static void RegistrarUbicacion()
        {
            FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
            var Ubicacion = client.Child("Ubicacion").OnceAsync<Ubicacion>();
            if (Ubicacion.Result.Count == 0)
            {
                client.Child("Ubicacion").PostAsync(new Ubicacion { Nombre = "Dentro del Invernadero" });
                client.Child("Ubicacion").PostAsync(new Ubicacion { Nombre = "Fuera del Invernadero" });

            }

        }

        public static void RegistrarFunciones()
        {
            FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
            var Funcion = client.Child("Funcion").OnceAsync<Funcion>();
            if (Funcion.Result.Count == 0)
            {
                client.Child("Funcion").PostAsync(new Funcion { Nombre = "Alimentaria", Descripcion = "Planta cultivada para consumo humano." });

                client.Child("Funcion").PostAsync(new Funcion { Nombre = "Medicinal", Descripcion = "Planta utilizada por sus propiedades terapéuticas." });

                client.Child("Funcion").PostAsync(new Funcion { Nombre = "Ornamental", Descripcion = "Planta usada con fines decorativos o estéticos." });

                client.Child("Funcion").PostAsync(new Funcion { Nombre = "Polinizadora", Descripcion = "Planta que favorece la presencia de polinizadores." });


            }

        }

        public static void RegistrarEstado_Crecimiento()
        {
            FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
            var Estados_crecimiento = client.Child("Estados_crecimiento").OnceAsync<Estados_crecimiento>();
            if (Estados_crecimiento.Result.Count == 0)
            {
                client.Child("Estados_crecimiento").PostAsync(new Estados_crecimiento { Nombre = "Germinacion", Descripcion = "La semilla comienza a desarrollarse." });

                client.Child("Estados_crecimiento").PostAsync(new Estados_crecimiento { Nombre = "Plántula", Descripcion = "La planta joven emerge con sus primeras hojas." });

                client.Child("Estados_crecimiento").PostAsync(new Estados_crecimiento { Nombre = "Crecimiento vegetativo", Descripcion = "Desarrolla hojas, tallos y raíces activamente." });

                client.Child("Estados_crecimiento").PostAsync(new Estados_crecimiento { Nombre = "Floración", Descripcion = "Aparecen flores o estructuras reproductivas." });

                client.Child("Estados_crecimiento").PostAsync(new Estados_crecimiento { Nombre = "Fructificación", Descripcion = "Se desarrollan frutos o semillas." });

                client.Child("Estados_crecimiento").PostAsync(new Estados_crecimiento { Nombre = "Cosecha", Descripcion = "Etapa de recolección del producto." });




            }

        }

        //Insectos 

        public static void RegistrarTipo_Insectos()
        {
            FirebaseClient client = new FirebaseClient("https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/");
            var Tipo_Insecto = client.Child("Tipo_Insecto").OnceAsync<Tipo_Insecto>();
            if (Tipo_Insecto.Result.Count == 0)
            {
                client.Child("Tipo_Insecto").PostAsync(new Tipo_Insecto { Nombre = "Fauna autóctona (nativa)", Descripcion = "Es la fauna propia de un ecosistema." });
                client.Child("Tipo_Insecto").PostAsync(new Tipo_Insecto { Nombre = "Fauna invasora", Descripcion = "Es aquella que no pertenece al ecosistema y qcausa daños ambientales" });

            }
        }
    }
}
