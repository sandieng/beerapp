﻿using BeerRosterAPI.Services;
using BeerRosterAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BeerRosterAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class EmailController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private IEmailService _emailService;
        private ResponseVM<EmailVM> _response;
            

        public EmailController(IHostingEnvironment environment, IEmailService emailService)
        {
            _hostingEnvironment = environment;
            _emailService = emailService;
            _response = new ResponseVM<EmailVM>();
        }

  
        // POST api/email/send
        [HttpPost]
        [Route("send")]
        public async Task<ActionResult> SendAsync([FromBody] EmailVM email)
        {
            if (!ModelState.IsValid)
            {
                _response.Message = "Please provide all required data for sign up.";
                return BadRequest(_response);
            }

            var result = await _emailService.Send(email);

            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                return Ok(_response);

            return BadRequest(_response);
        }
    }
}
