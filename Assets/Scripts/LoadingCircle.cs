using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadingCircle : MonoBehaviour
{
    // Start is called before the first frame update
    private Image loadingCircle;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        this.loadingCircle = this.GetComponent<Image>();
        loadingCircle.fillAmount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,-0.3f));
        loadingCircle.fillAmount +=  (1 / (Math.Abs(loadingCircle.fillAmount - 0.5f) + 0.5f)) * 0.005f;
        if(loadingCircle.fillAmount >= 1) loadingCircle.fillAmount = 0;
    }
}
