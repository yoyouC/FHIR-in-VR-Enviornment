using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YoyouOculusFramework;
using UnityEngine.Assertions;

namespace OculusFHIR
{
    public class ObservationDetailsCanvas : MonoBehaviour
    {
        public Observation observation;

        private PropertyArea status;
        private PropertyArea category;
        private PropertyArea time;
        public ObservationCanvas observationCanvas; 
        private HandTrackingButton BackButton; 


        void Awake() {
            status = transform.Find("status").GetComponent<PropertyArea>();
            category = transform.Find("category").GetComponent<PropertyArea>();
            time = transform.Find("time").GetComponent<PropertyArea>();
            BackButton = transform.Find("Back Button").GetComponent<HandTrackingButton>();
        }
        void Start()
        {
            Assert.IsNotNull(observation);
            status.setPropertyName("Status");
            category.setPropertyName("Category");
            time.setPropertyName("Duration");
            BackButton.OnExitActionZone.AddListener(ToObservationPage);

            status.setPropertyValue(observation.status != null ? observation.status : "");
            category.setPropertyValue(observation.code.text != null ? observation.code.text : "");
            time.setPropertyValue(observation.valueQuantity != null ? observation.valueQuantity.value + " " + observation.valueQuantity.unit : "");
        }

        private void ToObservationPage()
        {
            observationCanvas.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
