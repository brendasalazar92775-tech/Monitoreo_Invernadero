using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoreo_Invernadero_1.Models
{
    public class Alumnos
    {
        public string Key { get; set; }
        public string Nombre { get; set; }
        public string P_Apellido { get; set; }
        public string S_Apellido { get; set; }
        public string Telefono { get; set; }
        public string CorreoI { get; set; }
        public Carrera carrera { get; set; }
        public Grupo Grupo { get; set; }
        public string Proyectos { get; set; }

        public string NombreCompleto => $"{Nombre} {P_Apellido}";

    }
}
