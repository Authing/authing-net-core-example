using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AuthingNetCoreExample.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpGet]
        public void Home()
        {
            string clientId = Configuration["Authing:OidcClientId"];
            string redirectUrl = Configuration["Authing:OidcRedirectUrl"];
            Response.Redirect($"https://authing-net-sdk-demo.authing.cn/oauth/oidc/auth?client_id={clientId}&redirect_uri={redirectUrl}&scope=openid profile&response_type=code&state=jacket");
        }
    }
}
