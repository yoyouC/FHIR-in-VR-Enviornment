using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hl7.Fhir.Model;
using UnityEngine.Assertions;

namespace OculusFHIR
{
    public class PatientBasicInfoCanvas : MonoBehaviour
    {
        public Patient patient;

        private PropertyArea patientName;
        private PropertyArea birthdate;
        private PropertyArea gender;
        private PropertyArea active;  
        private PropertyArea maritalStatus;
        private PropertyArea address;

        public AddressDetailsCanvas addressDetailsCanvas;

        void Awake() {
            patientName = transform.Find("name").GetComponent<PropertyArea>();
            birthdate = transform.Find("birthdate").GetComponent<PropertyArea>();
            gender = transform.Find("gender").GetComponent<PropertyArea>();
            active = transform.Find("active").GetComponent<PropertyArea>();
            maritalStatus = transform.Find("marital status").GetComponent<PropertyArea>();
            address = transform.Find("address").GetComponent<PropertyArea>();
        }
        void Start()
        {
            Assert.IsNotNull(patient);
            patientName.setPropertyName("Name");
            birthdate.setPropertyName("Birthdate");
            gender.setPropertyName("Gender");
            active.setPropertyName("Active");
            maritalStatus.setPropertyName("maritalStatus");
            address.setPropertyName("address");

            address.addMoreDetailsButton(ToAddressPage);
            patientName.setPropertyValue(patient.name.Count > 0 ? patient.name[0].given[0] + " " + patient.name[0].family : "");
            birthdate.setPropertyValue(patient.birthDate != null ? patient.birthDate : "");
            gender.setPropertyValue(patient.gender != null ? patient.gender.ToString() : "");
            active.setPropertyValue(patient.active != null ? patient.active.ToString() : "");
            maritalStatus.setPropertyValue(patient.maritalStatus != null ? patient.maritalStatus.ToString(): "");

            ToAddressPage();
            
        }

        void ToAddressPage()
        {
            this.gameObject.SetActive(false);
            addressDetailsCanvas.gameObject.SetActive(false);
            addressDetailsCanvas = Instantiate(addressDetailsCanvas, transform.position, transform.rotation);
            addressDetailsCanvas.address = patient.address[0];
            addressDetailsCanvas.gameObject.SetActive(true);
            Debug.Log(addressDetailsCanvas.gameObject.activeSelf);
        }
    }
}
