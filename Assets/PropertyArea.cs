using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YoyouOculusFramework;
using UnityEngine.Events;

namespace OculusFHIR
{
    public class PropertyArea : MonoBehaviour
    {
        public string propertyName{get; private set;}
        public string propertyValue{get; private set;}

        private Text propertyNameT;
        private Text propertyValueT;
        private HandTrackingButton moreDetailsButton;

        private void Awake() 
        {
            propertyNameT = transform.Find("Name").GetComponent<Text>();
            propertyValueT = transform.Find("Value").GetComponent<Text>();
            moreDetailsButton = transform.Find("More Details Button").GetComponent<HandTrackingButton>();
            moreDetailsButton.gameObject.SetActive(true);
        }
        public void setPropertyName(string name)
        {
            propertyNameT.text = name;
        }
        public void setPropertyValue(string value)
        {
            propertyValueT.text = value;
        }
        public void addMoreDetailsButton(UnityAction call)
        {
            // moreDetailsButton.gameObject.SetActive(true);
            moreDetailsButton.OnExitActionZone.AddListener(call);
        }
    }
}
