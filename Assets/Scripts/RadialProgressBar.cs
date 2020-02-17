using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RadialProgressBar : MonoBehaviour
{
    public Image LoadingBar;
    public Text text;
    // Start is called before the first frame update
    [SerializeField]
    private float increasingRadio;
    private float data;

    // Update is called once per frame
    void Update()
    {
        updateData();
        updateText();
        LoadingBar.fillAmount = data/100f;
    }

    void updateData()
    {
        data += 10 * Time.deltaTime;
        data = data % 100;
    }

    void updateText()
    {
        text.text = ((int)data).ToString();
    }
}
