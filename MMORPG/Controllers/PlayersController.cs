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
        
        [HttpGet("myplayer/{id}")]
        public async Task<IActionResult> GetMyPlayer(Guid id){
            try{
                var playerTask = await repository.Get(id);
                var jsonPlayer = JsonConvert.SerializeObject(playerTask);
                return Ok(jsonPlayer);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePlayer([FromBody]NewPlayer player){
            try{
                var newPlayer = new Player{
                    Name = player.Name,
                    CreationTime = DateTime.Now.ToUniversalTime()
                };
                var playerTask = await repository.Create(newPlayer);
                var jsonPlayer = JsonConvert.SerializeObject(playerTask);
                return new OkObjectResult(jsonPlayer);
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException());
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }
        [HttpPost("modify/{id}")]
        public async Task<IActionResult> ModifyPlayer(Guid id, [FromBody]ModifiedPlayer modifiedPlayer){
            try{
                var playerTask = await repository.Modify(id, modifiedPlayer);
                var jsonPlayer = JsonConvert.SerializeObject(playerTask);
                return new OkObjectResult(jsonPlayer);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id){
            try{
                var removedPlayer = await repository.Delete(id);
                removedPlayer.IsDeleted = true;
                var jsonPlayer = JsonConvert.SerializeObject(removedPlayer);
                return new OkObjectResult(jsonPlayer);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
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
    }
}