using FilaCepac.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace FilaCepac.Controllers
{
    public class ArquivoFilaController : MainAPIController
    {

        public IQueryable<ArquivoFila> GetArquivosFila([FromUri] ArquivoFila afQuery)
        {
            IQueryable<ArquivoFila> result = db.ArquivosFila;

            if (afQuery.IdFilaCepac > 0)
            {
                result = result.Where(af => af.IdFilaCepac == afQuery.IdFilaCepac);
            }
            if (result == null || result.ToList().Count == 0)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NoContent));
            }
            return result;
        }

        [ResponseType(typeof(ArquivoFila))]
        public IHttpActionResult PostArquivoFila(ArquivoFila af)
        {
            if (!IsAuthorizedUser())
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            if (!IsAllowedHost())
            {
                return BadRequest();
            }
            db.ArquivosFila.Add(af);
            try
            {
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = af.Id }, af);
            }
            catch (Exception ex)
            {
                Logger.Write("Erro ao salvar arquivofila.", ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }

        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutArquivoFila(int id, ArquivoFila af)
        {
            if (!IsAuthorizedUser())
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            if (!IsAllowedHost())
            {
                return BadRequest();
            }

            if (id != af.Id)
            {
                if (af.Id == 0)
                {
                    af.Id = id;
                }
                else
                {
                    return BadRequest();
                }
            }
            af = (ArquivoFila)UpdateObject(af, db.ArquivosFila.Find(id));

            db.Entry(af).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArquivoFilaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Logger.Write("Erro ao atualizar dados arquivofila.", ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteArquivoFila(int id)
        {
            if (!IsAuthorizedUser())
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            if (!IsAllowedHost())
            {
                Logger.Write("Host não autorizado para esta operação.");
                return BadRequest("");
            }
            ArquivoFila af = db.ArquivosFila.Find(id);
            if (af == null)
            {
                return NotFound();
            }
            try
            {
                db.ArquivosFila.Remove(af);
                db.SaveChanges();
                return StatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.Write("Erro ao excluir ArquivoFila ", ex);
                return InternalServerError();
            }

        }

        private bool ArquivoFilaExists(int id)
        {
            return db.ArquivosFila.Count(e => e.Id == id) > 0;
        }

    }
}