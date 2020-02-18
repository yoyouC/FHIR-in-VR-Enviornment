using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OculusSampleFramework;
using UnityEngine.UI;

public class GestureListener : MonoBehaviour
{
    public Text text;
    public Hands Hands;
    private Hand LeftHand;
    private Hand RightHand;
    private Transform LeftHandT;
    private Transform RightHandT;

    private Queue<HandState> LeftHandStates;
    private Queue<HandState> RightHandStates;

    [SerializeField]
    private int QueueMaxLength;
    private float[] AveragePinchStrengthLeft;
    private float[] AveragePinchStrengthRight;

    public UnityEvent SlapRight;
    public UnityEvent OKgesture;
    private GestureEvent SlapRightG;

    void Start()
    {
        Hands = Hands.Instance;
        LeftHand = Hands.LeftHand;
        RightHand = Hands.RightHand;
        LeftHandT = LeftHand.gameObject.transform;
        RightHandT = RightHand.gameObject.transform;
        LeftHandStates = new Queue<HandState>();
        RightHandStates = new Queue<HandState>();
        AveragePinchStrengthLeft = AveragePinchStrengthRight = new float[5] {0f, 0f, 0f, 0f, 0f};
        SlapRightG = new GestureEvent(SlapRight);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHandsStates();
        CalculateAveragePinch();
        InvokeGestureEvents();
        // text.text = RightHandStates.Peek().fingerPinchStrengths[1].ToString();
    }

    private void UpdateHandsStates()
    {
        if(LeftHandStates.Count > QueueMaxLength)
        {
            LeftHandStates.Dequeue();
        }
        LeftHandStates.Enqueue(new HandState(LeftHandT.localPosition, LeftHandT.localRotation, LeftHand.State.PinchStrength));

        if(RightHandStates.Count > QueueMaxLength)
        {
            RightHandStates.Dequeue();
        }
        RightHandStates.Enqueue(new HandState(RightHandT.localPosition, RightHandT.localRotation, RightHand.State.PinchStrength));
    }

    private void CalculateAveragePinch()
    {
        AveragePinchStrengthLeft = AveragePinchStrengthRight = new float[5] {0f, 0f, 0f, 0f, 0f};
        HandState[] rightHandStates = RightHandStates.ToArray();
        int counter = 0;
        for (int i = 0; i < rightHandStates.Length; i += 5)
        {
            if(rightHandStates[i].fingerPinchStrengths != null){
                for (int j = 0; j < rightHandStates[i].fingerPinchStrengths.Length; j++)
                {
                    AveragePinchStrengthRight[j] = AveragePinchStrengthRight[j] + rightHandStates[i].fingerPinchStrengths[j];
                }
                counter++;
            }
        }
        if (counter > 0){
            for (int i = 0; i < 5; i++)
            {
                AveragePinchStrengthRight[i] = AveragePinchStrengthRight[i] / counter;
            }
        }

        // HandState[] leftHandStates = LeftHandStates.ToArray();
        // counter = 0;
        // for (int i = 0; i < leftHandStates.Length; i = i + 5)
        // {
        //     if(leftHandStates[i].fingerPinchStrengths != null){
        //         for (int j = 0; j < leftHandStates[i].fingerPinchStrengths.Length; j++)
        //         {
        //             AveragePinchStrengthLeft[j] = AveragePinchStrengthLeft[j] + leftHandStates[i].fingerPinchStrengths[j];
        //         }
        //         counter++;
        //     }
        // }
        // if(counter > 0)
        // {
        //     for (int i = 0; i < 5; i++)
        //     {
        //         AveragePinchStrengthLeft[i] = AveragePinchStrengthLeft[i] / counter;
        //     }
        // }
    }

    private void InvokeGestureEvents()
    {
        InvokeSlapRight();
        InvokeOKgesture();
    }

    private void InvokeSlapRight(){
        if (SlapRight != null){
            HandState[] rightHandStates = RightHandStates.ToArray();
            if (AveragePinchStrengthRight[1] == 0){
                float angleDiff = rightHandStates[rightHandStates.Length - 1].rotation.eulerAngles.y - rightHandStates[0].rotation.eulerAngles.y;
                // text.text = angleDiff.ToString();
                if (angleDiff > 180){
                    angleDiff = 360 - angleDiff;
                    if(angleDiff > 100){
                        SlapRightG.Invoke();
                    }
                }
            }
        }
    }

    private void InvokeOKgesture(){
        HandState[] rightHandStates = RightHandStates.ToArray();
        text.text = AveragePinchStrengthRight[2].ToString();
        if (AveragePinchStrengthRight[1] == 1 && AveragePinchStrengthRight[2] == 0){
            OKgesture.Invoke();
        }
    }

    public class HandState{
        public readonly Vector3 position;
        public readonly Quaternion rotation;
        public readonly float[] fingerPinchStrengths;

        public HandState(Vector3 position, Quaternion rotation, float[] fingerPinchStrengths)
        {
            this.position = position;
            this.rotation = rotation;
            this.fingerPinchStrengths = fingerPinchStrengths;
        }
    }

    public class GestureEvent{
        private UnityEvent Event;
        private long timer;

        public GestureEvent(UnityEvent Event){
            this.Event = Event;
            timer = 0;
        }

        public void Invoke(){
           long TimeElapsed = DateTimeOffset.Now.ToUnixTimeMilliseconds() - timer;
           if (TimeElapsed > 1000){
               Event.Invoke();
               timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
           }
        }

    }
}
