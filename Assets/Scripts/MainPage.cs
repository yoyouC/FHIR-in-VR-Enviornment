using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UnityEngine.UI;

namespace OculusFHIR
{
    public class MainPage : MonoBehaviour
    {
        public Canvas LoadingPage;
        public List<Patient> patients = new List<Patient>();
        private bool patientsLoaded = false;
        public PatientIcon patientIconPrefeb;
        public PatientBasicInfoCanvas patientBasicInfoCanvasPrefeb;

        private List<PatientIcon> patientIcons = new List<PatientIcon>();
        public Text text;
        
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Start()
        {
            Client.CallBack OnRecieveData = ParsePatientData;
            Client.INSTANCE.GetPatientData(OnRecieveData);
            StartCoroutine(DisPlayLoadingPageTillDataGet());
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>

        private void ParsePatientData(string json)
        {
            StartCoroutine(ParseTenPatients(json));
        }

        private IEnumerator DisPlayLoadingPageTillDataGet()
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;
            LoadingPage = Instantiate(LoadingPage, transform.position, transform.rotation);

            while(!patientsLoaded) yield return null;

            Destroy(LoadingPage.gameObject);
            this.gameObject.GetComponent<Canvas>().enabled = true;
            initializePage();
        }

        private IEnumerator ParseTenPatients(string json)
        {
            JArray patientBundles = JArray.Parse(json);
            foreach(JObject patientBundle in patientBundles)
            {
                JArray patientEntries = (JArray)patientBundle.GetValue("entry");

                foreach(JObject item in patientEntries)
                {
                    Patient patient = JsonConvert.DeserializeObject<Patient>(item.GetValue("resource").ToString());
                    patients.Add(patient);
                    yield return null;
                }
            }
            patientsLoaded = true;
        }

        private void initializePage()
        {
            Vector2 CanvasTopLeft = new Vector2(-(transform.GetComponent<RectTransform>().sizeDelta.x / 2), transform.GetComponent<RectTransform>().sizeDelta.y  / 2);
            Vector2 goDown = new Vector2(0, -patientIconPrefeb.GetComponent<RectTransform>().sizeDelta.y);
            Vector2 goRight = new Vector2(patientIconPrefeb.GetComponent<RectTransform>().sizeDelta.x, 0);
            Vector2 StartingPoint = CanvasTopLeft + goDown/2 + goRight/2;
            Vector2 currentIconPosition = StartingPoint;

            foreach(Patient patient in patients)
            {
                PatientIcon Icon = Instantiate(patientIconPrefeb, transform.position, transform.rotation);
                patientIcons.Add(Icon);
                Icon.GetComponent<RectTransform>().localScale = this.GetComponent<RectTransform>().localScale;
                Icon.transform.parent = transform;
                Icon.originPos = currentIconPosition;
                Icon.GetComponent<RectTransform>().localPosition = currentIconPosition;
                Icon.setFamilyName(patient.name[0].family);
                Icon.setGivenyName(patient.name[0].given[0]);
                Icon.addMoreDetailsButton(delegate {ToPatientBasicInfoPage(patient);});

                if(currentIconPosition.x + goRight.x < -CanvasTopLeft.x)
                {
                    currentIconPosition = currentIconPosition + goRight;
                }
                else if(currentIconPosition.y + goDown.y > -CanvasTopLeft.y)
                {
                    currentIconPosition = new Vector2(CanvasTopLeft.x + goRight.x/2, currentIconPosition.y) + goDown;
                }
                else
                {
                    break;
                }
                ToPatientBasicInfoPage(patients[0]);
            }
        }

        public void ToPatientBasicInfoPage(Patient patient)
        {
            text.text = "here";
            this.gameObject.SetActive(false);
            PatientBasicInfoCanvas patientBasicInfoCanvas;
            patientBasicInfoCanvas = Instantiate(patientBasicInfoCanvasPrefeb, transform.position, transform.rotation);
            patientBasicInfoCanvas.patient = patient;
            patientBasicInfoCanvas.parentPage = this;
            patientBasicInfoCanvas.gameObject.SetActive(true);
        }


    }
}
