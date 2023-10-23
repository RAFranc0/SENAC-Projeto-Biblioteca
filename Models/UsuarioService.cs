using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
        }

        public Usuario Listar(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }

        public void incluirUsuario(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Add(u);
                bc.SaveChanges();
            }
        }

        public void EditarUsuario(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuarioBD = bc.Usuarios.Find(u.Id);
                usuarioBD.Nome = u.Nome;
                usuarioBD.Login = u.Login;

                if(usuarioBD.Senha != u.Senha)
                {
                    usuarioBD.Senha = Criptografo.TextoCriptografado(u.Senha);
                }
                else
                {
                    usuarioBD.Senha = u.Senha;
                }
                
                usuarioBD.Tipo = u.Tipo;

                bc.SaveChanges();
            }
        }

        public void excluirUsuario(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }
        }
    }
}