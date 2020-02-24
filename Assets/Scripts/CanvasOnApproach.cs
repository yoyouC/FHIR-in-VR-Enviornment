using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOnApproach : MonoBehaviour
{
    public void onApproach()
    {
        Image[] images = transform.gameObject.GetComponentsInChildren<Image>();
        for(int i = 0; i < images.Length; i++){
            images[i].color = new Color(100, 100, 100, 0f);
        }
    }
}
