using AuthenticApi.App_Data;
using AuthenticApi.Services.UserService;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.WebApi;

namespace AuthenticApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<AuthenticContext>();
            container.RegisterType<IUserQueryService, UserQueryService>();
            container.RegisterType<IUserCommandService, UserCommandService>();

            // MVC 5
            DependencyResolver.SetResolver(
                new Unity.Mvc5.UnityDependencyResolver(container)
            );

            // Web API 2
            GlobalConfiguration.Configuration.DependencyResolver = 
                new UnityDependencyResolver(container);
        }
    }
}