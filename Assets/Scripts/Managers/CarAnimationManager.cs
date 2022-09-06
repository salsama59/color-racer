using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimationManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer carSpriteRenderer;
    private string animationConditionName = null;

    private void OnEnable()
    {
        GhostCarSpawnerManager.OnghostSpawned += GetCarCurrentAnimationConditionName;
        GhostCarSpawnerManager.OnCarAnimationChange += ApplyAnimation;
    }

    private void OnDisable()
    {
        GhostCarSpawnerManager.OnghostSpawned -= GetCarCurrentAnimationConditionName;
        GhostCarSpawnerManager.OnCarAnimationChange -= ApplyAnimation;
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

        this.animationConditionName = animationConditionName;
        this.ApplyAnimation(carAnimator, animationConditionName);
    }

    public void ApplyAnimation(Animator carAnimator, string animationConditionName)
    {
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

    public string GetCarCurrentAnimationConditionName()
    {
        return this.animationConditionName;
    }
}
