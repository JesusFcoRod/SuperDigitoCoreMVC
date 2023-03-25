using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetByUserName(ML.Usuario usuario)
        {
            ML.Result resultUserName = new ML.Result();

            try
            {
                using (DL.JrodriguezSuperDigitoContext contex = new DL.JrodriguezSuperDigitoContext())
                {
                    var query = contex.Usuarios.FromSqlRaw($"[UsuarioGetByUserName] '{usuario.UserName}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuarioLogin = new ML.Usuario();

                        usuarioLogin.IdUsuario = query.IdUsuario; 
                        usuarioLogin.UserName = query.UserName;
                        usuarioLogin.Password = query.Password;


                        resultUserName.Object = usuarioLogin;
                        resultUserName.Correct = true;
                    }
                    else
                    {
                        resultUserName.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                resultUserName.Ex = ex;
                resultUserName.Correct = false;
                resultUserName.ErrorMessage = ex.Message;
            }
            return resultUserName;

        }


    }
}