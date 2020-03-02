using System;
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
using System.Collections.Generic;
using UnityEngine.Networking;

namespace FHIRClient
{
    public class Client : MonoBehaviour
    {
        public List<Patient> patients;

        void Awake()
        { 
            List<Patient> patients = new List<Patient>();
            StartCoroutine(GetRequest("http://localhost:5000/api/Patient/"));
            
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
                    parsePatientData(webRequest.downloadHandler.text);
                    // foreach(Patient patient in patients)
                    // {
                    //     Debug.Log(patient.Name[0].Family);
                    // }
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

            FhirJsonParser parser = new FhirJsonParser(new ParserSettings { AcceptUnknownMembers = true});

            foreach(JObject item in patientEntries)
            {
                patients.Add(parser.Parse<Patient>(item.GetValue("resource").ToString()));
            }
        }
    }
}
