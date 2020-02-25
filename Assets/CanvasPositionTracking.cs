using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CanvasControl
{
    public class CanvasPositionTracking : MonoBehaviour
    {
        [SerializeField]
        private Transform DefaultTransform;
        void Start()
        {
            transform.localPosition = DefaultTransform.localPosition;
            transform.localRotation = DefaultTransform.localRotation; 
        }

        // Update is called once per frame
        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<CanvasBlocker>() != null)
            {
                transform.localPosition = DefaultTransform.localPosition;
                transform.localRotation = DefaultTransform.localRotation; 
            }
        }

        /// <summary>
        /// OnTriggerStay is called once per frame for every Collider other
        /// that is touching the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnTriggerStay(Collider other)
        {
            if(other.GetComponent<CanvasBlocker>() != null)
            {
                transform.localPosition = DefaultTransform.localPosition;
                transform.localRotation = DefaultTransform.localRotation; 
            }
        }
    }
}