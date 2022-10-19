using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameMenuGameObject;
    [SerializeField]
    private TextMeshProUGUI maximumSpeedText;
    [SerializeField]
    private TextMeshProUGUI fuelText;
    [SerializeField]
    private TextMeshProUGUI driftText;
    [SerializeField]
    private TextMeshProUGUI damageText;

    [SerializeField]
    private TextMeshProUGUI driverNameText;
    [SerializeField]
    private TextMeshProUGUI driverExperienceText;
    [SerializeField]
    private TextMeshProUGUI driverSkillPointsText;
    [SerializeField]
    private TextMeshProUGUI driverManaText;
    [SerializeField]
    private TextMeshProUGUI driverLuckText;

    [SerializeField]
    private TextMeshProUGUI bodyEquipmentText;
    [SerializeField]
    private TextMeshProUGUI brakesEquipmentText;
    [SerializeField]
    private TextMeshProUGUI engineEquipmentText;
    [SerializeField]
    private TextMeshProUGUI wheelEquipmentText;
    [SerializeField]
    private TextMeshProUGUI steeringSystemEquipmentText;
    [SerializeField]
    private TextMeshProUGUI fuelTankEquipmentText;

    private CarStatus carStatus;
    private DriverStatus driverStatus;
    private CarEquipments carEquipments;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerCarGameObject = GameObject.FindGameObjectWithTag(TagsConstants.PLAYER_TAG);
        this.carStatus = CarUtils.GetCarStatus(playerCarGameObject);
        this.driverStatus = CarUtils.GetDriverStatus(playerCarGameObject);
        this.carEquipments = CarUtils.GetCarEquipments(playerCarGameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.isGameInPause = true;
            this.gameMenuGameObject.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Y))
        {
            this.gameMenuGameObject.SetActive(false);
            GameManager.isGameInPause = false;
        }

        this.UpdateStatusMenuCarStatusTexts();
        this.UpdateStatusMenuDriverStatusTexts();
        this.UpdateStatusMenuCarequipmentsTexts();
    }

    private void UpdateStatusMenuCarStatusTexts()
    {
        this.maximumSpeedText.text = $"Maximum speed : {this.carStatus.MaxSpeed.GetEnhancedValue()}";
        this.fuelText.text = $"Fuel : {this.carStatus.Fuel.BaseValue} /  {this.carStatus.MaxFuel.GetEnhancedValue()}";
        this.driftText.text = $"Drift effect :{this.carStatus.DriftFactor.GetEnhancedValue()}";
        this.damageText.text = $"Damage : {this.carStatus.Damage.BaseValue} / {this.carStatus.MaxDamage.GetEnhancedValue()}";
    }

    private void UpdateStatusMenuDriverStatusTexts()
    {
        this.driverNameText.text = $"Name : {this.driverStatus.DriverName}";
        this.driverExperienceText.text = $"Experience : {this.driverStatus.CurrentExperience} / {this.driverStatus.ExperienceToReachNextLevel}";
        this.driverSkillPointsText.text = $"Skill points : {this.driverStatus.SkillPoints}";
        this.driverManaText.text = $"Mana : {this.driverStatus.DriverMana} / {this.driverStatus.DriverMaximumMana}";
        this.driverLuckText.text = $"Luck : {this.driverStatus.Luck}";
    }

    private void UpdateStatusMenuCarequipmentsTexts()
    {
        this.bodyEquipmentText.text = $"Body : {this.carEquipments.BodyEquipment.Name}";
        this.brakesEquipmentText.text = $"Brakes : {this.carEquipments.BrakesEquipment.Name}";
        this.engineEquipmentText.text = $"Engine : {this.carEquipments.EngineEquipment.Name}";
        this.wheelEquipmentText.text = $"Wheels : {this.carEquipments.WheelEquipment.Name}";
        this.steeringSystemEquipmentText.text = $"Steering : {this.carEquipments.SteeringEquipment.Name}";
        this.fuelTankEquipmentText.text = $"Fuel tank : {this.carEquipments.FuelTankEquipment.Name}";
    }
}
