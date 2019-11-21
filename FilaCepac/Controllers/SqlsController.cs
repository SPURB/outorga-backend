using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FilaCepac.Data;
using FilaCepac.Models;

namespace FilaCepac.Controllers
{
    public class SqlsController : MainAPIController
    {
        private readonly FilaCepacContext db = new FilaCepacContext();

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
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
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
                    return BadRequest();
                }
            }
            Sql sqlOriginal = db.Sqls.Find(id);
            sql = (Sql)UpdateObject(sql, sqlOriginal);

            db.Entry(sql).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SqlExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sqls.Add(sql);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sql.Id }, sql);
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