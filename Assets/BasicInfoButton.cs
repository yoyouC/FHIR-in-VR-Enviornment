using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInfoButton : MonoBehaviour
{
    public void increaseSize()
    {
        this.transform.parent.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    public void toOriginSize()
    {
        this.transform.parent.localScale = new Vector3(1f, 1f, 1f);
    }
}
