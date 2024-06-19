using System;
using System.Web.Http;

namespace web_api.Controllers
{
    public class PacientesController : ApiController
    {
        private readonly Repositories.IRepository<Models.Paciente> repository;

        public PacientesController()
        {
            try
            {
                repository = new Repositories.Database.SQLServer.ADO.Paciente(Configurations.SQLServer.getConnectionString());
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
            }
        }

        // GET: api/Pacientes
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
        //Get: por nome
        //localhost/Pacintes ? nome=
        public IHttpActionResult Get(string nome)
        {
            try
            {
                Models.Paciente paciente = repository.getNome(nome);

                if (paciente.Codigo == 0|| paciente.Nome != paciente.Nome)
                    return NotFound();

                return Ok(paciente);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
           


           

        }

        // GET: api/Pacientes/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Models.Paciente paciente = repository.getById(id);

                if (paciente.Codigo == 0)
                    return NotFound();

                return Ok(paciente);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }

        // POST: api/Pacientes
        public IHttpActionResult Post(Models.Paciente paciente)
        {
            /*if (paciente.Nome.Trim() == "" || paciente.Email.Trim() == "")
                return BadRequest("Nome e/ou Email do paciente não pode(m) ser vazio(s).");*/

           try
            {
                if (String.IsNullOrWhiteSpace(paciente.Nome) || String.IsNullOrWhiteSpace(paciente.Email))
                   return BadRequest("Nome e/ou Email do paciente não pode(m) ser vazio(s).");                
                if (paciente.Nome.Length > 200 || paciente.Email.Length > 100)
                    return BadRequest("Nome e Email do paciente não podem ser maiores que 200 e 100 caracteres respectivamente.");

                repository.add(paciente);
               
                return Ok(paciente);
           }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }

        // PUT: api/Pacientes/5
        public IHttpActionResult Put(int id, Models.Paciente paciente)
        {
            try
            {
                //Validação - Padrão Return Early
                if (id != paciente.Codigo)
                    return BadRequest("Código enviado no parâmetro é diferente do código do paciente.");

                if (paciente.Nome.Trim() == "" || paciente.Email.Trim() == "")
                    return BadRequest("Nome e/ou Email do paciente não pode(m) ser vazio(s).");

                int linhasAfetadas = repository.update(id, paciente);               

                if (linhasAfetadas == 0)
                    return NotFound();

                return Ok(paciente);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }

        // DELETE: api/Pacientes/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                int linhasAfetadas = repository.delete(id);

                if (linhasAfetadas == 0)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }
    }
}
