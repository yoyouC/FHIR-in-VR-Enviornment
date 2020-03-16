using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gradient : MonoBehaviour
{
    Texture2D backgroundTexture;
    void Awake()
    {
        backgroundTexture  = new Texture2D(2, 1);
        backgroundTexture.wrapMode = TextureWrapMode.Clamp;
        backgroundTexture.filterMode = FilterMode.Bilinear;
        Color a = new Color(255, 102, 204);
        Color b = new Color(255, 102, 153);
        SetColor(a, b);
    }

    // public void SetColor( Color color1, Color color2 )
    // {
    //     backgroundTexture.SetPixels( new Color[] { color1, color2, color1, color2 } );
    //     backgroundTexture.Apply();
    //     transform.GetComponentInChildren<RawImage>().texture = backgroundTexture;
    // }



    public void SetColor( Color color1, Color color2 )
    {
        backgroundTexture.SetPixels( new Color[] { color1, color2 } );
        backgroundTexture.Apply();
        transform.GetComponentInChildren<RawImage>().texture = backgroundTexture;
    }
}
