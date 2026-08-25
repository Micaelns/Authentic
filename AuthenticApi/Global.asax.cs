using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AuthenticApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            // Validação manual do token JWT no cabeçalho Authorization

            var jwtSecret = ConfigurationManager.AppSettings["JwtSecret"];
            var authHeader = Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring(7);
                try
                {
                    // Valide o token usando a biblioteca System.IdentityModel.Tokens.Jwt
                    var handler = new JwtSecurityTokenHandler();
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    var principal = handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                    HttpContext.Current.User = principal;
                }
                catch
                {
                    // Token inválido
                }
            }
        }
    }
}
