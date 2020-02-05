using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    private Transform LeftHand;
    private Transform RightHand;

    public Hands Hands;
    public Text LeftHandT;
    public Text RightHandT;


    void Start()
    {
        Hands = Hands.Instance;
    }
    void Update()
    {
        LeftHand = Hands.LeftHand.gameObject.transform;
        RightHand = Hands.RightHand.gameObject.transform;
        LeftHandT.text = LeftHand.position.x + " " + LeftHand.localPosition.y + " " + LeftHand.localPosition.z;
        RightHandT.text = RightHand.localPosition.x + " " + RightHand.localPosition.y + " " + RightHand.localPosition.z;

    }
}
