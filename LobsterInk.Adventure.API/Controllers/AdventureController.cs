using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LobsterInk.Adventure.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LobsterInk.Adventure.Domain;

namespace LobsterInk.Adventure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdventureController : ControllerBase
    {
        private readonly IAdventureService _adventureService;

        public AdventureController(IAdventureService adventureService)
        {
            _adventureService = adventureService;
        }

        [HttpGet]
        public async Task<ActionResult<Domain.Adventure>> GetAdventure(int id)
        {
            try
            {
                if(id <=0 )
                {
                    return BadRequest();
                }
                var result = await _adventureService.GetAdventureDetails(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("GetPlot")]
        public async Task<ActionResult<Plot>> GetPlot(int plotId)
        {
            try
            {
                if(plotId <= 0)
                {
                    return BadRequest();
                }

                var res = await _adventureService.GetPlot(plotId);
                if(res == null)
                {
                    return NotFound();
                }

                return Ok(res);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("SavePlayerData")]
        public async Task SavePlayerData(string email, int advId, int[] selectedPlotIds)
        {
            await _adventureService.SavePlayerData(email, advId, selectedPlotIds);
        }
    }
}