using System;
using System.Collections.Generic;
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
        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard(){
            try{
                var players = await repository.GetAll();
                RemoveId(players);
                var jsonPlayers = JsonConvert.SerializeObject(players);
                return Ok(jsonPlayers);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }

        static void RemoveId(IEnumerable<Player> players){
            foreach (var player in players){
                player.Id = Guid.Empty;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer(Guid id){
            try{
                var playerTask = await repository.Get(id);
                var jsonPlayer = JsonConvert.SerializeObject(playerTask);
                return Ok(jsonPlayer);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }
        [HttpPost("new/{player}")]
        public async Task<IActionResult> CreatePlayer(Player player){
            try{
                var playerTask = await repository.Create(player);
                var jsonPlayer = JsonConvert.SerializeObject(playerTask);
                return Ok(jsonPlayer);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }
        [HttpPost("{id}/{modifiedPlayer}")]
        public async Task<IActionResult> ModifyPlayer(Guid id, ModifiedPlayer modifiedPlayer){
            try{
                var playerTask = await repository.Modify(id, modifiedPlayer);
                var jsonPlayer = JsonConvert.SerializeObject(playerTask);
                return Ok(jsonPlayer);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id){
            try{
                var playerTask = await repository.Delete(id);
                var jsonPlayer = JsonConvert.SerializeObject(playerTask);
                return Ok(jsonPlayer);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }
    }
}