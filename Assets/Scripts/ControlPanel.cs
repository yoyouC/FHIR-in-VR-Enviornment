using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ControlPanel : MonoBehaviour
{
    [SerializeField]
    private Canvas startPage;
    [SerializeField]
    private Canvas[] pages;
    private Canvas currentPage;
    // public UnityEvent[] turnPageEvents;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Canvas StartPage = Instantiate(startPage, transform.localPosition, transform.localRotation);
        currentPage = startPage;
        // StartPage.transform.SetParent(transform);
    }

    private void closePage(Canvas page)
    {
        page.enabled = false;
    }

    private void openPage(Canvas page)
    {
        page.enabled = true;
    }

    public void toStartPage()
    {
        closePage(currentPage);
        openPage(startPage);
    }
    public enum Page
    {
        StartPage = 0
    }

    public void toPage(Page page)
    {
        closePage(currentPage);
        currentPage = pages[(int)page];
        openPage(startPage);
    }
}
