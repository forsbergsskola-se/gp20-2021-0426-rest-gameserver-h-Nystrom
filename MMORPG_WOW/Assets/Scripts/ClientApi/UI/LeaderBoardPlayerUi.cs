using UnityEngine;
using UnityEngine.UI;

namespace ClientApi.UI{
    public class LeaderBoardPlayerUi : MonoBehaviour{
        [SerializeField] Text playerInfoText;

        public void SetUp(int placement, IPlayer player){
            playerInfoText.text = $"{placement}. Name: {player.Name}: XP: {player.Xp}, Gold: {player.Gold}";
        }
        public void Destroy(){
            Destroy(gameObject);
        }
    }
}
