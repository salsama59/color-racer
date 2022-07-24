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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLap()
    {
        this.lapCounter++;
        this.lapCounterText.text = $"Laps: {this.lapCounter}";
    }
}
