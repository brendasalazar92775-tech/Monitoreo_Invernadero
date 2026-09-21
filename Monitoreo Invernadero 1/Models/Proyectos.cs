
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoreo_Invernadero_1.Models
{
    public class Proyectos
    {
        public string Key { get; set; }
        public string Nombre {  get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }    
        public DateTime FechaFibalizacion {  get; set; } 
        public Estado Estado { get; set; }

    }
}
