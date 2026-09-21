using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoreo_Invernadero_1.Models
{
    public class Usuarios
    {
        public string Key { get; set; }
        public string Nombre { get; set; }
        public string p_apellido { get; set; }
        public string s_apellido { get; set;}
        public Roles Roles { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public string Numero { get; set; }

        public string NombreCompeto => $"{Nombre} {p_apellido} {s_apellido}";
    }
}
