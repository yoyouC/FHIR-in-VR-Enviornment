using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YoyouOculusFramework;
using UnityEngine.Events;

namespace OculusFHIR
{
    public class ObservationIcon : MonoBehaviour
    {
        public string Date{get; private set;}

        private Text DateT;
        private HandTrackingButton basicInfoButton;
        public Vector2 originPos {get;set;}

        private void Awake() 
        {
            DateT = transform.Find("Date").GetComponent<Text>();
            basicInfoButton = transform.Find("BasicInfo Button").GetComponent<HandTrackingButton>();
            basicInfoButton.gameObject.SetActive(false);
        }

        public void setDate(string value)
        {
            DateT.text = value;
        }
        
        public void addMoreDetailsButton(UnityAction call)
        {
            basicInfoButton.gameObject.SetActive(true);
            basicInfoButton.OnExitActionZone.AddListener(call);
        }
    }
}
