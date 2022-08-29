using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StarterLightManager : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> spriteList;
    private float elapsedTime = 0f;
    private SpriteRenderer starterLigthSpriteRenderer;
    private int spriteIndex = 0;
    private bool isStartPhaseFinished = false;


    public static event Action OnCountDownTrigger;

    // Start is called before the first frame update
    void Start()
    {
        this.starterLigthSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isStartPhaseFinished)
        {
            this.elapsedTime += Time.deltaTime;

            if (elapsedTime >= 1f)
            {
                this.starterLigthSpriteRenderer.sprite = spriteList[spriteIndex];
                OnCountDownTrigger?.Invoke();
                spriteIndex++;
                if (spriteIndex == this.spriteList.Count)
                {
                    this.isStartPhaseFinished = true;
                }
                elapsedTime = 0f;
            }

        }
    }
}
