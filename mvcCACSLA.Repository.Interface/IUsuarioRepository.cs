using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcCACSLA.Repository.Interface
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetUsuario();

        Usuario GetUsuario(int id);

        void AddUsuario(Usuario newUsuario);

        void updateUsuario(int id, Usuario updateUsuario);

        void DeleteUsuario(int id);

        void UpdateUsuarios(IEnumerable<Usuario> updateUsuarios);

    }
}
