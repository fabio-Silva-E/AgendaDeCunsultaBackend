using System;
using System.Web.Http;
using Models;
using Repositories;

namespace web_api.Controllers
{
    public class UsuariosController : ApiController
    {
        private readonly ILoge<Models.Usuario> repository;
      
        public UsuariosController()
        {
            try
            {
                repository = new Repositories.Database.SQLServer.ADO.Usuario(Configurations.SQLServer.getConnectionString());
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
            }
        }

         public IHttpActionResult Get(string login,string senha)
          {
            try
            {

                Models.Usuario usuario = repository.getLogin(login, senha);
               
                if (usuario.Login=="")
                    return NotFound();
                if (usuario.Senha == "")
                    return NotFound();
                return Ok(usuario);
            }
            catch(Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
          }
       public IHttpActionResult Post(Models.Usuario usuario)

        {
            try
            {
                if (String.IsNullOrWhiteSpace(usuario.Login) || String.IsNullOrWhiteSpace(usuario.Senha))
                    return BadRequest("lgin e/ou Senha  não pode(m) ser vazio(s).");
                if (usuario.Login.Length > 100 || usuario.Senha.Length > 100)
                    return BadRequest("Login e Senha  não podem ser maiores que 100 e 100 caracteres respectivamente.");

                repository.add(usuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }
        public IHttpActionResult Delete(string login)
        {
            try
            {
                repository.delete(login);

               

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(repository.get());
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }
        public IHttpActionResult Put(string login,string senha, Models.Usuario usuario)
        {
            try
            {
                //Validação - Padrão Return Early
                if (login != usuario.Login)
                    return BadRequest("login enviado no parâmetro é diferente ");

                if (usuario.Login.Trim() == "" || usuario.Senha.Trim() == "")
                    return BadRequest("senha e/ou login não pode(m) ser vazio(s).");

                 repository.update(login,senha, usuario);

               

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }
    }
}