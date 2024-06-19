using System;
using System.Web.Http;
using Models;
using Repositories;

namespace web_api.Controllers
{
    public class UsuarioadsController : ApiController
    {
        private readonly ILoge<Models.Usuarioad> repository;
      
        public UsuarioadsController()
        {
            try
            {
                repository = new Repositories.Database.SQLServer.ADO.Usuarioad(Configurations.SQLServer.getConnectionString());
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

                Models.Usuarioad usuarioad = repository.getLogin(login, senha);
               
                  
                if (usuarioad.Login=="")
                    return NotFound();
                if (usuarioad.Senha == "")
                    return NotFound();
                return Ok(usuarioad);
            }
            catch(Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
          }
       public IHttpActionResult Post(Models.Usuarioad usuarioad)

        {
            try
            {
                if (String.IsNullOrWhiteSpace(usuarioad.Login) || String.IsNullOrWhiteSpace(usuarioad.Senha))
                    return BadRequest("lgin e/ou Senha  não pode(m) ser vazio(s).");
                if (usuarioad.Login.Length > 100 || usuarioad.Senha.Length > 100)
                    return BadRequest("Login e Senha  não podem ser maiores que 100 e 100 caracteres respectivamente.");

                repository.add(usuarioad);

                return Ok(usuarioad);
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
        public IHttpActionResult Put(string login,string senha, Models.Usuarioad usuarioad)
        {
            try
            {
                //Validação - Padrão Return Early
                if (login != usuarioad.Login)
                    return BadRequest("login enviado no parâmetro é diferente ");

                if (usuarioad.Login.Trim() == "" || usuarioad.Senha.Trim() == "")
                    return BadRequest("senha e/ou login não pode(m) ser vazio(s).");

                 repository.update(login,senha, usuarioad);

               

                return Ok(usuarioad);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }
    }
}