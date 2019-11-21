using FilaCepac.Data;
using FilaCepac.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace FilaCepac.Controllers
{
    public class SetoresController : ApiController
    {
        private readonly FilaCepacContext db = new FilaCepacContext();

        // GET: api/Setores
        public IQueryable<Setor> GetSetores()
        {
            return db.Setores.Include(s => s.OperacaoUrbana);
        }

        [ResponseType(typeof(Setor))]
        public IHttpActionResult GetSetor(int id)
        {
            Setor setor = db.Setores.Include(s => s.OperacaoUrbana).First(s => s.Id == id);
            if (setor == null)
            {
                return NotFound();
            }

            return Ok(setor);
        }

    }
}
