using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Monitoreo_Invernadero_1.Models;
using Monitoreo_Invernadero_1.ViewsCoordinador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace Monitoreo_Invernadero_1.Services
{
    public class FirebaseAuthService
    {
        private const string ApiKey = "AIzaSyDhjQi-EGqWgaRysDiHlGDSADDJh0vBvqU";
        private const string DatabaseUrl = "https://monitoreo-invernadero-7d43b-default-rtdb.firebaseio.com/";
        private readonly FirebaseAuthProvider authProvider;
        private readonly FirebaseClient client;
        private readonly HttpClient http = new HttpClient();

        public FirebaseAuthService()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            client = new FirebaseClient(DatabaseUrl);
        }

        //Registrar Usuario 

        public async Task<bool> RegistraUsuario(string nombre, string p_apellido, string s_apellido, string correo, string contraseña, Roles rol, string numero)
        {
            try
            {
                var created = await authProvider.CreateUserWithEmailAndPasswordAsync(correo, contraseña);
                var uid = created.User.LocalId;
                var idToken = created.FirebaseToken;

                var usuario = new
                {
                    key = uid,
                    nombre = nombre,
                    p_apellido = p_apellido,
                    s_apellido = s_apellido,
                    correo = correo,
                    numero = numero,
                    Roles = new { Nombre = rol.Nombre }
                };

                var json = JsonSerializer.Serialize(usuario);
                var putUrl = $"{DatabaseUrl}/Usuarios/{uid}.json?auth={idToken}";

                var resp = await http.PutAsync(putUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                resp.EnsureSuccessStatusCode();

                return true;
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception($"Error de autenticación: {ex.Reason}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar usuario: {ex.Message}");
            }
        }

     
        public async Task<Usuarios> Login(string email, string password)
        {
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            var uid = auth.User.LocalId;
            var idToken = auth.FirebaseToken;

            var getUrl = $"{DatabaseUrl}/Usuarios/{uid}.json?auth={idToken}";
            var resp = await http.GetAsync(getUrl);
            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json) || json == "null")
                return null;
            
            var usuario = JsonSerializer.Deserialize<Usuarios>(json);

            // Guardar el UID en la propiedad Key
            usuario.Key = uid;

            return usuario;
        }

        //Registrar Proyectos
        public async Task<bool> RegistrarProyectos(string Nombre, string Descripcion, DateTime FechaInicio, DateTime FechaFinalizacion, Estado estado)
        {
            try
            {
                var proyecto = new Proyectos
                {
                    Nombre = Nombre,
                    Descripcion = Descripcion,
                    FechaInicio = FechaInicio,
                    FechaFibalizacion = FechaFinalizacion,
                    Estado = estado
                };

                await client.Child("Proyectos")
                    .PostAsync(proyecto);
                return true;
            }
            catch
            {
                return false;
            }
        } 

        public async Task<bool> RegistrarAlumno(string nombre, string p_apellido, string s_apellido, string telefono, string correoI ,Carrera carrera, Grupo Grupo, string proyectos)
        {
            try
            {
                var alumno = new Alumnos
                {
                    Nombre = nombre,
                    P_Apellido = p_apellido,
                    S_Apellido = s_apellido,
                    carrera = carrera,
                    Telefono = telefono,
                    CorreoI = correoI,
                    Grupo = Grupo,
                    Proyectos = proyectos
                };
                await client.Child("Alumnos")
                    .PostAsync(alumno);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RegistrarFlora(string nombre, string nombreC, string descripcion, string temperatura, string humedad, double cantidad, List<TipoFlora> tipoflora, Ubicacion ubicacion, Estados_crecimiento crecimiento, List<Funcion> funcion)
        {
            try
            {
                var flora = new Flora
                {
                    Nombre = nombre,
                    NombreCientifico = nombreC,
                    Descripcion = descripcion,
                    Temperatura = temperatura,
                    Humedad = humedad,
                    Cantidad = cantidad,
                    tipoFlor = tipoflora,
                    ubicacion = ubicacion,
                    crecimiento = crecimiento,
                    funcion = funcion
                };

                await client.Child("Flora")
                    .PostAsync(flora);
                return true;

            }
            catch 
            { 
                return false;
            }
        }
       

        public async Task ActualizarUsuario(string key, Usuarios usuario)
        {
            await client
            .Child("Usuarios")
            .Child(key)
            .PutAsync(usuario);
        }

        public async Task ActualizarProyecto(string key, Proyectos proyecto)
        {
            await client
                .Child("Proyectos")
                .Child (key)
                .PutAsync(proyecto);
        }

        // REGISTRAR INSECTOS

        public async Task<bool> RegistrarInsectos(string Nombre, string NombreCientifico, Tipo_Insecto Tipo, string Descripcion, string Metodo_Control)
        {
            try
            {
                var insecto = new Insectos { 
                Nombre = Nombre,
                NombreCientifico = NombreCientifico,
                Tipo = Tipo,
                Descripcion = Descripcion,
                Metodo_Control = Metodo_Control
                };

                await client.Child("Fauna")
                    .PostAsync(insecto);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<Sensor?> ObtenerDatosSensor()
        {
            try
            {
                var datos = await client
                    .Child("Sensor")
                    .OnceSingleAsync<Sensor>();

                if (datos == null)
                {
                    Console.WriteLine("[Firebase] Sensor es NULL");
                    return null;
                }

                Console.WriteLine($"[Firebase] Temperatura: {datos.Temperatura}");
                Console.WriteLine($"[Firebase] Humedad: {datos.Humedad}");

                return datos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Firebase: {ex.Message}");
                return null;
            }
        }
    }
    }

