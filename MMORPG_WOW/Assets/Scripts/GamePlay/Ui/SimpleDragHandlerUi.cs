using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay.Ui{
    [RequireComponent(typeof(RectTransform))]
    public class SimpleDragHandlerUi : MonoBehaviour, IDragHandler{
        RectTransform dragRectTransform;
        void Start(){
            dragRectTransform = GetComponent<RectTransform>();
        }
        public void OnDrag(PointerEventData eventData){
            dragRectTransform.anchoredPosition += eventData.delta;
        }
    }
}
