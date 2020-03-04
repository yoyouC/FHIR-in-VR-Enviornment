using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using YoyouOculusFramework;

namespace OculusFHIR
{
    public class AddressDetailsCanvas : MonoBehaviour
    {
            public Address address;

            private PropertyArea line;
            private PropertyArea city;
            private PropertyArea state;
            private PropertyArea country;

            private HandTrackingButton backButton;
            public Canvas parentCanvas {get;set;}

            void Awake() {
                line = transform.Find("line").GetComponent<PropertyArea>();
                city = transform.Find("city").GetComponent<PropertyArea>();
                state = transform.Find("state").GetComponent<PropertyArea>();
                country = transform.Find("country").GetComponent<PropertyArea>();
                backButton = transform.Find("Back Button").GetComponent<HandTrackingButton>();
            }
            void Start()
            {
                Assert.IsNotNull(address);
                line.setPropertyName("Line");
                city.setPropertyName("City");
                state.setPropertyName("State");
                country.setPropertyName("Country");


                line.setPropertyValue(address.line.Count > 0 ?address.line[0] : "");
                city.setPropertyValue(address.city != null ? address.city : "");
                state.setPropertyValue(address.state != null ? address.state.ToString() : "");
                country.setPropertyValue(address.country != null ? address.country.ToString() : "");

                if(parentCanvas != null)
                {
                    backButton.OnExitActionZone.AddListener(ToParentCanvas);
                }
            }

            /// <summary>
            /// This function is called when the object becomes enabled and active.
            /// </summary>
            public void ToParentCanvas()
            {
                Destroy(this);
                parentCanvas.gameObject.SetActive(true);
            }

    }
}
