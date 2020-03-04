using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YoyouOculusFramework;
using UnityEngine.Events;

namespace OculusFHIR
{
    public class PatientIcon : MonoBehaviour
    {
        public string Given{get; private set;}
        public string Family{get; private set;}

        private Text GivenT;
        private Text FamilyT;
        private HandTrackingButton basicInfoButton;
        public Vector2 originPos {get;set;}

        private void Awake() 
        {
            GivenT = transform.Find("Given").GetComponent<Text>();
            FamilyT = transform.Find("Family").GetComponent<Text>();
            basicInfoButton = transform.Find("BasicInfo Button").GetComponent<HandTrackingButton>();
            basicInfoButton.gameObject.SetActive(false);
        }
        public void setGivenyName(string name)
        {
            GivenT.text = name;
        }
        public void setFamilyName(string value)
        {
            FamilyT.text = value;
        }
        public void addMoreDetailsButton(UnityAction call)
        {
            basicInfoButton.gameObject.SetActive(true);
            basicInfoButton.OnExitActionZone.AddListener(call);
        }
    }
}
