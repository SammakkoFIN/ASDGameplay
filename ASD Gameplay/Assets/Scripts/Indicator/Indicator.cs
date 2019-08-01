using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Indicator")]
public class Indicator : ScriptableObject
{
    [SerializeField] private string startText;
    public string StartText { get => startText; set => startText = value; }

    [SerializeField] private Input inputKey;
    public Input InputKey { get => inputKey; set => inputKey = value; }

    [SerializeField] private string endText;
    public string EndText { get => endText; set => endText = value; }

    [SerializeField] private string extraText;
    public string ExtraText { get => extraText; set => extraText = value; }
}