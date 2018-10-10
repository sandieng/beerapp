using AutoMapper;
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
        private ResponseVM _response;
            

        public MemberController(IHostingEnvironment environment, IMemberService memberService)
        {
            _hostingEnvironment = environment;
            _memberService = memberService;
            _response = new ResponseVM();
        }

        // GET api/member
        [HttpGet]
        public ActionResult<IEnumerable<MemberVM>> Get()
        {
            var members = _memberService.GetAll().ToList();
            var results = Mapper.Map<IEnumerable<MemberVM>>(members);
            return Ok(results);
        }

        // GET api/member/atomy@gmail.com
        [HttpGet("{email}")]
        public ActionResult<MemberVM> Get(string email)
        {
            var foundMember = _memberService.GetByEmail(email);
            var result = Mapper.Map<MemberVM>(foundMember);
            if (foundMember != null)
                return Ok(foundMember);
            else
            {
                _response.Message = $"Can't find the member with the email: {email}";
                return NotFound(_response);

            }
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

            return Ok();
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
                return Ok(jwtToken);
            }

            _response.Message = "Invalid email or password";
            return NotFound(_response);
        }

        // PUT api/member/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MemberVM member)
        {
            var updateMember = Mapper.Map<Member>(member);
            _memberService.Update(updateMember);
        }

        [HttpPost("{id}")]
        public void Post(int id, [FromBody] JObject member)
        {
            var updateMember = member.ToObject<Member>();
            var currentMember = _memberService.GetById(updateMember.ID);
            currentMember.FirstName = updateMember.FirstName;
            currentMember.LastName = updateMember.LastName;
            currentMember.DateJoined = updateMember.DateJoined;

            _memberService.Update(currentMember);
        }

        // DELETE api/member/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
