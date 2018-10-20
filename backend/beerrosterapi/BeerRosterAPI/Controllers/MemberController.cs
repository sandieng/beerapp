﻿using AutoMapper;
using BeerRosterAPI.Entities;
using BeerRosterAPI.Services;
using BeerRosterAPI.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace BeerRosterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class MemberController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private IMemberService _memberService;
        private ResponseVM<MemberVM> _response;
            

        public MemberController(IHostingEnvironment environment, IMemberService memberService)
        {
            _hostingEnvironment = environment;
            _memberService = memberService;
            _response = new ResponseVM<MemberVM>();
        }

        // GET api/member
        [HttpGet]
        //public ActionResult Get() OR BE SPECIFIC With return type
        public ActionResult<ResponseVM<MemberVM>> Get()
        {
            var members = _memberService.GetAll().ToList();
            var results = Mapper.Map<List<MemberVM>>(members);
            // var jwtToken = JwtService.UpdateJwt(Request.Headers["Authorization"]);
            var jwtToken = JwtService.GetUpdatedJwt();
            _response.Token = jwtToken;
            _response.Payload = results;
            return Ok(_response);
        }

        // GET api/member/atomy@gmail.com
        [HttpGet("{email}")]
        //public ActionResult Get(string email) OR BE SPECIFIC with return type
        public ActionResult<ResponseVM<MemberVM>> Get(string email)
        {
            var foundMember = _memberService.GetByEmail(email);
            var result = Mapper.Map<MemberVM>(foundMember);
            if (foundMember != null)
            {
                // var jwtToken = JwtService.UpdateJwt(Request.Headers["Authorization"]);

                //result.Token = jwtToken.ToString();
                var jwtToken = JwtService.GetUpdatedJwt();
                _response.Payload = new List<MemberVM> { result };
                _response.Token = jwtToken.ToString();
                return Ok(_response);
            }

            _response.Message = $"Can't find the member with the email: {email}";
            return NotFound(_response);
        }

        // POST api/member
        [HttpPost]
        [Route("signup")]
        public ActionResult Signup([FromBody] MemberVM member)
        {
            if (!ModelState.IsValid)
            {
                _response.Message = "Please provide all required data for sign up.";
                return BadRequest(_response);
            }

            var foundMember = _memberService.GetByEmail(member.Email);

            if (foundMember != null)
            {
                _response.Message = "User has already signed up.";
                return BadRequest(_response);
            }

            var newMember = Mapper.Map<Member>(member);
            _memberService.Save(newMember);

            var jwtToken = JwtService.GenerateJwt(member.Email);
            _response.Token = jwtToken;

            return Ok(_response);
        }

        // POST api/member
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] LoginVM member)
        {
            var found = _memberService.GetByEmail(member.Email);

            if (found != null && found.Password == member.Password)
            {
                var jwtToken = JwtService.GenerateJwt(member.Email);
                _response.Token = jwtToken;
                return Ok(_response);
            }

            _response.Message = "Invalid email or password";
            return NotFound(_response);
        }

        // PUT api/member/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] MemberVM member)
        {
            var updateMember = Mapper.Map<Member>(member);
            _memberService.Update(updateMember);

            //var jwtToken = JwtService.UpdateJwt(Request.Headers["Authorization"]);
            var jwtToken = JwtService.GetUpdatedJwt();
            _response.Token = jwtToken;
            return Ok(_response);
        }

        [HttpPost("{id}")]
        public ActionResult Post(int id, [FromBody] JObject member)
        {
            var updateMember = member.ToObject<Member>();
            var currentMember = _memberService.GetById(updateMember.ID);
            currentMember.FirstName = updateMember.FirstName;
            currentMember.LastName = updateMember.LastName;
            currentMember.DateJoined = updateMember.DateJoined;
            currentMember.IsActive = updateMember.IsActive;
            currentMember.Email = updateMember.Email;

            _memberService.Update(currentMember);

            //var jwtToken = JwtService.UpdateJwt(Request.Headers["Authorization"]);
            var jwtToken = JwtService.GetUpdatedJwt();
            _response.Token = jwtToken;
            return Ok(_response);
        }

        // DELETE api/member/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
