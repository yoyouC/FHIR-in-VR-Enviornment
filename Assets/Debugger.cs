using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    public Transform LeftHand;
    public Transform RightHand;

    public Text LeftHandT;
    public Text RightHandT;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        LeftHandT.text = LeftHand.localPosition.x + " " + LeftHand.localPosition.y + " " + LeftHand.localPosition.z;
        RightHandT.text = RightHand.localPosition.x + " " + RightHand.localPosition.y + " " + RightHand.localPosition.z;
    }
}
