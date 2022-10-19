using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class CarEquipment
{
    [SerializeField]
    private string name;
    [SerializeField]
    private float price;
    [SerializeField]
    private string description;
    [SerializeField]
    private EquipmentTypeEnum equipmentType;

    public string Name { get => name; set => name = value; }
    public float Price { get => price; set => price = value; }
    public string Description { get => description; set => description = value; }
    public EquipmentTypeEnum EquipmentType { get => equipmentType; set => equipmentType = value; }
}

