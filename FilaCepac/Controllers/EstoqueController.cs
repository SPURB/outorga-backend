using FilaCepac.Data;
using FilaCepac.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;

namespace FilaCepac.Controllers
{
    public class EstoqueController : ApiController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EstoqueController));

        private readonly FilaCepacContext db = new FilaCepacContext();

        // GET: api/Estoque
        public List<EstoqueSetor> GetEstoques([FromUri] EstoqueSetor estoqueQuery)
        {
           IEnumerable<EstoqueSetor> result;
            string sql = "SELECT * FROM ouc.vw_estoque_setor";
            try
            {
                result = db.Database.SqlQuery<EstoqueSetor>(sql).ToList();
                if (estoqueQuery.Setor != 0)
                {
                    result = result.Where(es => es.Setor.Equals(estoqueQuery.Setor));
                }
                if (estoqueQuery.IdStatus != 0)
                {
                    result = result.Where(es => es.IdStatus == estoqueQuery.IdStatus);
                }
                return result.ToList();
            }
            catch(Exception ex)
            {
                Logger.Error("Erro ao consultar a view de estoques.", ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }




        }

    }
}
