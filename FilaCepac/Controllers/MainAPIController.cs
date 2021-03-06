﻿using FilaCepac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Log;

namespace FilaCepac.Controllers
{

    public class MainAPIController : ApiController
    {

        protected static readonly string DeniedHost = "servicos.spurbanismo.sp.gov.br";
        
        protected readonly FilaCepacContext db = new FilaCepacContext();

        protected static readonly Logger Logger = new Logger("C:\\inetpub\\logs\\", "filacepac");

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
            return db.Autorizacoes.Count(a => a.Usuario.Equals(user)) > 0;
            */
            
            IEnumerable<string> values;
            if (Request.Headers.TryGetValues("Authorization", out values))
            {
                string user = values.FirstOrDefault();
                return db.Autorizacoes.Count(
                    a => a.Usuario.ToUpper().Equals(user.ToUpper())
                    && a.Ativo
                ) > 0;
            }
            Logger.Write("Requisição sem informação do usuário para autorizar.");
            return false;
            
            
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
                if ( (newVal != null && origVal != newVal) && !(newVal is int num && num == 0) )
                {
                    prop.SetValue(origObj, newVal, null);
                }
            }
            return origObj;
        }

    }
}