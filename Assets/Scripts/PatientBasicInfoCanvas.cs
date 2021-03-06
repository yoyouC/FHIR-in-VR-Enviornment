﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hl7.Fhir.Model;
using UnityEngine.Assertions;
using YoyouOculusFramework;

namespace OculusFHIR
{
    public class PatientBasicInfoCanvas : MonoBehaviour
    {
        public Patient patient;
        public Client.CallBack callBack;
        public MainPage parentPage;

        private PropertyArea patientName;
        private PropertyArea birthdate;
        private PropertyArea gender;
        private PropertyArea active;  
        private PropertyArea maritalStatus;
        private PropertyArea address;
        private HandTrackingButton BackButton;
        private HandTrackingButton ObservationButton;

        public AddressDetailsCanvas addressDetailsCanvas_prefeb;
        public NameDetailsCanvas nameDetailsCanvas_prefeb;
        public ObservationCanvas observationCanvas_prefeb;

        void Awake() {
            patientName = transform.Find("name").GetComponent<PropertyArea>();
            birthdate = transform.Find("birthdate").GetComponent<PropertyArea>();
            gender = transform.Find("gender").GetComponent<PropertyArea>();
            active = transform.Find("active").GetComponent<PropertyArea>();
            maritalStatus = transform.Find("marital status").GetComponent<PropertyArea>();
            address = transform.Find("address").GetComponent<PropertyArea>();
            BackButton = transform.Find("Back Button").GetComponent<HandTrackingButton>();
            ObservationButton = transform.Find("Observations").GetComponent<HandTrackingButton>();
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
            maritalStatus.setPropertyValue(patient.maritalStatus.text != null ? patient.maritalStatus.text: "");
            address.setPropertyValue(patient.address[0].city != null ? patient.address[0].city + ", " + patient.address[0].country: "");

            address.addMoreDetailsButton(ToAddressPage);
            patientName.addMoreDetailsButton(ToNamePage);
            BackButton.OnExitActionZone.AddListener(ToMainPage);
            ObservationButton.OnExitActionZone.AddListener(ToObservationPage);

        }

        public void ToAddressPage()
        {
            this.gameObject.SetActive(false);
            // addressDetailsCanvas.gameObject.SetActive(false);
            AddressDetailsCanvas addressDetailsCanvas = Instantiate(addressDetailsCanvas_prefeb, transform.position, transform.rotation);
            addressDetailsCanvas.address = patient.address[0];
            addressDetailsCanvas.parentCanvas = this;
            addressDetailsCanvas.gameObject.SetActive(true);
        }

        public void ToNamePage()
        {
            this.gameObject.SetActive(false);
            // addressDetailsCanvas.gameObject.SetActive(false);
            NameDetailsCanvas nameDetailsCanvas = Instantiate(nameDetailsCanvas_prefeb, transform.position, transform.rotation);
            nameDetailsCanvas.Name = patient.name[0];
            nameDetailsCanvas.parentCanvas = this;
            nameDetailsCanvas.gameObject.SetActive(true);
        }

        public void ToObservationPage()
        {
            this.gameObject.SetActive(false);
            ObservationCanvas observationCanvas = Instantiate(observationCanvas_prefeb, transform.position, transform.rotation);
            observationCanvas.patient = patient;
            observationCanvas.patientBasicInfoCanvas = this;
            observationCanvas.gameObject.SetActive(true);
        }

        public void ToMainPage()
        {
            parentPage.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }

        IEnumerator test()
        {
            yield return new WaitForSeconds(1);
            ToAddressPage();
        }
    }
}
