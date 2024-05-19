using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretSO : ScriptableObject
{
    public GameObject turretPrefab;
    public Sprite icon;
    public int range;
    public int baseCost;
    [TextArea]
    public string baseDescription;
    public string[] upgradeNames;
    public Sprite[] upgradeIcons;
    public int[] upgradeCosts;
    [TextArea]
    public string[] upgradeDescriptions;
}
