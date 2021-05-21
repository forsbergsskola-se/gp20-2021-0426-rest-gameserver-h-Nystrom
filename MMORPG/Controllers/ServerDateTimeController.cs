using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.ServerApi;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/mmorpg/[controller]")]
    public class ServerDateTimeController : ControllerBase{
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent(){
            var run = await Task.Run(() => {
                try{
                    var dateTime = DateTime.Now.ToUniversalTime();
                    return Ok(dateTime);
                }
                catch (Exception e){
                    return e.GetBaseException().GetHttpCodeStatus();
                }
            });
            return run;
        }
    }
}