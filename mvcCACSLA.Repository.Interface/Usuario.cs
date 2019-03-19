using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mvcCACSLA.Models;

namespace mvcCACSLA.Repository.Interface
{
    public partial class Usuario
    {
      
        public int IdUsuario { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Descripcion { get; set; }
        public int IdNivel { get; set; }
        public string NivelDescripcion { get; set; }
        public int Activo { get; set; }
        public int IdCarrera { get; set; }
        public string CarreraDescripcion { get; set; }
        public int IdCoordinacion { get; set; }
        public string CoordinacionDescripcion { get; set; }
        public Nullable<bool> SubeCarreras { get; set; }
    }
}
