﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Data;
using MyFootballRestApi.Models;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {

        private readonly IRepository<League> _leagueRepository;

        public LeagueController()
        {
            _leagueRepository = new CouchbaseRepository<League>();
        }

        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var leagues = await _leagueRepository.GetAll(typeof(League));
                return Ok(leagues);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeagueById([FromRoute]string id)
        {
            try
            {
                var league = await _leagueRepository.Get(id);
                return Ok(league);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region POST

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] League league)
        {
            try
            {
                var result = await _leagueRepository.Create(league);
                if (result == null) { return BadRequest(result); }
                return Created(string.Format("/api/Leagues/{0}",league.Id),result);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] League league)
        {
            try
            {
                var result = await _leagueRepository.Upsert(league);
                if (result == null) { return BadRequest(); }
                return Created(string.Format("/api/Leagues/{0}", league.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]League league)
        {
            try
            {
                var result = await _leagueRepository.Update(league);
                if (result == null) { return BadRequest(result); }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region DELETE

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            try
            {
                await _leagueRepository.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

    }
}