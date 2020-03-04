using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OculusFHIR
{
    public class Observation
    {
        public string id {get;set;}
        public Meta meta {get;set;}
        public string status {get;set;}
        public List<CodeableConcept> category {get;set;}
        public CodeableConcept code {get;set;}
        public string effectiveDateTime {get;set;}
        public string issued {get;set;}
        public Quatity valueQuantity {get;set;}
    }

    public class Quatity
    {
        public float value {get;set;}
        public string comparator {get;set;}
        public string unit {get;set;}
        public string system {get;set;}
        public string code {get;set;}
    }

    public class Meta
    {
        public string versionId {get;set;}
        public string lastUpdated {get;set;}
    }
}