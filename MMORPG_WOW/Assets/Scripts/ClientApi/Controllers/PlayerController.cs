using UnityEngine;

namespace ClientApi.Controllers{
    public class PlayerController : MonoBehaviour{
        IPlayer player;

        public void SetUp(IPlayer playerData){
            player = playerData;
        }
    }
}