using System;
using ClientApi.Broker;
using ClientApi.Broker.Messages;
using ClientApi.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace ClientApi.UI{
    public class HudController : MonoBehaviour{
        [SerializeField] Text GoldUi;
        [SerializeField] Text XpUi;
        void Awake(){
            EventBroker.Instance().SubscribeMessage<UpdateHudMessage>(UpdateHud);
        }
        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<UpdateHudMessage>(UpdateHud);

        }

        void UpdateHud(UpdateHudMessage hudMessage){
            GoldUi.text = hudMessage.Gold.ToString("Gold: 0");
            XpUi.text = hudMessage.Xp.ToString("Xp: 0");
        }
    }
}