using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FilaCepac.Models;
using Log;

namespace FilaCepac.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class FilaController : MainAPIController
    {

        // GET: api/Fila
        public IQueryable<Fila> GetFilas([FromUri] Fila filaQuery)
        {
            try
            {

                IQueryable<Fila> result = db.Filas.
                    Include(f => f.Status).
                    Include(f => f.SetorObj.OperacaoUrbana).
                    OrderBy(f => f.Data);

                if (filaQuery.Sei != null)
                {
                    result = result.Where(f => f.Sei == filaQuery.Sei);
                }
                if (filaQuery.IdSetor != null)
                {
                    result = result.Where(f => f.IdSetor == filaQuery.IdSetor);
                }
                if (filaQuery.SubSetor != null)
                {
                    result = result.Where(f => f.SubSetor == filaQuery.SubSetor);
                }
                if (filaQuery.IdStatus != null)
                {
                    result = result.Where(f => f.IdStatus == filaQuery.IdStatus);
                }

                return result;
            }
            catch(Exception ex)
            {
                Logger.Write("Erro ao obter dados da fila.", ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
            

        }

        // GET: api/Fila/5
        [ResponseType(typeof(Fila))]
        public IHttpActionResult GetFila(int id)
        {
            if (!FilaExists(id))
            {
                return NotFound();
            }

            try
            {
                Fila fila = db.Filas.
                    Include(f => f.Status).
                    Include(f => f.SetorObj.OperacaoUrbana).
                    First(f => f.Id == id);
                if (fila == null)
                {
                    return NotFound();
                }

                return Ok(fila);
            }catch(Exception ex)
            {
                Logger.Write("Erro ao obter dados da fila.", ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


        // PUT: api/Fila/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFila(int id, Fila fila)
        {
            if (!IsAuthorizedUser())
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            if (!IsAllowedHost())
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                Logger.Write("Dados da fila inválidos.");
                foreach (var err in ModelState.Keys)
                {
                    Logger.Write(err);
                }
                return BadRequest(ModelState);
            }
            

            if (id != fila.Id)
            {
                if(fila.Id == 0)
                {
                    fila.Id = id;
                }
                else
                {
                    return BadRequest();
                }
            }
            fila.DataAlteracao = DateTime.Now;
            //fila.UsuarioAlteracao = User.Identity.Name;
            fila = (Fila)UpdateObject(fila, db.Filas.Find(id));

            db.Entry(fila).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }catch(Exception ex){
                Logger.Write("Erro ao salvar dados da fila.", ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Fila
        [ResponseType(typeof(Fila))]
        public IHttpActionResult PostFila(Fila fila)
        {
            if (!IsAuthorizedUser())
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            if (!IsAllowedHost())
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                Logger.Write("Dados inválidos.");
                foreach(var err in ModelState.Keys)
                {
                    Logger.Write(err);
                }
                return BadRequest(ModelState);
            }
            fila.DataAlteracao = DateTime.Now;
            db.Filas.Add(fila);
            try
            {
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = fila.Id }, fila);
            }
            catch (Exception ex)
            {
                Logger.Write("Erro ao obter dados da fila.", ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilaExists(int id)
        {
            return db.Filas.Count(e => e.Id == id) > 0;
        }
    }
}