using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Bson;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace OculusFHIR
{
    public class Client : MonoBehaviour
    {
        public Text[] texts;
        public PatientBasicInfoCanvas patientBasicInfoCanvas;
        public List<Patient> patients = new List<Patient>();

        void Awake()
        { 
            List<Patient> patients = new List<Patient>();
            StartCoroutine(GetRequest("https://178.62.0.181:5001/api/Patient/"));
            texts[0].text = "here";
            // Hl7.Fhir.Model.Patient
        }

        System.Collections.IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.certificateHandler = new CertHandler();
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    texts[1].text = "here";
                    try{
                        parsePatientData(webRequest.downloadHandler.text);
                    }catch(Exception e){
                        texts[2].text = e.ToString();
                    };
                    // foreach(Patient patient in patients)
                    // {
                    //     Debug.Log(patient.Name[0].Family);
                    // }
                    
                    patientBasicInfoCanvas.gameObject.SetActive(false);
                    patientBasicInfoCanvas = Instantiate(patientBasicInfoCanvas);
                    patientBasicInfoCanvas.patient = patients[0];
                    patientBasicInfoCanvas.gameObject.SetActive(true);
                }
            }
        }
        public class CertHandler : CertificateHandler
        {
            protected override bool ValidateCertificate(byte[] certificateData)
            {
                return true;
            }
        }

        private void parsePatientData(string jsonString)
        {
            JObject patientBundle = (JObject)JArray.Parse(jsonString).First;
            JArray patientEntries = (JArray)patientBundle.GetValue("entry");

            foreach(JObject item in patientEntries)
            {
                Patient patient = JsonConvert.DeserializeObject<Patient>(item.GetValue("resource").ToString());
                patients.Add(patient);
                Debug.Log(patient.name[0].suffix);
            }
        }
    }
}
