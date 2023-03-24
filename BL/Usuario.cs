using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetById(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezSuperDigitoContext contex = new DL.JrodriguezSuperDigitoContext())
                {
                    var query = contex.Usuarios.FromSqlRaw($"[UsuarioGetById] {usuario.IdUsuario}").AsEnumerable().FirstOrDefault();
                    ML.Usuario usuarioid = new ML.Usuario();

                    usuarioid.IdUsuario = query.IdUsuario;
                    usuarioid.UserName = query.UserName;
                    usuarioid.Password = query.Password;

                    usuarioid.Historial = new ML.Historial(); 

                    usuarioid.Historial.Numero = query.Numero;
                    usuarioid.Historial.Resultado= query.Resultado;
                    usuarioid.Historial.FechaIngreso = query.FechaHora.ToString("dd-MM-yyyy");

                    result.Object = usuarioid;
                    result.Correct = true;
                }

            }catch(Exception ex)
            {
                result.Ex= ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetByUserName(ML.Usuario usuario)
        {
            ML.Result resultUserName = new ML.Result();

            try
            {
                using (DL.JrodriguezSuperDigitoContext contex = new DL.JrodriguezSuperDigitoContext())
                {
                    var query = contex.Usuarios.FromSqlRaw($"UsuarioGetByUserName '{usuario.UserName}'").AsEnumerable().FirstOrDefault();

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