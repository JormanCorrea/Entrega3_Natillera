using Examen_3_ServWeb_Natillera.Models;
using System.Collections.Generic;
using System.Linq;

namespace Examen_3_ServWeb_Natillera.Clases
{
    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }

        private DBNatilleraEntities db = new DBNatilleraEntities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }

        private bool ValidarUsuario()
        {
            var admin = db.Administradors
                .FirstOrDefault(a => a.Usuario == login.Usuario);

            if (admin == null)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = "El usuario no existe";
                return false;
            }

            return true;
        }

        private bool ValidarClave()
        {
            var admin = db.Administradors
                .FirstOrDefault(a => a.Usuario == login.Usuario && a.Clave == login.Clave);

            if (admin == null)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = "La clave no coincide";
                return false;
            }

            return true;
        }

        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario() && ValidarClave())
            {
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);

                return new List<LoginRespuesta>
                {
                    new LoginRespuesta
                    {
                        Usuario = login.Usuario,
                        Autenticado = true,
                        Perfil = "Administrador",
                        PaginaInicio = "Eventos",
                        Token = token,
                        Mensaje = "Inicio de sesión exitoso"
                    }
                }.AsQueryable();
            }
            else
            {
                return new List<LoginRespuesta> { loginRespuesta }.AsQueryable();
            }
        }
    }
}
