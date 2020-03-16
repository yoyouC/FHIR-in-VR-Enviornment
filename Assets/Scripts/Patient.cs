using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OculusFHIR
{
    public class Patient
    {
        public string id {get;set;}
        public string birthDate {get;set;}
        public List<ContactPoint> telecom {get; set;}
        public string active {get; set;}
        // List<Identifier> Identifier { get; set;}
        public string gender {get; set;}
        public List<HumanName> name {get;set;}
        public List<Address> address {get; set;}
        public CodeableConcept maritalStatus {get; set;}
        public List<BackboneElement> communication {get; set;}
        public Extention extention {get;set;}
    }
    public class HumanName
    {
        public string family { get; set; }
        public List<string> given {get; set;}
        public List<string> prefix {get; set;}
        public List<string> suffix {get; set;}
        public string use {get;set;}
        public Period period {get;set;}
        public string text {get;set;}
        public Extention extention {get;set;}
    }

    public class Period
    {
        public string start {get;set;}
        public string end {get;set;}
        public Extention extention {get;set;}
    }

    public class Address
    {
        public string use {get;set;}
        public string type {get;set;}
        public string text {get;set;}
        public List<string> line {get;set;}
        public string city {get;set;}
        public string district {get;set;}
        public string state {get;set;}
        public string postalCode {get;set;}
        public string country {get;set;}
        public string period {get;set;}
        public Extention extention {get;set;}
    }

    public class CodeableConcept
    {
        public List<Coding> coding {get;set;}
        public string text {get;set;}
        public Extention extention {get;set;}
    }

    public class Coding
    {
        public string system {get;set;}
        public string version {get;set;}
        public string code {get;set;}
        public string display {get;set;}
        public string userSelected {get;set;}
        public Extention extention {get;set;}
    }
    
    public class ContactPoint
    {
        public string system {get;set;}
        public string value {get;set;}
        public string use {get;set;}
        public int rank {get;set;}
        public Period period {get;set;}
        public Extention extention {get;set;}
    }

    public class BackboneElement
    {
        public List<Extention> modifierExtension {get;set;}
        public Extention extention {get;set;}
    }

    public class Extention
    {
        public string url {get;set;}
        public List<string> value {get;set;}
        public Extention extention {get;set;}
    }

    public class MaritalStatus
    {
        public static string get(string s)
        {
            switch(s)
            {
                case "A":
                    return "Annulled";
                case "D":
                    return "Divorced";
                case "I":
                    return "Interlocutory";
                case "L":
                    return "Legally Separated";
                case "M":
                    return "Married";
                case "P":
                    return "Polygamous";
                case "S":
                    return "Never Married";
                case "T":
                    return "Domestic";
                case "U":
                    return "unmarried";
                case "W":
                    return "Widowed";
                case "UNK":
                    return "unknown";
                default:
                    return "";
            }
        }
    }
}
