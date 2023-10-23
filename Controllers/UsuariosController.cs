using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Administrador()
        {
            return View();
        }

        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View(new UsuarioService().Listar());
        }

        public IActionResult RegistrarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {
            novoUser.Senha = Criptografo.TextoCriptografado(novoUser.Senha);

            new UsuarioService().incluirUsuario(novoUser);
            
            return RedirectToAction("ListaDeUsuarios");
        }

        public IActionResult EditarUsuario (int Id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar(Id));
        }

        [HttpPost]
        public IActionResult EditarUsuario (Usuario userEditado)
        {

            new UsuarioService().EditarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");
        }

        [HttpPost]
        public IActionResult ExcluirUsuario(int Id)
        {
            
            new UsuarioService().excluirUsuario(Id);
            
            return RedirectToAction("ListaDeUsuarios");
        }
    }
}