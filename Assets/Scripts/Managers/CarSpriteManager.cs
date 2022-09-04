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

    public void UpdateCarRunningAnimation(BonusEnum currentCarBonus, Animator carAnimator)
    {
        string animationConditionName = null;

        //this.carSpriteRenderer.sprite = this.carSpriteList[(int)currentCarBonus];
        switch (currentCarBonus)
        {
            case BonusEnum.SPEED:
                //Blue
                animationConditionName = "HasBlueBonus";
                break;
            case BonusEnum.DAMAGE_REPAIR:
                //Orange
                animationConditionName = "HasOrangeBonus";
                break;
            case BonusEnum.FUEL_REGENERATION:
                //Red
                animationConditionName = "HasRedBonus";
                break;
            case BonusEnum.MANIABILITY:
                //Green
                animationConditionName = "HasGreenBonus";
                break;
        }

        carAnimator.SetBool(animationConditionName, true);
        carAnimator.SetBool("IsDefaultCar", false);
        this.DeactivateOtherBonusAnimations(animationConditionName, carAnimator);
    }


    private void DeactivateOtherBonusAnimations(string conditionNameToSkip, Animator carAnimator)
    {
        List<string> animationConditionNames = new List<string>() {
            "HasBlueBonus",
            "HasOrangeBonus",
            "HasRedBonus",
            "HasGreenBonus"
        };

        foreach (string animationConditionName in animationConditionNames)
        {
            if(animationConditionName != conditionNameToSkip)
            {
                carAnimator.SetBool(animationConditionName, false);
            }
        }

    }

    public Sprite GetCarCurrentSprite()
    {
        return this.carSpriteRenderer.sprite;
    }
}
