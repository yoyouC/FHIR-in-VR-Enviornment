using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;

public class VRRig : MonoBehaviour
{
    public Transform headConstraint;
    public Vector3 headBodyOffset;
    public VRMAp head;
    public VRMAp rightHandM;
    public HandFingerRotation rightHandFingerRotation;
    public Hands Hands;
    public Text text;



    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;
        head.Map();
        rightHandM.Map();
        // Hand_Index1.localRotation = Hands.RightHand.State.BoneRotations[(int)OVRPlugin.BoneId.Hand_Index1].FromFlippedZQuatf();
        rightHandFingerRotation.Map(Hands.RightHand.State);
    }

    [System.Serializable]
    public class VRMAp
    {
        public Transform vrTarget;
        public Transform rigTarget;
        public Vector3 trackingPositionOffset;
        public Vector3 trackingRotationOffset;

        public void Map()
        {
            rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
            rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
        }
    }
    
    [System.Serializable]
    public class HandFingerRotation
    {
        // public Transform Hand_Thumb0;
		// public Transform Hand_Thumb1;           
		public Transform Hand_Thumb2;      
		public Transform Hand_Thumb3;
		public Transform Hand_Index1;
		public Transform Hand_Index2;
		public Transform Hand_Index3;
		public Transform Hand_Middle1;
		public Transform Hand_Middle2;
		public Transform Hand_Middle3;
		public Transform Hand_Ring1;
		public Transform Hand_Ring2;
		public Transform Hand_Ring3;
		// public Transform Hand_Pinky0;
		public Transform Hand_Pinky1;
		public Transform Hand_Pinky2;
		public Transform Hand_Pinky3;
		// Hand_MaxSkinnable       = Hand_Start + 19,
		// // Bone tips are position only. They are not used for skinning but are useful for hit-testing.
		// // NOTE: Hand_ThumbTip == Hand_MaxSkinnable since the extended tips need to be contiguous
		// Hand_ThumbTip           = Hand_Start + Hand_MaxSkinnable + 0, // tip of the thumb
		// Hand_IndexTip           = Hand_Start + Hand_MaxSkinnable + 1, // tip of the index finger
		// Hand_MiddleTip          = Hand_Start + Hand_MaxSkinnable + 2, // tip of the middle finger
		// Hand_RingTip            = Hand_Start + Hand_MaxSkinnable + 3, // tip of the ring finger
		// Hand_PinkyTip           = Hand_Start + Hand_MaxSkinnable + 4, // tip of the pinky
        public Vector3 FingerTrackingRotationOffset;

        public void Map(OVRPlugin.HandState handState)
        {
            if (handState.BoneRotations != null)
            {
                // Hand_Thumb0.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Index1].FromFlippedZQuatf();
                // Hand_Thumb1.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Thumb1].FromFlip1pedZQuatf();
                Hand_Thumb2.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Thumb1].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Thumb3.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Thumb2].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Index1.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Index1].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Index2.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Index2].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Index3.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Index3].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Middle1.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Middle1].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Middle2.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Middle2].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Middle3.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Middle3].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Ring1.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Ring1].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Ring2.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Ring2].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Ring3.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Ring3].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                // Hand_Pinky0.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Index1].FromFlippedZQuatf();
                Hand_Pinky1.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Pinky1].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Pinky2.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Pinky2].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
                Hand_Pinky3.localRotation = handState.BoneRotations[(int)OVRPlugin.BoneId.Hand_Pinky3].FromQuatf() * Quaternion.Euler(FingerTrackingRotationOffset);
            }
        }

    }
}
