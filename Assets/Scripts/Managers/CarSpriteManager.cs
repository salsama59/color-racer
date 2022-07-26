using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpriteManager : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> carSpriteList;
    [SerializeField]
    private SpriteRenderer carSpriteRenderer;

    private void OnEnable()
    {
        GhostCarSpawnerManager.OnghostSpawned += GetCarCurrentSprite;
    }

    private void OnDisable()
    {
        GhostCarSpawnerManager.OnghostSpawned -= GetCarCurrentSprite;
    }

    public void UpdateCarSprite(BonusEnum currentCarBonus)
    {
        this.carSpriteRenderer.sprite = this.carSpriteList[(int)currentCarBonus];
    }

    public Sprite GetCarCurrentSprite()
    {
        return this.carSpriteRenderer.sprite;
    }
}
