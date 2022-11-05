using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceStartManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI starterText;
    private int count = 3;


    private void OnEnable()
    {
        StarterLightManager.OnCountDownTrigger += StartCountDown;
    }

    private void OnDisable()
    {
        StarterLightManager.OnCountDownTrigger -= StartCountDown;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.isRacePreparationDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartCountDown()
    {
        starterText.gameObject.SetActive(true);
        if(count > 0)
        {
            starterText.text = count.ToString();
            count--;
        } else
        {
            starterText.text = "GO!!";
            count = 3;
            GameManager.isRacePreparationDone = true;
            StartCoroutine(this.HideCountdown());
        }
        
    }

    private IEnumerator HideCountdown()
    {
        yield return new WaitForSeconds(1f);
        this.starterText.gameObject.SetActive(false);
    }
}
