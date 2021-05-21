using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Models;
using MMORPG.ServerApi;
using MMORPG.Utility;
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
                if (id == Guid.Empty)
                    BadRequest("Needs to contain a player with a name!");
                
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
                if (player == null || player.Name == string.Empty)
                    BadRequest("Needs to contain a player with a name!");
                var newPlayer = new Player{
                    Name = player.Name,
                    CreationTime = DateTime.Now.ToUniversalTime(),
                    Items = new List<Item>()
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
        //TODO:Change to Update.push instead...
        [HttpPost("modify/{id}")]
        public async Task<IActionResult> ModifyPlayer(Guid id, [FromBody]ModifiedPlayer modifiedPlayer){
            try{
                Player player;
                player = await repository.Modify(id, nameof(modifiedPlayer.Xp), modifiedPlayer.Xp);
                player.Xp += modifiedPlayer.Xp;
                player = await repository.Modify(id, nameof(modifiedPlayer.Gold), modifiedPlayer.Gold);
                player.Gold += modifiedPlayer.Gold;
                var jsonPlayer = JsonConvert.SerializeObject(player);
                return new OkObjectResult(jsonPlayer);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id){
            try{
                if (id == Guid.Empty)
                    BadRequest("Needs to contain a player with a name!");
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
            const int maxLenght = 10;
            try{
                var players = await repository.GetAll();
                RemoveId(players);
                var sortedPlayers = players.SortByXp()
                    .Resize(maxLenght)
                    .Reverse();
                
                var jsonPlayers = JsonConvert.SerializeObject(sortedPlayers);
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