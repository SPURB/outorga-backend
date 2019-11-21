using FilaCepac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FilaCepac.Authorize
{
    public class AlteracaoAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (!filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new HttpException();
            }
            string usuario = filterContext.RequestContext.HttpContext.User.Identity.Name;
            usuario = GetLoginUsuarioDominio(usuario);
            if (!Autorizado(usuario))
            {
                throw new HttpException();
            }



        }

        private bool Autorizado(string usuario)
        {
            //FilaCepacContext db = new FilaCepacContext();

            return usuario.Equals("E059000");
            

        }

        private string GetLoginUsuarioDominio(string usuario)
        {
            //Remove o nome do domínio, se houver
            char domainSeparator = '\\';
            int separatorIndex = usuario.IndexOf(domainSeparator);
            return (separatorIndex >= 0) ? usuario.Substring(separatorIndex + 1) : usuario;
        }

    }
}