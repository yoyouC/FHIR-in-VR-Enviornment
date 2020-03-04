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


        void Awake() {
            status = transform.Find("status").GetComponent<PropertyArea>();
            category = transform.Find("category").GetComponent<PropertyArea>();
            time = transform.Find("time").GetComponent<PropertyArea>();
        }
        void Start()
        {
            Assert.IsNotNull(observation);
            status.setPropertyName("Name");
            category.setPropertyName("category");
            time.setPropertyName("time");

            status.setPropertyValue(observation.status != null ? observation.status : "");
            category.setPropertyValue(observation.code.text != null ? observation.code.text : "");
            time.setPropertyValue(observation.valueQuantity != null ? observation.valueQuantity.value + " " + observation.valueQuantity.unit : "");
        }
    }
}
