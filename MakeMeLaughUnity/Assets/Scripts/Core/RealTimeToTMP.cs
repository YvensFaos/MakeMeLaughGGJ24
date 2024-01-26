using System;
using TMPro;
using UnityEngine;

public class RealTimeToTMP : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timeText;
    
    private void Update()
    {
        timeText.text = DateTime.Now.ToString("HH:mm");
    }
}
