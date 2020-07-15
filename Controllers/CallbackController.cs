using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AuthingNetCoreExample.Controllers
{
    [Route("api/callback")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        public CallbackController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpGet]
        public async Task<string> Callback()
        {
            var code = Request.Query["code"];
            using var client = new HttpClient();

            var param = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "client_id", Configuration["Authing:OidcClientId"] },
                { "client_secret", Configuration["Authing:OidcClientSecret"] },
                { "redirect_uri", Configuration["Authing:OidcRedirectUrl"] },
                { "code", code }
            };

            // 使用 client_secret_post 方式换取 token
            using var res = await client.PostAsync($"https://{Configuration["Authing:Domain"]}.authing.cn/oauth/oidc/token", new FormUrlEncodedContent(param));
            return await res.Content.ReadAsStringAsync();
        }
    }
}
