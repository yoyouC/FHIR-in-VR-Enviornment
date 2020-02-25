using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class HandTrackingGrabber : OVRGrabber
{
    private Hand hand;
    private float pinchThreshold = 0.1f;
    protected override void Start()
    {
        base.Start();
        hand = GetComponent<Hand>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckIndexPinch();
    }

    void CheckIndexPinch()
    {
        float pinchStrength = hand.PinchStrength(OVRPlugin.HandFinger.Index);
        bool isPinching = pinchStrength > pinchThreshold;

        if(!m_grabbedObj && isPinching && m_grabCandidates.Count > 0)
        {
            GrabBegin();
        }
        else if(m_grabbedObj && !isPinching)
        {
            GrabEnd();
        }
    }
}
