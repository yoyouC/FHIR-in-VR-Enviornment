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
        public delegate void CallBack(String data);

        public static Client INSTANCE;

        void Awake()
        { 
            INSTANCE = this;
        }

        System.Collections.IEnumerator GetRequest(string uri, CallBack callBack)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.certificateHandler = new CertHandler();
                yield return webRequest.SendWebRequest();
                Debug.Log("here");
                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    // try{
                    //     parsePatientData(webRequest.downloadHandler.text);
                    // }catch(Exception e){
                    //     texts[2].text = e.ToString();
                    // };
                    // Debug.Log(webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    callBack(webRequest.downloadHandler.text);
                    // patientBasicInfoCanvas = Instantiate(patientBasicInfoCanvas, transform.position, transform.rotation);
                    // patientBasicInfoCanvas.patient = patients[0];
                    // patientBasicInfoCanvas.gameObject.SetActive(true);
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

        public void GetObservation(string patientID, CallBack callBack)
        {
            string url = "https://178.62.0.181:5001/api/Observation/" + patientID;
            Debug.Log(url);
            StartCoroutine(GetRequest(url, callBack));
        }

        public void GetPatientData(CallBack callBack)
        {
            string url = "https://178.62.0.181:5001/api/Patient/";
            StartCoroutine(GetRequest(url, callBack));
        }

        
    }
}
