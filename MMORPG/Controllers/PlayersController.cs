using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Models;
using MMORPG.ServerApi;
using Newtonsoft.Json;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/mmorpg/[controller]")]
    public class PlayersController : ControllerBase{
        readonly IRepository<Player> repository;
        public PlayersController(IRepository<Player> repository){
            this.repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPlayers(){
            try{
                var players = await repository.GetAll();
                var jsonPlayers = JsonConvert.SerializeObject(players);
                return Ok(jsonPlayers);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }
    }
}