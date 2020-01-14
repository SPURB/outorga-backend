using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FilaCepac.Models;

namespace FilaCepac.Controllers
{
    public class SqlsController : MainAPIController
    {

        // GET: api/Sqls
        public IQueryable<Sql> GetSqls([FromUri] Sql sql)
        {
            IQueryable<Sql> result = db.Sqls;
            if (sql.IdFilaCepac > 0)
            {
                result = result.Where(s => s.IdFilaCepac == sql.IdFilaCepac);
            }
            
            if(sql.NumeroSql != null)
            {
                result = result.Where(s => s.NumeroSql == sql.NumeroSql);
            }
            return result;
        }

        // GET: api/Sqls/5
        [ResponseType(typeof(Sql))]
        public IHttpActionResult GetSql(int id)
        {
            Sql sql = db.Sqls.Find(id);
            if (sql == null)
            {
                return NotFound();
            }

            return Ok(sql);
        }

        // PUT: api/Sqls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSql(int id, Sql sql)
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
            if (!ModelState.IsValid)
            {
                Logger.Write("Formulário de Sql inválido.");
                return BadRequest(ModelState);
            }

            if (id != sql.Id)
            {
                if(sql.Id == 0)
                {
                    sql.Id = id;
                }
                else
                {
                    Logger.Write("ID " + id + " Sql inválido.");
                    return BadRequest();
                }
            }

            try
            {
                Sql sqlOriginal = db.Sqls.Find(id);
                sql = (Sql)UpdateObject(sql, sqlOriginal);

                db.Entry(sql).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Logger.Write("Erro ao atualizar SQL ", ex);
                if (!SqlExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }catch (Exception ex)
            {
                Logger.Write("Erro ao salvar dados SQL.", ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Sqls
        [ResponseType(typeof(Sql))]
        public IHttpActionResult PostSql(Sql sql)
        {
            if (!IsAuthorizedUser())
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            if (!IsAllowedHost())
            {
                Logger.Write("Host não autorizado para esta operação.");
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                Logger.Write("Formulário de Sql inválido.");
                return BadRequest(ModelState);
            }

            db.Sqls.Add(sql);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sql.Id }, sql);
        }

        public IHttpActionResult DeleteSql(int id)
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
            Sql sql = db.Sqls.Find(id);
            if(sql == null)
            {
                return NotFound();
            }
            db.Sqls.Remove(sql);
            return StatusCode(HttpStatusCode.NoContent);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SqlExists(int id)
        {
            return db.Sqls.Count(e => e.Id == id) > 0;
        }
    }
}