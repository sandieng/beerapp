using AutoMapper;
using BeerRosterAPI.BusinessLayer;
using BeerRosterAPI.DTOs;
using BeerRosterAPI.Entities;
using BeerRosterAPI.Services;
using BeerRosterAPI.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerRosterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class RosterController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private IRosterService _rosterService;
        private IMemberService _memberService;
        private ResponseVM _response;

        public RosterController(IHostingEnvironment environment, IRosterService rosterService, IMemberService memberService)
        {
            _hostingEnvironment = environment;
            _rosterService = rosterService;
            _memberService = memberService;
            _response = new ResponseVM();
        }

        // GET api/roster
        [HttpGet]
        public ActionResult<IEnumerable<RosterVM>> Get()
        {
            var roster = _rosterService.GetAll().ToList();
            var result = Mapper.Map<List<RosterVM>>(roster);
      
            return Ok(result);
        }

        // GET api/roster/5
        [HttpGet("{id}")]
        public ActionResult<RosterVM> Get(int id)
        {
            return new RosterVM();
        }

        // POST api/roster/create
        [HttpPost]
        [Route("create")]
        public ActionResult Create([FromBody] object cycle)
        {
            var cycleJson = JsonConvert.SerializeObject(cycle);
            dynamic numberOfCycles = JsonConvert.DeserializeObject(cycleJson);

            var members = _memberService.GetAll()
                .Where(x => x.IsActive)
                .OrderBy(x => x.DateJoined)
                .ToList();

            if (!members.Any())
            {
                _response.Message = "No active members found to create a roster!";
                return NotFound(_response);
            }

            var roster = _rosterService.GetAll().ToList();
            var lastRosteredDate = (roster.Any() ? roster.Max(x => x.RosteredDate) : new DateTime());
            var scheduler = new Scheduler();
            var beerRoster = scheduler.CreateRoster(members, lastRosteredDate, int.Parse(numberOfCycles["cycle"].Value.ToString()));
            _rosterService.Save(beerRoster);

            return Ok();
        }

        // POST api/roster/create
        [HttpPost]
        [Route("create/newmembers")]
        public ActionResult CreateNewMembers()
        {
            var members = _memberService.GetAll()
                .Where(x => x.IsActive)
                .OrderBy(x => x.DateJoined)
                .ToList();

            if (!members.Any())
            {
                _response.Message = "No active/new members found to create a roster!";
                return NotFound(_response);
            }

            var roster = _rosterService.GetAll().ToList();

            // Eliminate old members from new members
            var newMembers = _memberService.GetNewMembers(members, roster);

            var lastRosteredDate = (roster.Any() ? roster.Max(x => x.RosteredDate) : new System.DateTime());
            var scheduler = new Scheduler();
            var beerRoster = scheduler.CreateRoster(newMembers, lastRosteredDate);
            _rosterService.Save(beerRoster);

            return Ok();
        }

        // PUT api/roster/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] RosterDto roster)
        {
            // var updateRoster = Mapper.Map<Roster>(roster);
            var updateRoster = new Roster
            {
                ID = roster.ID,
                MemberID = roster.MemberID,
                RosteredDate = roster.RosteredDate
            };

            _rosterService.Update(updateRoster);
        }

        // DELETE api/roster/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deleteRoster = _rosterService.GetById(id);
            _rosterService.Delete(deleteRoster);
        }
    }
}
