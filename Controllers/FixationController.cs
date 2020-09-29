using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

        [Route("api/fixations/add")]
        [HttpPost]
        public IActionResult AddFixation(Fixation fixation)
        {
            _logger.LogInformation("[request] [POST] FIXATION ADD");
            _fixationRepository.Create(fixation);
            return Ok();
        }

        [Route("api/fixations/getFixationBySpeedAndDate")]
        [HttpGet]
        public IActionResult GetFixationBySpeedAndDate([FromHeader]double speed, [FromHeader]DateTime date)
        {
            _logger.LogInformation($"[request] [GET] GETTING FIXATION WHERE SPEED UPPER: {speed} ; DATE: {date}");

            TimeSpan startTime = TimeSpan.Parse(this._configuration.GetValue<string>("StartTime"));
            TimeSpan endTime = TimeSpan.Parse(this._configuration.GetValue<string>("EndTime"));

            if (DateTime.Now.TimeOfDay > startTime && DateTime.Now.TimeOfDay < endTime)
            {
                List<Fixation> fixations = _fixationRepository.GetFixationsByDateAndSpeed(date, speed);
                return Ok(JsonSerializer.Serialize(fixations));
            }
            return StatusCode(500);
        }

        [Route("api/fixations/getMaxMinByDate")]
        [HttpGet]
        public IActionResult GetMaxAndMinSpeedByDate([FromHeader]DateTime date)
        {
            _logger.LogInformation($"[request] [GET] GETTING MAX AND MIN FIXATIONS IN DATE: {date}");

            TimeSpan startTime = TimeSpan.Parse(this._configuration.GetValue<string>("StartTime"));
            TimeSpan endTime = TimeSpan.Parse(this._configuration.GetValue<string>("EndTime"));

            if (DateTime.Now.TimeOfDay > startTime && DateTime.Now.TimeOfDay < endTime)
            {
                List<Fixation> fixations = _fixationRepository.GetMaxAndMinSpeedbyDate(date);
                return Ok(JsonSerializer.Serialize(fixations));
            }
            return StatusCode(500);
        }

    }
}