using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SupplierWebApi.Framework;
using SupplierWebApi.Models;


namespace SupplierWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IOptions<ConfigMsg> config;

        public WeatherForecastController(IOptions<ConfigMsg> configMsg)
        {
            config = configMsg;
        }

        [HttpGet]
        public string Get()
        {
            return config.Value.SystemId;
        }
    }
}
