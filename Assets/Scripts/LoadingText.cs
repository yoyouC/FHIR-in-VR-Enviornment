using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    
    private Text loadingText;
    private bool corotineStarted = false;
    void Awake()
    {
        this.loadingText = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!corotineStarted)
        {
            StartCoroutine(changeText());
        }
    }

    IEnumerator changeText()
    {
        corotineStarted = true;
        for(int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(1);
            loadingText.text += ".";
        }
        loadingText.text = "Loading";
        corotineStarted = false;
    }
}
