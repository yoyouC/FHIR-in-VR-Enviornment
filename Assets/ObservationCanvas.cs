using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace OculusFHIR
{
    public class ObservationCanvas : MonoBehaviour
    {
        public List<Observation> observations = new List<Observation>();
        public Client.CallBack callBack;
        private bool observationLoaded;
        public Canvas LoadingPage;
        public Patient patient;
        public ObservationIcon observationIconPrefeb;

        void Start()
        {
            Client.CallBack OnRecieveData = ParseObservationData;
            Client.INSTANCE.GetObservation(patient.id, OnRecieveData);
            StartCoroutine(DisPlayLoadingPageTillDataGet());
        }

        private void ParseObservationData(string json)
        {
            StartCoroutine(ParseObservation(json));
        }

        private IEnumerator ParseObservation(string json)
        {
            JArray observationBundles = JArray.Parse(json);
            yield return null;
            foreach(JObject observationBundle in observationBundles)
            {
                JArray observationtEntries = (JArray)observationBundle.GetValue("entry");

                foreach(JObject item in observationtEntries)
                {
                    Observation observation = JsonConvert.DeserializeObject<Observation>(item.GetValue("resource").ToString());
                    observations.Add(observation);
                    yield return null;
                }
            }
            observationLoaded = true;
        }

        private IEnumerator DisPlayLoadingPageTillDataGet()
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;
            LoadingPage = Instantiate(LoadingPage, transform.position, transform.rotation);

            while(!observationLoaded) yield return null;

            Destroy(LoadingPage.gameObject);
            this.gameObject.GetComponent<Canvas>().enabled = true;
            initializePage();
        }

        private void initializePage()
        {
            Vector2 CanvasTop = new Vector2(0, transform.GetComponent<RectTransform>().sizeDelta.y  / 2);
            Vector2 goDown = new Vector2(0, -observationIconPrefeb.GetComponent<RectTransform>().sizeDelta.y);
            Vector2 StartingPoint = CanvasTop + goDown;
            Vector2 currentIconPosition = StartingPoint;

            foreach(Observation observation in observations)
            {
                ObservationIcon Icon = Instantiate(observationIconPrefeb, transform.position, transform.rotation);
                Icon.GetComponent<RectTransform>().localScale = this.GetComponent<RectTransform>().localScale;
                Icon.transform.parent = transform;
                Icon.GetComponent<RectTransform>().localPosition = currentIconPosition;
                Icon.setDate(observation.effectiveDateTime);
                // Icon.addMoreDetailsButton(delegate {ToPatientBasicInfoPage(patient);});


                if(currentIconPosition.y + goDown.y > -CanvasTop.y)
                {
                    currentIconPosition = new Vector2(0, currentIconPosition.y) + goDown;
                }
                else
                {
                    break;
                }

            }
        }

    }
}
