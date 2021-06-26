using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TurretBlueprint
{

    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public float turretVar = Turret.dmgVar;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
