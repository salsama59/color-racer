using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lapCounterText;
    private int lapCounter = 0;

    private void OnEnable()
    {
        LapCollider.OnLapFinished += UpdateLap;
    }

    private void OnDisable()
    {
        LapCollider.OnLapFinished -= UpdateLap;
    }
    public void UpdateLap()
    {
        this.LapCounter++;
        this.lapCounterText.text = $"Laps: {this.LapCounter}";
    }

    public int LapCounter { get => lapCounter; set => lapCounter = value; }
}
