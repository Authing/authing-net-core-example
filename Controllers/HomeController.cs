using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public string Home()
        {
            var claims = User.Claims;
            var str = "";
            foreach (var item in claims)
            {
                str = str + item.Type + ": " + item.Value + "\n";
            }
            return str;
        }
    }
}
