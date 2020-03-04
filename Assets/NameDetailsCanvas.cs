using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using YoyouOculusFramework;

namespace OculusFHIR
{
    public class NameDetailsCanvas : MonoBehaviour
    {
            public HumanName Name;

            private PropertyArea given;
            private PropertyArea family;
            private PropertyArea suffix;
            private PropertyArea prefix;

            private HandTrackingButton backButton;
            public Canvas parentCanvas {get;set;}

            void Awake() {
                given = transform.Find("given").GetComponent<PropertyArea>();
                family = transform.Find("family").GetComponent<PropertyArea>();
                suffix = transform.Find("suffix").GetComponent<PropertyArea>();
                prefix = transform.Find("prefix").GetComponent<PropertyArea>();
                backButton = transform.Find("Back Button").GetComponent<HandTrackingButton>();
            }
            void Start()
            {
                Assert.IsNotNull(Name);
                given.setPropertyName("given");
                family.setPropertyName("family");
                suffix.setPropertyName("State");
                prefix.setPropertyName("prefix");


                given.setPropertyValue(Name.given.Count > 0 ?Name.given[0] : "");
                family.setPropertyValue(Name.family != null ? Name.family : "");
                suffix.setPropertyValue(Name.suffix != null ? Name.suffix.ToString() : "");
                prefix.setPropertyValue(Name.prefix != null ? Name.prefix.ToString() : "");

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
