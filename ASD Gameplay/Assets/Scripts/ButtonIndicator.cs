using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicator : MonoBehaviour
{
    public static ButtonIndicator BI = null;
    private GameObject buttonIndicator;
    private GameObject extra;
    private Text startText;
    private Text buttonText;
    private Text endText;
    private Text extraButtonText;
    private Text extraText;
    private static bool wasVisibleLastFrame;

    private void OnEnable()
    {
        BI = this;
        buttonIndicator = transform.Find("ButtonIndicator").gameObject;
        startText = buttonIndicator.transform.Find("StartText").GetComponent<Text>();
        buttonText = buttonIndicator.transform.Find("Button").transform.Find("ButtonText").GetComponent<Text>();
        endText = buttonIndicator.transform.Find("EndText").GetComponent<Text>();
        extra = buttonIndicator.transform.Find("Extra").gameObject;
        extraButtonText = extra.transform.Find("ExtraButton").transform.Find("Text").GetComponent<Text>();
        extraText = extra.transform.Find("ExtraText").GetComponent<Text>();
    }
    
    private void Update() {
        if (!wasVisibleLastFrame)
            DisplayButtonIndicator(null);
        wasVisibleLastFrame = false;
    }

    public bool isDisplaying()
    {
        return wasVisibleLastFrame;
    }

    public void DisplayButtonIndicator(Indicator indicator)
    {
        buttonIndicator.SetActive(false);
        extra.SetActive(false);
        if (indicator != null)
        {
            startText.text = indicator.StartText;
            buttonText.text = "E";
            endText.text = indicator.EndText;
            buttonIndicator.SetActive(true);
            wasVisibleLastFrame = true;
        }
    }
}