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
    public class ItemsController : ControllerBase{
        readonly IRepository<Player> repository;
        
        public ItemsController(IRepository<Player> repository){
            this.repository = repository;
        }
        //TODO:Implement:
        [HttpGet("newquest/{id}")]
        public async Task<IActionResult> GetNewQuest(Guid id){
            try{
                var player = await repository.Get(id);
                if (player.Items == null || HasNoQuest(player.Items)){
                    var playerTask = await repository.UpdateItems(id, new Item{CreationDate = DateTime.Now, Gold = 10, Id = player.Id,Xp = 5, ItemId = 4, Name = "EasyQuest", ItemType = "Quest"});
                    var responseMessage = JsonConvert.SerializeObject(playerTask);
                    return Ok(responseMessage);
                }
                return new UnauthorizedResult();
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }
        static bool HasNoQuest(IEnumerable<Item> items){
            foreach (var item in items){
                if (item.ItemType == "Quest"){
                    return false;
                }
            }
            return true;
        }
    }
}