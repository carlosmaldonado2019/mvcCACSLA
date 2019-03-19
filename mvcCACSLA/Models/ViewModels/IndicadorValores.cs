using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace mvcCACSLA.Models.ViewModels
{
    public class Indicadores
    {
        public int IdEstandar { get; set; }
        public int IdVariable { get; set; }
        public string DescripcionEstandar { get; set; }
        public string DescripcionVariable { get; set; }
        public int Referente { get; set; }
        public string DescripcionReferente { get; set; }
        public string Anexo { get; set; }
        public int ValorMax { get; set; }
        public int ValorObt { get; set; }
        public int IdCoordinacion { get; set; }
    }
}