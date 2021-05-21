using ClientApi.Broker;
using ClientApi.Models;
using UnityEngine;

namespace ClientApi.Controllers{
    public class EnemyDummyController : MonoBehaviour{
        
        public void GetKillReword(int buttonId){
            var gold = 0;
            var xp = 0;
            switch (buttonId){
                case 1:
                    gold = 10;
                    break;
                case 2:
                    xp = 10;
                    break;
                case 3:
                    xp = 5;
                    gold = 20;
                    break;
                default:
                    xp = 5;
                    gold = 5;
                    break;
            }
            EventBroker.Instance().SendMessage(new ModifiedPlayer{Gold = gold, Xp = xp});
        }
    }
}
