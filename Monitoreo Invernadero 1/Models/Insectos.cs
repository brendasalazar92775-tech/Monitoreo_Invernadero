using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoreo_Invernadero_1.Models
{
    public class Insectos
    {
        public string Key { get; set; }
        public string Nombre { get; set; }
        public string NombreCientifico { get; set; }
        public Tipo_Insecto Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Metodo_Control { get;set; }

    } 
}
