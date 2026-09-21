using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoreo_Invernadero_1.Models
{
    public class Flora
    {
        public string key { get; set; }
        public string Nombre { get; set; }
        public string NombreCientifico {  get; set; }
        public string Descripcion { get; set; }
        public string Temperatura { get; set; }
        public string Humedad {  get; set; }
        public double Cantidad { get; set; }
        public List<TipoFlora> tipoFlor {  get; set; }
        public Ubicacion ubicacion { get; set; }
        public Estados_crecimiento crecimiento { get; set; }
        public List<Funcion> funcion { get; set; }

    }
}
