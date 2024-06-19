using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace web_api.Controllers
{
    public class ConsultasController : ApiController
    {
        private readonly Repositories.IRepository<Models.Consulta> repository;

        public ConsultasController()
        {
            try
            {
                repository = new Repositories.Database.SQLServer.ADO.Consulta(Configurations.SQLServer.getConnectionString());
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
            }
        }
        // GET: api/consulta
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
        public IHttpActionResult Get(int id)
        {
            try
            {
                Models.Consulta consulta = repository.getById(id);

                if (consulta.IdConsulta == 0)
                    return NotFound();

                return Ok(consulta);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }

        // POST: api/consulta
        public IHttpActionResult Post(Models.Consulta consulta)
        {
            
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                repository.add(consulta);

                return Ok(consulta);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }
        // PUT: api/consulta
        public IHttpActionResult Put(int id, Models.Consulta consulta)
        {
            try
            {
                //Validação - Padrão Return Early
                if (id != consulta.IdConsulta)
                    return BadRequest("Código enviado no parâmetro é diferente do id da consuta.");

               

                int linhasAfetadas = repository.update(id, consulta);

                if (linhasAfetadas == 0)
                    return NotFound();

                return Ok(consulta);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
                return InternalServerError();
            }
        }
        // DELETE: api/consulta
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
        //localhost/medicos?nome=
        public IHttpActionResult Get(string nome)
        {
            


            return null; 

        }
    }
}