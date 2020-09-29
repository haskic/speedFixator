using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpeedFixator.Models;

namespace SpeedFixator.Controllers
{
    [ApiController]
    public class FixationController : ControllerBase
    {
        private readonly ILogger<FixationController> _logger;
        private readonly IFixationRepository _fixationRepository;
        private readonly IConfiguration _configuration;
        public FixationController(IConfiguration configuration, ILogger<FixationController> logger, IFixationRepository fixationRepository)
        {
            this._logger = logger;
            this._fixationRepository = fixationRepository;
            this._configuration = configuration;
        }



    }
}