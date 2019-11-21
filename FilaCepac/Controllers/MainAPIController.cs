using FilaCepac.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FilaCepac.Controllers
{

    public class MainAPIController : ApiController
    {

        protected static readonly string DeniedHost = "servicos.spurbanismo.sp.gov.br";
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(MainAPIController));
        
        protected readonly FilaCepacContext db = new FilaCepacContext();

        /*
         * Verifica o host da requisicao está na lista
         */
        protected static bool IsAllowedHost()
        {
            return !(HttpContext.Current.Request.Url.Host.Contains(DeniedHost));
        }

        /*
         * Usuário está autenticado e incluído na lista de autorizados
         * (AuthorizeAttribute nao atendeu)
         */
        protected bool IsAuthorizedUser()
        {
            /*
            if (!User.Identity.IsAuthenticated)
            {
                return false;
            }
            string user = this.GetDomainUser(User.Identity.Name);
            */
            IEnumerable<string> values;
            string user = null;
            if (Request.Headers.TryGetValues("", out values))
            {
                user = values.FirstOrDefault();
            }
            return db.Autorizacoes.Count(a => a.Usuario.Equals(user)) > 0;
        }

        private string GetDomainUser(string user)
        {
            //Remove o nome do domínio, se houver
            char domainSeparator = '\\';
            int separatorIndex = user.IndexOf(domainSeparator);
            return (separatorIndex >= 0) ? user.Substring(separatorIndex + 1) : user;
        }

        /*
         * O objeto original do bd tem as propriedades alteradas pelas do objeto novo
         */
        protected Object UpdateObject(Object newObj, Object origObj)
        {
            if(newObj.GetType() != origObj.GetType())
            {
                throw new Exception("Os objetos para update devem ser do mesmo tipo.");
            }

            foreach (var prop in newObj.GetType().GetProperties())
            {
                var origVal = prop.GetValue(origObj, null);
                var newVal = prop.GetValue(newObj, null);
                if (newVal != null && origVal != newVal)
                {
                    prop.SetValue(origObj, newVal, null);
                }
            }
            return origObj;
        }

    }
}