using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpriteManager : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> carSpriteList;
    [SerializeField]
    private SpriteRenderer carSpriteRenderer;

    public void UpdateCarSprite(BonusEnum currentCarBonus)
    {
        this.carSpriteRenderer.sprite = this.carSpriteList[(int)currentCarBonus];
    }
}
