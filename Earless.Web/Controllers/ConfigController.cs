using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Earless.Web.DTO;

namespace Facturatie.Web.Controllers
{
    [Route("api/Config")]
    public class ConfigController : Controller
    {
        private readonly IConfiguration config;

        public ConfigController(IConfiguration configuration)
        {
            config = configuration;
        }

        [HttpGet]
        public ConfigDto Index()
        {
            ConfigDto configDto = new ConfigDto { ApiServerUrl = config["ApiServer:SchemeAndHost"]};

            return configDto;
        }
    }
}
